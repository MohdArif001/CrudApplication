using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApplication.Models.ViewModels
{
    public class SignUpModel
    {
        public string FirstName { get; set; }


        public string LastName { get; set; }


        [EmailAddress]
        public string Email { get; set; }


        public string Password { get; set; }


        [Compare("Password", ErrorMessage = "Password Mismatched")]
        public string ConfirmPassword { get; set; }

        public string Gender { get; set; }
    }
}
