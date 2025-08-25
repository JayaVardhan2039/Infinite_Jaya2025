using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Code_Challenge_9.Models
{
    public class MoviesDbContext:DbContext
    {

        public MoviesDbContext() : base("MoviesDbConnection") { }

        public DbSet<Movie> Movies { get; set; }

    }
}