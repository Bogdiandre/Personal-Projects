using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;
using Interfaces;


namespace LogicLibrary.Managers
{
    public class UserManager
    {
        private iUserRepository userRepository;

        public UserManager(iUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool AddUser(User user)
        {
            return userRepository.AddUser(user);
        }

        public bool DeleteUser(int userId)
        {
            return userRepository.DeleteUser(userId);
        }
        public bool UpdateUser(int userId, User usr)
        {
            return userRepository.UpdateUser(userId, usr);
        }
        public User GetUserById(int userId)
        {
            return userRepository.GetUserById(userId);
        }

        public List<User> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public User Login(string email, string password)
        {
            return userRepository.Login(email,password);
        }
        public User GetUserByEmail(string email)
        {
            return userRepository.GetUserByEmail(email);    
        }
    }
}
