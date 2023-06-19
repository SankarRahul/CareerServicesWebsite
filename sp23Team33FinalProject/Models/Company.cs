using System.ComponentModel.DataAnnotations;

namespace sp23Team33FinalProject.Models
{
    public enum Industry
    {
        Accounting,
        Consulting,
        Chemicals,
        Energy,
        Engineering,
        [Display(Name = "Financial Services")]
        FinancialServices,
        Manufacturing, 
        Hospitality,
        Insurance,
        Marketing,
        [Display(Name = "Real Estate")]
        RealEstate,
        Technology,
        Retail,
        Transportation
    }
    public class Company
    {
        public Int32 CompanyID { get; set; }

        [Display(Name = "Company Name:")]
        [Required(ErrorMessage = "Company name is required.")]
        public String CompanyName { get; set; }

        [Display(Name = "Company Description:")]
        [Required(ErrorMessage = "Company description is required.")]
        public String CompanyDesc { get; set; }

        [Display(Name = "Company Email:")]
        [Required(ErrorMessage = "Company email is required.")]
        public String CompanyEmail { get; set; }

        [Display(Name = "Industry 1:")]
        [Required(ErrorMessage = "Industry 1 is required.")]
        public Industry Industry1 { get; set; }

        [Display(Name = "Industry 2:")]
        public Industry? Industry2 { get; set; }

        [Display(Name = "Industry 3:")]
        public Industry? Industry3 { get; set; }

        public Company()
        {
            Recruiters ??= new List<AppUser>();
            Positions ??= new List<Position>();
        }

        // navigational properties
        public List<AppUser> Recruiters { get; set; }
        public List<Position> Positions { get; set; }
    }
}
