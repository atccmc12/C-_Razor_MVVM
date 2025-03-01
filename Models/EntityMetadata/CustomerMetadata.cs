using System.ComponentModel.DataAnnotations;

namespace MVVMExample.DataAccess
{
    public class CustomerMetadata
    {
        [Display(Name = "Customer ID")]
        [Required(ErrorMessage = "Customer ID is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

    }
}
