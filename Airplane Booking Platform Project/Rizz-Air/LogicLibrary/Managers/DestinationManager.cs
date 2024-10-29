using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLibrary.Domains;
using DomainLibrary.Enumerations;
using Interfaces;

namespace LogicLibrary.Managers
{
    public class DestinationManager
    {
        private iDestinationRepository destinationRepository;
        public DestinationManager(iDestinationRepository destinationRepository)
        {
            this.destinationRepository = destinationRepository; 
        }

        public List<Destination> GetAllDestinations()
        {
            return destinationRepository.GetAllDestinations();
        }

        public Destination GetDestinationByID(int id)
        {
            return destinationRepository.GetDestinationByID(id);
        }

        public bool UpdateDestination(int destinationID, Destination updatedDes)
        {
            return destinationRepository.UpdateDestination(destinationID, updatedDes);  
        }

        public bool AddDestination(Destination des)
        {
            return destinationRepository.AddDestination(des);
        }
        public bool DeleteDestination(int destinationID)
        {
            return destinationRepository.DeleteDestination(destinationID);
        }
        public bool DeleteEverythingForDestination(int destinationID)
        {
            return destinationRepository.DeleteEverythingForDestination(destinationID);
        }
    }
}
