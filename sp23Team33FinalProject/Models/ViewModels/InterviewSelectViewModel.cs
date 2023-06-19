using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{
    public class InterviewSelectViewModel
    {
        [Required]
        [Display(Name = "Position ID:")]
        public Int32 PositionID { get; set; }
        
        [Display(Name = "Position Name:")]
        public String PositionName { get; set; }

        [Display(Name = "Company Name:")]
        public String CompanyName { get; set; }

        [Required]
        [Display(Name = "Interview Day:")]
        [DataType(DataType.Date)]
        public DateTime? SelectedDate { get; set; }

        [Required]
        [Display(Name = "Interview Hour (8 am - 4 pm):")]
        [Range(1,12)]
        public Int32 SelectedHour { get; set; }

        public List<Interview>? Interviews { get; set; }
    }
}
