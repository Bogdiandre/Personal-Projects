using DomainLibrary.Domains;
using LogicLibrary.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DALLibrary;
using Microsoft.AspNetCore.Identity;
using DomainLibrary.Exceptions;

namespace WebsiteApplication.Pages
{
    public class PilotRequestPageModel : PageModel
    {
        public DateTime CurrentDate { get; private set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime FinishDate { get; set; }
        [BindProperty]
        public List<Helicopter> AvailableHelicopters { get; private set; }

        public AircraftManager aircraftManager;
        

        public PilotRequestPageModel()
        {
            SetDefaultDateValues();
            aircraftManager = new AircraftManager(new AircraftDAL());

        }
        public void OnGet()
        {
            SetDefaultDateValues();
            aircraftManager = new AircraftManager(new AircraftDAL());
        }

        public void OnPost()
        {

            if (StartDate.Date < DateTime.Today.Date)
            {
                ModelState.AddModelError("StartDate", "Start date must be at least the current date.");
                SetDefaultDateValues();
            }

            if (FinishDate.Date <= StartDate.Date)
            {
                ModelState.AddModelError("FinishDate", "Finish date must be at least one day after the start date.");
                SetDefaultDateValues();
            }

            if (ModelState.IsValid)
            {
                ModelState.AddModelError("FinishDate", "Correct!");
                StartDate = StartDate.Date;
                FinishDate = FinishDate.Date;
                AvailableHelicopters = aircraftManager.GetAvailableHelicopterForPrivateRequest(StartDate, FinishDate);

            }

        }

        public async Task<IActionResult> OnPostCreatePilotRequest(int helicopterID )
        {
            //DateTime startDate = StartDate;
            //DateTime finishDate = FinishDate;
            try
            {
                Helicopter helicopter = aircraftManager.GetHelicopterByID(helicopterID);

                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                UserManager userManager = new UserManager(new UserDAL());
                User user = userManager.GetUserById(userId);

                PilotRequest pilotRequest = new PilotRequest(StartDate, FinishDate, helicopter, user);

                PilotRequestManager pilotRequestManager = new PilotRequestManager(new PilotRequestDAL());

                pilotRequestManager.AddPilotRequest(pilotRequest);

                TempData["Message"] = "Request made successfully.";

                return RedirectToPage("/Home");
            }
            catch (PilotException a) { TempData["Message"] = a.Message; return RedirectToPage(); }
            catch (DatabaseException ex) { TempData["Message"] = "Something went wrong!"; return RedirectToPage(); }
        }

        private void SetDefaultDateValues()
        {
            CurrentDate = DateTime.Today.Date;

            // Set default values
            StartDate = CurrentDate;
            FinishDate = CurrentDate.AddDays(1);
        }
    }
}
