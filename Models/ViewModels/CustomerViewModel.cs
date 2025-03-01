using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using MVVMExample.DataAccess;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MVVMExample.Models.ViewModels
{
    public partial class CustomerViewModel
    {
        public Customer Customer { get; set; }

        public Address BillingAddress { get; set; }

        public Address ShippingAddress { get; set; }

        public CustomerViewModel() {
        
        }

        public CustomerViewModel(Customer customer) {

            if (customer != null)
            {
                this.Customer = customer;
            
                BillingAddress = (Customer.Addresses.FirstOrDefault<Address>(a => a.AddressType.Trim() == "Billing")) ?? new Address();

                ShippingAddress = (Customer.Addresses.FirstOrDefault<Address>(a => a.AddressType.Trim() == "Shipping")) ?? new Address();
            }
        }

    }
}
