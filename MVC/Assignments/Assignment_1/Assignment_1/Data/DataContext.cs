using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Assignment_1.Models;

namespace Assignment_1.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext() : base("ContactContext1") { }
        public DbSet<Contact> Contacts { get; set; }
    }
    
}