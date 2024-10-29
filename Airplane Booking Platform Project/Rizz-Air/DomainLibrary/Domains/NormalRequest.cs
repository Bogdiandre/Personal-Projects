using DomainLibrary.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Domains
{
    public class NormalRequest
    {
        public int NormalRequestID { get; private set; }
        public DateTime DepartureDay { get; private set; }
        public DateTime ComingBackDay { get; private set; }
        public double RequestMaxPrice { get; private set; }
        public double RequestMinPrice { get; private set; }
        public Destination Destination { get; private set; }
        public Aircraft Aircraft { get; private set; }
        public User User { get; private set; }
        public bool Accepted { get; private set; }

        //Get from the database constructor
        public NormalRequest( double requestMinPrice, double requestMaxPrice, DateTime departureDay, DateTime commingBackDay, Destination destination, Aircraft aircraft, User user, bool accepted, int normalRequestID = 0)
        {
            NormalRequestID = normalRequestID;
            DepartureDay = departureDay;
            ComingBackDay = commingBackDay;
            Destination = destination;
            Aircraft = aircraft;
            User = user;
            RequestMaxPrice = Math.Round(requestMaxPrice, 2);
            RequestMinPrice = Math.Round(requestMinPrice, 2);
            Accepted = accepted;

        }

        //Create for the first time constructor
        public NormalRequest(DateTime departureDay, DateTime commingBackDay, double minimumSalaryPilot, double maximumSalaryPilot, Destination destination, Aircraft aircraft, User user, int normalRequestID = 0)
        {
            NormalRequestID = normalRequestID;
            DepartureDay = departureDay;
            ComingBackDay = commingBackDay;
            Destination = destination;
            Aircraft = aircraft;
            User = user;
            RequestMaxPrice = CalculateMaxPrice(maximumSalaryPilot, aircraft);
            RequestMinPrice = CalculateMinPrice(minimumSalaryPilot, aircraft);
            Accepted = false;
            //Calculate Max and Min Price for the Request.
            //CalculateMinMax(maximumSalaryPilot, minimumSalaryPilot, aircraft);

        }

        

        public double CalculateMinPrice(double minimumSalaryPilot, Aircraft aircraft)
        {
            
            if (aircraft != null)
            {
                return Math.Round((2*((minimumSalaryPilot * Destination.Distance / 100) + (aircraft.Consumption * Destination.Distance / 100))), 2);
            }
            else
            {
                throw new NormalRequestException("Aircraft cannot be null!");
            }
        }
        public double CalculateMaxPrice(double maximumSalaryPilot, Aircraft aircraft)
        {
            if (aircraft != null)
            {
                return Math.Round((2*((maximumSalaryPilot * Destination.Distance / 100) + (aircraft.Consumption * Destination.Distance / 100))), 2);
            }
            else
            {
                throw new NormalRequestException("Aircraft cannot be null!");
            }
        }
        public Rent AproveRequest(Pilot pilot)
        {

             
                if (!Accepted)
                {
                    Rent rent = new Rent(pilot, this);
                    Accepted = true;
                    return rent;
                }
                
                else
                {
                    throw new NormalRequestException("Normal Request is Accepted!");
                }
                

        }
        
        public void DisApproveRequest()
        {
            
            if (Accepted)
            {
                Accepted = false;
            }
            else
            {
                throw new NormalRequestException("Normal Request is Unaccepted!");
            }

        }

        public int CalculateHoursForNormalRequest()
        {

            int hours = Destination.Distance / Aircraft.AverageSpeed;

            return hours;
        }

        public int CalculateMinutesForNormalRequest()
        {
            double ratio = Destination.Distance / Aircraft.AverageSpeed; // gets the ratio Ex: 3.2

            ratio = ratio - CalculateHoursForNormalRequest(); // 3.2 - 3 = 0.2

            ratio = ratio * 60; // 0.2 of 60 gets the minutes out of 60

            int minutes = Convert.ToInt32(ratio);

            return minutes;
        }
       
        public override string ToString()
        {
            string DepartureDayString = DepartureDay.ToString("dd/MM/yyyy");
            string ComingBackDayString = ComingBackDay.ToString("dd//MM//yyyy");

            return $"{this.Aircraft.Name.Trim()}|{DepartureDayString}|{ComingBackDayString}";
        }

        public string GetInfo()
        {
            string DepartureDayString = this.DepartureDay.ToString("dd/MM/yyyy");
            string ComingBackDayString = this.ComingBackDay.ToString("dd//MM//yyyy");

            return $"{this.Aircraft.GetType()}: {this.Aircraft.Name} " +
                    $"\nDestination: {this.Destination.Name} {this.Destination.Distance} km" +
                    $"\nStart Date: {DepartureDayString}" +
                    $"\nFinish Date: {ComingBackDayString}" +
                    $"\nClient Name: {this.User.FirstName} {this.User.LastName}" +
                    $"\nMaximum Price: {this.RequestMaxPrice}" +
                    $"\nMinimum Price: {this.RequestMinPrice}";
        }


    }
}
