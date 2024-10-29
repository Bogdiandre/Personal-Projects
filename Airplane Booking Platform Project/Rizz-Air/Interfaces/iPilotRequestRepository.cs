using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;

namespace Interfaces
{
    public interface iPilotRequestRepository
    {
        public List<PilotRequest> GetAllPilotRequests();
        public bool AddPilotRequest(PilotRequest pilReq);
        public bool DeletePilotRequest(int pilotRequestID);
        public bool UpdatePilotRequest(int pilotRequestID, PilotRequest pilReq);
        public PilotRequest GetPilotRequestById(int pilotRequestID);
        public List<PilotRequest> GetPilotRequestByUserID(int userID);
        public List<PilotRequest> GetAllPilotRequestsByHelicopterID(int helicopterID);
    }
}
