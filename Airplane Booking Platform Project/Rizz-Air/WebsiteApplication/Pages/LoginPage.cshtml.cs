using DomainLibrary.Domains;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using DTO;
using LogicLibrary.Managers;
using DALLibrary;
using DomainLibrary.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace WebsiteApplication.Pages
{
    [AllowAnonymous]
    public class LoginPageModel : PageModel
    {
        [BindProperty]
        public LoginDTO Login { get; set; }

        public LoginPageModel()
        {
            Login = new LoginDTO();
        }
        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                // User is authenticated, redirect to home page
                Response.Redirect("/Home");
            }
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                UserManager loginManager = new UserManager(new UserDAL());
                User user = loginManager.Login(Login.Mail, Login.Password);

                if (user != null)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("UserId", user.UserId.ToString()));

                    if (user.License == true)
                    {
                        claims.Add(new Claim("License", user.License.ToString()));
                    }

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(new ClaimsPrincipal(claimsIdentity));

                    return RedirectToPage("/Home");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid credentials");
                    return Page();
                }
            }
            catch (DatabaseException ex) { TempData["Message"] = "Something went wrong!"; return RedirectToPage(); }
        }
    }
}