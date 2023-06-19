using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace sp23Team33FinalProject.Models
{
    public enum PositionType {
        [Display(Name = "Full-time")]
        FullTime,
        [Display(Name = "Internship")]
        Internship 
    }

    public enum GraduationYearEnum
    {
        [Display(Name = "2022")]
        Y2022,
        [Display(Name = "2023")]
        Y2023,
        [Display(Name = "2024")]
        Y2024,
        [Display(Name = "2025")]
        Y2025,
        [Display(Name = "2026")]
        Y2026,
        [Display(Name = "2027")]
        Y2027,
        [Display(Name = "2028")]
        Y2028,
        [Display(Name = "2029")]
        Y2029,
        [Display(Name = "2030")]
        Y2030
    }

    public class AppUser : IdentityUser
    {

        // Properties required for CSOs, recruiters, and students
        [Display(Name="First Name:")]
        [Required(ErrorMessage = "First name is required.")]
        public String FirstName { get; set; }

        [Display(Name = "Last Name:")]
        [Required(ErrorMessage = "First name is required.")]
        public String LastName { get; set; }

        // Additional properties for students
        [Display(Name = "Middle Initial:")]
        public String? MiddleInitial { get; set; }

        public DateTime? Birthday { get; set; }

        public String? Street { get; set; }

        public String? City { get; set; }

        public String? State { get; set; }

        public String? Zip { get; set; }

        [Display(Name = "Position Type:")]
        public PositionType? PositionType { get; set; }

        [Display(Name = "Graduation Year:")]
        public String GraduationYear { get; set; }

        [Range(0,4)]
        public Decimal? GPA { get; set; }

        public AppUser()
        {
            Applications ??= new List<Application>();
            HostRoomBookings ??= new List<Interview>();
            StudentInterviews ??= new List<Interview>();
        }

        // Navigational Properties
        public Major? Major { get; set; }
        public List<Application>? Applications { get; set; }
        public Company? Company { get; set; }

        [InverseProperty("Host")]
        public List<Interview> HostRoomBookings { get; set; }

        [InverseProperty("Student")]
        public List<Interview> StudentInterviews { get; set; }
    }
}
