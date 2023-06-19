using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{
    public enum Room
    {
        [Display(Name = "Room 1")]
        Room1,
        [Display(Name = "Room 2")]
        Room2,
        [Display(Name = "Room 3")]
        Room3,
        [Display(Name = "Room 4")]
        Room4
    }
    public class Interview
    {
        public Int32 InterviewID { get; set; }

        [Display(Name = "Date & Time:")]
        [Required(ErrorMessage = "Interview date & time is required.")]
        public DateTime InterviewDateTime { get; set; }

        [Display(Name = "Room name:")]
        [Required(ErrorMessage = "Room choice is required.")]
        public Room Room { get; set; }

        // navigational property
        public int? AppForeignKey { get; set; }
        public Application? Application { get; set; }
        public Position? Position { get; set; }
        public AppUser? Student { get; set; }

        // used for both recruiter and CSO
        // has to be non-null because it needs a host
        public AppUser Host { get; set; }
    }
}