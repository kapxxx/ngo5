using NGO3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NGO3.Data
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Fund> funds { get; set; }
        public DbSet<Donation> donations { get; set; }
        public DbSet<ContactUs> contactUs { get; set; }
        public DbSet<Blog> blogs { get; set; }
        public DbSet<RaisedFund> raisedFunds { get; set; }
        
    }
}