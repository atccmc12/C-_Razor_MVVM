using System;
using System.Collections.Generic;

namespace MVVMExample.DataAccess
{
    public partial class Address
    {
        public int Id { get; set; }
        public string Address1 { get; set; } = null!;
        public string? Address2 { get; set; }
        public string City { get; set; } = null!;
        public string ProvinceCode { get; set; } = null!;
        public string AddressType { get; set; } = null!;
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
