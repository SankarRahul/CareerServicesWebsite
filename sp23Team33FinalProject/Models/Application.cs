using System.ComponentModel.DataAnnotations;
using Microsoft.SqlServer.Server;

namespace sp23Team33FinalProject.Models
{
    public enum Status { 
        Accepted, 
        Rejected, 
        Pending
    }
    public class Application
    {
        public Int32 ApplicationID { get; set; }

        [Display(Name = "Application Status:")]
        [Required(ErrorMessage = "AppStatus is required.")]
        public Status AppStatus { get; set; }

        // navigational properties
        public Position Position { get; set; }
        public AppUser Student { get; set; }  
        public Interview? Interview { get; set; }
    }
}
