using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;
using Interfaces;


namespace LogicLibrary.Managers
{
    public class AircraftManager
    {
        private iAircraftRepository aircraftRepository;

        public AircraftManager(iAircraftRepository aircraftRepository)
        {
            this.aircraftRepository = aircraftRepository;
        }
        public bool AddAircraft(Aircraft aircraft)
        {
           return aircraftRepository.AddAircraft(aircraft);
        }
        public bool DeleteAircraft(int aircraftId)
        {
           return aircraftRepository.DeleteAircraft(aircraftId);
        }
        public bool UpdateAircraft(int aircraftId, Aircraft aircraft)
        {
           return aircraftRepository.UpdateAircraft(aircraftId, aircraft);
        }
        public Aircraft GetAircraftById(int aircraftId)
        {
            return aircraftRepository.GetAircraftById(aircraftId);
        }

        public List<Aircraft> GetAllAircrafts()
        {
            return aircraftRepository.GetAllAircrafts();
        }

        public List<PrivateJet> GetAllPrivateJets()
        {
            return aircraftRepository.GetAllPrivateJets();
        }

        public List<Helicopter> GetAllHelicopters()
        {
            return aircraftRepository.GetAllHelicopters();
        }

        public PrivateJet GetPrivateJetByID(int privateJetID)
        {
            return aircraftRepository.GetPrivateJetById(privateJetID);
        }

        public Helicopter GetHelicopterByID(int helicopterID)
        {
            return aircraftRepository.GetHelicopterById(helicopterID);
        }

        public List<Helicopter> GetAvailableHelicopterForPrivateRequest(DateTime RequestStart, DateTime RequestEnd)
        {
            return aircraftRepository.GetAvailableHelicopterForPrivateRequest(RequestStart, RequestEnd);
        }

        public List<Aircraft> GetAvailableAircraftsForNormalRequest(DateTime RequestStart, DateTime RequestEnd, int Distance, List<Pilot> pilotList)
        {
            return aircraftRepository.GetAvailableAircraftsForNormalRequest(RequestStart,RequestEnd,Distance, pilotList);
        }

        public bool DeleteEverythingForAircraft(int aircraftID)
        {
            return aircraftRepository.DeleteEverythingForAircraft(aircraftID);
        }
    }
}
