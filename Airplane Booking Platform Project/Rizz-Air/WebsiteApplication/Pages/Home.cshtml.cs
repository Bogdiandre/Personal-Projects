using DALLibrary;
using DomainLibrary.Domains;
using DomainLibrary.Exceptions;
using LogicLibrary.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace WebsiteApplication.Pages
{
    public class HomeModel : PageModel
    {
        private readonly UserManager _userManager;

        public HomeModel()
        {
            _userManager = new UserManager(new UserDAL());

        }

        public void OnGet()
        {
            try
            {
                var userId = User.FindFirstValue("UserId");

                if (userId != null)
                {
                    User user = _userManager.GetUserById(int.Parse(userId));

                    ViewData["UserName"] = user.FirstName + " " + user.LastName;
                    ViewData["UserEmail"] = user.Email;
                    ViewData["License"] = user.License;
                }
            }
            catch (DatabaseException ex) { TempData["Message"] = "Something went wrong!";  }

        }
    }
}
