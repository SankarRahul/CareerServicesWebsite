using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{ 
    public class PositionSearchViewModel
    {
        [Display(Name = "Search by Company:")]
        public List<Int32> CompanyIDs { get; set; }

        [Display(Name = "Search by Position Title:")]
        public String PositionTitle { get; set; }

        // BONUS: search by multiple industries??
        [Display(Name = "Search by Industry(s):")]
        public Industry? Industry { get; set; }

        [Display(Name = "Search by Position Type:")]
        public PositionType? PositionType { get; set; }

        [Display(Name = "Search by Major:")]
        public List<Int32> MajorIDs { get; set; }

        [Display(Name = "Search by Location:")]
        public String Location { get; set; }

    }
}
