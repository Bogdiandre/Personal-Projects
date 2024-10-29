using DomainLibrary.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Domains
{
    public class PrivateJet : Aircraft
    {
        [Required(ErrorMessage = "Seat number is required!")]
        [Range(4, 30, ErrorMessage = "Seat number must be between 4 and 30 !")]
        public int SeatNumber { get; private set; }

        public PrivateJet(string name, int age, int weight, int averageSpeed, int range, double consumption, int seatNumber, int aircraftId = 0) : base(name, age, weight, averageSpeed, range, consumption, aircraftId)
        {
            this.SeatNumber = seatNumber;
        }

        public override string ToString()
        {
            return $"{this.Name}";
        }

        public override int GetSeats()
        {
            return this.SeatNumber;
        }
    }
}

