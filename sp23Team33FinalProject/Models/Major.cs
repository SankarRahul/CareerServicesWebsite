using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{
    public class Major
    {
        public Int32 MajorId { get; set; }

        [Display(Name = "Major Name:")]
        [Required(ErrorMessage = "Major name is required.")]
        public String MajorName { get; set; }

        public Major()
        {
            Students ??= new List<AppUser>();
            Positions ??= new List<Position>();
        }

        // Navigational properties
        public List<AppUser> Students { get; set; }
        public List<Position> Positions { get; set; }
    }
}
