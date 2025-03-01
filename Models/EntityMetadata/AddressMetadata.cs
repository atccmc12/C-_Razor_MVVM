using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace MVVMExample.DataAccess
{
    public class AddressMetadata
    {
        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Street Address is required")]
        public string Address1 { get; set; } = null!;

        [Display(Name = "Address 2")]
        public string? Address2 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Must select a province")]
        [Display(Name = "Province")]
        [RegularExpression(@"^(ON)|(QC)|(BC)|(AB)|(MB)$", ErrorMessage = "Must be a valid province code")]
        public string ProvinceCode { get; set; } = null!;
    }
}
