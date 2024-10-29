using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;
using Interfaces;

namespace LogicLibrary.Managers
{
    public class PilotRequestManager
    {
        private iPilotRequestRepository pilotRequestRepository;

        public PilotRequestManager(iPilotRequestRepository pilotRequestRepository)
        {
            this.pilotRequestRepository = pilotRequestRepository;
        }

        public void AddPilotRequest(PilotRequest pilotRequest)
        {
            pilotRequestRepository.AddPilotRequest(pilotRequest);
        }

        public void DeletePilotRequest(int pilotRequestId)
        {
            pilotRequestRepository.DeletePilotRequest(pilotRequestId);
        }

        public void UpdatePilotRequest(int pilotRequestId, PilotRequest pilotRequest)
        {
            pilotRequestRepository.UpdatePilotRequest(pilotRequestId, pilotRequest);
        }

        public PilotRequest GetPilotRequestById(int pilotRequestId)
        {
            return pilotRequestRepository.GetPilotRequestById(pilotRequestId);
        }

        public List<PilotRequest> GetAllPilotRequests()
        {
            return pilotRequestRepository.GetAllPilotRequests();
        }

        public List<PilotRequest> GetAllPilotRequestsByHelicopterID(int helicopterID)
        {
            return pilotRequestRepository.GetAllPilotRequestsByHelicopterID(helicopterID);
        }

        public List<PilotRequest> GetPilotRequestByUserID(int userID)
        {
            return pilotRequestRepository.GetPilotRequestByUserID(userID);
        }
    }
}
