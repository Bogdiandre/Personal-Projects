using DomainLibrary.Domains;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using DTO;
using LogicLibrary.Managers;
using DALLibrary;
using System.Diagnostics.Contracts;
using DomainLibrary.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace WebsiteApplication.Pages
{
    [AllowAnonymous]
    public class RegisterPageModel : PageModel
    {
        [BindProperty]
        public UserDTO User { get; set; }

        [BindProperty]
        public DateTime DateOfBirth { get; set; }

        public bool ShowMessageBox { get; set; }
        public string MessageBoxMessage { get; set; }

        public RegisterPageModel()
        {
            User = new UserDTO();
        }

        public void OnGet()
        {
            DateOfBirth = DateTime.Now;
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {

                UserManager registerManager = new UserManager(new UserDAL());

                

                if (!ModelState.IsValid)
                {
                    

                    

                    
                    return Page();
                }

                else
                {
                    if (registerManager.GetUserByEmail(User.Email) == null)
                    {
                        User user = new User(User.Email, User.LastName, User.FirstName, User.DateOfBirth, User.Password, User.License);
                        registerManager.AddUser(user);
                        user = registerManager.Login(User.Email, User.Password);

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
                        ViewData["Message"] = string.Format("You cannot use this email adress!");
                        return RedirectToPage();
                    }
                }
            }
            catch (DatabaseException ex) { TempData["Message"] = "Something went wrong!"; return RedirectToPage(); }

        }
    }
}
