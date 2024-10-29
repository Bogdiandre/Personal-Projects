using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using DomainLibrary.Domains;
using DomainLibrary.Exceptions;
using DomainLibrary;
using Interfaces;
using DALLibrary.CONS;
using DomainLibrary.Enumerations;
using System.Reflection;
using System.Net;
using System.ComponentModel;

namespace DALLibrary
{
    public class EmployeeDAL : Cons, IEmployeeRepository
    {

        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            return conn;
        }

        public bool AddEmployee(Employee employee)
        {
            SqlConnection conn = GetConnection();

            int newEmployeeID;

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [Employees] (FirstName, LastName, Email, PasswordSalt, PasswordHash, PhoneNumber ,DateOfBirth) VALUES (@FirstName, @LastName, @Email, @PasswordSalt, @PasswordHash, @PhoneNumber ,@DateOfBirth); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                        command.Parameters.Add(new SqlParameter("LastName", employee.LastName));
                        command.Parameters.Add(new SqlParameter("@Email", employee.Email));
                        command.Parameters.Add(new SqlParameter("@PasswordHash", employee.PasswordHash));
                        command.Parameters.Add(new SqlParameter("@PasswordSalt", employee.PasswordSalt));
                        command.Parameters.Add(new SqlParameter("@PhoneNumber", employee.PhoneNumber));
                        command.Parameters.Add(new SqlParameter("@DateOfBirth", employee.DateOfBirth));


                        newEmployeeID = Convert.ToInt32(command.ExecuteScalar());
                    }




                    if (employee.GetType() == typeof(Manager))
                    {
                        return CreateManager((Manager)employee, newEmployeeID);
                    }
                    if (employee.GetType() == typeof(Pilot))
                    {
                        return CreatePilot((Pilot)employee, newEmployeeID);
                    }

