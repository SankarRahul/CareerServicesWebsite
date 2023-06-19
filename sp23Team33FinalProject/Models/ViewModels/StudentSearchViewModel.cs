using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{
    public enum GPASearchType { LessThan, GreaterThan }
    public class StudentSearchViewModel
    {
        [Display(Name = "Search by Student Name:")]
        public String StudentName { get; set; }

        [Display(Name = "Search by Graduation Year:")]
        public GraduationYearEnum? GradYear { get; set; }

        [Display(Name = "Search by Major:")]
        public List<Int32> MajorIDs { get; set; }

        [Display(Name = "Search by GPA:")]
        [Range(0, 4, ErrorMessage = "GPA must be 0-4.")]
        public Decimal? GPA { get; set; }

        [DefaultValue(GPASearchType.LessThan)]
        [Display(Name = "Type of Search:  ")]
        public GPASearchType GPASearchType { get; set; }

        [Display(Name = "Search by Position Type:")]
        public PositionType? PositionType { get; set; }
    }
}
