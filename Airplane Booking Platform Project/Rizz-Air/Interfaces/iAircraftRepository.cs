using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;

namespace Interfaces
{
    public interface iAircraftRepository
    {
        public List<Aircraft> GetAllAircrafts();
        public bool AddAircraft(Aircraft aircraft);
        public bool DeleteAircraft(int AircraftID);
        public bool UpdateAircraft(int AircraftID, Aircraft aircraft);
        public Aircraft GetAircraftById(int AircraftID);
        public List<PrivateJet> GetAllPrivateJets();
        public List<Helicopter> GetAllHelicopters();
        public PrivateJet GetPrivateJetById(int privateJetID);
        public Helicopter GetHelicopterById(int helicopterID);
        public List<Helicopter> GetAvailableHelicopterForPrivateRequest(DateTime RequestStart, DateTime RequestEnd);
        
        public List<Aircraft> GetAvailableAircraftsForNormalRequest(DateTime RequestStart, DateTime RequestEnd, int Distance, List<Pilot> pilotList);
        public bool DeleteEverythingForAircraft(int aircraftID);
    }
}
