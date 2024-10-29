using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;
using Interfaces;

namespace LogicLibrary.Managers
{
    public class EmployeeManager
    {
        private IEmployeeRepository employeeRepository;

        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        public bool AddEmployee(Employee employees)
        {
            return employeeRepository.AddEmployee(employees);
        }
        public bool DeleteEmployee(int employeeId)
        {
            return employeeRepository.DeleteEmployee(employeeId);
        }
        public bool UpdateEmployee(int employeeId, Employee emp)
        {
            return employeeRepository.UpdateEmployee(employeeId, emp);
        }
        

        public List<Employee> GetAllEmployees()
        {
            return employeeRepository.GetAllEmployees();
        }

        public List<Manager> GetAllManagers()
        {
            return employeeRepository.GetAllManagers();
        }

        public List<Pilot> GetAllPilots()
        {
            return employeeRepository.GetAllPilots();
        }

        public Manager ManagerLogin(string email, string password)
        {
            return employeeRepository.ManagerLogin(email, password);
        }
        public Pilot PilotLogin(string email, string password)
        {
            return employeeRepository.PilotLogin(email, password);
        }
        public Manager GetManagerById(int managerId)
        {
            return employeeRepository.GetManagerByID(managerId);
        }

        public Pilot GetPilotById(int pilotId)
        {
            return employeeRepository.GetPilotByID(pilotId);
        }
        public Tuple<double, double> GetMinAndMaxPilotSallaryForNormalRequest(List<Pilot> listPilot)
        {
            return employeeRepository.GetMinAndMaxPilotSalaryForNormalRequest(listPilot);
        }
        public List<Pilot> GetAvailablePilots(DateTime DepartureDay, DateTime ComingBackDay, List<Pilot> listPilot)
        {
            return employeeRepository.GetAvailablePilots(DepartureDay, ComingBackDay, listPilot);    
        }
        public List<Pilot> FilterPilotsForPrivateJet(List<Pilot> listPilot)
        {
            return employeeRepository.FilterPilotsForPrivateJet(listPilot);
        }
        public List<Pilot> FilterPilotsForHelicopter(List<Pilot> listPilot)
        {
            return employeeRepository.FilterPilotsForHelicopter(listPilot);
        }

        public List<Pilot> FilterPilotsForAircraft(Aircraft aircraft, List<Pilot> availablePilots)
        {
            return employeeRepository.FilterPilotsForAircraft(aircraft, availablePilots);
        }

        public List<Pilot> GetAvailablePilotsForNormalRequest(NormalRequest normalRequest)
        {
            return FilterPilotsForAircraft(normalRequest.Aircraft,GetAvailablePilots(normalRequest.DepartureDay, normalRequest.ComingBackDay, GetAllPilots()));
        }

        public Pilot GetCheapestPilotForNormalRequest(NormalRequest normalRequest)
        {
            List<Pilot> listPilots = GetAvailablePilotsForNormalRequest(normalRequest);//this can be 0

            double salary = double.MaxValue;
            Pilot CheapestPilot = null;

            foreach (Pilot pilot in listPilots)
            {
                if(pilot.SallaryPerKm < salary)
                {
                    salary = pilot.SallaryPerKm;
                    CheapestPilot = pilot;
                }
            }

            return CheapestPilot;
        }

        public Pilot GetRandomPilotForNormalRequest(NormalRequest normalRequest)
        {
            List<Pilot> listPilots = GetAvailablePilotsForNormalRequest(normalRequest);//this can be 0

            Random random = new Random();
            int randomNumber = random.Next(listPilots.Count);//this can be 0

            return listPilots[randomNumber];
        }

        public Manager GetManagerByEmail(string email)
        {
            return employeeRepository.GetManagerByEmail(email);
        }

        public Pilot GetPilotByEmail(string email)
        {
            return employeeRepository.GetPilotByEmail(email);
        }
    }
}
