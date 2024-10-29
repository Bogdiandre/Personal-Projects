using DomainLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface iUserRepository
    {
        public List<User> GetAllUsers();
        public bool AddUser(User user);
        public bool DeleteUser(int userId);
        public bool UpdateUser(int userId, User usr);
        public User GetUserById(int userId);
        public User Login(string Email, string Password);
        public User GetUserByEmail(string email);

    }
}
