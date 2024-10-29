using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
    public class UserDTO
    {
        public UserDTO()
        {

        }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [ValidationAge(18, ErrorMessage = "User must be at least 18 years old.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "License is required.")]
        public bool License { get; set; }
    }
}
