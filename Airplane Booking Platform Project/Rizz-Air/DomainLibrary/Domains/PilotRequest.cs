using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Exceptions;

namespace DomainLibrary.Domains
{
    public class PilotRequest
    {
        public int PilotRequestID { get; private set; }
        public DateTime RequestStart { get; private set; }
        public DateTime RequestEnd { get; private set; }
        public double RequestPrice { get; private set; }
        public Helicopter Helicopter { get; private set; }
        public User User { get; private set; }

        public PilotRequest ( DateTime requestStart, DateTime requestEnd, double requestPrice,  Helicopter helicopter, User user ,int pilotRequestID = 0 )
        {
            PilotRequestID = pilotRequestID;
            RequestStart = requestStart;
            RequestEnd = requestEnd;
            RequestPrice = Math.Round(requestPrice,2);
            Helicopter = helicopter;
            User = user;
        }

        public PilotRequest(DateTime requestStart, DateTime requestEnd, Helicopter helicopter, User user, int pilotRequestID = 0)
        {
            PilotRequestID = pilotRequestID;
            RequestStart = requestStart;
            RequestEnd = requestEnd;
            Helicopter = helicopter;

            //Calculation of the RequestPrice
            RequestPrice = CalculatePrice(requestStart, requestEnd);
            //

            User = user;
        }

        public double CalculatePrice(DateTime requestStart, DateTime requestEnd)
        {
            if (requestStart != null && requestEnd != null)
            {
                TimeSpan timeDifference = requestEnd.Subtract(requestStart);
                int numberOfDays = timeDifference.Days;
                double price = Helicopter.PricePerDay * numberOfDays * 1.2;
                return Math.Round(price, 2);
            }
            else
            {
                throw new PilotRequestException("The dates cannot be null!");
            }

            
        }

        public override string ToString()
        {
            string DepartureDayString = RequestStart.ToString("dd/MM/yyyy");
            string ComingBackDayString = RequestEnd.ToString("dd//MM//yyyy");

            return $"{this.Helicopter.Name.Trim()}|{DepartureDayString}|{ComingBackDayString}";
        }

        public string GetInfo()
        {
            string DepartureDayString = this.RequestStart.ToString("dd/MM/yyyy");
            string ComingBackDayString = this.RequestEnd.ToString("dd//MM//yyyy");

            return $"Helicopter: {this.Helicopter.Name} " +
                    $"\nStart Date: {DepartureDayString}" +
                    $"\nFinish Date: {ComingBackDayString}" +
                    $"\nClient Name: {this.User.FirstName.Trim()} {this.User.LastName.Trim()}" +
                    $"\nPrice: {this.RequestPrice}";
        }

    }
}
