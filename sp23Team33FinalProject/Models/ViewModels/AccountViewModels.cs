using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel;

namespace sp23Team33FinalProject.Models
{ 
    //NOTE: This is the view model used to allow the user to login
    //The user only needs teh email and password to login
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    //NOTE: This is the view model used to register a user
    //When the user registers, they only need to specify the
    //properties listed in this model
    public class RegisterViewModel
    {   
        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public String Email { get; set; }

        [Display(Name = "Major Name:")]
        [Required(ErrorMessage = "Major name is required.")]
        public Int32 MajorID { get; set; }

        [Required(ErrorMessage = "Position type is required.")]
        [Display(Name = "Position Type:")]
        public PositionType? PositionType { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name:")]
        public String FirstName { get; set; }

        [Display(Name = "Middle Initial:")]
        [StringLength(1, ErrorMessage = "Only one letter!", MinimumLength = 0)]
        public String MiddleInitial { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name:")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Graduation Year is required.")]
        [Display(Name = "Graduation Year:")]
        public GraduationYearEnum GraduationYear { get; set; }

        [Required(ErrorMessage = "GPA is required.")]
        [Range(0, 4)]
        [Display(Name = "GPA:")]
        public Decimal? GPA { get; set; }

        //NOTE: Here is the logic for putting in a password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public String Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public String ConfirmPassword { get; set; }
    }

    public class EditProfileViewModel
    {
        [Required]
        public String UserID { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public String Email { get; set; }

        [Display(Name = "Major Name:")]
        [Required(ErrorMessage = "Major name is required.")]
        public Int32 MajorID { get; set; }

        [Required(ErrorMessage = "Position type is required.")]
        [Display(Name = "Position Type:")]
        public PositionType? PositionType { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name:")]
        public String FirstName { get; set; }

        [Display(Name = "Middle Initial:")]
        [StringLength(1, ErrorMessage = "Only one letter!", MinimumLength = 0)]
        public String MiddleInitial { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name:")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Graduation Year is required.")]
        [Display(Name = "Graduation Year:")]
        public GraduationYearEnum GraduationYear { get; set; }

        [Required(ErrorMessage = "GPA is required.")]
        [Range(0, 4)]
        [Display(Name = "GPA:")]
        public Decimal? GPA { get; set; }

        [Display(Name = "Status:")]
        [DefaultValue(false)]
        public Boolean Deactivate { get; set; }
    }

    public class RecruiterRegisterViewModel
    {

        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        //TODO: Add any fields that you need for creating a new user
        //First name is provided as an example
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name:")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name:")]
        public String LastName { get; set; }

        [Display(Name = "Company:")]
        public Int32 CompanyID { get; set; }

        //NOTE: Here is the logic for putting in a password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RecruiterEditViewModel
    {
        [Required]
        public String UserID { get; set; }

        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        //TODO: Add any fields that you need for creating a new user
        //First name is provided as an example
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name:")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name:")]
        public String LastName { get; set; }

        [Display(Name = "Company:")]
        public Int32 CompanyID { get; set; }

        [Display(Name = "Company Name:")]
        public String CompanyName { get; set; }

        [Display(Name = "Status:")]
        [DefaultValue(false)]
        public Boolean Deactivate { get; set; } 
    }

    public class CSORegisterViewModel
    {
        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        //TODO: Add any fields that you need for creating a new user
        //First name is provided as an example
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name:")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name:")]
        public String LastName { get; set; }

        //NOTE: Here is the logic for putting in a password
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password:")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class CSOEditiewModel
    {
        [Required]
        public String UserID { get; set; }

        //NOTE: Here is the property for email
        [Required]
        [EmailAddress]
        [Display(Name = "Email:")]
        public string Email { get; set; }

        //TODO: Add any fields that you need for creating a new user
        //First name is provided as an example
        [Required(ErrorMessage = "First name is required.")]
        [Display(Name = "First Name:")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name:")]
        public String LastName { get; set; }
    }

    //NOTE: This is the view model used to allow the user to 
    //change their password
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }   

    //NOTE: This is the view model used to display basic user information
    //on the index page
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String? MajorName { get; set; }
        public String? GraduationYear { get; set; }
        public PositionType? PositionType { get; set; }
        public Decimal? GPA { get; set; }
        
    }

    public class RecruiterIndexViewModel
    {
        public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String CompanyName { get; set; }

    }

    public class CSOIndexViewModel
    {
        public bool HasPassword { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }
        public String UserID { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }

    }

    public class SetDateViewModel
    {
        [Required]
        [Display(Name = "App Date:")]
        public DateTime? SetDate { get; set; }
    }
}
