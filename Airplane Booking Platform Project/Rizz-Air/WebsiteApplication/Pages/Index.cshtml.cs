using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using DTO;
using LogicLibrary.Managers;
using DALLibrary;
using DomainLibrary.Domains;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WebsiteApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public int UserId { get; set; }
        public bool HasLicense { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                // User is authenticated, redirect to home page
                Response.Redirect("/Home");
            }
            else
            {
                Response.Redirect("/LoginPage");
            }
        }
    }
}
