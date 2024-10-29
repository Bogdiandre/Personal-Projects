using DomainLibrary.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Domains
{
    public class User
    {
        public int UserId { get; private set; }
  
        public string Email { get; private set; }
 
        public string LastName { get; private set; }
   
        public string FirstName { get; private set; }
       
        public DateTime DateOfBirth { get; private set; }
    
        public string PasswordHash { get; private set; }

        public string PasswordSalt { get; private set; }

        public bool License { get; private set; }

        public User( string email, string lastName, string firstName, DateTime dateOfBirth, string password, bool license, int userID = 0)
        {
            UserId = userID;
            Email = email.Trim();
            LastName = lastName.Trim();
            FirstName = firstName.Trim();
            DateOfBirth = dateOfBirth;
            PasswordSalt = GenerateSalt();
            PasswordHash = HashPassword(password, PasswordSalt);
            License = license;

        }

        public User(string email, string lastName, string firstName, DateTime dateOfBirth, string passwordHash, string passwordSalt, bool license, int userID = 0)
        {
            UserId = userID;
            Email = email;
            LastName = lastName;
            FirstName = firstName;
            DateOfBirth = dateOfBirth;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            License = license;
        }

     

        public string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        public bool ValidatePassword(string plainTextPassword)
        {
            string hashedInput = HashPassword(plainTextPassword, PasswordSalt);
            return hashedInput == PasswordHash;
        }
        public string HashPassword(string password, string salt)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password + salt);
                SHA256Managed sHA256ManagedString = new SHA256Managed();
                byte[] hash = sHA256ManagedString.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public override string ToString()
        {
            return $"{this.Email}";
        }
    }
}
