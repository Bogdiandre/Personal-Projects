using DomainLibrary.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Interfaces
{
    public interface iDestinationRepository
    {
        public List<Destination> GetAllDestinations();
        public Destination GetDestinationByID(int id);
        public bool AddDestination(Destination des);
        public bool DeleteDestination(int id);
        public bool UpdateDestination(int id, Destination des);
        public bool DeleteEverythingForDestination(int destinationID);

    }
}
