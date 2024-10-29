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
    public class NormalRequestPageModel : PageModel
    {
        public DateTime CurrentDate { get; set; }
        [BindProperty]
        public DateTime StartDate { get; set; }
        [BindProperty]
        public DateTime FinishDate { get; set; }
        [BindProperty]
        public List<Aircraft>AvailableAircrafts{ get; set; }
        [BindProperty]
        public double MinSalaryHelicopter{ get; set; }
        [BindProperty]
        public double MaxSalaryHelicopter{ get; set; }
        [BindProperty]
        public double MinSalaryPrivateJet { get; set; }
        [BindProperty]
        public double MaxSalaryPrivateJet { get; set; }


        [BindProperty]
        public int DestinationID { get; set; }
        [BindProperty]
        public int Distance { get; set; }

        public readonly AircraftManager aircraftManager;
        public readonly DestinationManager destinationManager;
        public readonly EmployeeManager employeeManager;


        public void OnGet(int destinationID)
        {

            DestinationID = destinationID;
            Destination destination = destinationManager.GetDestinationByID(DestinationID);
            if(destination != null) 
            {
                Distance = destination.Distance;
            }
            else
            {
                Response.Redirect("/Home");
            }

        }

        public NormalRequestPageModel()
        {
            SetDefaultDateValues();
            
            aircraftManager = new AircraftManager(new AircraftDAL());
            destinationManager = new DestinationManager(new DestinationDAL());
            employeeManager = new EmployeeManager(new EmployeeDAL());
            
            //destination = destinationManager.GetDestinationByID(DestinationID);

        }
    
        private void SetDefaultDateValues()
        {
            CurrentDate = DateTime.Today.Date;

            // Set default values
            StartDate = CurrentDate;
            FinishDate = CurrentDate.AddDays(1);
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

                Destination destination = destinationManager.GetDestinationByID(DestinationID);

                List<Pilot>availablePilots = employeeManager.GetAvailablePilots(StartDate, FinishDate, employeeManager.GetAllPilots());

                AvailableAircrafts = aircraftManager.GetAvailableAircraftsForNormalRequest(StartDate, FinishDate, destination.Distance, availablePilots);

                Tuple<double, double> salariesForHelicopter = null;
                salariesForHelicopter = employeeManager.GetMinAndMaxPilotSallaryForNormalRequest(employeeManager.FilterPilotsForHelicopter(availablePilots));
                MinSalaryHelicopter = salariesForHelicopter.Item1;
                MaxSalaryHelicopter= salariesForHelicopter.Item2;

                Tuple<double, double> salariesForPrivateJet = null;
                salariesForPrivateJet = employeeManager.GetMinAndMaxPilotSallaryForNormalRequest(employeeManager.FilterPilotsForPrivateJet(availablePilots));
                MinSalaryPrivateJet = salariesForPrivateJet.Item1;
                MaxSalaryPrivateJet = salariesForPrivateJet.Item2;
                Distance = destination.Distance;
                
            }
            //return RedirectToPage();

        }

        public async Task<IActionResult> OnPostCreateNormalRequest(int aircraftID,  int destinationID)
        {
            try
            {
                UserManager userManager = new UserManager(new UserDAL());
                EmployeeManager employeeManager = new EmployeeManager(new EmployeeDAL());

                //Get the user

                int userId = Convert.ToInt32(User.FindFirst("UserId").Value);
                User user = userManager.GetUserById(userId);

                //Get the aircraft

                Aircraft aircraft = aircraftManager.GetAircraftById(aircraftID);

                //Get the salaries from the pilots

                double MinSalary = double.MinValue;
                double MaxSalary = double.MaxValue;

                if(aircraft is Helicopter)
                {
                    MinSalary = MinSalaryHelicopter;
                    MaxSalary = MaxSalaryHelicopter;
                }
                else
                {
                    MinSalary = MinSalaryPrivateJet;
                    MaxSalary = MaxSalaryPrivateJet;
                }    

                /*List<Pilot> availablePilots = employeeManager.GetAvailablePilots(StartDate, FinishDate, employeeManager.GetAllPilots());
                List<Pilot> availablePilotsForAircraft = employeeManager.FilterPilotsForAircraft(aircraft, availablePilots);
                Tuple<double, double> salaries = employeeManager.GetMinAndMaxPilotSallaryForNormalRequest(availablePilotsForAircraft);
                */

                //Get Destination 

                Destination destination = destinationManager.GetDestinationByID(destinationID);

                //Create the Normal Request
                NormalRequestManager normalRequestManager = new NormalRequestManager(new NormalRequestDAL());
                NormalRequest normalRequest = new NormalRequest(StartDate, FinishDate, MinSalary, MaxSalary, destination, aircraft, user);
                normalRequestManager.AddNormalRequest(normalRequest);

                return RedirectToPage("/Home");
                
            }
            catch (NormalRequestException x) { TempData["Message"] = x.Message; return RedirectToPage(); }
            catch (DatabaseException ex) { TempData["Message"] = "Something went wrong!"; return RedirectToPage(); }
        }

        

        
    }
}
