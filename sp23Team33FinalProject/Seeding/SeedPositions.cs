using System;
using Microsoft.SqlServer.Server;
using sp23Team33FinalProject.Models;
using sp23Team33FinalProject.DAL;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;

namespace sp23Team33FinalProject.Seeding
{
	public class SeedPositions
	{
        public static async Task SeedAllPositions(AppDbContext db)
        {
            if (db.Positions.Count() == 27)
            {
                throw new NotSupportedException("The database already contains all 27 positions!");
            }

            Int32 intPositionsAdded = 0;
            String strPositionName = "Begin"; //helps to keep track of error on books
            List<Position> Positions = new List<Position>();

            try
            {
                Position p1 = new Position()
                {
                    PositionTitle = "Financial Planning Intern",
                    PositionType = PositionType.Internship,
                    Location = "Orlando, Florida",
                    Deadline = new DateTime(2023, 06, 01, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Finance"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Accounting"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Business Honors") }
                };
                p1.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Academy Sports & Outdoors");
                Positions.Add(p1);

                Position p2 = new Position()
                {
                    PositionTitle = "Digital Product Manager",
                    PositionType = PositionType.FullTime,
                    Location = "Houston, Texas",
                    Deadline = new DateTime(2023, 06, 01, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Marketing"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Business Honors"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Management")}
                };
                p2.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Academy Sports & Outdoors");
                Positions.Add(p2);

                Position p3 = new Position()
                {
                    PositionTitle = "Consultant ",
                    PositionDescription = "Full-time consultant position",
                    PositionType = PositionType.FullTime,
                    Location = "Houston, Texas",
                    Deadline = new DateTime(2023, 04, 15, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Accounting"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Business Honors") }
                };
                p3.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Accenture");
                Positions.Add(p3);

                Position p4 = new Position()
                {
                    PositionTitle = "Digital Intern",
                    PositionType = PositionType.Internship,
                    Location = "Dallas, Texas",
                    Deadline = new DateTime(2023, 05, 20, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Marketing")}
                };
                p4.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Accenture");
                Positions.Add(p4);

                Position p5 = new Position()
                {
                    PositionTitle = "Marketing Intern",
                    PositionDescription = "Help our marketing team develop new advertising strategies for local Austin businesses",
                    PositionType = PositionType.Internship,
                    Location = "Austin, Texas",
                    Deadline = new DateTime(2023, 04, 30, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Marketing")}
                };
                p5.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Adlucent");
                Positions.Add(p5);

                Position p6 = new Position()
                {
                    PositionTitle = "Sales Associate",
                    PositionType = PositionType.FullTime,
                    Location = "Los Angeles, California",
                    Deadline = new DateTime(2023, 04, 01, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Marketing"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Accounting"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Finance") }
                };
                p6.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Aon");
                Positions.Add(p6);

                Position p7 = new Position()
                {
                    PositionTitle = "Project Manager",
                    PositionType = PositionType.FullTime,
                    Location = "Chicago, Illinois",
                    Deadline = new DateTime(2023, 06, 01, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Supply Chain Management"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Finance"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Marketing"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Accounting"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "International Business"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Business Honors")}
                };
                p7.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Aon");
                //p0.Majors = db.Majors
                Positions.Add(p7);

                Position p8 = new Position()
                {
                    PositionTitle = "Web Development",
                    PositionDescription = "Developing a great new website for customer portfolio management",
                    PositionType = PositionType.FullTime,
                    Location = "Richmond, Virginia",
                    Deadline = new DateTime(2023, 03, 14, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS") }
                };
                p8.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Capital One");
                //p0.Majors = db.Majors
                Positions.Add(p8);

                Position p9 = new Position()
                {
                    PositionTitle = "Financial Analyst",
                    PositionType = PositionType.FullTime,
                    Location = "Richmond, Virginia",
                    Deadline = new DateTime(2023, 04, 15, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Finance") }
                };
                p9.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Capital One");
                //p0.Majors = db.Majors
                Positions.Add(p9);

                Position p10 = new Position()
                {
                    PositionTitle = "Analyst Development Program",
                    PositionType = PositionType.Internship,
                    Location = "Plano, Texas",
                    Deadline = new DateTime(2023, 05, 20, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Business Honors"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Finance") }
                };
                p10.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Capital One");
                //p0.Majors = db.Majors
                Positions.Add(p10);

                Position p11 = new Position()
                {
                    PositionTitle = "Accounting Intern",
                    PositionDescription = "Work in our audit group",
                    PositionType = PositionType.Internship,
                    Location = "Austin, Texas",
                    Deadline = new DateTime(2023, 05, 01, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Accounting") }
                };
                p11.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Deloitte");
                //p0.Majors = db.Majors
                Positions.Add(p11);

                Position p12 = new Position()
                {
                    PositionTitle = "Account Manager",
                    PositionType = PositionType.FullTime,
                    Location = "Dallas, Texas",
                    Deadline = new DateTime(2023, 02, 25, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Business Honors"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Accounting") }
                };
                p12.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Deloitte");
                //p0.Majors = db.Majors
                Positions.Add(p12);

                Position p13 = new Position()
                {
                    PositionTitle = "Amenities Analytics Intern",
                    PositionDescription = "Help analyze our amenities and customer rewards programs",
                    PositionType = PositionType.Internship,
                    Location = "New York, New York",
                    Deadline = new DateTime(2023, 03, 30, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Business Honors"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Finance"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Marketing")}
                };
                p13.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Hilton Worldwide");
                //p0.Majors = db.Majors
                Positions.Add(p13);

                Position p14 = new Position()
                {
                    PositionTitle = "Junior Programmer",
                    PositionType = PositionType.Internship,
                    Location = "Redmond, Washington",
                    Deadline = new DateTime(2023, 04, 03, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS") }
                };
                p14.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Microsoft");
                //p0.Majors = db.Majors
                Positions.Add(p14);

                Position p15 = new Position()
                {
                    PositionTitle = "Corporate Recruiting Intern",
                    PositionType = PositionType.Internship,
                    Location = "Redmond, Washington",
                    Deadline = new DateTime(2023, 04, 30, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Management") }
                };
                p15.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Microsoft");
                //p0.Majors = db.Majors
                Positions.Add(p15);

                Position p16 = new Position()
                {
                    PositionTitle = "Business Analyst",
                    PositionType = PositionType.FullTime,
                    Location = "Austin, Texas",
                    Deadline = new DateTime(2023, 05, 31, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS") }
                };
                p16.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Microsoft");
                //p0.Majors = db.Majors
                Positions.Add(p16);

                Position p17 = new Position()
                {
                    PositionTitle = "Product Marketing Intern",
                    PositionType = PositionType.Internship,
                    Location = "Redmond, Washington",
                    Deadline = new DateTime(2023, 06, 01, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Marketing") }
                };
                p17.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Microsoft");
                //p0.Majors = db.Majors
                Positions.Add(p17);

                Position p18 = new Position()
                {
                    PositionTitle = "Program Manager",
                    PositionType = PositionType.FullTime,
                    Location = "Redmond, Washington",
                    Deadline = new DateTime(2023, 06, 01, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Marketing") }
                };
                p18.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Microsoft");
                //p0.Majors = db.Majors
                Positions.Add(p18);

                Position p19 = new Position()
                {
                    PositionTitle = "Security Analyst",
                    PositionType = PositionType.FullTime,
                    Location = "Redmond, Washington",
                    Deadline = new DateTime(2023, 06, 01, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS") }
                };
                p19.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Microsoft");
                //p0.Majors = db.Majors
                Positions.Add(p19);

                Position p20 = new Position()
                {
                    PositionTitle = "Supply Chain Internship",
                    PositionType = PositionType.Internship,
                    Location = "Houston, Texas",
                    Deadline = new DateTime(2023, 05, 05, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Supply Chain Management") }
                };
                p20.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Shell");
                //p0.Majors = db.Majors
                Positions.Add(p20);

                Position p21 = new Position()
                {
                    PositionTitle = "Procurements Associate",
                    PositionDescription = "Handle procurement and vendor accounts",
                    PositionType = PositionType.FullTime,
                    Location = "Houston,  Texas",
                    Deadline = new DateTime(2023, 05, 30, 23, 59, 59)
                };
                p21.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Shell");
                //p0.Majors = db.Majors
                Positions.Add(p21);

                Position p22 = new Position()
                {
                    PositionTitle = "Programmer Analyst",
                    PositionType = PositionType.FullTime,
                    Location = "Minneapolis, Minnesota",
                    Deadline = new DateTime(2023, 05, 15, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Supply Chain Management") }
                };
                p22.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Target");
                //p0.Majors = db.Majors
                Positions.Add(p22);

                Position p23 = new Position()
                {
                    PositionTitle = "Intern",
                    PositionType = PositionType.Internship,
                    Location = "Minneapolis, Minnesota",
                    Deadline = new DateTime(2023, 05, 15, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Supply Chain Management"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Finance"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Marketing"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Accounting"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "International Business"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Business Honors"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Science and Technology Management"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Management")}
                };
                p23.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Target");
                //p0.Majors = db.Majors
                Positions.Add(p23);

                Position p24 = new Position()
                {
                    PositionTitle = "IT Rotational Program",
                    PositionType = PositionType.FullTime,
                    Location = "Dallas, Texas",
                    Deadline = new DateTime(2023, 05, 30, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS") }
                };
                p24.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Texas Instruments");
                //p0.Majors = db.Majors
                Positions.Add(p24);

                Position p25 = new Position()
                {
                    PositionTitle = "Sales Rotational Program",
                    PositionType = PositionType.FullTime,
                    Location = "Dallas, Texas",
                    Deadline = new DateTime(2023, 05, 30, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Marketing"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Management"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Accounting"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Finance") }
                };
                p25.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Texas Instruments");
                //p0.Majors = db.Majors
                Positions.Add(p25);

                Position p26 = new Position()
                {
                    PositionTitle = "Accounting Rotational Program",
                    PositionType = PositionType.FullTime,
                    Location = "Austin, Texas",
                    Deadline = new DateTime(2023, 05, 30, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "Accounting") }
                };
                p26.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "Texas Instruments");
                //p0.Majors = db.Majors
                Positions.Add(p26);

                Position p27 = new Position()
                {
                    PositionTitle = "Pilot",
                    PositionType = PositionType.FullTime,
                    Location = "Ft. Worth, Texas",
                    Deadline = new DateTime(2023, 10, 08, 23, 59, 59),
                    Majors = { db.Majors.FirstOrDefault(m => m.MajorName == "MIS"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Supply Chain Management"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Finance"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Marketing"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Accounting"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "International Business"),
                               db.Majors.FirstOrDefault(m => m.MajorName == "Business Honors") }
                };
                p27.Company = db.Companies.FirstOrDefault(c => c.CompanyName == "United Airlines");
                Positions.Add(p27);


                try
                {
                    foreach (Position positionToAdd in Positions)
                    {
                        strPositionName = positionToAdd.PositionTitle;
                        Position dbPosition = db.Positions.FirstOrDefault(b => b.PositionTitle == positionToAdd.PositionTitle);
                        if (dbPosition == null) //this position doesn't exist
                        {
                            db.Positions.Add(positionToAdd);
                            db.SaveChanges();
                            intPositionsAdded += 1;
                        }
                        else //company exists - update values back to the original values in the seeded data file
                        {
                            dbPosition.PositionTitle = positionToAdd.PositionTitle;
                            dbPosition.PositionDescription = positionToAdd.PositionDescription;
                            dbPosition.PositionType = positionToAdd.PositionType;
                            dbPosition.Location = positionToAdd.Location;
                            dbPosition.Deadline = positionToAdd.Deadline;
                            dbPosition.Company = positionToAdd.Company;
                            dbPosition.Majors = positionToAdd.Majors;
                            db.Update(dbPosition);
                            db.SaveChanges();
                            intPositionsAdded += 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    String msg = "  Repositories added:" + intPositionsAdded + "; Error on " + strPositionName;
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

