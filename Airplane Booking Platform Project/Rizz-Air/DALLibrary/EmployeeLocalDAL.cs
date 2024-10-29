using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;
using DomainLibrary.Enumerations;

namespace DALLibrary
{
    public class EmployeeLocalDAL : IEmployeeRepository
    {
        List<Employee> employees = new List<Employee>();
        public EmployeeLocalDAL()
        {
            employees = new List<Employee>(); 
        }

        public bool AddEmployee(Employee employee)
        {
            employees.Add(employee);
            return true;
        }

        public bool DeleteEmployee(int employeeId)
        {
            for (int i = 0; i < employees.Count(); i++)
            {
                if (employees[i].EmployeeId == employeeId)
                    employees.Remove(employees[i]);
                return true;
            }
            return false;
        }

        public List<Pilot> FilterPilotsForAircraft(Aircraft aircraft, List<Pilot> availablePilots)
        {
            throw new NotImplementedException();
        }

        public List<Pilot> FilterPilotsForHelicopter(List<Pilot> listPilot)
        {
            throw new NotImplementedException();
        }

        public List<Pilot> FilterPilotsForPrivateJet(List<Pilot> listPilot)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployees()
        {
            return employees;
        }

        public List<Manager> GetAllManagers()
        {
            throw new NotImplementedException();
        }

        public List<Pilot> GetAllPilots()
        {
            throw new NotImplementedException();
        }

        public List<Pilot> GetAllPilotsForHelicopter()
        {
            throw new NotImplementedException();
        }

        public List<Pilot> GetAllPilotsForPrivateJet()
        {
            throw new NotImplementedException();
        }

        public List<Pilot> GetAvailablePilots(DateTime DepartureDay, DateTime ComingBackDay, Aircraft aircraft)
        {
            throw new NotImplementedException();
        }

        public List<Pilot> GetAvailablePilots(DateTime DepartureDay, DateTime ComingBackDay, List<Pilot> listPilot)
        {
            throw new NotImplementedException();
        }

        public List<Pilot> GetAvailablePilotsForNormalRequest(NormalRequest normalRequest)
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            foreach(Employee employee in employees)
            {
                if (employee.EmployeeId == employeeId)
                    return employee;
            }

            throw new Exception($"Employee with the Id number {employeeId} not found!");
        }

        public Manager GetManagerByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Manager GetManagerByID(int managerId)
        {
            throw new NotImplementedException();
        }

        public Tuple<double, double> GetMinAndMaxPilotSalaryForNormalRequest(List<Pilot> listPilot)
        {
            throw new NotImplementedException();
        }

        public Pilot GetPilotByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public Pilot GetPilotByID(int pilotId)
        {
            throw new NotImplementedException();
        }

        public Manager Login(Employee emp)
        {
            throw new NotImplementedException();
        }

        public Manager ManagerLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Pilot PilotLogin(string email, string password)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmployee(int employeeId , Employee Emp)
        {
            for(int i = 0; i < employees.Count(); i++)
            {
                if (employees[i].EmployeeId == employeeId)
                    employees[i] = Emp;

                return true;
            }
            return false;
        }
    }
}
