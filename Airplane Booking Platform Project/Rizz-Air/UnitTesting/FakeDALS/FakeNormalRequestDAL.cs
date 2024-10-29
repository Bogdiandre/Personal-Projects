using DomainLibrary.Domains;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTesting.FakeDALS
{
    public class FakeNormalRequestDAL : iNormalRequestRepository
    {
        List<NormalRequest> _normalRequests;

        public FakeNormalRequestDAL()
        {
            _normalRequests = new List<NormalRequest>();
        }
        public bool AddNormalRequest(NormalRequest norReq)
        {
            _normalRequests.Add(norReq);
            return true;
        }

        public bool DeleteNormalRequest(int normalRequestID)
        {
            NormalRequest norReq = GetNormalRequestById(normalRequestID);
            _normalRequests.Remove(norReq);
            return true;
        }

        public List<NormalRequest> GetAllNormalRequests()
        {
            return _normalRequests;
        }

        public List<NormalRequest> GetAllNormalRequestsByDestinationID(int destinationID)
        {
            List<NormalRequest> normalRequestList = new List<NormalRequest>();
            foreach (NormalRequest norReq in _normalRequests)
            {
                if (norReq.Destination.DestinationId == destinationID)
                {
                    normalRequestList.Add(norReq);
                }
            }

            return normalRequestList;
        }

        public List<NormalRequest> GetAllNormalRequestsByEmail(string email)
        {
            List<NormalRequest> normalRequestList = new List<NormalRequest>();
            foreach (NormalRequest norReq in _normalRequests)
            {
                if (norReq.User.Email == email)
                {
                    normalRequestList.Add(norReq);
                }
            }

            return normalRequestList;
        }

        public NormalRequest GetNormalRequestById(int normalRequestID)
        {
            foreach (NormalRequest norReq in _normalRequests)
            {
                if(norReq.NormalRequestID == normalRequestID)
                {
                    return norReq;
                }
            }

            return null;
        }

        public List<NormalRequest> GetNormalRequestByUserID(int UserID)
        {
            List<NormalRequest> normalRequestList = new List<NormalRequest>();
            foreach (NormalRequest norReq in _normalRequests)
            {
                if (norReq.User.UserId == UserID)
                {
                    normalRequestList.Add(norReq);
                }
            }

            return normalRequestList;
        }

        public List<NormalRequest> GetNormalRequestsByAircraftID(int aircraftID)
        {
            List<NormalRequest> normalRequestList = new List<NormalRequest>();
            foreach (NormalRequest norReq in _normalRequests)
            {
                if (norReq.Aircraft.AircraftId == aircraftID)
                {
                    normalRequestList.Add(norReq);
                }
            }

            return normalRequestList;
        }

        public bool UpdateNormalRequest(int normalRequestID, NormalRequest norReq)
        {
            NormalRequest norRequest = GetNormalRequestById(normalRequestID);
            _normalRequests.Remove(norRequest);
            _normalRequests.Add(norReq);

            return true; 
        }
    }
}
