using DomainLibrary.Domains;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary.Managers
{
    public class RentManager : iRentRepository
    {
        private iRentRepository rentRepository;

        public RentManager(iRentRepository rentRepository)
        {
            this.rentRepository = rentRepository;
        }

        public List<Rent> GetAllRents()
        {
            return rentRepository.GetAllRents();
        }

        public bool AddRent(Rent rent)
        {
            return rentRepository.AddRent(rent);
        }

        public bool DeleteRent(int rentID)
        {
            return rentRepository.DeleteRent(rentID);
        }

        public bool UpdateRent(int rentID, Rent rent)
        {
            return rentRepository.UpdateRent(rentID, rent);
        }

        public Rent GetRentById(int rentID)
        {
            return rentRepository.GetRentById(rentID);
        }

        public List<Rent> GetRentsByUserId(int userID)
        {
            return rentRepository.GetRentsByUserId(userID);
        }

        public List<Rent> GetAllRentsByAircraftID(int aircraftID)
        {
            return rentRepository.GetAllRentsByAircraftID(aircraftID);
        }

        public List<Rent> GetAllRentsByPilotID(int pilotID)
        {
            return rentRepository.GetAllRentsByPilotID(pilotID);
        }

        public bool DeleteAllRentsForPilot(int pilotID)
        {
            List<Rent> rentList = GetAllRentsByPilotID(pilotID);

            foreach(Rent rent in rentList)
            {
                DeleteRent(rent.RentID);
            }

            return true;
        }
        public bool DeleteRentWithNormalRequest(int rentID)
        {
            return rentRepository.DeleteRentWithNormalRequest(rentID);
        }
        public bool DeleteRentAndUnacceptNormalRequest(int rentID)
        {
            return rentRepository.DeleteRentAndUnacceptNormalRequest(rentID);
        }


    }

}
