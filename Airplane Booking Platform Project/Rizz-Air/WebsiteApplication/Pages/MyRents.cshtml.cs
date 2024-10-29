using DALLibrary;
using DomainLibrary.Domains;
using LogicLibrary.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DTO;
using System.Reflection.Metadata.Ecma335;
using DomainLibrary.Exceptions;

namespace WebsiteApplication.Pages
{
    [Authorize]
    public class MyRentsModel : PageModel
    {
        [BindProperty]
        public List<Rent> rents { get; set; }
        [BindProperty]
        public List<PilotRequest> pilotRequests { get; set; }

        public readonly RentManager rentManager;
        public readonly PilotRequestManager pilotRequestManager;

        public void OnGet()
        {
            try
            {
                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                rents = rentManager.GetRentsByUserId(userId);
                pilotRequests = pilotRequestManager.GetPilotRequestByUserID(userId);
            }
            catch (DatabaseException ex) { TempData["Message"] = "Something went wrong!";  }
        }

        public MyRentsModel()
        {
            rentManager = new RentManager(new RentDAL());
            pilotRequestManager = new PilotRequestManager(new PilotRequestDAL());
        }
    }
}
