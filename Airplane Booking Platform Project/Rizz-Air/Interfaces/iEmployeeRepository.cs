using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;

namespace Interfaces
{

        public interface IEmployeeRepository
        {
            public List<Employee> GetAllEmployees();
            public bool AddEmployee(Employee employee);
            public bool DeleteEmployee(int employeeId);
            public bool UpdateEmployee(int employeeId , Employee emp);
            public List<Manager> GetAllManagers();
            public List<Pilot> GetAllPilots();
            public Manager GetManagerByID(int managerId);
            public Pilot GetPilotByID(int pilotId);
            public Manager ManagerLogin(string email, string password);
            public Pilot PilotLogin(string email, string password);
            public List<Pilot> GetAvailablePilots(DateTime DepartureDay, DateTime ComingBackDay, List<Pilot> listPilot);
            public Tuple<double, double> GetMinAndMaxPilotSalaryForNormalRequest(List<Pilot> listPilot);
            public List<Pilot> FilterPilotsForHelicopter(List<Pilot> listPilot);
            public List<Pilot> FilterPilotsForPrivateJet(List<Pilot> listPilot);
            public List<Pilot> FilterPilotsForAircraft(Aircraft aircraft, List<Pilot> availablePilots);
            public Pilot GetPilotByEmail(string email);
            public Manager GetManagerByEmail(string email);
        }
    }

