using DomainLibrary.Domains;
using Interfaces;

namespace UnitTest
{
    public class FakeEmployeeMediator : IEmployeeRepository
    {
        
        private Dictionary<int, Employee> _employees;

        public FakeEmployeeMediator()
        {
            this._employees = new Dictionary<int, Employee>();
        }

        public bool AddEmployee(Employee employee)
        {
            _employees.Add(employee.EmployeeId,employee);
            return true;
        }

        public bool DeleteEmployee(int employeeId)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        public List<Manager> GetAllManagers()
        {
            throw new NotImplementedException();
        }

        public List<Pilot> GetAllPilots()
        {
            throw new NotImplementedException();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employees[employeeId];
        }

        public Manager GetManagerByID(int managerId)
        {
            throw new NotImplementedException();
        }

        public Pilot GetPilotByID(int pilotId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateEmployee(int employeeId, Employee emp)
        {
            throw new NotImplementedException();
        }
    }
}