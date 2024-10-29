using DomainLibrary.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Exceptions;

namespace DomainLibrary.Domains
{
    public class Helicopter : Aircraft
    {
        [Required(ErrorMessage = "Price per day is required!")]
        [Range(3000, 10000, ErrorMessage = "Price per day must be between 3000 and 10000 $!")]
        public int PricePerDay { get; private set; }

        public Helicopter(string name,  int age,  int weight, int averageSpeed, int range, double consumption, int pricePerDay, int aircraftId = 0) : base(name, age, weight,  averageSpeed , range, consumption , aircraftId)
        {
            this.PricePerDay = pricePerDay;
        }

        public double CalculatePrivateRequest(DateTime StartDate, DateTime FinishDate)
        {
            if (StartDate != null && FinishDate != null)
            {

                double Price;

                TimeSpan timeDifference = FinishDate.Subtract(StartDate);

                int numberOfDays = timeDifference.Days;

                Price = PricePerDay * numberOfDays;

                return Price;
            }
            else
            {
                throw new HelicopterException("Dates cannot be null!");
            }
        }
    }
}
