using DomainLibrary.Domains;
using Interfaces;
using DALLibrary.CONS;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using DomainLibrary.Enumerations;
using DomainLibrary;
using DomainLibrary.Exceptions;
using System.Runtime.CompilerServices;

namespace DALLibrary
{
    public class AircraftDAL : Cons, iAircraftRepository
    {
        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            return conn;
        }

        public bool AddAircraft(Aircraft aircraft)
        {
            SqlConnection conn = GetConnection();

            int newAircraftID;

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [Aircraft] (Name, Age, Weight, Range, AverageSpeed, Consumption) VALUES (@Name, @Age, @Weight, @Range, @AverageSpeed, @Consumption); SELECT SCOPE_IDENTITY();";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@Name", aircraft.Name));
                        command.Parameters.Add(new SqlParameter("@Age", aircraft.Age));
                        command.Parameters.Add(new SqlParameter("@Weight", aircraft.Weight));
                        command.Parameters.Add(new SqlParameter("@Range", aircraft.Range));
                        command.Parameters.Add(new SqlParameter("@AverageSpeed", aircraft.AverageSpeed));
                        command.Parameters.Add(new SqlParameter("@Consumption", aircraft.Consumption));



                        newAircraftID = Convert.ToInt32(command.ExecuteScalar());
                    }




                    if (aircraft.GetType() == typeof(PrivateJet))
                    {
                        return CreatePrivateJet((PrivateJet)aircraft, newAircraftID);
                    }
                    if (aircraft.GetType() == typeof(Helicopter))
                    {
                        return CreateHelicopter((Helicopter)aircraft, newAircraftID);
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

        private bool CreatePrivateJet(PrivateJet privateJet, int privateJetID)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [PrivateJet] (PrivateJetID, SeatNumber) VALUES (@PrivateJetID, @SeatNumber)";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@PrivateJetID", privateJetID));
                        command.Parameters.Add(new SqlParameter("@SeatNumber", privateJet.SeatNumber));
                        
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

        private bool CreateHelicopter(Helicopter helicopter, int helicopterID)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [Helicopter] (HelicopterID, PricePerDay) VALUES (@HelicopterID, @PricePerDay)";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@HelicopterID", helicopterID));
                        command.Parameters.Add(new SqlParameter("@PricePerDay", helicopter.PricePerDay));

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

        public bool DeleteAircraft(int aircraftId)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();

                    
                    

                    using (SqlCommand command = new SqlCommand("DELETE FROM Helicopter WHERE HelicopterID = @aircraftId", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@aircraftId", aircraftId));
                        command.ExecuteNonQuery();
                    }

                    
                    using (SqlCommand command = new SqlCommand("DELETE FROM PrivateJet WHERE PrivateJetID = @aircraftId", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@aircraftId", aircraftId));
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand("DELETE FROM Aircraft WHERE ID = @aircraftId", conn))
                    {
                        command.Parameters.Add(new SqlParameter("@aircraftId", aircraftId));
                        command.ExecuteNonQuery();
                    }

                    return true;
                }
                catch (SqlNullValueException ex)
                {
                    throw new DatabaseException("Error, reading null values. :" + ex.ToString());
                }
                catch (InvalidOperationException ex)
                {
                    throw new DatabaseException("An operation is attempted that is not valid for the current state of the database GetConnection(). :" + ex.ToString());
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

            return false;
        }

        public Aircraft GetAircraftById(int AircraftID)
        {
            string aircraftType = null;
            string query = @"
        SELECT 'PrivateJet' AS AircraftType FROM PrivateJet AS PJ WHERE PJ.PrivateJetID = @AircraftId
        UNION
        SELECT 'Helicopter' AS AircraftType FROM Helicopter AS H WHERE H.HelicopterID = @AircraftId";

            using (SqlConnection conn = GetConnection())
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@AircraftId", AircraftID);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            aircraftType = reader["AircraftType"].ToString();
                        }
                    }
                }
            }

            switch (aircraftType)
            {
                case "PrivateJet":
                    return GetPrivateJetById(AircraftID);
                case "Helicopter":
                    return GetHelicopterById(AircraftID);
                default:
                    return null;
            }
        }

        public PrivateJet GetPrivateJetById(int privateJetID)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT a.Name, a.Age, a.Weight, a.Range, a.AverageSpeed, a.Consumption, pj.SeatNumber " +
                                   "FROM Aircraft AS a INNER JOIN PrivateJet AS pj ON a.ID = pj.PrivateJetID " +
                                   "WHERE a.ID = @privateJetId;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@privateJetId", privateJetID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = (string)reader["Name"];
                                int age = (int)reader["Age"];
                                int weight = (int)reader["Weight"];
                                int range = (int)reader["Range"];
                                int averageSpeed = (int)reader["AverageSpeed"];
                                int consumption = (int)reader["Consumption"];
                                int seatNumber = (int)reader["SeatNumber"];

                                return new PrivateJet(name.Trim(), age, weight, averageSpeed, range, consumption, seatNumber, privateJetID);
                            }
                        }
                    }
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
                catch (Exception ex) { throw new DatabaseException(ex.ToString()); }
            }

            return null;
        }

        public Helicopter GetHelicopterById(int helicopterID)
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT a.Name, a.Age, a.Weight, a.Range, a.AverageSpeed, a.Consumption, h.PricePerDay " +
                                   "FROM Aircraft AS a INNER JOIN Helicopter AS h ON a.ID = h.HelicopterID " +
                                   "WHERE a.ID = @helicopterId;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@helicopterId", helicopterID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = (string)reader["Name"];
                                int age = (int)reader["Age"];
                                int weight = (int)reader["Weight"];
                                int range = (int)reader["Range"];
                                int averageSpeed = (int)reader["AverageSpeed"];
                                int consumption = (int)reader["Consumption"];
                                int pricePerDay = (int)reader["PricePerDay"];

                                return new Helicopter(name.Trim(), age, weight, averageSpeed, range, consumption, pricePerDay, helicopterID);
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

        public List<Aircraft> GetAllAircrafts()
        {
            SqlConnection conn = GetConnection();
            List<Aircraft> aircraftList = new List<Aircraft>();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT a.ID, a.Name, a.Age, a.Weight, a.Range, a.AverageSpeed, a.Consumption, h.PricePerDay, pj.SeatNumber FROM [Aircraft] AS a LEFT JOIN Helicopter AS h ON a.ID = h.HelicopterID LEFT JOIN PrivateJet AS pj ON a.ID = pj.PrivateJetID";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int aircraftId = Convert.ToInt32(reader[0]);
                                string name = reader[1].ToString().Trim();
                                int age = Convert.ToInt32(reader[2]);
                                int weight = Convert.ToInt32(reader[3]);
                                int range = Convert.ToInt32(reader[4]);
                                int averageSpeed = Convert.ToInt32(reader[5]);
                                int consumption = Convert.ToInt32(reader[6]);

                                if (reader.IsDBNull(7))
                                {
                                    int seatNumber = Convert.ToInt32(reader[8]);

                                    PrivateJet privateJet = new PrivateJet(name, age, weight, averageSpeed, range, consumption, seatNumber, aircraftId);
                                    aircraftList.Add(privateJet);
                                }
                                else
                                {
                                    int pricePerDay = Convert.ToInt32(reader[7]);

                                    Helicopter helicopter = new Helicopter(name, age, weight, averageSpeed, range, consumption, pricePerDay, aircraftId);
                                    aircraftList.Add(helicopter);
                               }
                            }
                        }
                    }

                    return aircraftList;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
                catch (Exception ex) { throw new DatabaseException(ex.ToString()); }
            }
        }

        public List<Helicopter> GetAllHelicopters()
        {
            SqlConnection conn = GetConnection();
            List<Helicopter> helicopterList = new List<Helicopter>();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT a.ID, a.Name, a.Age, a.Weight, a.Range, a.AverageSpeed, a.Consumption, h.PricePerDay FROM [Aircraft] AS a INNER JOIN Helicopter AS h ON a.ID = h.HelicopterID";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int helicopterId = Convert.ToInt32(reader[0]);
                                string name = reader[1].ToString().Trim();
                                int age = Convert.ToInt32(reader[2]);
                                int weight = Convert.ToInt32(reader[3]);
                                int range = Convert.ToInt32(reader[4]);
                                int averageSpeed = Convert.ToInt32(reader[5]);
                                int consumption = Convert.ToInt32(reader[6]);
                                int pricePerDay = Convert.ToInt32(reader[7]);

                                Helicopter helicopter = new Helicopter(name, age, weight, averageSpeed, range, consumption, pricePerDay, helicopterId);
                                helicopterList.Add(helicopter);
                            }
                        }
                    }

                    return helicopterList;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
                catch (Exception ex) { throw new DatabaseException(ex.ToString()); }
            }
        }

        public List<PrivateJet> GetAllPrivateJets()
        {
            SqlConnection conn = GetConnection();
            List<PrivateJet> privateJetList = new List<PrivateJet>();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = "SELECT a.ID, a.Name, a.Age, a.Weight, a.Range, a.AverageSpeed, a.Consumption, pj.SeatNumber FROM [Aircraft] AS a INNER JOIN PrivateJet AS pj ON a.ID = pj.PrivateJetID";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int privateJetId = Convert.ToInt32(reader[0]);
                                string name = reader[1].ToString().Trim();
                                int age = Convert.ToInt32(reader[2]);
                                int weight = Convert.ToInt32(reader[3]);
                                int range = Convert.ToInt32(reader[4]);
                                int averageSpeed = Convert.ToInt32(reader[5]);
                                int consumption = Convert.ToInt32(reader[6]);
                                int seatNumber = Convert.ToInt32(reader[7]);

                                PrivateJet privateJet = new PrivateJet(name, age, weight, averageSpeed, range, consumption, seatNumber, privateJetId);
                                privateJetList.Add(privateJet);
                            }
                        }
                    }

                    return privateJetList;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
                
            }
        }

        private List<Aircraft> GetAllHelicoptersAsAircaft()
        {
            List<Aircraft> listAircraft = new List<Aircraft>();

            foreach(Aircraft aircraft in GetAllHelicopters())
            {
                listAircraft.Add(aircraft);
            }

            return listAircraft;
        }

        private List<Aircraft> GetAllPrivateJetsAsAircraft()
        {
            List<Aircraft> listAircraft = new List<Aircraft>();

            foreach (Aircraft aircraft in GetAllPrivateJets())
            {
                listAircraft.Add(aircraft);
            }

            return listAircraft;
        }

        public bool UpdateAircraft(int AircraftID, Aircraft aircraft)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string updateSql = $"UPDATE [Aircraft] SET Name = @Name, Age = @Age, Weight = @Weight, Range = @Range, AverageSpeed = @AverageSpeed, Consumption = @Consumption WHERE ID = @AircraftID";
                    using (SqlCommand updateCommand = new SqlCommand(updateSql, conn))
                    {
                        updateCommand.Parameters.Add(new SqlParameter("@AircraftID", AircraftID));
                        updateCommand.Parameters.Add(new SqlParameter("@Name", aircraft.Name));
                        updateCommand.Parameters.Add(new SqlParameter("@Age", aircraft.Age));
                        updateCommand.Parameters.Add(new SqlParameter("@Weight", aircraft.Weight));
                        updateCommand.Parameters.Add(new SqlParameter("@Range", aircraft.Range));
                        updateCommand.Parameters.Add(new SqlParameter("@AverageSpeed", aircraft.AverageSpeed));
                        updateCommand.Parameters.Add(new SqlParameter("@Consumption", aircraft.Consumption));

                        updateCommand.ExecuteNonQuery();
                    }

                    if (aircraft is PrivateJet privateJet)
                    {
                        return UpdatePrivateJet(AircraftID, privateJet);
                    }
                    if (aircraft is Helicopter helicopter)
                    {
                        return UpdateHelicopter(AircraftID, helicopter);
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

        private bool UpdatePrivateJet(int PrivateJetID, PrivateJet privateJet)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string updateSql = $"UPDATE [PrivateJet] SET SeatNumber = @SeatNumber WHERE PrivateJetID = @PrivateJetID";
                    using (SqlCommand updateCommand = new SqlCommand(updateSql, conn))
                    {
                        updateCommand.Parameters.Add(new SqlParameter("@PrivateJetID", PrivateJetID));
                        updateCommand.Parameters.Add(new SqlParameter("@SeatNumber", privateJet.SeatNumber));

                        updateCommand.ExecuteNonQuery();
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

        private bool UpdateHelicopter(int HelicopterID, Helicopter helicopter)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string updateSql = $"UPDATE [Helicopter] SET PricePerDay = @PricePerDay WHERE HelicopterID = @HelicopterID";
                    using (SqlCommand updateCommand = new SqlCommand(updateSql, conn))
                    {
                        updateCommand.Parameters.Add(new SqlParameter("@HelicopterID", HelicopterID));
                        updateCommand.Parameters.Add(new SqlParameter("@PricePerDay", helicopter.PricePerDay));

                        updateCommand.ExecuteNonQuery();
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

        private List<Aircraft> GetAllAircraftForDestination(int Distance, List<Aircraft> aircraftList)
        {
            
            List<Aircraft> list = new List<Aircraft>();

            if (aircraftList != null)
            {
                foreach (Aircraft aircraft in aircraftList)
                {
                    if (aircraft.Range >= Distance)
                    {
                        list.Add(aircraft);
                    }
                }

                return list;
            }

            return null;
        }

        public List<Helicopter> GetAvailableHelicopterForPrivateRequest(DateTime RequestStart, DateTime RequestEnd)
        {
            

            PilotRequestDAL pilotRequestDAL = new PilotRequestDAL();
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();
            List<Helicopter> availableHelicopters = new List<Helicopter>();

            foreach (Helicopter heli in GetAllHelicopters())
            {

                bool free = true;

                List<PilotRequest> pilotRequests = pilotRequestDAL.GetAllPilotRequestsByHelicopterID(heli.AircraftId);
                List<NormalRequest> normalRequests = normalRequestDAL.GetNormalRequestsByAircraftID(heli.AircraftId);

                if (pilotRequests != null || normalRequests != null)
                {
                    if (pilotRequests != null)
                    {

                        foreach (PilotRequest pr in pilotRequests)
                        {
                            if (!(RequestEnd < pr.RequestStart || RequestStart > pr.RequestEnd))
                            {
                                free = false;
                                break;
                            }
                        }
                        
                    }

                    if(normalRequests != null && free == true)
                    {
                       // bool free = true;

                        foreach (NormalRequest nr in normalRequests)
                        {
                            if (!(RequestEnd < nr.DepartureDay || RequestStart > nr.ComingBackDay))
                            {
                                free = false;
                                break;
                            }
                        }
                        
                    }
                    if (free == true)
                    {
                        availableHelicopters.Add(heli);
                    }
                }
                else
                {
                    availableHelicopters.Add(heli);
                }
            }

            return availableHelicopters;

        }

        private List<Aircraft> GetAvailableAircraftForNormalRequestMethod(DateTime RequestStart, DateTime RequestEnd, int Distance, List<Aircraft> listAircraft)
        {
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();
            PilotRequestDAL pilotRequestDAL = new PilotRequestDAL();
            List<Aircraft> aircraftList = new List<Aircraft>();

            foreach (Aircraft aircraft in GetAllAircraftForDestination(Distance, listAircraft)) //based on the Distance, I get all of the available aircrafts
            {

                bool free = true;

                //List<Rent> rentList = rentDAL.GetAllRentsByAircraftID(aircraft.AircraftId); //I get all of the rents based on the aircraftID
                List<NormalRequest> normalRequests = normalRequestDAL.GetNormalRequestsByAircraftID(aircraft.AircraftId);
                List<PilotRequest> pilotRequests = new List<PilotRequest>();
                if (aircraft is Helicopter)
                {
                    pilotRequests = pilotRequestDAL.GetAllPilotRequestsByHelicopterID(aircraft.AircraftId);
                }

                if (normalRequests != null || pilotRequests != null)
                {
                    if (normalRequests != null)
                    {
                        foreach (NormalRequest nr in normalRequests)
                        {
                            if (!(RequestEnd < nr.DepartureDay || RequestStart > nr.ComingBackDay))
                            {
                                free = false;
                                break;
                            }

                        }

                    }
                    if (pilotRequests != null && free == true)
                    {
                        foreach (PilotRequest pr in pilotRequests)
                        {
                            if (!(RequestEnd < pr.RequestStart || RequestStart > pr.RequestEnd))
                            {
                                free = false;
                                break;
                            }

                        }

                    }
                    if (free)
                    {
                        aircraftList.Add(aircraft);
                    }
                }
                else
                {
                    aircraftList.Add(aircraft);
                }
            }

            return aircraftList;
        }

        public List<Aircraft> GetAvailableAircraftsForNormalRequest(DateTime RequestStart, DateTime RequestEnd, int Distance, List<Pilot> pilotList)
        {
            List<Aircraft> aircraftList = new List<Aircraft>();
            

            Boolean isPrivateJet = false;
            Boolean isHelicopter = false;

            foreach (Pilot p in pilotList)
            {
                if (p.License == AircraftType.Helicopter)
                {
                    isHelicopter = true;
                }
                if (p.License == AircraftType.PrivateJet)
                {
                    isPrivateJet = true;
                }
                if (isPrivateJet == true && isHelicopter == true)
                {
                    break;
                }
            }


            if (isPrivateJet && isHelicopter )
            {
                aircraftList = GetAvailableAircraftForNormalRequestMethod(RequestStart, RequestEnd, Distance, GetAllAircrafts());
            }
            else if(isPrivateJet && !isHelicopter)
            {
                aircraftList = GetAvailableAircraftForNormalRequestMethod(RequestStart, RequestEnd, Distance, GetAllPrivateJetsAsAircraft());
            }
            else if (!isPrivateJet && isHelicopter)
            {
                aircraftList = GetAvailableAircraftForNormalRequestMethod(RequestStart, RequestEnd, Distance, GetAllHelicoptersAsAircaft());
            }
            else if (!isPrivateJet && !isHelicopter)
            {
                aircraftList = null;
            }

                return aircraftList;

        }

        public bool DeleteEverythingForAircraft(int aircraftID)
        {
            NormalRequestDAL normalRequestDAL = new NormalRequestDAL();
            RentDAL rentDAL = new RentDAL();

            List<NormalRequest> normalRequestList = normalRequestDAL.GetNormalRequestsByAircraftID(aircraftID);
            if (normalRequestList.Count != 0)
            { foreach (NormalRequest normalRequest in normalRequestList)
                {
                    if (normalRequest.Accepted)
                    {

                        Rent rent = rentDAL.GetRentByNormalRequestID(normalRequest.NormalRequestID);
                        rentDAL.DeleteRent(rent.RentID);

                    }

                    normalRequestDAL.DeleteNormalRequest(normalRequest.NormalRequestID);
                }
            }

            if(GetAircraftById(aircraftID) is Helicopter)
            {
                PilotRequestDAL pilotRequestDAL = new PilotRequestDAL();

                List<PilotRequest> listPilotRequest = pilotRequestDAL.GetAllPilotRequestsByHelicopterID(aircraftID);

                if(listPilotRequest.Count != 0)
                {
                    foreach (PilotRequest pilotRequest in listPilotRequest)
                    {
                        pilotRequestDAL.DeletePilotRequest(pilotRequest.PilotRequestID);
                    }
                }
            }

            return true;
        }

    }
}
