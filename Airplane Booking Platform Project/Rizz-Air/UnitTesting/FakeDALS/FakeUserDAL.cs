using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;
using DomainLibrary.Enumerations;
using Interfaces;

namespace UnitTesting.FakeDALS
{
    public class FakeUserDAL : iUserRepository
    {
        List<User> users;

        public FakeUserDAL()
        {
            users = new List<User>();   
        }

        public bool AddUser(User user)
        {
            users.Add(user);
            return true;
        }

        public bool DeleteUser(int userId)
        {
            User deleltedUser = GetUserById(userId);
            foreach(User user in users)
            {
                if(user.UserId == deleltedUser.UserId)
                {
                    users.Remove(user);
                    
                }
            }

            return true;
        }

        public List<User> GetAllUsers()
        {
            return users;
        }

        public User GetUserByEmail(string email)
        {
            foreach (User user in users)
            {
                if (user.Email == email)
                {
                    return user;
                }
            }

            return null;
        }

        public User GetUserById(int userId)
        {
            foreach (User user in users)
            {
                if(user.UserId == userId)
                {
                    return user;
                }
            }

            return null;
        }

        public User Login(string Email, string Password)
        {
            foreach (User user in users)
            {
                //if(user.Email == Email && user.Password == Password)
                {
                    return user;
                }
            }

            return null;
        }

        public bool UpdateUser(int userId, User usr)
        {
            //User updatedUser = GetUserById(userId);
            foreach (User user in users)
            {
                if(user.UserId == userId)
                {
                    users.Remove(user);
                    users.Add(usr);
                }
            }

            return true;
        }
    }
}
