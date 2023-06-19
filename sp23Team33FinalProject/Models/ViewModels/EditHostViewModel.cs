using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{
    public class EditHostViewModel
    {
        public Int32 CompanyID { get; set; }
        public Int32 InterviewID { get; set; }

        [Required]
        [Display(Name ="Enter New Host Email:")]
        public String newHostEmail { get; set; }

        [Display(Name = "Date & Time:")]
        public DateTime InterviewDateTime { get; set; }

        [Display(Name = "Room name:")]
        public Room Room { get; set; }

        [Display(Name = "Current Host:")]
        public String CurrentHost { get; set; }

        public List<AppUser> Hosts { get; set; }
    }
}
