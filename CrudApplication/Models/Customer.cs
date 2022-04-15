using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApplication.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(10)]
        [RegularExpression("(0|91)?[7-9][0-9]{9}", ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile_No { get; set; }
  

        [Required]
        public string Address { get; set; }

        [Required(ErrorMessage ="Please Select State")]
        public int StateId { get; set; }

        [Required(ErrorMessage ="Please Select City")]
        public int CityId { get; set; }
    }
}
