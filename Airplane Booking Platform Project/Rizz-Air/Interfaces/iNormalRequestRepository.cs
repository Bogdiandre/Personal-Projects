using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;

namespace Interfaces
{
    public interface iNormalRequestRepository
    {
        public List<NormalRequest> GetAllNormalRequests();
        public bool AddNormalRequest(NormalRequest norReq);
        public bool DeleteNormalRequest(int normalRequestID);
        public bool UpdateNormalRequest(int normalRequestID, NormalRequest norReq);
        public NormalRequest GetNormalRequestById(int normalRequestID);
        public List<NormalRequest> GetNormalRequestByUserID(int UserID);
        public List<NormalRequest> GetNormalRequestsByAircraftID(int aircraftID);
        public List<NormalRequest> GetAllNormalRequestsByEmail(string email);
        public List<NormalRequest> GetAllNormalRequestsByDestinationID(int destinationID);

    }
}
