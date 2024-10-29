using DALLibrary.CONS;
using DomainLibrary;
using DomainLibrary.Domains;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using DomainLibrary.Exceptions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALLibrary
{
    public class RentDAL : Cons, iRentRepository
    {
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            return conn;
        }
        public bool AddRent(Rent rent)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [Rent] (FinalPrice, PilotID, NormalRequestID) VALUES (@FinalPrice, @PilotID, @NormalRequestID)";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@FinalPrice", rent.FinalPrice));
                        command.Parameters.Add(new SqlParameter("@PilotID", rent.Pilot.EmployeeId));
                        command.Parameters.Add(new SqlParameter("@NormalRequestID", rent.NormalRequest.NormalRequestID));

                        command.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
                catch (Exception ex) { throw new DatabaseException(ex.ToString()); }
            }
        }

        public bool DeleteRent(int rentID)
        {

          
            
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "DELETE FROM [Rent] WHERE RentID = @RentID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@RentID", rentID);

                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected to determine if the deletion was successful
                        return rowsAffected > 0;
                    }
                }
                catch (SqlNullValueException ex)
                {
                    throw new DatabaseException("Error, reading null values. :" + ex.ToString());
                }
                catch (InvalidOperationException ex)
                {
                    throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString());
                }
                catch (SqlException ex)
                {
                    throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString());
                }
                catch (TimeoutException ex)
                {
                    throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString());
                }
                
            }

            
        }

        public List<Rent> GetAllRents()
        {
            List<Rent> rents = new List<Rent>();
            EmployeeDAL pilotDAL = new EmployeeDAL();
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [Rent]";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int rentID = (int)reader["RentID"];
                                double finalPrice = (double)reader["FinalPrice"];
                                int pilotID = (int)reader["PilotID"];
                                int normalRequestID = (int)reader["NormalRequestID"];

                                Pilot pilot = pilotDAL.GetPilotByID(pilotID);
                                NormalRequest normalRequest = normalRequestDAL.GetNormalRequestById(normalRequestID);

                                Rent rent = new Rent(pilot, normalRequest, finalPrice, rentID);
                                rents.Add(rent);
                            }
                        }
                    }
                }
                catch (SqlNullValueException)
                {
                    throw new DatabaseException("Error, reading null values.");
                }
                catch (InvalidOperationException)
                {
                    throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection.");
                }
                catch (SqlException)
                {
                    throw new DatabaseException("An error occurred in the SQL Server database.");
                }
                catch (TimeoutException)
                {
                    throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.");
                }
                
            }

            return rents;
        }

        public Rent GetRentById(int rentID)
        {
            Rent rent = null;
            EmployeeDAL pilotDAL = new EmployeeDAL();
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [Rent] WHERE RentID = @RentID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@RentID", rentID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                double finalPrice = (double)reader["FinalPrice"];
                                int pilotID = (int)reader["PilotID"];
                                int normalRequestID = (int)reader["NormalRequestID"];

                                Pilot pilot = pilotDAL.GetPilotByID(pilotID);
                                NormalRequest normalRequest = normalRequestDAL.GetNormalRequestById(normalRequestID);

                                rent = new Rent(pilot, normalRequest, finalPrice, rentID);
                            }
                        }
                    }
                }
                catch (SqlNullValueException)
                {
                    throw new DatabaseException("Error, reading null values.");
                }
                catch (InvalidOperationException)
                {
                    throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection.");
                }
                catch (SqlException)
                {
                    throw new DatabaseException("An error occurred in the SQL Server database.");
                }
                catch (TimeoutException)
                {
                    throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.");
                }
                catch (Exception)
                {
                    throw new DatabaseException("Error in the database.");
                }
            }

            return rent;
        }

        public List<Rent> GetRentsByUserId(int userID)
        {
            List<Rent> rents = new List<Rent>();
            EmployeeDAL pilotDAL = new EmployeeDAL();
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();

                    string sql = "SELECT r.* FROM [Rent] r " +
                                 "INNER JOIN [NormalRequest] nr ON r.NormalRequestID = nr.NormalRequestID " +
                                 "WHERE nr.UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int rentID = (int)reader["RentID"];
                                double finalPrice = (double)reader["FinalPrice"];
                                int pilotID = (int)reader["PilotID"];
                                int normalRequestID = (int)reader["NormalRequestID"];

                                Pilot pilot = pilotDAL.GetPilotByID(pilotID);
                                NormalRequest normalRequest = normalRequestDAL.GetNormalRequestById(normalRequestID);

                                Rent rent = new Rent(pilot, normalRequest, finalPrice, rentID);
                                rents.Add(rent);
                            }
                        }
                    }
                }
                catch (SqlNullValueException)
                {
                    throw new DatabaseException("Error, reading null values.");
                }
                catch (InvalidOperationException)
                {
                    throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection.");
                }
                catch (SqlException)
                {
                    throw new DatabaseException("An error occurred in the SQL Server database.");
                }
                catch (TimeoutException)
                {
                    throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.");
                }
               
            }

            return rents;
        }

        public bool UpdateRent(int rentID, Rent updatedRent)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE [Rent]
                           SET FinalPrice = @FinalPrice,
                               PilotID = @PilotID,
                               NormalRequestID = @NormalRequestID
                           WHERE RentID = @RentID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@RentID", rentID);
                        command.Parameters.AddWithValue("@FinalPrice", updatedRent.FinalPrice);
                        command.Parameters.AddWithValue("@PilotID", updatedRent.Pilot.EmployeeId);
                        command.Parameters.AddWithValue("@NormalRequestID", updatedRent.NormalRequest.NormalRequestID);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                catch (SqlNullValueException ex)
                {
                    throw new DatabaseException("Error, reading null values. :" + ex.ToString());
                }
                catch (InvalidOperationException ex)
                {
                    throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString());
                }
                catch (SqlException ex)
                {
                    throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString());
                }
                catch (TimeoutException ex)
                {
                    throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString());
                }
                catch (Exception ex)
                {
                    throw new DatabaseException(ex.ToString());
                }
            }
        }

        public List<Rent> GetAllRentsByAircraftID(int aircraftID)
        {
            List<Rent> rentList = new List<Rent>();
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();

            foreach(Rent rent in GetAllRents())
            {
                foreach(NormalRequest normalRequest in normalRequestDAL.GetNormalRequestsByAircraftID(aircraftID))
                {
                    if(rent.NormalRequest == normalRequest)
                    {
                        rentList.Add(rent);
                        break;
                    }
                }
            }

            return rentList;
        }

        public List<Rent> GetAllRentsByPilotID(int PilotID)
        {
            List<Rent> rentList = new List<Rent>();

            foreach(Rent rent in GetAllRents())
            {
                if(PilotID == rent.Pilot.EmployeeId)
                {
                    rentList.Add(rent);
                }
            }

            return rentList;
        }

        public Rent GetRentByNormalRequestID(int normalRequestID)
        {
            Rent rent = null;
            EmployeeDAL pilotDAL = new EmployeeDAL();
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT r.* FROM [Rent] r WHERE r.NormalRequestID = @NormalRequestID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@NormalRequestID", normalRequestID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int rentID = (int)reader["RentID"];
                                double finalPrice = (double)reader["FinalPrice"];
                                int pilotID = (int)reader["PilotID"];

                                Pilot pilot = pilotDAL.GetPilotByID(pilotID);
                                NormalRequest normalRequest = normalRequestDAL.GetNormalRequestById(normalRequestID);

                                rent = new Rent(pilot, normalRequest, finalPrice, rentID);
                            }
                        }
                    }
                }
                catch (SqlNullValueException)
                {
                    throw new DatabaseException("Error, reading null values.");
                }
                catch (InvalidOperationException)
                {
                    throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection.");
                }
                catch (SqlException)
                {
                    throw new DatabaseException("An error occurred in the SQL Server database.");
                }
                catch (TimeoutException)
                {
                    throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.");
                }
                
            }

            return rent;
        }
        public bool DeleteRentWithNormalRequest(int rentID)
        {
            //First delete the NormalRequest that has this rent.

            //////////////////////////////////////////////////////////////////
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();
            Rent rent = GetRentById(rentID);
            DeleteRent(rentID);
            normalRequestDAL.DeleteNormalRequest(rent.NormalRequest.NormalRequestID);
            ///////////////////////////////////////////////////////////////////////

            

            return true;
        }

        public bool DeleteRentAndUnacceptNormalRequest(int rentID)
        {
            //First update the NormalRequest that has this rent.

            //////////////////////////////////////////////////////////////////
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();
            Rent rent = GetRentById(rentID);
            NormalRequest oldNormalRequest = normalRequestDAL.GetNormalRequestById(rent.NormalRequest.NormalRequestID);
            oldNormalRequest.DisApproveRequest();
            normalRequestDAL.UpdateNormalRequest(oldNormalRequest.NormalRequestID, oldNormalRequest);
            ///////////////////////////////////////////////////////////////////////

            DeleteRent(rentID);

            return true;
        }

    }
}
