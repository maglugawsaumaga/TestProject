using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexisourceApplicationTest.Models
{
    public class Products
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string Location { get; set; }
        public bool IsAvailable { get; set; }
    }
}
