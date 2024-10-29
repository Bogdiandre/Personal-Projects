using DomainLibrary.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Domains
{
    public abstract class Aircraft
    {

        public int AircraftId { get; private set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Age is required!")]
        [Range(0, int.MaxValue, ErrorMessage = "Age must be at least 0!")]
        public int Age { get; private set; }

        [Required(ErrorMessage = "Weight is required!")]
        [Range(12000, 30000, ErrorMessage = "Weight must be between 12000 and 30000 kg!")]
        public int Weight { get; private set; }

        [Required(ErrorMessage = "Average speed is required!")]
        [Range(500, 800, ErrorMessage = "Average speed must be between 500 and 800 km/h!")]
        public int AverageSpeed { get; private set; }

        [Required(ErrorMessage = "Range is required!")]
        [Range(1000, 8000, ErrorMessage = "Range must be between 1000 and 8000!")]
        public int Range { get; private set; }

        [Required(ErrorMessage = "Consumption is required!")]
        [Range(1200, 4000, ErrorMessage = "Consumption cost must be between 1200 and 4000 $!")]
        public double Consumption { get; private set; }

        public Aircraft( string name,  int age,  int weight, int averageSpeed, int range, double consumption, int aircraftId = 0)
        {
            AircraftId = aircraftId;
            Name = name;
            Age = age;
            Weight = weight;
            AverageSpeed = averageSpeed;
            Range = range;
            Consumption = consumption;

        }

        public bool CheckIfPrivateJet()
        {
            if (this is PrivateJet)
                return true;
            else
                return false;
        }

        public double CalculateMinPriceForRequest(double minimumSalaryPilot, int distance)
        {
            double Price = 2*(minimumSalaryPilot * distance / 100) + (this.Consumption * distance / 100);
            return Math.Round(Price, 2);
        }

        public double CalculateMaxPriceForRequest(double maximumSalaryPilot, int distance)
        {
            double Price = 2*(maximumSalaryPilot * distance / 100) + (this.Consumption * distance / 100);
            return Math.Round(Price, 2);
        }

        public int CalculateHours(int distance)
        {
            int hours = distance / AverageSpeed;

            return hours;
        }

        public int CalculateMinutes(int distance)
        {
            float ratio = (float) distance / AverageSpeed; // gets the ratio Ex: 3.2 
            
            ratio = ratio - CalculateHours(distance); // 3.2 - 3 = 0.2
            
            ratio = ratio * 60; // 0.2 of 60 gets the minutes out of 60
            int minutes = Convert.ToInt32(ratio);

            return minutes;
        }


        public override string ToString()
        {
            return $"{this.Name}";
        }


        public virtual int GetSeats()
        {
            return 0;
        }
        
    }
}
