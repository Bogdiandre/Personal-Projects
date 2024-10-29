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
    public class NormalRequestDAL : Cons, iNormalRequestRepository
    {
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            return conn;
        }
        public bool AddNormalRequest(NormalRequest norReq)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [NormalRequest] (DepartureDay, ComingBackDay, RequestMaxPrice, RequestMinPrice, DestinationID, AircraftID, UserID, Accepted) VALUES (@DepartureDay, @ComingBackDay, @RequestMaxPrice, @RequestMinPrice, @DestinationID, @AircraftID, @UserID, @Accepted)";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@DepartureDay", norReq.DepartureDay));
                        command.Parameters.Add(new SqlParameter("@ComingBackDay", norReq.ComingBackDay));
                        command.Parameters.Add(new SqlParameter("@RequestMaxPrice", norReq.RequestMaxPrice));
                        command.Parameters.Add(new SqlParameter("@RequestMinPrice", norReq.RequestMinPrice));
                        command.Parameters.Add(new SqlParameter("@DestinationID", norReq.Destination.DestinationId));
                        command.Parameters.Add(new SqlParameter("@AircraftID", norReq.Aircraft.AircraftId));
                        command.Parameters.Add(new SqlParameter("@UserID", norReq.User.UserId));
                        command.Parameters.Add(new SqlParameter("@Accepted", norReq.Accepted));

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

        public bool DeleteNormalRequest(int normalRequestID)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"DELETE FROM [NormalRequest] WHERE NormalRequestID = @normalRequestID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@normalRequestID", normalRequestID);

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

        public List<NormalRequest> GetAllNormalRequests()
        {
            List<NormalRequest> normalRequests = new List<NormalRequest>();
            AircraftDAL aircraftDAL = new AircraftDAL();
            UserDAL userDAL = new UserDAL();
            DestinationDAL destinationDAL = new DestinationDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [NormalRequest]";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int normalRequestId = (int)reader["NormalRequestID"];
                                DateTime departureDay = (DateTime)reader["DepartureDay"];
                                DateTime comingBackDay = (DateTime)reader["ComingBackDay"];
                                double requestMaxPrice = (double)reader["RequestMaxPrice"];
                                double requestMinPrice = (double)reader["RequestMinPrice"];
                                int destinationId = (int)reader["DestinationID"];
                                int aircraftId = (int)reader["AircraftID"];
                                int userId = (int)reader["UserID"];
                                bool accepted = (bool)reader["Accepted"];

                                Destination destination = destinationDAL.GetDestinationByID(destinationId);
                                Aircraft aircraft = aircraftDAL.GetAircraftById(aircraftId);
                                User user = userDAL.GetUserById(userId);

                                NormalRequest normalRequest = new NormalRequest( requestMinPrice, requestMaxPrice, departureDay, comingBackDay, destination, aircraft, user, accepted, normalRequestId);
                                normalRequests.Add(normalRequest);
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

            return normalRequests;
        }

        public NormalRequest GetNormalRequestById(int normalRequestID)
        {
            NormalRequest normalRequest = null;
            AircraftDAL aircraftDAL = new AircraftDAL();
            UserDAL userDAL = new UserDAL();
            DestinationDAL destinationDAL = new DestinationDAL(); // Assuming you have a DestinationDAL class

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [NormalRequest] WHERE NormalRequestID = @NormalRequestID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@NormalRequestID", normalRequestID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DateTime departureDay = (DateTime)reader["DepartureDay"];
                                DateTime comingBackDay = (DateTime)reader["ComingBackDay"];
                                double requestMaxPrice = (double)reader["RequestMaxPrice"];
                                double requestMinPrice = (double)reader["RequestMinPrice"];
                                int destinationId = (int)reader["DestinationID"];
                                int aircraftId = (int)reader["AircraftID"];
                                int userId = (int)reader["UserID"];
                                bool accepted = (bool)reader["Accepted"];

                                Destination destination = destinationDAL.GetDestinationByID(destinationId);
                                Aircraft aircraft = aircraftDAL.GetAircraftById(aircraftId);
                                User user = userDAL.GetUserById(userId);

                                normalRequest = new NormalRequest(requestMinPrice, requestMaxPrice, departureDay, comingBackDay, destination, aircraft, user, accepted, normalRequestID);
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

            return normalRequest;
        }

        public bool UpdateNormalRequest(int normalRequestID, NormalRequest norReq)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = @"UPDATE [NormalRequest]
                           SET DepartureDay = @DepartureDay,
                               ComingBackDay = @ComingBackDay,
                               RequestMaxPrice = @RequestMaxPrice,
                               RequestMinPrice = @RequestMinPrice,
                               DestinationID = @DestinationID,
                               AircraftID = @AircraftID,
                               UserID = @UserID,
                               Accepted = @Accepted
                           WHERE NormalRequestID = @NormalRequestID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@NormalRequestID", normalRequestID);
                        command.Parameters.AddWithValue("@DepartureDay", norReq.DepartureDay);
                        command.Parameters.AddWithValue("@ComingBackDay", norReq.ComingBackDay);
                        command.Parameters.AddWithValue("@RequestMaxPrice", norReq.RequestMaxPrice);
                        command.Parameters.AddWithValue("@RequestMinPrice", norReq.RequestMinPrice);
                        command.Parameters.AddWithValue("@DestinationID", norReq.Destination.DestinationId);
                        command.Parameters.AddWithValue("@AircraftID", norReq.Aircraft.AircraftId);
                        command.Parameters.AddWithValue("@UserID", norReq.User.UserId);
                        command.Parameters.AddWithValue("@Accepted", norReq.Accepted);

                        int rowsAffected = command.ExecuteNonQuery();

                        // Check if any rows were affected to determine if the update was successful
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

        public List<NormalRequest> GetNormalRequestByUserID(int UserID)
        {
            List<NormalRequest> normalRequests = new List<NormalRequest>();
            AircraftDAL aircraftDAL = new AircraftDAL();
            UserDAL userDAL = new UserDAL();
            DestinationDAL destinationDAL = new DestinationDAL();

            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [NormalRequest] WHERE UserID = @UserID AND Accepted = 0";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@UserID", UserID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int normalRequestId = (int)reader["NormalRequestID"];
                                DateTime departureDay = (DateTime)reader["DepartureDay"];
                                DateTime comingBackDay = (DateTime)reader["ComingBackDay"];
                                double requestMaxPrice = (double)reader["RequestMaxPrice"];
                                double requestMinPrice = (double)reader["RequestMinPrice"];
                                int destinationId = (int)reader["DestinationID"];
                                int aircraftId = (int)reader["AircraftID"];
                                bool accepted = (bool)reader["Accepted"];

                                Destination destination = destinationDAL.GetDestinationByID(destinationId);
                                Aircraft aircraft = aircraftDAL.GetAircraftById(aircraftId);
                                User user = userDAL.GetUserById(UserID);

                                NormalRequest normalRequest = new NormalRequest(requestMinPrice, requestMaxPrice, departureDay, comingBackDay, destination, aircraft, user, accepted, normalRequestId);
                                normalRequests.Add(normalRequest);
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

            return normalRequests;
        }

        public List<NormalRequest> GetNormalRequestsByAircraftID(int aircraftID)
        {
            List<NormalRequest> normalRequests = new List<NormalRequest>();
            AircraftDAL aircraftDAL = new AircraftDAL();
            UserDAL userDAL = new UserDAL();
            DestinationDAL destinationDAL = new DestinationDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [NormalRequest] WHERE AircraftID = @AircraftID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@AircraftID", aircraftID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int normalRequestId = (int)reader["NormalRequestID"];
                                DateTime departureDay = (DateTime)reader["DepartureDay"];
                                DateTime comingBackDay = (DateTime)reader["ComingBackDay"];
                                double requestMaxPrice = (double)reader["RequestMaxPrice"];
                                double requestMinPrice = (double)reader["RequestMinPrice"];
                                int destinationId = (int)reader["DestinationID"];
                                int userId = (int)reader["UserID"];
                                bool accepted = (bool)reader["Accepted"];

                                Destination destination = destinationDAL.GetDestinationByID(destinationId);
                                Aircraft aircraft = aircraftDAL.GetAircraftById(aircraftID);
                                User user = userDAL.GetUserById(userId);

                                NormalRequest normalRequest = new NormalRequest(requestMinPrice, requestMaxPrice, departureDay, comingBackDay, destination, aircraft, user, accepted, normalRequestId);
                                normalRequests.Add(normalRequest);
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

            return normalRequests;
        }

        public List<NormalRequest> GetAllNormalRequestsByEmail(string email)
        {
            try
            {


                UserDAL userDAL = new UserDAL();
                User user = userDAL.GetUserByEmail(email);

                List<NormalRequest> normalRequests = new List<NormalRequest>();
                if (user != null)
                {
                    normalRequests = GetNormalRequestByUserID(user.UserId);
                }
                else
                    normalRequests = null;


                return normalRequests;
            }
            catch (SqlNullValueException)
            {
                throw new DatabaseException("Not found! ");
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

        public List<NormalRequest> GetAllNormalRequestsByDestinationID(int destinationID)
        {
            List<NormalRequest> normalRequests = new List<NormalRequest>();
            AircraftDAL aircraftDAL = new AircraftDAL();
            UserDAL userDAL = new UserDAL();
            DestinationDAL destinationDAL = new DestinationDAL();

            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [NormalRequest] WHERE DestinationID = @DestinationID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@DestinationID", destinationID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int normalRequestId = (int)reader["NormalRequestID"];
                                DateTime departureDay = (DateTime)reader["DepartureDay"];
                                DateTime comingBackDay = (DateTime)reader["ComingBackDay"];
                                double requestMaxPrice = (double)reader["RequestMaxPrice"];
                                double requestMinPrice = (double)reader["RequestMinPrice"];
                                int aircraftId = (int)reader["AircraftID"];
                                int userId = (int)reader["UserID"];
                                bool accepted = (bool)reader["Accepted"];

                                Destination destination = destinationDAL.GetDestinationByID(destinationID);
                                Aircraft aircraft = aircraftDAL.GetAircraftById(aircraftId);
                                User user = userDAL.GetUserById(userId);

                                NormalRequest normalRequest = new NormalRequest(requestMinPrice, requestMaxPrice, departureDay, comingBackDay, destination, aircraft, user, accepted, normalRequestId);
                                normalRequests.Add(normalRequest);
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

            return normalRequests;
        }



    }
}
