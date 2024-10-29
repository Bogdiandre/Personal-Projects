using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using DomainLibrary.Domains;
using DomainLibrary;
using Interfaces;
using DALLibrary.CONS;
using DomainLibrary.Exceptions;
using DomainLibrary.Enumerations;

namespace DALLibrary
{
    public class PilotRequestDAL : Cons, iPilotRequestRepository
    {
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            return conn;
        }

        public bool AddPilotRequest(PilotRequest pilotRequest)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [PilotRequest] (RequestStart, RequestEnd, RequestPrice, HelicopterID, UserID) VALUES (@RequestStart, @RequestEnd, @RequestPrice, @HelicopterID, @UserID)";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@RequestStart", pilotRequest.RequestStart));
                        command.Parameters.Add(new SqlParameter("@RequestEnd", pilotRequest.RequestEnd));
                        command.Parameters.Add(new SqlParameter("@RequestPrice", pilotRequest.RequestPrice));
                        command.Parameters.Add(new SqlParameter("@HelicopterID", pilotRequest.Helicopter.AircraftId));
                        command.Parameters.Add(new SqlParameter("@UserID", pilotRequest.User.UserId));

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

        public bool DeletePilotRequest(int pilotRequestID)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"DELETE FROM [PilotRequest] WHERE PilotRequestID = @pilotRequestID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@pilotRequestID", pilotRequestID);

                        command.ExecuteNonQuery();
                    }

                    return true;
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

                return false;
            }
        }

        public List<PilotRequest> GetAllPilotRequests()
        {
            List<PilotRequest> pilotRequests = new List<PilotRequest>();
            AircraftDAL aircraftDAL = new AircraftDAL();
            UserDAL userDAL = new UserDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [PilotRequest]";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int pilotRequestId = (int)reader["PilotRequestID"];
                                DateTime requestStart = (DateTime)reader["RequestStart"];
                                DateTime requestEnd = (DateTime)reader["RequestEnd"];
                                double requestPrice = (double)reader["RequestPrice"];
                                int helicopterId = (int)reader["HelicopterID"];
                                int userId = (int)reader["UserID"];

                                
                                Helicopter helicopter = aircraftDAL.GetHelicopterById(helicopterId);
                                User user = userDAL.GetUserById(userId);

                                PilotRequest pilotRequest = new PilotRequest(requestStart, requestEnd, requestPrice, helicopter, user, pilotRequestId);
                                pilotRequests.Add(pilotRequest);
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

            return pilotRequests;
        }

        public PilotRequest GetPilotRequestById(int pilotRequestID)
        {
            PilotRequest pilotRequest = null;

            AircraftDAL aircraftDAL = new AircraftDAL();
            UserDAL userDAL = new UserDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [PilotRequest] WHERE PilotRequestID = @PilotRequestID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@PilotRequestID", pilotRequestID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime requestStart = (DateTime)reader["RequestStart"];
                                DateTime requestEnd = (DateTime)reader["RequestEnd"];
                                double requestPrice = (double)reader["RequestPrice"];
                                int helicopterId = (int)reader["HelicopterID"];
                                int userId = (int)reader["UserID"];

                                Helicopter helicopter = aircraftDAL.GetHelicopterById(helicopterId);
                                User user = userDAL.GetUserById(userId);

                                pilotRequest = new PilotRequest(requestStart, requestEnd, requestPrice, helicopter, user, pilotRequestID);
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

            return pilotRequest;
        }

        public bool UpdatePilotRequest(int pilotRequestID, PilotRequest pilReq)
        {
            throw new NotImplementedException();
        }

        public List<PilotRequest> GetPilotRequestByUserID(int userID)
        {
            List<PilotRequest> pilotRequests = new List<PilotRequest>();
            AircraftDAL aircraftDAL = new AircraftDAL();
            UserDAL userDAL = new UserDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [PilotRequest] WHERE UserID = @UserID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", userID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int pilotRequestId = (int)reader["PilotRequestID"];
                                DateTime requestStart = (DateTime)reader["RequestStart"];
                                DateTime requestEnd = (DateTime)reader["RequestEnd"];
                                double requestPrice = (double)reader["RequestPrice"];
                                int helicopterId = (int)reader["HelicopterID"];

                                Helicopter helicopter = aircraftDAL.GetHelicopterById(helicopterId);
                                User user = userDAL.GetUserById(userID);

                                PilotRequest pilotRequest = new PilotRequest(requestStart, requestEnd, requestPrice, helicopter, user, pilotRequestId);
                                pilotRequests.Add(pilotRequest);
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

            return pilotRequests;
        }

        public List<PilotRequest> GetAllPilotRequestsByHelicopterID(int helicopterID)
        {
            List<PilotRequest> pilotRequests = new List<PilotRequest>();
            AircraftDAL aircraftDAL = new AircraftDAL();
            UserDAL userDAL = new UserDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [PilotRequest] WHERE HelicopterID = @HelicopterID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@HelicopterID", helicopterID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int pilotRequestId = (int)reader["PilotRequestID"];
                                DateTime requestStart = (DateTime)reader["RequestStart"];
                                DateTime requestEnd = (DateTime)reader["RequestEnd"];
                                double requestPrice = (double)reader["RequestPrice"];
                                int userId = (int)reader["UserID"];

                                Helicopter helicopter = aircraftDAL.GetHelicopterById(helicopterID);
                                User user = userDAL.GetUserById(userId);

                                PilotRequest pilotRequest = new PilotRequest(requestStart, requestEnd, requestPrice, helicopter, user, pilotRequestId);
                                pilotRequests.Add(pilotRequest);
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

            return pilotRequests;
        }


    }
}
