using System;
using Microsoft.SqlServer.Server;
using sp23Team33FinalProject.Models;
using sp23Team33FinalProject.DAL;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace sp23Team33FinalProject.Seeding
{
    public class SeedApplications
    {
        public static async Task SeedAllApplications(UserManager<AppUser> userManager, AppDbContext db)
        {
            if (db.Applications.Count() == 18)
            {
                throw new NotSupportedException("The database already contains all 18 applications!");
            }

            Int32 intApplicationsAdded = 0;
            System.String strAppStudent = "Begin"; //helps to keep track of error on books
            List<Application> Applications = new List<Application>();

            try
            {
                Application a1 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a1.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Marketing Intern");
                a1.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Adlucent");
                a1.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Lou Ann" && d.LastName == "Feeley");
                Applications.Add(a1);

                Application a2 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a2.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Marketing Intern");
                a2.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Adlucent");
                a2.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Eryn" && d.LastName == "Rice");
                Applications.Add(a2);

                Application a3 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a3.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Corporate Recruiting Intern");
                a3.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Microsoft");
                a3.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Charles" && d.LastName == "Miller");
                Applications.Add(a3);

                Application a4 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a4.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Account Manager");
                a4.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Deloitte");
                a4.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Eric" && d.LastName == "Stuart");
                Applications.Add(a4);

                Application a5 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a5.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Web Development");
                a5.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Capital One");
                a5.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Christopher" && d.LastName == "Baker");
                Applications.Add(a5);

                Application a6 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a6.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Amenities Analytics Intern");
                a6.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Hilton Worldwide");
                a6.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Eryn" && d.LastName == "Rice");
                Applications.Add(a6);

                Application a7 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a7.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Amenities Analytics Intern");
                a7.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Hilton Worldwide");
                a7.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Tesa" && d.LastName == "Freeley");
                Applications.Add(a7);

                Application a8 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a8.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Amenities Analytics Intern");
                a8.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Hilton Worldwide");
                a8.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Lim" && d.LastName == "Chou");
                Applications.Add(a8);

                Application a9 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a9.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Supply Chain Internship");
                a9.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Shell");
                a9.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Brad" && d.LastName == "Ingram");
                Applications.Add(a9);

                Application a10 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a10.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Supply Chain Internship");
                a10.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Shell");
                a10.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Sarah" && d.LastName == "Saunders");
                Applications.Add(a10);

                Application a11 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a11.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Financial Analyst");
                a11.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Capital One");
                a11.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "John" && d.LastName == "Smith");
                Applications.Add(a11);

                Application a12 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a12.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Accounting Intern");
                a12.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Deloitte");
                a12.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Chuck" && d.LastName == "Luce");
                Applications.Add(a12);

                Application a13 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a13.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Consultant");
                a13.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Accenture");
                a13.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Eric" && d.LastName == "Stuart");
                Applications.Add(a13);

                Application a14 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a14.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Consultant");
                a14.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Accenture");
                a14.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "John" && d.LastName == "Hearn");
                Applications.Add(a14);

                Application a15 = new Application()
                {
                    AppStatus = Status.Accepted

                };
                a15.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Account Manager");
                a15.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Deloitte");
                a15.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Jim Bob" && d.LastName == "Evans");
                Applications.Add(a15);

                Application a16 = new Application()
                {
                    AppStatus = Status.Pending

                };
                a16.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Account Manager");
                a16.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Deloitte");
                a16.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Reagan" && d.LastName == "Wood");
                Applications.Add(a16);

                Application a17 = new Application()
                {
                    AppStatus = Status.Pending

                };
                a17.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Accounting Rotational Program");
                a17.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Texas Instruments");
                a17.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Reagan" && d.LastName == "Wood");
                Applications.Add(a17);

                Application a18 = new Application()
                {
                    AppStatus = Status.Pending

                };
                a18.Position = db.Positions.FirstOrDefault(c => c.PositionTitle == "Consultant ");
                a18.Position.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Accenture");
                a18.Student = userManager.Users.FirstOrDefault(d => d.FirstName == "Reagan" && d.LastName == "Wood");
                Applications.Add(a18);


                try
                {
                    foreach (Application applicationToAdd in Applications)
                    {
                        strAppStudent = applicationToAdd.Student.FirstName;
                        Application dbApplication = db.Applications.FirstOrDefault(b => b.ApplicationID == applicationToAdd.ApplicationID);
                        if (dbApplication == null) //this company doesn't exist
                        {
                            db.Applications.Add(applicationToAdd);
                            db.SaveChanges();
                            intApplicationsAdded += 1;
                        }
                        else //company exists - update values back to the original values in the seeded data file
                        {
                            dbApplication.Student.FirstName = applicationToAdd.Student.FirstName;
                            dbApplication.Student.LastName = applicationToAdd.Student.LastName;
                            dbApplication.Position = applicationToAdd.Position;
                            dbApplication.Position.Company = applicationToAdd.Position.Company;
                            dbApplication.AppStatus = applicationToAdd.AppStatus;
                            db.Update(dbApplication);
                            db.SaveChanges();
                            intApplicationsAdded += 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.String msg = "  Repositories added:" + intApplicationsAdded + "; Error on " + strAppStudent;
                    throw new InvalidOperationException(ex.Message + msg);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }


        }
    }
}

