using DomainLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface iRentRepository
    {
        public List<Rent> GetAllRents();
        public bool AddRent(Rent rent);
        public bool DeleteRent(int rentID);
        public bool UpdateRent(int rentID, Rent Rent);
        public Rent GetRentById(int rentID);
        public List<Rent> GetRentsByUserId(int userID);
        public List<Rent> GetAllRentsByAircraftID(int aircraftID);

        public List<Rent> GetAllRentsByPilotID(int PilotID);
        public bool DeleteRentWithNormalRequest(int rentID);
        public bool DeleteRentAndUnacceptNormalRequest(int rentID);

    }
}
