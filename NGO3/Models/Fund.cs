using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NGO3.Models
{
    public class Fund
    {
        [Key]
        public int Fid { get; set; }
        [Required]
        public int DonationMoney { get; set; }

        public int Uid { get; set; }
        public virtual User UserData { get; set; }

        public int Did { get; set; }
        public virtual Donation Donation { get; set; }
    }
}