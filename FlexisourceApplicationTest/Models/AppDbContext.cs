using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexisourceApplicationTest.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext")
        {

        }
        public System.Data.Entity.DbSet<FlexisourceApplicationTest.Models.Products> Products { get; set; }
        public System.Data.Entity.DbSet<FlexisourceApplicationTest.Models.Customers> Customers { get; set; }
    }
}