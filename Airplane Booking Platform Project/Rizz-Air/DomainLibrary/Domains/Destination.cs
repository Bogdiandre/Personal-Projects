using DomainLibrary.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Domains
{
    public class Destination
    {
         public int DestinationId { get; private set; }
        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; private set; }

        [Required(ErrorMessage = "Distance is required!")]
        [Range(200, 8000, ErrorMessage = "Distance must be between 200 and 8000!")]
        public int Distance {get; private set; }


        public Destination(string name, int distance, int destinationID = 0)
        {
            Name = name;
            Distance = distance;
            DestinationId = destinationID;
        }

         

        public override string ToString()
        {
            return $"{Name} {Distance} km";
        }
    }
}
