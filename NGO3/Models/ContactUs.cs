using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGO3.Models
{
    public class ContactUs
    {
        [Key]
        public int ContactusId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }      
        [Required]
        public string Mobile { get; set; } 
        [Required]
        public string Massage { get; set; }

        
    }
}