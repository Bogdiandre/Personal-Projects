using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALLibrary.CONS;
using DomainLibrary;
using DomainLibrary.Domains;
using DomainLibrary.Enumerations;
using Interfaces;
using DomainLibrary.Exceptions;

namespace DALLibrary
{
    public class DestinationDAL : Cons, iDestinationRepository
    {
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            return conn;
        }

        public List<Destination> GetAllDestinations()
        {
            List<Destination> destinations = new List<Destination>();


            SqlConnection conn = GetConnection();
            using (conn)
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Destination";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                destinations.Add(new Destination(reader.GetString("Name"), reader.GetInt32("Distance"), reader.GetInt32("DestinationID")));

                            }
                        }
                    }
                    return destinations;
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
                    throw new DatabaseException("An error occured in the SQL Server database.");
                }
                catch (TimeoutException)
                {
                    throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  ");
                }
                catch (Exception)
                {
                    throw new DatabaseException("Error in the database.");
                }
            }
        }

        public Destination GetDestinationByID(int destinationId)
        {
            Destination destination = null;
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT * FROM [Destination] WHERE DestinationID = @DestinationId";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("@DestinationId", destinationId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                destination = new Destination(
                                    reader.GetString(reader.GetOrdinal("Name")),
                                    reader.GetInt32(reader.GetOrdinal("Distance")),
                                    reader.GetInt32(reader.GetOrdinal("DestinationID"))
                                );
                            }
                        }
                    }

                    return destination;
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
        }

        public bool AddDestination(Destination des)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [Destination] (Name, Distance) VALUES (@Name, @Distance)";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@Name", des.Name));
                        command.Parameters.Add(new SqlParameter("@Distance", des.Distance));
                        


                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occured in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
                catch (Exception ex) { throw new DatabaseException(ex.ToString()); }
                return false;
            }
        }

        public bool DeleteDestination(int id)
        {
            SqlConnection conn = GetConnection();
            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"DELETE FROM [Destination] WHERE DestinationID = @destinationID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@destinationId", id);

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
                    throw new DatabaseException("An error occured in the SQL Server database.");
                }
                catch (TimeoutException)
                {
                    throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  ");
                }
                catch (Exception)
                {
                    throw new DatabaseException("Error in the database.");
                }
                return false;

            }
        }

        public bool UpdateDestination(int id, Destination des)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();


                    string updateQuery = "UPDATE Destination SET " +
                        "Name = @Name, " +
                        "Distance = @Distance " +
                        "WHERE DestinationID = @destinationId";

                    using (SqlCommand command = new SqlCommand(updateQuery, conn))
                    {

                        command.Parameters.AddWithValue("@destinationId", id);
                        command.Parameters.AddWithValue("@Name", des.Name);
                        command.Parameters.AddWithValue("@Distance", des.Distance);
                        

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
        public bool DeleteEverythingForDestination(int destinationID)
        {
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();
            RentDAL rentDAL = new RentDAL();

            List<NormalRequest> normalRequestList = normalRequestDAL.GetAllNormalRequestsByDestinationID(destinationID);

            foreach(NormalRequest normalRequest in normalRequestList)
            {
                if (normalRequest.Accepted)
                {
                    Rent rent = rentDAL.GetRentByNormalRequestID(normalRequest.NormalRequestID);
                    rentDAL.DeleteRent(rent.RentID);

                }

                normalRequestDAL.DeleteNormalRequest(normalRequest.NormalRequestID);
            }

            return true;

        }
    }
}
