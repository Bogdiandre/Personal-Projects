using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using DomainLibrary.Domains;
using DomainLibrary;
using Interfaces;
using DomainLibrary.Exceptions;
using DALLibrary.CONS;
using DomainLibrary.Enumerations;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace DALLibrary
{
    public class UserDAL : Cons, iUserRepository
    {

        public SqlConnection GetConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);

            return conn;
        }
        public bool AddUser(User user)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"INSERT INTO [Users] (Email, LastName, FirstName, DateOfBirth, PasswordHash, PasswordSalt, License) VALUES (@Email, @LastName, @FirstName, @DateOfBirth, @PasswordHash, @PasswordSalt, @License);";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Add(new SqlParameter("@Email", user.Email));
                        command.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                        command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                        command.Parameters.Add(new SqlParameter("@DateOfBirth", user.DateOfBirth));
                        command.Parameters.Add(new SqlParameter("@PasswordHash", user.PasswordHash));
                        command.Parameters.Add(new SqlParameter("@PasswordSalt", user.PasswordSalt));
                        command.Parameters.Add(new SqlParameter("@License", user.License));

                        command.ExecuteNonQuery();
                    }
                    return true;
                }
                catch (SqlNullValueException ex) { throw new DatabaseException("Error, reading null values. :" + ex.ToString()); }
                catch (InvalidOperationException ex) { throw new DatabaseException("An operation is attempted that is not valid for the current state of the database connection. :" + ex.ToString()); }
                catch (SqlException ex) { throw new DatabaseException("An error occurred in the SQL Server database. : " + ex.ToString()); }
                catch (TimeoutException ex) { throw new DatabaseException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString()); }
                
            }
            
        }

        public bool DeleteUser(int userId)
        {
            SqlConnection conn = GetConnection();
            using (conn)
            {
                try
                {
                    conn.Open();
                    string sql = $"DELETE FROM [Users] WHERE ID = @userID";

                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@userId", userId);

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
        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            SqlConnection conn = GetConnection();
            using (conn)
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Users";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                users.Add(new User(
                                    reader.GetString("Email"),
                                    reader.GetString("LastName"),
                                    reader.GetString("FirstName"),
                                    reader.GetDateTime("DateOfBirth"),
                                    reader.GetString("PasswordHash"), 
                                    reader.GetString("PasswordSalt"), 
                                    reader.GetBoolean("License"),
                                    reader.GetInt32("ID")
                                ));
                            }
                        }
                    }
                    return users;
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
        }

        public User GetUserById(int userId)
        {
            User user = null;
            SqlConnection conn = GetConnection();
            using (conn)
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Users WHERE ID = @userId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User(
                                    reader.GetString("Email"),
                                    reader.GetString("LastName"),
                                    reader.GetString("FirstName"),
                                    reader.GetDateTime("DateOfBirth"),
                                    reader.GetString("PasswordHash"),
                                    reader.GetString("PasswordSalt"),
                                    reader.GetBoolean("License"),
                                    reader.GetInt32("ID")
                                );
                            }
                        }
                    }
                    return user;
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
                
            }
        }

        public bool UpdateUser(int userId, User usr)
        {
            SqlConnection conn = GetConnection();

            using (conn)
            {
                try
                {
                    conn.Open();


                    string updateQuery = "UPDATE Users SET " +
                        "Email = @Email, " +
                        "LastName = @LastName, " +
                        "FirstName = @FirstName, " +
                        "DateOfBirth = @DateOfBirth," +
                        "Password = @Password" +
                        "License = @License" +
                        "WHERE ID = @UserID";

                    using (SqlCommand command = new SqlCommand(updateQuery, conn))
                    {

                        command.Parameters.AddWithValue("@UserID", userId);
                        command.Parameters.AddWithValue("@Email", usr.Email);
                        command.Parameters.AddWithValue("@LastName", usr.LastName);
                        command.Parameters.AddWithValue("@DateOfBirth", usr.DateOfBirth);
                        //command.Parameters.AddWithValue("@Password", usr.Password);
                        command.Parameters.AddWithValue("@License", usr.License);
                        
                        



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
                
                return false;
            }
        }
        public User Login(string email, string password)
        {
            User user = null;
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT * " +
                        "FROM [Users] " +
                        "WHERE Email = @email;";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        //cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@email", email);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                user = new User(reader.GetString("Email"), reader.GetString("LastName"), reader.GetString("FirstName"), reader.GetDateTime("DateOfBirth"), reader.GetString("PasswordHash"), reader.GetString("PasswordSalt"), reader.GetBoolean("License"), reader.GetInt32("ID"));
                            }

                        }
                    }
                   
                }
                if (user.ValidatePassword(password))
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (SqlNullValueException ex)
            {
                throw new SqlNullValueException("Error, reading null values. :" + ex.ToString());
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("An operation is attempted that is not valid for the current state of the database GetConnection(). :" + ex.ToString());
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException("Database operation takes too long to complete, and the timeout period is exceeded.  " + ex.ToString());
            }
            catch (SqlException ex)
            {
                throw new Exception("An error occurred in the SQL Server database. : " + ex.ToString());
            }
            

        }
        public User GetUserByEmail(string email)
        {
            User user = null;
            SqlConnection conn = GetConnection();
            using (conn)
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Users WHERE Email = @userEmail";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userEmail", email);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                user = new User(
                                    reader.GetString("Email"),
                                    reader.GetString("LastName"),
                                    reader.GetString("FirstName"),
                                    reader.GetDateTime("DateOfBirth"),
                                    reader.GetString("PasswordHash"),
                                    reader.GetString("PasswordSalt"),
                                    reader.GetBoolean("License"),
                                    reader.GetInt32("ID")
                                );
                            }
                        }
                    }
                    return user;
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
        }


    }
}
