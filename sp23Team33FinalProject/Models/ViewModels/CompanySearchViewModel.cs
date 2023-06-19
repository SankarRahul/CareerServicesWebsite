using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{
    public class CompanySearchViewModel
    {
        // : name, location, industry, and position type
        [Display(Name = "Company Name:")]
        public String CompanyName { get; set; }

        [Display(Name = "Search by Industry(s):")]
        public Industry? Industry { get; set; }

        [Display(Name = "Position Type:")]
        public PositionType? PositionType { get; set; }

        [Display(Name = "Location:")]
        public String Location { get; set; }
    }
}
