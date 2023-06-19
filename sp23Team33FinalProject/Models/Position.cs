using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{

    public class Position
    {
        public Int32 PositionID { get; set; }

        [Display(Name = "Position Title:")]
        [Required(ErrorMessage = "Position title is required.")]
        public String PositionTitle { get; set; }

        [Display(Name = "Position Description:")]
        public String? PositionDescription { get; set; }

        [Display(Name = "Position Type:")]
        [Required(ErrorMessage = "Position type is required.")]
        public PositionType PositionType { get; set; }

        [Display(Name = "Location:")]
        [Required(ErrorMessage = "Location is required.")]
        public String Location { get; set; }

        [Display(Name = "Deadline:")]
        [Required(ErrorMessage = "Deadline is required.")]
        public DateTime Deadline { get; set; }
        public Position()
        {
            Majors ??= new List<Major>();
            Applications ??= new List<Application>();
        }

        // navigational properties
        public Company Company { get; set; }
        public List<Application> Applications { get; set; }
        public List<Major> Majors { get; set; }
    }
}