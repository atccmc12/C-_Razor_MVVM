using System.ComponentModel.DataAnnotations;

namespace MVVMExample.Models.ViewModels
{
    public class SearchViewModel
    {
        [Required]
        [MinLength(2, ErrorMessage="You must enter at least 2 characters") ]
        [Display(Name = "Customer name Contains:")]
        public string NameSearchString { get; set; }

    }
}
