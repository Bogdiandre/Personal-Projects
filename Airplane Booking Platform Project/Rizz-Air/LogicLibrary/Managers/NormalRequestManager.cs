using DomainLibrary;
using DomainLibrary.Domains;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLibrary.Managers
{
    public class NormalRequestManager : iNormalRequestRepository
    {
        private iNormalRequestRepository normalRequestRepository;

        public NormalRequestManager(iNormalRequestRepository normalRequestRepository)
        {
            this.normalRequestRepository = normalRequestRepository;
        }

        public List<NormalRequest> GetAllNormalRequests()
        {
            return normalRequestRepository.GetAllNormalRequests();
        }

        public bool AddNormalRequest(NormalRequest norReq)
        {
            return normalRequestRepository.AddNormalRequest(norReq);
        }

        public bool DeleteNormalRequest(int normalRequestID)
        {
            return normalRequestRepository.DeleteNormalRequest(normalRequestID);
        }

        public bool UpdateNormalRequest(int normalRequestID, NormalRequest norReq)
        {
            return normalRequestRepository.UpdateNormalRequest(normalRequestID, norReq);
        }

        public NormalRequest GetNormalRequestById(int normalRequestID)
        {
            return normalRequestRepository.GetNormalRequestById(normalRequestID);
        }

        public List<NormalRequest> GetNormalRequestByUserID(int userID)
        {
            return normalRequestRepository.GetNormalRequestByUserID(userID);
        }

        public List<NormalRequest> GetNormalRequestsByAircraftID(int aircraftID)
        {
            return normalRequestRepository.GetNormalRequestsByAircraftID(aircraftID);
        }

        public List<NormalRequest> GetAllNormalRequestsByEmail(string email)
        {
            return normalRequestRepository.GetAllNormalRequestsByEmail(email);
        }

        public List<NormalRequest> FilterUnaccptedNormalRequest(List<NormalRequest> normalRequests)
        {

                List<NormalRequest> unacceptedNormalRequests = new List<NormalRequest>();

                foreach (NormalRequest nr in normalRequests)
                {
                    if (nr.Accepted == false)
                    {
                        unacceptedNormalRequests.Add(nr);
                    }
                }


            return unacceptedNormalRequests;
        }
        public List<NormalRequest> GetAllNormalRequestsByDestinationID(int destinationID)
        {
            return normalRequestRepository.GetAllNormalRequestsByDestinationID(destinationID);
        }

    }
}
