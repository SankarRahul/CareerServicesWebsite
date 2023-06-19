using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{
    public class ReserveRoomViewModel
    {
        [Required]
        public String HostUserName { get; set; }

        public Int32 PositionID { get; set; }

        [Required]
        [Display(Name = "Starting Availability:")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        //[RegularExpression(@"^\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}$", ErrorMessage = "Please enter a valid date and time in the format yyyy-MM-dd HH:mm.")]
        public DateTime StartAvail { get; set; }

        [Required]
        [Display(Name = "Ending Availability:")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        //[RegularExpression(@"^\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}$", ErrorMessage = "Please enter a valid date and time in the format yyyy-MM-dd HH:mm.")]
        public DateTime EndAvail { get; set; }

        [Required]
        public Room Room { get; set; }
    }
}
