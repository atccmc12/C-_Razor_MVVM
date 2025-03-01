using System;
using System.Collections.Generic;

namespace MVVMExample.DataAccess
{
    public partial class Customer
    {
        public Customer()
        {
            Addresses = new HashSet<Address>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
    }
}