                    return true;


                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occured in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }

                return false;
            }
        }

        private bool CreateManager(Manager manager, int managerID)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [Managers] (ManagerID, Type) VALUES (@ManagerID, @Type)";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@ManagerID", managerID));
                        command.Parameters.Add(new SqlParameter("@Type", manager.Type));




                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occured in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }

                return false;
            }
        }

        private bool CreatePilot(Pilot pilot, int pilotID)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [Pilot] (PilotID, License, SalaryPerKm) VALUES (@PilotID, @License, @SalaryPerKm)";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@PilotID", pilotID));
                        command.Parameters.Add(new SqlParameter("@License", pilot.License));
                        command.Parameters.Add(new SqlParameter("@SalaryPerKm", pilot.SallaryPerKm));



                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occured in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }

                return false;
            }
        }

        public bool DeleteEmployee(int employeeID)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();


                    using (SqlCommand command = new SqlCommand("DELETE FROM Managers WHERE ManagerID = @employeeID", conn))
                    {
                        command.Parameters.AddWithValue("@employeeID", employeeID);
                        command.ExecuteNonQuery();
                    }


                    using (SqlCommand command = new SqlCommand("DELETE FROM Pilot WHERE PilotID = @employeeID", conn))
                    {
                        command.Parameters.AddWithValue("@employeeID", employeeID);
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand("DELETE FROM Employees WHERE ID = @employeeID", conn))
                    {
                        command.Parameters.AddWithValue("@employeeID", employeeID);
                        command.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (SqlNullValueException ex)
                {
                    throw new DatabaseException("Error reading null values: " + ex.ToString());
                }
                catch (InvalidOperationException ex)
                {
                    throw new DatabaseException("Invalid operation for the current state of the database connection: " + ex.ToString());
                }
                catch (SqlException ex)
                {
                    throw new DatabaseException("Error in the SQL Server database: " + ex.ToString());
                }
                catch (TimeoutException ex)
                {
                    throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded: " + ex.ToString());
                }
                catch (Exception ex)
                {
                    throw new DatabaseException("Error in the database: " + ex.ToString());
                }
            }
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            foreach (Manager manager in GetAllManagers())
            {
                employees.Add(manager);
            }
            foreach (Pilot pilot in GetAllPilots())
            {
                employees.Add(pilot);
            }

            return employees;

        }

        public bool UpdateEmployee(int employeeID, Employee employee)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string updateSql = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, PasswordSalt = @PasswordSalt, PasswordHash = @PasswordHash, PhoneNumber = @PhoneNumber, DateOfBirth = @DateOfBirth WHERE ID = @EmployeeID";

                    using (SqlCommand updateCommand = new SqlCommand(updateSql, conn))
                    {
                        updateCommand.Parameters.Add(new SqlParameter("@EmployeeID", employeeID));
                        updateCommand.Parameters.Add(new SqlParameter("@FirstName", employee.FirstName));
                        updateCommand.Parameters.Add(new SqlParameter("@LastName", employee.LastName));
                        updateCommand.Parameters.Add(new SqlParameter("@Email", employee.Email));
                        updateCommand.Parameters.Add(new SqlParameter("@PasswordSalt", employee.PasswordSalt));
                        updateCommand.Parameters.Add(new SqlParameter("@PasswordHash", employee.PasswordHash));
                        updateCommand.Parameters.Add(new SqlParameter("@PhoneNumber", employee.PhoneNumber));
                        updateCommand.Parameters.Add(new SqlParameter("@DateOfBirth", employee.DateOfBirth));

                        updateCommand.ExecuteNonQuery();
                    }

                    if (employee is Manager manager)
                    {
                        return UpdateManager(employeeID, manager);
                    }
                    else if (employee is Pilot pilot)
                    {
                        return UpdatePilot(employeeID, pilot);
                    }

                    return true;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error reading null values: " + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("Invalid operation for the current state of the database connection: " + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("Error in the SQL Server database: " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded: " + ex.ToString()); }
                catch (Exception ex) { throw new DatabaseException("Error in the database: " + ex.ToString()); }
            }
        }

        private bool UpdateManager(int managerID, Manager manager)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string updateSql = "UPDATE Managers SET Type = @ManagerType WHERE ManagerID = @ManagerID";

                    using (SqlCommand updateCommand = new SqlCommand(updateSql, conn))
                    {
                        updateCommand.Parameters.Add(new SqlParameter("@ManagerID", managerID));
                        updateCommand.Parameters.Add(new SqlParameter("@ManagerType", manager.Type));

                        updateCommand.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error reading null values: " + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("Invalid operation for the current state of the database connection: " + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("Error in the SQL Server database: " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded: " + ex.ToString()); }
                catch (Exception ex) { throw new DatabaseException("Error in the database: " + ex.ToString()); }
            }
        }

        private bool UpdatePilot(int pilotID, Pilot pilot)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string updateSql = "UPDATE Pilot SET License = @License, SalaryPerKm = @SalaryPerKm WHERE PilotID = @PilotID";

                    using (SqlCommand updateCommand = new SqlCommand(updateSql, conn))
                    {
                        updateCommand.Parameters.Add(new SqlParameter("@PilotID", pilotID));
                        updateCommand.Parameters.Add(new SqlParameter("@License", pilot.License));
                        updateCommand.Parameters.Add(new SqlParameter("@SalaryPerKm", pilot.SallaryPerKm));

                        updateCommand.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error reading null values: " + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("Invalid operation for the current state of the database connection: " + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("Error in the SQL Server database: " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded: " + ex.ToString()); }

            }
        }


        public Manager GetManagerByID(int managerID)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT e.FirstName, e.LastName, e.Email, e.PasswordSalt, e.PasswordHash, e.PhoneNumber, e.DateOfBirth, m.Type " +
                                   "FROM Employees AS e INNER JOIN Managers AS m ON e.ID = m.ManagerID " +
                                   "WHERE e.ID = @managerID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@managerID", managerID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string firstName = (string)reader["FirstName"];
                                string lastName = (string)reader["LastName"];
                                string email = (string)reader["Email"];
                                string passwordHash = (string)reader["PasswordHash"];
                                string passwordSalt = (string)reader["PasswordSalt"];
                                string phoneNumber = (string)reader["PhoneNumber"];
                                DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                                ManagerType managerType = (ManagerType)reader["Type"];

                                return new Manager(firstName, lastName, email, passwordHash, passwordSalt, phoneNumber, managerType, dateOfBirth, managerID);
                            }
                        }
                    }
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
            }

            return null;
        }

        public Pilot GetPilotByID(int pilotID)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT e.FirstName, e.LastName, e.Email, e.PasswordSalt, e.PasswordHash, e.PhoneNumber, e.DateOfBirth, p.License, p.SalaryPerKm " +
                                   "FROM Employees AS e INNER JOIN Pilot AS p ON e.ID = p.PilotID " +
                                   "WHERE e.ID = @pilotID;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@pilotID", pilotID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string firstName = (string)reader["FirstName"];
                                string lastName = (string)reader["LastName"];
                                string email = (string)reader["Email"];
                                string passwordSalt = (string)reader["PasswordSalt"];
                                string passwordHash = (string)reader["PasswordHash"];
                                string phoneNumber = (string)reader["PhoneNumber"];
                                DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                                AircraftType license = (AircraftType)reader["License"];
                                double sallaryPerKm = (double)reader["SalaryPerKm"];

                                return new Pilot(firstName, lastName, email, passwordHash, passwordSalt, phoneNumber, license, sallaryPerKm, dateOfBirth, pilotID);
                            }
                        }
                    }
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
            }

            return null;
        }

        public List<Manager> GetAllManagers()
        {
            List<Manager> managers = new List<Manager>();

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT e.ID, e.FirstName, e.LastName, e.Email, e.PasswordSalt, e.PasswordHash, e.PhoneNumber, e.DateOfBirth," +
                        " m.Type FROM [Employees] AS e INNER JOIN Managers AS m ON e.ID = m.ManagerID", conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                managers.Add(new Manager((string)reader[1], (string)reader[2], (string)reader[3], (string)reader[5], (string)reader[4], (string)reader[6], (ManagerType)reader[8], (DateTime)reader[7], (int)reader[0]));
                            }
                        }
                    }
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database GetConnection(). :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occured in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }

            }

            return managers;
        }

        public List<Pilot> GetAllPilots()
        {
            List<Pilot> pilots = new List<Pilot>();

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand command = new SqlCommand("SELECT e.ID, e.FirstName, e.LastName, e.Email, e.PasswordSalt, e.PasswordHash, e.PhoneNumber, e.DateOfBirth," +
                        " p.License, p.SalaryPerKm FROM [Employees] AS e INNER JOIN Pilot AS p ON e.ID = p.PilotID", conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                pilots.Add(new Pilot((string)reader[1], (string)reader[2], (string)reader[3], (string)reader[5], (string)reader[4], (string)reader[6], (AircraftType)reader[8], (double)reader[9], (DateTime)reader[7], (int)reader[0]));
                            }
                        }
                    }
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database GetConnection(). :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occured in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }

            }

            return pilots;
        }

        public Manager ManagerLogin(string email, string password)
        {
            Manager manager = null;
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT e.ID, e.FirstName, e.LastName, e.Email, e.PasswordSalt, e.PasswordHash, e.PhoneNumber, e.DateOfBirth, m.Type " +
                                 "FROM Employees AS e INNER JOIN Managers AS m ON e.ID = m.ManagerID " +
                                 "WHERE e.Email = @email;";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@email", email);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int managerID = reader.GetInt32("ID");
                                string firstName = reader.GetString("FirstName");
                                string lastName = reader.GetString("LastName");
                                string phoneNumber = reader.GetString("PhoneNumber");
                                DateTime dateOfBirth = reader.GetDateTime("DateOfBirth");
                                ManagerType managerType = (ManagerType)reader.GetInt32("Type");

                                manager = new Manager(firstName, lastName, email, password, phoneNumber, managerType, dateOfBirth, managerID);
                            }
                        }
                    }
                }
                if (manager == null)
                {
                    return null;
                }
                else
                {
                    if (manager.ValidatePassword(password))
                    {
                        return manager;
                    }
                    else
                    {
                        return null;
                    }
                }

            }
            catch (SqlNullValueException ex)
            {
                throw new DatabaseException("Error, reading null values. :" + ex.ToString());
            }
            catch (InvalidOperationException ex)
            {
                throw new DatabaseException("An operation is attempted that is not valid for the current state of the database GetConnection(). :" + ex.ToString());
            }
            catch (TimeoutException ex)
            {
                throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString());
            }



        }
        public Pilot PilotLogin(string email, string password)
        {
            Pilot pilot = null;
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT e.ID, e.FirstName, e.LastName, e.Email, e.PasswordSalt, e.PasswordHash, e.PhoneNumber, e.DateOfBirth, p.License, p.SalaryPerKm " +
                                 "FROM Employees AS e INNER JOIN Pilot AS p ON e.ID = p.PilotID " +
                                 "WHERE e.Email = @email";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@email", email);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int pilotID = reader.GetInt32("ID");
                                string firstName = reader.GetString("FirstName");
                                string lastName = reader.GetString("LastName");
                                string phoneNumber = reader.GetString("PhoneNumber");
                                DateTime dateOfBirth = reader.GetDateTime("DateOfBirth");
                                AircraftType license = (AircraftType)reader.GetInt32("License");
                                double salaryPerKm = reader.GetDouble("SalaryPerKm");

                                pilot = new Pilot(firstName, lastName, email, password, phoneNumber, license, salaryPerKm, dateOfBirth, pilotID);
                            }
                        }
                    }
                }
                if (pilot == null)
                {
                    return null;
                }
                else
                {
                    if (pilot.ValidatePassword(password))
                    {
                        return pilot;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (SqlNullValueException ex)
            {
                throw new DatabaseException("Error, reading null values. :" + ex.ToString());
            }
            catch (InvalidOperationException ex)
            {
                throw new DatabaseException("An operation is attempted that is not valid for the current state of the database GetConnection(). :" + ex.ToString());
            }
            catch (TimeoutException ex)
            {
                throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString());
            }
            catch (SqlException ex)
            {
                throw new DatabaseException("Error!");
            }


        }

        public List<Pilot> GetAvailablePilots(DateTime DepartureDay, DateTime ComingBackDay, List<Pilot> pilotList)
        {
            List<Pilot> ListPilot = new List<Pilot>();


            RentDAL rentDAL = new RentDAL();

            foreach (Pilot pilot in pilotList)
            {
                List<Rent> rentList = rentDAL.GetAllRentsByPilotID(pilot.EmployeeId);

                bool free = true;

                if (rentList != null)
                {
                    foreach (Rent rent in rentList)
                    {
                        if (!(ComingBackDay < rent.NormalRequest.DepartureDay || DepartureDay > rent.NormalRequest.ComingBackDay))
                        {
                            free = false;
                            break;
                        }
                    }
                    if (free == true)
                    {
                        ListPilot.Add(pilot);
                    }
                }
                else
                {
                    ListPilot.Add(pilot);
                }
            }

            return ListPilot;
        }
        public Tuple<double, double> GetMinAndMaxPilotSalaryForNormalRequest(List<Pilot> listPilot)
        {

            double MinSalary = double.MaxValue;
            double MaxSalary = double.MinValue;

            foreach (Pilot pilot in listPilot)
            {

                if (pilot.SallaryPerKm >= MaxSalary)
                {
                    MaxSalary = pilot.SallaryPerKm;
                }


                if (pilot.SallaryPerKm <= MinSalary)
                {
                    MinSalary = pilot.SallaryPerKm;
                }
            }

            Tuple<double, double> salaries = Tuple.Create(MinSalary, MaxSalary);

            return salaries;
        }
        public List<Pilot> FilterPilotsForPrivateJet(List<Pilot> listPilot)
        {
            List<Pilot> pilotList = new List<Pilot>();

            foreach (Pilot pilot in listPilot)
            {
                if (pilot.License == AircraftType.PrivateJet)
                {
                    pilotList.Add(pilot);
                }
            }

            return pilotList;
        }

        public List<Pilot> FilterPilotsForHelicopter(List<Pilot> listPilot)
        {
            List<Pilot> pilotList = new List<Pilot>();

            foreach (Pilot pilot in listPilot)
            {
                if (pilot.License == AircraftType.Helicopter)
                {
                    pilotList.Add(pilot);
                }
            }

            return pilotList;
        }

        public List<Pilot> FilterPilotsForAircraft(Aircraft aircraft, List<Pilot> listPilots)
        {

            if (aircraft is Helicopter)
            {
                return FilterPilotsForHelicopter(listPilots);
            }
            else
            {
                return FilterPilotsForPrivateJet(listPilots);
            }

        }

        public Manager GetManagerByEmail(string email)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT e.ID, e.FirstName, e.LastName, e.Email, e.PasswordSalt, e.PasswordHash, e.PhoneNumber, e.DateOfBirth, m.Type " +
                                   "FROM Employees AS e INNER JOIN Managers AS m ON e.ID = m.ManagerID " +
                                   "WHERE e.Email = @email;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int managerID = (int)reader["ID"];
                                string firstName = (string)reader["FirstName"];
                                string lastName = (string)reader["LastName"];
                                string passwordHash = (string)reader["PasswordHash"];
                                string passwordSalt = (string)reader["PasswordSalt"];
                                string phoneNumber = (string)reader["PhoneNumber"];
                                DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                                ManagerType managerType = (ManagerType)reader["Type"];

                                return new Manager(firstName, lastName, email, passwordHash, passwordSalt, phoneNumber, managerType, dateOfBirth, managerID);
                            }
                        }
                    }
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
            }

            return null;
        }

        public Pilot GetPilotByEmail(string email)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT e.ID, e.FirstName, e.LastName, e.Email, e.PasswordSalt, e.PasswordHash, e.PhoneNumber, e.DateOfBirth, p.License, p.SalaryPerKm " +
                                   "FROM Employees AS e INNER JOIN Pilots AS p ON e.ID = p.PilotID " +
                                   "WHERE e.Email = @email;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@email", email);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int pilotID = (int)reader["ID"];
                                string firstName = (string)reader["FirstName"];
                                string lastName = (string)reader["LastName"];
                                string passwordHash = (string)reader["PasswordHash"];
                                string passwordSalt = (string)reader["PasswordSalt"];
                                string phoneNumber = (string)reader["PhoneNumber"];
                                DateTime dateOfBirth = (DateTime)reader["DateOfBirth"];
                                AircraftType license = (AircraftType)reader["License"];
                                double salaryPerKm = (double)reader["SalaryPerKm"];

                                return new Pilot(firstName, lastName, email, passwordHash, passwordSalt, phoneNumber, license, salaryPerKm, dateOfBirth, pilotID);
                            }
                        }
                    }
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
            }

            return null;
        }



    }
}
