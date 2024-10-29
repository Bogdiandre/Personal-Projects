using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Domains
{
    public class Rent
    {
        public int RentID { get; private set; }
        public double FinalPrice { get; private set; }
        public Pilot Pilot { get; private set; }
        public NormalRequest NormalRequest { get; private set; }

        public Rent(Pilot pilot, NormalRequest normalRequest, double finalPrice, int rentID = 0)
        {
            RentID = rentID;
            FinalPrice = Math.Round(finalPrice,2);
            Pilot = pilot;
            NormalRequest = normalRequest;
        }

        public Rent( Pilot pilot, NormalRequest normalRequest, int rentID = 0)
        {
            RentID = rentID;
            Pilot = pilot;
            NormalRequest = normalRequest;

            FinalPrice = CalculateFinalPrice(pilot, normalRequest);
        }

        private double CalculateFinalPrice(Pilot pilot, NormalRequest normalRequest)
        {
            return Math.Round((pilot.SallaryPerKm * normalRequest.Destination.Distance / 100) + (normalRequest.Aircraft.Consumption * normalRequest.Destination.Distance / 100));
        }

        public override string ToString()
        {
            return $"{this.NormalRequest.Destination.Name} {this.NormalRequest.Aircraft.Name}";
        }

        public string GetInfoForPilot()
        {
            string DepartureDayString = this.NormalRequest.DepartureDay.ToString("dd/MM/yyyy");
            string ComingBackDayString = this.NormalRequest.ComingBackDay.ToString("dd//MM//yyyy");

            return $"Aircraft: {this.NormalRequest.Aircraft.Name} " +
                    $"\nDestination: {this.NormalRequest.Destination.Name.Trim()} {this.NormalRequest.Destination.Distance} km" +
                    $"\nStart Date: {DepartureDayString}" +
                    $"\nFinish Date: {ComingBackDayString}" +
                    $"\nClient Name: {this.NormalRequest.User.FirstName.Trim()} {this.NormalRequest.User.LastName.Trim()}";
        }

        public string GetInfo()
        {
            return $"{GetInfoForPilot()}\nPilot: {this.Pilot.FirstName.Trim()} {this.Pilot.LastName.Trim()}" +
                $"\nPrice: {this.FinalPrice}";
        }

    }
}
