using DomainLibrary.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Exceptions;

namespace DomainLibrary.Domains
{
    public class Pilot : Employee
    {
        [Required(ErrorMessage = "License type is required!")]
        public AircraftType License { get; private set; }

        [Required(ErrorMessage = "Salary is required!")]
        [Range(50, 100, ErrorMessage = "Salary must between 50 and 100$!")]
        public double SallaryPerKm { get; private set; }

        public Pilot(string firstName, string lastName, string email, string password, string phoneNumber, AircraftType license, double sallaryPerKm, DateTime dateOfBirth, int userId = 0)
            : base(firstName, lastName, email, password, phoneNumber, dateOfBirth, userId)
        {
            License = license;
            SallaryPerKm = sallaryPerKm;
        }
        public Pilot(string firstName, string lastName, string email, string passwordHash, string passwordSalt, string phoneNumber, AircraftType license, double sallaryPerKm, DateTime dateOfBirth, int userId = 0)
            : base(firstName, lastName, email, passwordHash, passwordSalt , phoneNumber, dateOfBirth, userId)
        {
            License = license;
            SallaryPerKm = sallaryPerKm;
        }

        public double GetSalary(List<Rent> rentList)
        {
            if (rentList.Count != 0)
            {
                double salary = 0;

                foreach (Rent rent in rentList)
                {
                    salary = salary + 2 * (rent.NormalRequest.Destination.Distance * SallaryPerKm / 100);
                }

                return Math.Round(salary, 2);
            }
            else
            {
                throw new PilotException("List of rent cannot be null!");
            }
        }

        public override string ToString()
        {
            return $"{base.ToString()} {this.License} {this.SallaryPerKm}";
        }
    }
}
