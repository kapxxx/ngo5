using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGO3.Models
{
    public class Donation
    {
        [Key]
        public int DId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int TargetAmount { get; set; }

        public int? RaisedAmount { get; set; } 

        public DateTime? TargetReachDate { get; set; }

        [Required]
        public string Reason { get; set; }

        public bool IsApproved { get; set; }

        public string AdharCardNumber { get; set; } = String.Empty;

        public int createdby { get; set; }

        public int Cid { get; set; }
        public virtual Category Category { get; set; }
    }
}