using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Exceptions
{
    public class HelicopterException : Exception
    {
        public HelicopterException(string message) : base($"There is an error with this NormalRequest: {message}")
        {

        }
    }
}
