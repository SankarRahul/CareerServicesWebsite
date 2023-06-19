using System;
using Microsoft.SqlServer.Server;
using sp23Team33FinalProject.Models;
using sp23Team33FinalProject.DAL;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace sp23Team33FinalProject.Seeding
{
	public class SeedCompanies
	{
        public static async Task SeedAllCompanies(AppDbContext db)
        {
            if (db.Companies.Count() == 13)
            {
                throw new NotSupportedException("The database already contains all 13 companies!");
            }

            Int32 intCompaniesAdded = 0;
            String strCompanyTitle = "Begin"; //helps to keep track of error on books
            List<Company> Companies = new List<Company>();

            try
            {
                Company c1 = new Company()
                {
                    CompanyName = "Accenture",
                    CompanyDesc = "Accenture is a global management consulting, technology services and outsourcing company.",
                    CompanyEmail = "accenture@example.com",
                    Industry1 = Industry.Consulting,
                    Industry2 = Industry.Technology
                };
                Companies.Add(c1);

                Company c2 = new Company()
                {
                    CompanyName = "Shell",
                    CompanyDesc = "Shell Oil Company, including its consolidated companies and its share in equity companies, is one of America's leading oild and natural gas producers, natural gas marketers, gasoline marketers and petrochemical manufacturers.",
                    CompanyEmail = "shell@example.com",
                    Industry1 = Industry.Energy,
                    Industry2 = Industry.Chemicals
                };
                Companies.Add(c2);

                Company c3 = new Company()
                {
                    CompanyName = "Deloitte",
                    CompanyDesc = "Deloitte is one of the leading professional services organizations in the United States specializing in audit, tax, consulting, and financial advisory services with clients in more than 20 industries.",
                    CompanyEmail = "deloitte@example.com",
                    Industry1 = Industry.Accounting,
                    Industry2 = Industry.Consulting,
                    Industry3 = Industry.Technology
                };
                Companies.Add(c3);

                Company c4 = new Company()
                {
                    CompanyName = "Capital One",
                    CompanyDesc = "Capital One offers a broad spectrum of financial products and services to consumers, small businesses and commercial clients.",
                    CompanyEmail = "capitalone@example.com",
                    Industry1 = Industry.FinancialServices,
                };
                Companies.Add(c4);

                Company c5 = new Company()
                {
                    CompanyName = "Texas Instruments",
                    CompanyDesc = "TI is one of the world’s largest global leaders in analog and digital semiconductor design and manufacturing",
                    CompanyEmail = "texasinstruments@example.com",
                    Industry1 = Industry.Manufacturing
                };
                Companies.Add(c5);

                Company c6 = new Company()
                {
                    CompanyName = "Hilton Worldwide",
                    CompanyDesc = "Hilton Worldwide offers business and leisure travelers the finest in accommodations, service, amenities and value.",
                    CompanyEmail = "hiltonworldwide@example.com",
                    Industry1 = Industry.Hospitality
                };
                Companies.Add(c6);

                Company c7 = new Company()
                {
                    CompanyName = "Aon",
                    CompanyDesc = "Aon is the leading global provider of risk management, insurance and reinsurance brokerage, and human resources solutions and outsourcing services.",
                    CompanyEmail = "aon@example.com",
                    Industry1 = Industry.Insurance,
                    Industry2 = Industry.Consulting
                };
                Companies.Add(c7);

                Company c8 = new Company()
                {
                    CompanyName = "Adlucent",
                    CompanyDesc = "Adlucent is a technology and analytics company specializing in selling products through the Internet for online retail clients.",
                    CompanyEmail = "adlucent@example.com",
                    Industry1 = Industry.Marketing,
                    Industry2 = Industry.Technology
                };
                Companies.Add(c8);

                Company c9 = new Company()
                {
                    CompanyName = "Stream Realty Partners",
                    CompanyDesc = "Stream Realty Partners, L.P. (Stream) is a national, commercial real estate firm with locations across the country.",
                    CompanyEmail = "streamrealtypartners@example.com",
                    Industry1 = Industry.RealEstate
                };
                Companies.Add(c9);

                Company c10 = new Company()
                {
                    CompanyName = "Microsoft",
                    CompanyDesc = "Microsoft is the worldwide leader in software, services and solutions that help people and businesses realize their full potential.",
                    CompanyEmail = "microsoft@example.com",
                    Industry1 = Industry.Technology
                };
                Companies.Add(c10);

                Company c11 = new Company()
                {
                    CompanyName = "Academy Sports & Outdoors",
                    CompanyDesc = "Academy Sports is intensely focused on being a leader in the sporting goods, outdoor and lifestyle retail arena",
                    CompanyEmail = "academysports@example.com",
                    Industry1 = Industry.Retail
                };
                Companies.Add(c11);

                Company c12 = new Company()
                {
                    CompanyName = "United Airlines",
                    CompanyDesc = "United Airlines has the most modern and fuel-efficient fleet (when adjusted for cabin size), and the best new aircraft order book, among U.S. network carriers",
                    CompanyEmail = "unitedairlines@example.com",
                    Industry1 = Industry.Transportation
                };
                Companies.Add(c12);

                Company c13 = new Company()
                {
                    CompanyName = "Target",
                    CompanyDesc = "Target is a leading retailer",
                    CompanyEmail = "target@example.com",
                    Industry1 = Industry.Retail
                };
                Companies.Add(c13);

                try
                {
                    foreach (Company companyToAdd in Companies)
                    {
                        strCompanyTitle = companyToAdd.CompanyName;
                        Company dbCompany = db.Companies.FirstOrDefault(b => b.CompanyName == companyToAdd.CompanyName);
                        if (dbCompany == null) //this company doesn't exist
                        {
                            db.Companies.Add(companyToAdd);
                            db.SaveChanges();
                            intCompaniesAdded += 1;
                        }
                        else //company exists - update values back to the original values in the seeded data file
                        {
                            dbCompany.CompanyName = companyToAdd.CompanyName;
                            dbCompany.CompanyDesc = companyToAdd.CompanyDesc;
                            dbCompany.CompanyEmail = companyToAdd.CompanyEmail;
                            dbCompany.Industry1 = companyToAdd.Industry1;
                            dbCompany.Industry2 = companyToAdd.Industry2;
                            dbCompany.Industry3 = companyToAdd.Industry3;
                            db.Update(dbCompany);
                            db.SaveChanges();
                            intCompaniesAdded += 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    String msg = "  Repositories added:" + intCompaniesAdded + "; Error on " + strCompanyTitle;
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
