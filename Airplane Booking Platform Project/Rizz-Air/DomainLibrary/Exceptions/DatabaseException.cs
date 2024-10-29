using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLibrary.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException(string message) : base($"Database error: {message}")
        {

        }
    }
}
