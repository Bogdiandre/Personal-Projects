using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Exceptions
{
    public class PilotException : Exception
    {
        public PilotException(string message) : base($"There is an error with this Pilot: {message}")
        {

        }
    }
}
