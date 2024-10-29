using DomainLibrary.Domains;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.FakeDALS
{
    public class FakeRentDAL : iRentRepository
    {
        List<Rent> rents ;

        public FakeRentDAL()
        {
            rents = new List<Rent>();
        }
        public bool AddRent(Rent rent)
        {
            rents.Add(rent);
            return true;
        }

        public bool DeleteRent(int rentID)
        {
            Rent rent = GetRentById(rentID);
            rents.Remove(rent);
            return true;
        }

        public bool DeleteRentAndUnacceptNormalRequest(int rentID)
        {


            //First update the NormalRequest that has this rent.

            //////////////////////////////////////////////////////////////////
            FakeNormalRequestDAL fakeNormalRequestDAL = new FakeNormalRequestDAL();
            Rent rent = GetRentById(rentID);
            NormalRequest oldNormalRequest = fakeNormalRequestDAL.GetNormalRequestById(rent.NormalRequest.NormalRequestID);
            oldNormalRequest.DisApproveRequest();
            fakeNormalRequestDAL.UpdateNormalRequest(oldNormalRequest.NormalRequestID, oldNormalRequest);
            ///////////////////////////////////////////////////////////////////

            DeleteRent(rentID);

            return true;

        }

        public bool DeleteRentWithNormalRequest(int rentID)
        {
            //First delete the NormalRequest that has this rent.

            //////////////////////////////////////////////////////////////////
            FakeNormalRequestDAL fakeNormalRequestDAL = new FakeNormalRequestDAL();
            Rent rent = GetRentById(rentID);
            fakeNormalRequestDAL.DeleteNormalRequest(rent.NormalRequest.NormalRequestID);
            ///////////////////////////////////////////////////////////////////////

            DeleteRent(rentID);

            return true;
        }

        public List<Rent> GetAllRents()
        {
            return rents;
        }

        public List<Rent> GetAllRentsByAircraftID(int aircraftID)
        {
            List<Rent> rentList = new List<Rent>();
            foreach (Rent rent in rents)
            {
                if (rent.NormalRequest.Aircraft.AircraftId == aircraftID)
                {
                    rentList.Add(rent);
                }
            }

            return rentList;
        }

        public List<Rent> GetAllRentsByPilotID(int PilotID)
        {
            List<Rent> rentList = new List<Rent>();
            foreach (Rent rent in rents)
            {
                if (rent.Pilot.EmployeeId == PilotID)
                {
                    rentList.Add(rent);
                }
            }

            return rentList;
        }

        public Rent GetRentById(int rentID)
        {
            foreach(Rent rent in rents)
            {
                if(rent.RentID == rentID)
                {
                    return rent;
                }
            }

            return null;
        }

        public List<Rent> GetRentsByUserId(int userID)
        {
            List<Rent> rentList = new List<Rent>();
            foreach (Rent rent in rents)
            {
                if (rent.NormalRequest.User.UserId == userID)
                {
                    rentList.Add(rent);
                }
            }

            return rentList;
        }

        public bool UpdateRent(int rentID, Rent Rent)
        {
            Rent rent = GetRentById(rentID);
            rents.Remove(rent);
            rents.Add(Rent);

            return true;
        }
    }
}
