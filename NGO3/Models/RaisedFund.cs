using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGO3.Models
{
    public class RaisedFund
    {
        [Key]
        public int RaisId { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public string AdharCardNumber { get; set; }
        [Required]
        public int Amount { get; set; }
        
        public bool IsApporuved { get; set; }

        public int Cid { get; set; }
        public virtual Category Category { get; set; }

        public int Uid { get; set; }
        public virtual User User { get; set; }
    }
}