using DALLibrary;
using DomainLibrary.Domains;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DALLibrary;
using LogicLibrary.Managers;
using DomainLibrary.Exceptions;

namespace WebsiteApplication.Pages
{
    public class AccountModel : PageModel
    {
        public UserDTO UserDTO { get; set; } = new UserDTO(); 

        public void OnGet()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                UserManager userManager = new UserManager(new UserDAL());
                User user = userManager.GetUserById(userId);


                UserDTO = new UserDTO
                {
                    Email = user.Email,
                   
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    License = user.License
                };
            }
            catch (DatabaseException ex) { TempData["Message"] = "Something went wrong!";  }
        }

    }
}