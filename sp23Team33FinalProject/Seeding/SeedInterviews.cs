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
    public class SeedInterviews
    {
        public static void SeedAllInterviews(AppDbContext db)
        {
            if (db.Interviews.Count() == 14)
            {
                throw new NotSupportedException("The database already contains all 14 interviews!");
            }

            Int32 intInterviewsAdded = 0;
            System.String strInterview = "Begin"; //helps to keep track of error on books
            List<Interview> Interviews = new List<Interview>();

            try
            {
                Interview i1 = new Interview()

                {
                    InterviewDateTime = new DateTime(2023, 05, 13, 13, 0, 0),
                    Room = Room.Room2,

                };
                i1.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Marketing Intern" &&
                    a.Position.Company.CompanyName == "Adlucent" && a.Student.UserName == "feeley@penguin.org");
                i1.Host = db.Users.FirstOrDefault(d => d.UserName == "taylordjay@aool.com");
                i1.Student = db.Users.FirstOrDefault(s => s.UserName == "feeley@penguin.org");
                Interviews.Add(i1);

                Interview i2 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 05, 14, 13, 0, 0),
                    Room = Room.Room2

                };
                i2.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Marketing Intern" &&
                    a.Position.Company.CompanyName == "Adlucent" && a.Student.UserName == "erynrice@aoll.com");
                i2.Host = db.Users.FirstOrDefault(d => d.UserName == "taylordjay@aool.com");
                i2.Student = db.Users.FirstOrDefault(s => s.UserName == "erynrice@aoll.com");
                Interviews.Add(i2);

                Interview i3 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 05, 15, 13, 0, 0),
                    Room = Room.Room2

                };
                i3.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Corporate Recruiting Intern" &&
                    a.Position.Company.CompanyName == "Microsoft" && a.Student.UserName == "cmiller@bob.com");
                i3.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Corporate Recruiting Intern");
                i3.Host = db.Users.FirstOrDefault(d => d.UserName == "louielouie@aool.com");
                i3.Student = db.Users.FirstOrDefault(s => s.UserName == "cmiller@bob.com");
                Interviews.Add(i3);

                Interview i4 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 05, 13, 10, 0, 0),
                    Room = Room.Room1

                };
                i4.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Account Manager" &&
                    a.Position.Company.CompanyName == "Deloitte" && a.Student.UserName == "estuart@anchor.net");
                i4.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Account Manager");
                i4.Host = db.Users.FirstOrDefault(d => d.UserName == "mclarence@aool.com");
                i4.Student = db.Users.FirstOrDefault(s => s.UserName == "estuart@anchor.net");
                Interviews.Add(i4);

                Interview i5 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 05, 16, 14, 0, 0),
                    Room = Room.Room2

                };
                i5.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Web Development" &&
                    a.Position.Company.CompanyName == "Capital One" && a.Student.UserName == "cbaker@example.com");
                i5.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Web Development");
                i5.Host = db.Users.FirstOrDefault(d => d.UserName == "smartinmartin.Martin@aool.com");
                i5.Student = db.Users.FirstOrDefault(s => s.UserName == "cbaker@example.com");
                Interviews.Add(i5);

                Interview i6 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 04, 01, 9, 0, 0),
                    Room = Room.Room1

                };
                i6.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Amenities Analytics Intern" &&
                    a.Position.Company.CompanyName == "Hilton Worldwide" && a.Student.UserName == "erynrice@aoll.com");
                i6.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Amenities Analytics Intern");
                i6.Host = db.Users.FirstOrDefault(d => d.UserName == "yhuik9.Taylor@aool.com");
                i6.Student = db.Users.FirstOrDefault(s => s.UserName == "erynrice@aoll.com");
                Interviews.Add(i6);

                Interview i7 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 04, 01, 10, 0, 0),
                    Room = Room.Room1

                };
                i7.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Amenities Analytics Intern" &&
                    a.Position.Company.CompanyName == "Hilton Worldwide" && a.Student.UserName == "tfreeley@minnetonka.ci.us");
                i7.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Amenities Analytics Intern");
                i7.Host = db.Users.FirstOrDefault(d => d.UserName == "yhuik9.Taylor@aool.com");
                i7.Student = db.Users.FirstOrDefault(s => s.UserName == "erynrice@aoll.com");
                Interviews.Add(i7);

                Interview i8 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 04, 02, 15, 0, 0),
                    Room = Room.Room4

                };
                i8.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Amenities Analytics Intern" &&
                    a.Position.Company.CompanyName == "Hilton Worldwide" && a.Student.UserName == "limchou@gogle.com");
                i8.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Amenities Analytics Intern");
                i8.Host = db.Users.FirstOrDefault(d => d.UserName == "yhuik9.Taylor@aool.com");
                i8.Student = db.Users.FirstOrDefault(s => s.UserName == "limchou@gogle.com");
                Interviews.Add(i8);

                Interview i9 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 05, 10, 9, 0, 0),
                    Room = Room.Room1

                };
                i9.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Supply Chain Internship" &&
                    a.Position.Company.CompanyName == "Shell" && a.Student.UserName == "ingram@jack.com");
                i9.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Supply Chain Internship");
                i9.Host = db.Users.FirstOrDefault(d => d.UserName == "elowe@netscrape.net");
                i9.Student = db.Users.FirstOrDefault(s => s.UserName == "ingram@jack.com");
                Interviews.Add(i9);

                Interview i10 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 05, 10, 11, 0, 0),
                    Room = Room.Room1

                };
                i10.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Supply Chain Internship" &&
                    a.Position.Company.CompanyName == "Shell" && a.Student.UserName == "saunders@pen.com");
                i10.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Supply Chain Internship");
                i10.Host = db.Users.FirstOrDefault(d => d.UserName == "elowe@netscrape.net");
                i10.Student = db.Users.FirstOrDefault(s => s.UserName == "saunders@pen.com");
                Interviews.Add(i10);

                Interview i11 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 05, 16, 15, 0, 0),
                    Room = Room.Room3

                };
                i11.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Financial Analyst" &&
                    a.Position.Company.CompanyName == "Capital One" && a.Student.UserName == "johnsmith187@aoll.com");
                i11.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Financial Analyst");
                i11.Host = db.Users.FirstOrDefault(d => d.UserName == "or@aool.com");
                i11.Student = db.Users.FirstOrDefault(s => s.UserName == "johnsmith187@aoll.com");
                Interviews.Add(i11);

                Interview i12 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 05, 16, 11, 0, 0),
                    Room = Room.Room4

                };
                i12.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Accounting Intern" &&
                    a.Position.Company.CompanyName == "Deloitte" && a.Student.UserName == "cluce@gogle.com");
                i12.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Accounting Intern");
                i12.Host = db.Users.FirstOrDefault(d => d.UserName == "nelson.Kelly@aool.com");
                i12.Student = db.Users.FirstOrDefault(s => s.UserName == "cluce@gogle.com");
                Interviews.Add(i12);

                Interview i13 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 06, 05, 14, 0, 0),
                    Room = Room.Room3

                };
                i13.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Consultant" &&
                    a.Position.Company.CompanyName == "Accenture" && a.Student.UserName == "estuart@anchor.net");
                i13.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Consultant");
                i13.Host = db.Users.FirstOrDefault(d => d.UserName == "michelle@example.com");
                i13.Student = db.Users.FirstOrDefault(s => s.UserName == "estuart@anchor.net");
                Interviews.Add(i13);

                Interview i14 = new Interview()
                {
                    InterviewDateTime = new DateTime(2023, 06, 05, 16, 0, 0),
                    Room = Room.Room3

                };
                i14.Application = db.Applications.FirstOrDefault(a => a.Position.PositionTitle == "Consultant" &&
                    a.Position.Company.CompanyName == "Accenture" && a.Student.UserName == "wjhearniii@umich.org");
                i14.Position = db.Positions.FirstOrDefault(p => p.PositionTitle == "Consultant");
                i14.Host = db.Users.FirstOrDefault(d => d.UserName == "toddy@aool.com");
                i14.Student = db.Users.FirstOrDefault(s => s.UserName == "wjhearniii@umich.org");
                Interviews.Add(i14);


                try
                {
                    foreach (Interview interviewToAdd in Interviews)
                    {
                        strInterview = interviewToAdd.Student.FirstName;
                        Interview dbInterview = db.Interviews.FirstOrDefault(b => b.InterviewDateTime == interviewToAdd.InterviewDateTime
                                                    && b.Room == interviewToAdd.Room);
                        if (dbInterview == null) //this company doesn't exist
                        {
                            db.Interviews.Add(interviewToAdd);
                            db.SaveChanges();
                            intInterviewsAdded += 1;
                        }
                        else //company exists - update values back to the original values in the seeded data file
                        {
                            dbInterview.Student = interviewToAdd.Student;
                            dbInterview.Host = interviewToAdd.Host;
                            dbInterview.InterviewDateTime = interviewToAdd.InterviewDateTime;
                            dbInterview.Application = interviewToAdd.Application;
                            dbInterview.Room = interviewToAdd.Room;
                            db.Update(dbInterview);
                            db.SaveChanges();
                            intInterviewsAdded += 1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.String msg = "  Repositories added: " + intInterviewsAdded + "; Error on " + strInterview;
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

