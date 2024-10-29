using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace DTO
{
    public class LoginDTO
    {
        public LoginDTO()
        {

        }

        public string Mail { get; set; }
        
        public string Password { get; set; }
    }
}