using DomainLibrary.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Domains
{
    public class Manager : Employee
    {
        [Required(ErrorMessage = "Type is required!")]
        public ManagerType Type { get; private set; }

        public Manager(string firstName, string lastName, string email, string password, string phoneNumber, ManagerType managerType, DateTime dateOfBirth, int userId = 0) 
            : base(firstName,lastName,email, password, phoneNumber, dateOfBirth, userId )
        {
            Type = managerType;
        }

        public Manager(string firstName, string lastName, string email, string passwordHash, string passwordSalt, string phoneNumber, ManagerType managerType, DateTime dateOfBirth, int userId = 0)
            : base(firstName, lastName, email, passwordHash, passwordSalt, phoneNumber, dateOfBirth, userId)
        {
            Type = managerType;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {this.Type}";
        }
    }


}
