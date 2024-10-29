using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Interfaces;
using DomainLibrary.Domains;
using LogicLibrary.Managers;
using DALLibrary;
using System.Security.Claims;
using DomainLibrary.Exceptions;

namespace WebsiteApplication.Pages
{
    public class DestinationsModel : PageModel
    {
        public DestinationManager DestinationManager { get; set; }
        [BindProperty]
        public List<Destination> Destinations { get; set; }
        public UserManager userManager { get; set; }
        public User PageUser { get; set; }
        public DestinationsModel()
        {
            DestinationManager = new DestinationManager(new DestinationDAL());
        }
        public void OnGet()
        {

            Destinations = DestinationManager.GetAllDestinations();

        }

        public async Task<IActionResult> OnPostChooseDestination(int destinationID)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    return RedirectToPage("/NormalRequestPage", new { destinationID });
                }
                else
                {
                    TempData["Message"] = "You are not logged in to choose a destination";
                    return RedirectToPage();
                }
            }
            catch (DatabaseException ex) { TempData["Message"] = "Something went wrong!"; return RedirectToPage(); }

        }
    }
}
    

