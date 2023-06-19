using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{
    public class RecruiterSearchViewModel
    {
        [Display(Name = "Search by Student Name:")]
        public String RecruiterName { get; set; }

        [Display(Name = "Search by Companies:")]
        public List<Int32> CompanyIDs { get; set; }
    }
}
