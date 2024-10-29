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
    public abstract class Employee
    {
         public int EmployeeId { get; private set; }

        [Required (ErrorMessage = "First name is required!")]
         public string FirstName { get; private set; }

        [Required(ErrorMessage = "Last name is required!")]
        public string LastName { get; private set; }

        [Required(ErrorMessage = "Email is required!")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; private set; }

        /*
        [Required(ErrorMessage = "Password is required!")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters.")]
        public string Password { get; private set; }
        */

        public string PasswordHash { get; private set; }

        public string PasswordSalt { get; private set; }



        [Required(ErrorMessage = "PhoneNumber is required!")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; private set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ValidationAge(18, ErrorMessage = "Employee must be at least 18 years old.")]
        public DateTime DateOfBirth { get; private set; }

        public Employee (string firstName, string lastName, string email, string password, string phoneNumber, DateTime dateOfBirth, int userId = 0)
        {
            EmployeeId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = GenerateSalt();
            PasswordHash = HashPassword(password, PasswordSalt);
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
        }

        public Employee(string firstName, string lastName, string email, string passwordHash, string passwordSalt, string phoneNumber, DateTime dateOfBirth, int userId = 0)
        {
            EmployeeId = userId;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordSalt = passwordSalt;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
            DateOfBirth = dateOfBirth;
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
            return $"{this.LastName.Trim()} {this.FirstName.Trim()}";
        }
    }
}
