using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Exceptions
{
    public class PilotRequestException : Exception
    {
        public PilotRequestException(string message) : base($"There is an error with this NormalRequest: {message}")
        {

        }
    }
}
