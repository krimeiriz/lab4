using labWork4.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labWork4.Models
{
    public class Contact
    {
        public int Id { set; get; } = 0!;
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
        public string? Email { get; set; }
       
        public override string ToString()
        {
            return "#" + Id + " Name:" + FirstName + "\n" +
                "Lastname: " + LastName + "\n" +
                "Phone number: " + PhoneNumber + "\n" +
                "E-mail: " + Email + "\n";
        }
    }
}
