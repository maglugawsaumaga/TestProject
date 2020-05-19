using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlexisourceApplicationTest.Models
{
    public class Customers
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime MemberSinceDate { get; set; }
        public bool IsActive { get; set; }

    }
}