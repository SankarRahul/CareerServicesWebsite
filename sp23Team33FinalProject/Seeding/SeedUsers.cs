using Microsoft.AspNetCore.Identity;
using sp23Team33FinalProject.Models;
using sp23Team33FinalProject.Utilities;
using sp23Team33FinalProject.DAL;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Text;
using static System.Reflection.Metadata.BlobBuilder;
using CsvHelper;

/// TODO: this needs to be update along with registration views
namespace sp23Team33FinalProject.Seeding
{
    public static class SeedUsers
    {
        public async static Task<IdentityResult> SeedAllUsers(UserManager<AppUser> userManager, AppDbContext context)
        {
            //THIS IS TO ADD THE CSOs ------------------------------------------------------------------------------
            List<AddUserModel> AllUsers = new List<AddUserModel>();

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "ra@aoo.com",
                    Email = "ra@aoo.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Allen",
                    LastName = "Rogers"

                },
                Password = "3wCynC",
                RoleName = "CSO"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "captain@enterprise.net",
                    Email = "captain@enterprise.net",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jean Luc",
                    LastName = "Picard"

                },
                Password = "Pbon0r",
                RoleName = "CSO"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "slayer@onegirl.net",
                    Email = "slayer@onegirl.net",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Buffy",
                    LastName = "Summers"

                },
                Password = "jW5fPP",
                RoleName = "CSO"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "liz@ggmail.com",
                    Email = "liz@ggmail.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Elizabeth",
                    LastName = "Markham"

                },
                Password = "0QyilL",
                RoleName = "CSO"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "twin@deservedbetter.com",
                    Email = "twin@deservedbetter.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Fred",
                    LastName = "Weasley"

                },
                Password = "atLm6W",
                RoleName = "CSO"
            });


            //THIS IS TO ADD THE STUDENTS --------------------------------------------------------------------------
            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "cbaker@example.com",
                    Email = "cbaker@example.com",
                    PhoneNumber = "152-275-7212",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Christopher",
                    LastName = "Baker",
                    MiddleInitial = "L",
                    Birthday = new DateTime(2001, 08, 02),
                    Street = "1 David Park",
                    City = "Austin",
                    State = "TX",
                    Zip = "78705",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.91m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "bookworm",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "banker@longhorn.net",
                    Email = "banker@longhorn.net",
                    PhoneNumber = "596-211-5872",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Michelle",
                    LastName = "Banks",
                    Birthday = new DateTime(2000, 11, 18),
                    Street = "10117 Swallow Road",
                    City = "Austin",
                    State = "TX",
                    Zip = "78712",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.52m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "International Business")
                },
                Password = "aclfest2017",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "franco@example.com",
                    Email = "franco@example.com",
                    PhoneNumber = "756-979-6344",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Franco",
                    LastName = "Broccolo",
                    MiddleInitial = "V",
                    Birthday = new DateTime(2002, 05, 02),
                    Street = "21344 Marcy Avenue",
                    City = "Austin",
                    State = "TX",
                    Zip = "78786",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.20m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "aggies",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "wchang@example.com",
                    Email = "wchang@example.com",
                    PhoneNumber = "220-613-2686",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Wendy",
                    LastName = "Chang",
                    MiddleInitial = "L",
                    Birthday = new DateTime(2001, 08, 20),
                    Street = "894 Kim Junction",
                    City = "Eagle Pass",
                    State = "TX",
                    Zip = "78852",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2025",
                    GPA = 3.56m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Finance")
                },
                Password = "alaskaboy",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "limchou@gogle.com",
                    Email = "limchou@gogle.com",
                    PhoneNumber = "728-717-9608",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Lim",
                    LastName = "Chou",
                    Birthday = new DateTime(2003, 04, 06),
                    Street = "703 Anthes Lane",
                    City = "Austin",
                    State = "TX",
                    Zip = "78729",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 2.63m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Finance")
                },
                Password = "allyrally",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "shdixon@aoll.com",
                    Email = "shdixon@aoll.com",
                    PhoneNumber = "338-796-7818",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Shan",
                    LastName = "Dixon",
                    MiddleInitial = "D",
                    Birthday = new DateTime(2002, 10, 21),
                    Street = "45927 Forest Run Trail",
                    City = "Georgetown",
                    State = "TX",
                    Zip = "78628",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2026",
                    GPA = 3.62m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "International Business")
                },
                Password = "Anchorage",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "j.b.evans@aheca.org",
                    Email = "j.b.evans@aheca.org",
                    PhoneNumber = "878-921-1122",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jim Bob",
                    LastName = "Evans",
                    MiddleInitial = "D",
                    Birthday = new DateTime(2001, 10, 08),
                    Street = "51 Miller Park",
                    City = "Austin",
                    State = "TX",
                    Zip = "78705",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 2.64m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Accounting")
                },
                Password = "billyboy",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "feeley@penguin.org",
                    Email = "feeley@penguin.org",
                    PhoneNumber = "389-364-3017",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Lou Ann",
                    LastName = "Feeley",
                    MiddleInitial = "K",
                    Birthday = new DateTime(2003, 06, 19),
                    Street = "80686 Ryan Terrace",
                    City = "Austin",
                    State = "TX",
                    Zip = "78704",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.66m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Marketing")
                },
                Password = "bunnyhop",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "tfreeley@minnetonka.ci.us",
                    Email = "tfreeley@minnetonka.ci.us",
                    PhoneNumber = "327-105-4962",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Tesa",
                    LastName = "Freeley",
                    MiddleInitial = "P",
                    Birthday = new DateTime(1996, 09, 12),
                    Street = "97327 Express Avenue",
                    City = "College Station",
                    State = "TX",
                    Zip = "77840",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2023",
                    GPA = 3.98m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "ACcounting")
                },
                Password = "dustydusty",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "mgarcia@gogle.com",
                    Email = "mgarcia@gogle.com",
                    PhoneNumber = "480-950-2469",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Margaret",
                    LastName = "Garcia",
                    MiddleInitial = "L",
                    Birthday = new DateTime(2002, 06, 17),
                    Street = "1 Arrowood Road",
                    City = "Austin",
                    State = "TX",
                    Zip = "78756",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.22m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "gowest",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "chaley@thug.com",
                    Email = "chaley@thug.com",
                    PhoneNumber = "439-864-2291",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Charles",
                    LastName = "Haley",
                    MiddleInitial = "E",
                    Birthday = new DateTime(1998, 05, 15),
                    Street = "5035 Dayton Court",
                    City = "Austin",
                    State = "TX",
                    Zip = "78746",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.78m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "hampton1",
                RoleName = "Student"
            }
            );

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "jeffh@sonic.com",
                    Email = "jeffh@sonic.com",
                    PhoneNumber = "287-989-2098",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jeffrey",
                    LastName = "Hampton",
                    MiddleInitial = "T",
                    Birthday = new DateTime(2003, 04, 08),
                    Street = "90461 Evergreen Place",
                    City = "Austin",
                    State = "TX",
                    Zip = "78756",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.66m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Science and Technology Management")
                },
                Password = "hickhickup",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "wjhearniii@umich.org",
                    Email = "wjhearniii@umich.org",
                    PhoneNumber = "759-247-6853",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "John",
                    LastName = "Hearn",
                    MiddleInitial = "B",
                    Birthday = new DateTime(2000, 09, 15),
                    Street = "973 Stephen Alley",
                    City = "Liberty",
                    State = "TX",
                    Zip = "77575",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.46m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Business Honors")
                },
                Password = "ingram2015",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "ahick@yaho.com",
                    Email = "ahick@yaho.com",
                    PhoneNumber = "603-447-9200",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Anthony",
                    LastName = "Hicks",
                    MiddleInitial = "J",
                    Birthday = new DateTime(2003, 05, 07),
                    Street = "80319 Forster Parkway",
                    City = "San Antonio",
                    State = "TX",
                    Zip = "78203",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.12m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Supply Chain Management")
                },
                Password = "jhearn22",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "ingram@jack.com",
                    Email = "ingram@jack.com",
                    PhoneNumber = "965-996-5936",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Brad",
                    LastName = "Ingram",
                    MiddleInitial = "S",
                    Birthday = new DateTime(2001, 02, 06),
                    Street = "96 Stang Hill",
                    City = "New Braunfels",
                    State = "TX",
                    Zip = "78132",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.72m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Supply Chain Management")
                },
                Password = "joejoejoe",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "toddj@yourmom.com",
                    Email = "toddj@yourmom.com",
                    PhoneNumber = "171-155-1224",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Todd",
                    LastName = "Jacobs",
                    MiddleInitial = "L",
                    Birthday = new DateTime(2001, 08, 29),
                    Street = "23726 Main Crossing",
                    City = "New York",
                    State = "NY",
                    Zip = "10101",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 2.64m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "jrod2017",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "thequeen@aska.net",
                    Email = "thequeen@aska.net",
                    PhoneNumber = "300-564-3682",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Victoria",
                    LastName = "Lawrence",
                    MiddleInitial = "M",
                    Birthday = new DateTime(2001, 01, 29),
                    Street = "6299 Moland Alley",
                    City = "Lockhart",
                    State = "TX",
                    Zip = "78644",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2025",
                    GPA = 3.72m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "longhorns",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "linebacker@gogle.com",
                    Email = "linebacker@gogle.com",
                    PhoneNumber = "968-319-5113",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Erik",
                    LastName = "Lineback",
                    MiddleInitial = "W",
                    Birthday = new DateTime(2004, 05, 21),
                    Street = "6 Truax Street",
                    City = "Kingwood",
                    State = "TX",
                    Zip = "77325",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2026",
                    GPA = 3.15m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Accounting")
                },
                Password = "louielouie",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "elowe@netscare.net",
                    Email = "elowe@netscare.net",
                    PhoneNumber = "932-455-8010",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Ernest",
                    LastName = "Lowe",
                    MiddleInitial = "S",
                    Birthday = new DateTime(2001, 12, 27),
                    Street = "50883 Heath Park",
                    City = "Beverly Hills",
                    State = "CA",
                    Zip = "90210",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.07m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Finance")
                },
                Password = "martin1234",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "cluce@gogle.com",
                    Email = "cluce@gogle.com",
                    PhoneNumber = "782-613-4758",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Chuck",
                    LastName = "Luce",
                    MiddleInitial = "B",
                    Birthday = new DateTime(2001, 12, 23),
                    Street = "5 Carberry Point",
                    City = "Navasota",
                    State = "TX",
                    Zip = "77868",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.87m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Accounting")
                },
                Password = "meganr34",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "mackcloud@george.com",
                    Email = "mackcloud@george.com",
                    PhoneNumber = "212-941-9557",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jennifer",
                    LastName = "MacLeod",
                    MiddleInitial = "D",
                    Birthday = new DateTime(2000, 11, 12),
                    Street = "10 Amoth Lane",
                    City = "Austin",
                    State = "TX",
                    Zip = "78712",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 4.00m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "meow88",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "cmartin@beets.com",
                    Email = "cmartin@beets.com",
                    PhoneNumber = "752-457-0330",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Elizabeth",
                    LastName = "Markham",
                    MiddleInitial = "P",
                    Birthday = new DateTime(2000, 12, 21),
                    Street = "1186 Pepper Wood Junction",
                    City = "Austin",
                    State = "TX",
                    Zip = "78712",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.53m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Business Honors")
                },
                Password = "mustangs",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "clarence@yoho.com",
                    Email = "clarence@yoho.com",
                    PhoneNumber = "589-146-8077",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Clarence",
                    LastName = "Martin",
                    MiddleInitial = "A",
                    Birthday = new DateTime(2002, 10, 27),
                    Street = "961 Cody Parkway",
                    City = "Schenectady",
                    State = "NY",
                    Zip = "12345",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.11m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Supply Chain Management")
                },
                Password = "mydogspot",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "gregmartinez@drdre.com",
                    Email = "gregmartinez@drdre.com",
                    PhoneNumber = "661-289-7904",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Gregory",
                    LastName = "Martinez",
                    MiddleInitial = "R",
                    Birthday = new DateTime(2003, 05, 11),
                    Street = "4921 High Crossing Way",
                    City = "Austin",
                    State = "TX",
                    Zip = "78717",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2025",
                    GPA = 3.43m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Business Honors")
                },
                Password = "nothinggood",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "cmiller@bob.com",
                    Email = "cmiller@bob.com",
                    PhoneNumber = "665-845-6330",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Charles",
                    LastName = "Miller",
                    MiddleInitial = "R",
                    Birthday = new DateTime(2001, 06, 16),
                    Street = "145 Old Gate Alley",
                    City = "Austin",
                    State = "TX",
                    Zip = "78727",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.14m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Management")
                    //Major = Majors.FirstOrDefault(g => g.CompanyName == "Management")
                },
                Password = "onetime",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "knelson@aoll.com",
                    Email = "knelson@aoll.com",
                    PhoneNumber = "930-565-2673",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Kelly",
                    LastName = "Nelson",
                    MiddleInitial = "T",
                    Birthday = new DateTime(2003, 07, 23),
                    Street = "6 Schlimgen Lane",
                    City = "Beaumont",
                    State = "TX",
                    Zip = "77720",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.03m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Finance")
                },
                Password = "painting",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "joewin@xfactor.com",
                    Email = "joewin@xfactor.com",
                    PhoneNumber = "420-256-9808",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Joe",
                    LastName = "Nguyen",
                    MiddleInitial = "C",
                    Birthday = new DateTime(2000, 12, 23),
                    Street = "6647 Eastlawn Trail",
                    City = "San Marcos",
                    State = "TX",
                    Zip = "78667",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.65m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Management")
                },
                Password = "Password1",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "orielly@foxnews.cnn",
                    Email = "orielly@foxnews.cnn",
                    PhoneNumber = "175-378-5467",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Bill",
                    LastName = "O'Reilly",
                    MiddleInitial = "T",
                    Birthday = new DateTime(2004, 11, 24),
                    Street = "7 Mallard Court",
                    City = "Bergheim",
                    State = "TX",
                    Zip = "78004",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.46m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Finance")
                },
                Password = "penguin12",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "ankaisrad@gogle.com",
                    Email = "ankaisrad@gogle.com",
                    PhoneNumber = "138-466-1566",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Anka",
                    LastName = "Radkovich",
                    MiddleInitial = "L",
                    Birthday = new DateTime(2001, 08, 08),
                    Street = "9517 Hooker Street",
                    City = "Austin",
                    State = "TX",
                    Zip = "78789",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.67m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Business Honors")
                },
                Password = "pepperoni",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "megrhodes@freserve.co.uk",
                    Email = "megrhodes@freserve.co.uk",
                    PhoneNumber = "676-163-8634",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Megan",
                    LastName = "Rhodes",
                    MiddleInitial = "C",
                    Birthday = new DateTime(2001, 05, 20),
                    Street = "9 Clemons Terrace",
                    City = "Orlando",
                    State = "FL",
                    Zip = "32830",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.14m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Management")
                },
                Password = "potato",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "erynrice@aoll.com",
                    Email = "erynrice@aoll.com",
                    PhoneNumber = "589-264-1451",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Eryn",
                    LastName = "Rice",
                    MiddleInitial = "M",
                    Birthday = new DateTime(2004, 04, 29),
                    Street = "37080 Darwin Parkway",
                    City = "South Padre Island",
                    State = "TX",
                    Zip = "78597",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2026",
                    GPA = 3.92m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Marketing")
                },
                Password = "radgirl",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "jorge@noclue.com",
                    Email = "jorge@noclue.com",
                    PhoneNumber = "number",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jorge",
                    LastName = "Rodriguez",
                    Birthday = new DateTime(2002, 03, 10),
                    Street = "61 Iowa Drive",
                    City = "Austin",
                    State = "TX",
                    Zip = "78744",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2025",
                    GPA = 3.64m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "raiders",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "mrrogers@lovelyday.com",
                    Email = "mrrogers@lovelyday.com",
                    PhoneNumber = "393-579-0324",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Allen",
                    LastName = "Rogers",
                    MiddleInitial = "B",
                    Birthday = new DateTime(2001, 02, 20),
                    Street = "56 Express Trail",
                    City = "Canyon Lake",
                    State = "TX",
                    Zip = "78133",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.01m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Marketing")
                },
                Password = "ricearoni",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "stjean@athome.com",
                    Email = "stjean@athome.com",
                    PhoneNumber = "721-977-0922",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Olivier",
                    LastName = "Saint-Jean",
                    MiddleInitial = "M",
                    Birthday = new DateTime(2002, 02, 26),
                    Street = "712 Dayton Terrace",
                    City = "Austin",
                    State = "TX",
                    Zip = "78779",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.24m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Science and Technology Management")
                },
                Password = "rogerthat",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "saunders@pen.com",
                    Email = "saunders@pen.com",
                    PhoneNumber = "751-939-8193",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Sarah",
                    LastName = "Saunders",
                    MiddleInitial = "J",
                    Birthday = new DateTime(2002, 02, 05),
                    Street = "77 International Drive",
                    City = "Austin",
                    State = "TX",
                    Zip = "78720",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.16m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Supply Chain Management")
                },
                Password = "slowwind",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "willsheff@email.com",
                    Email = "willsheff@email.com",
                    PhoneNumber = "240-536-6411",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "William",
                    LastName = "Sewell",
                    MiddleInitial = "T",
                    Birthday = new DateTime(2002, 10, 13),
                    Street = "9 Dahle Road",
                    City = "Austin",
                    State = "TX",
                    Zip = "78705",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.07m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "smitty444",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "sheffiled@gogle.com",
                    Email = "sheffiled@gogle.com",
                    PhoneNumber = "319-324-8954",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Martin",
                    LastName = "Sheffield",
                    MiddleInitial = "J",
                    Birthday = new DateTime(2002, 11, 10),
                    Street = "2036 Carpenter Alley",
                    City = "Round Rock",
                    State = "TX",
                    Zip = "78680",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.36m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "snowsnow",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "johnsmith187@aoll.com",
                    Email = "johnsmith187@aoll.com",
                    PhoneNumber = "880-673-7665",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "John",
                    LastName = "Smith",
                    MiddleInitial = "A",
                    Birthday = new DateTime(2001, 08, 19),
                    Street = "773 Sullivan Court",
                    City = "Austin",
                    State = "TX",
                    Zip = "78760",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.57m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Finance")
                },
                Password = "something",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "dustroud@mail.com",
                    Email = "dustroud@mail.com",
                    PhoneNumber = "224-668-7934",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Dustin",
                    LastName = "Stroud",
                    MiddleInitial = "P",
                    Birthday = new DateTime(2002, 03, 05),
                    Street = "59 Dakota Point",
                    City = "Sweet Home",
                    State = "TX",
                    Zip = "77987",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.49m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Marketing")
                },
                Password = "spotmydog",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "estuart@anchor.net",
                    Email = "estuart@anchor.net",
                    PhoneNumber = "762-772-8288",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Eric",
                    LastName = "Stuart",
                    MiddleInitial = "D",
                    Birthday = new DateTime(2004, 04, 17),
                    Street = "20644 Badeau Point",
                    City = "Corpus Christi",
                    State = "TX",
                    Zip = "78412",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.58m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Business Honors")
                },
                Password = "stewball",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "peterstump@noclue.com",
                    Email = "peterstump@noclue.com",
                    PhoneNumber = "num837-556-3954ber",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Peter",
                    LastName = "Stump",
                    MiddleInitial = "L",
                    Birthday = new DateTime(2009, 05, 02),
                    Street = "79 Starling Park",
                    City = "Pflugerville",
                    State = "TX",
                    Zip = "78660",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2025",
                    GPA = 2.55m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Supply Chain Management")
                },
                Password = "tanner5454",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "jtanner@mustang.net",
                    Email = "jtanner@mustang.net",
                    PhoneNumber = "772-648-7173",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jeremy",
                    LastName = "Tanner",
                    MiddleInitial = "S",
                    Birthday = new DateTime(2002, 12, 15),
                    Street = "71 Main Circle",
                    City = "Austin",
                    State = "TX",
                    Zip = "zip",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.16m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Management")
                },
                Password = "taylorbaylor",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "taylordjay@aoll.com",
                    Email = "taylordjay@aoll.com",
                    PhoneNumber = "799-951-2316",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Allison",
                    LastName = "Taylor",
                    MiddleInitial = "R",
                    Birthday = new DateTime(2001, 07, 27),
                    Street = "202 Ramsey Junction",
                    City = "Austin",
                    State = "TX",
                    Zip = "78713",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.07m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Marketing")
                },
                Password = "teeoff22",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "rtaylor@gogle.com",
                    Email = "rtaylor@gogle.com",
                    PhoneNumber = "659-189-0460",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Rachel",
                    LastName = "Taylor",
                    MiddleInitial = "K",
                    Birthday = new DateTime(2003, 05, 16),
                    Street = "831 Namekagon Avenue",
                    City = "Austin",
                    State = "TX",
                    Zip = "78712",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 2.88m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Finance")
                },
                Password = "texas1",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "teefrank@noclue.com",
                    Email = "teefrank@noclue.com",
                    PhoneNumber = "324-210-5709",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Frank",
                    LastName = "Tee",
                    MiddleInitial = "J",
                    Birthday = new DateTime(2003, 08, 22),
                    Street = "6587 Debs Junction",
                    City = "Austin",
                    State = "TX",
                    Zip = "78786",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.50m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Finance")
                },
                Password = "toddy25",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "ctucker@alphabet.co.uk",
                    Email = "ctucker@alphabet.co.uk",
                    PhoneNumber = "299-804-9719",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Clent",
                    LastName = "Tucker",
                    MiddleInitial = "J",
                    Birthday = new DateTime(2001, 09, 28),
                    Street = "37 Ohio Pass",
                    City = "San Antonio",
                    State = "TX",
                    Zip = "78279",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2024",
                    GPA = 3.04m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "tucksack1",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "avelasco@yoho.com",
                    Email = "avelasco@yoho.com",
                    PhoneNumber = "761-519-2765",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Allen",
                    LastName = "Velasco",
                    MiddleInitial = "G",
                    Birthday = new DateTime(2003, 06, 27),
                    Street = "3 Bartillon Junction",
                    City = "Navasota",
                    State = "TX",
                    Zip = "77868",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.55m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "MIS")
                },
                Password = "vinovino",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "vinovino@grapes.com",
                    Email = "vinovino@grapes.com",
                    PhoneNumber = "786-151-7351",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Janet",
                    LastName = "Vino",
                    MiddleInitial = "E",
                    Birthday = new DateTime(1997, 12, 17),
                    Street = "59699 Hovde Terrace",
                    City = "Boston",
                    State = "MA",
                    Zip = "02114",
                    PositionType = PositionType.Internship,
                    GraduationYear = "2025",
                    GPA = 3.28m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Business Honors")
                },
                Password = "whatever",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "westj@pioneer.net",
                    Email = "westj@pioneer.net",
                    PhoneNumber = "324-228-6078",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jake",
                    LastName = "West",
                    MiddleInitial = "T",
                    Birthday = new DateTime(2002, 11, 16),
                    Street = "89 Jenna Terrace",
                    City = "Marble Falls",
                    State = "TX",
                    Zip = "78654",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.66m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Finance")
                },
                Password = "whocares",
                RoleName = "Student"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "winner@hootmail.com",
                    Email = "winner@hootmail.com",
                    PhoneNumber = "num865-837-5028ber",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Louis",
                    LastName = "Winthorpe",
                    MiddleInitial = "L",
                    Birthday = new DateTime(2001, 01, 20),
                    Street = "4 Main Place",
                    City = "Austin",
                    State = "TX",
                    Zip = "78730",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 2.57m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Finance")
                },
                Password = "woodyman1",
                RoleName = "Student"
            });


            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "rwood@voyager.net",
                    Email = "rwood@voyager.net",
                    PhoneNumber = "958-745-9445",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Reagan",
                    LastName = "Wood",
                    MiddleInitial = "B",
                    Birthday = new DateTime(2001, 12, 25),
                    Street = "95 Longview Point",
                    City = "Austin",
                    State = "TX",
                    Zip = "78712",
                    PositionType = PositionType.FullTime,
                    GraduationYear = "2023",
                    GPA = 3.78m,
                    Major = context.Majors.FirstOrDefault(c => c.MajorName == "Accounting")
                },
                Password = "xcellent",
                RoleName = "Student"
            });


            //THIS IS TO ADD THE RECRUITERS ------------------------------------------------------------------------------
            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "michelle@example.com",
                    Email = "michelle@example.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Michelle",
                    LastName = "Banks",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Accenture")
                },
                Password = "jVb0Z6",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "toddy@aool.com",
                    Email = "toddy@aool.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Todd",
                    LastName = "Jacobs",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Accenture")
                },
                Password = "1PnrBV",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "elowe@netscrape.net",
                    Email = "elowe@netscrape.net",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Ernest",
                    LastName = "Lowe",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Shell")
                },
                Password = "v3n5AV",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "mclarence@aool.com",
                    Email = "mclarence@aool.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Clarence",
                    LastName = "Martin",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Deloitte")
                },
                Password = "zBLq3S",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "nelson.Kelly@aool.com",
                    Email = "nelson.Kelly@aool.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Kelly",
                    LastName = "Nelson",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Deloitte")
                },
                Password = "FSb8rA",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "megrhodes@freezing.co.uk",
                    Email = "megrhodes@freezing.co.uk",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Megan",
                    LastName = "Rhodes",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Deloitte")
                },
                Password = "1xVfHp",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "sheff44@ggmail.com",
                    Email = "sheff44@ggmail.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Martin",
                    LastName = "Sheffield",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Texas Instruments")
                },
                Password = "4XKLsd",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "peterstump@hootmail.com",
                    Email = "peterstump@hootmail.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Peter",
                    LastName = "Stump",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Texas Instruments")
                },
                Password = "1XdmSV",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "yhuik9.Taylor@aool.com",
                    Email = "yhuik9.Taylor@aool.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Rachel",
                    LastName = "Taylor",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Hilton Worldwide")
                },
                Password = "9yhFS3",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "tuck33@ggmail.com",
                    Email = "tuck33@ggmail.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Clent",
                    LastName = "Tucker",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Aon")
                },
                Password = "I6BgsS",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "taylordjay@aool.com",
                    Email = "taylordjay@aool.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Allison",
                    LastName = "Taylor",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Adlucent")
                },
                Password = "Vjb1wI",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "jojoe@ggmail.com",
                    Email = "jojoe@ggmail.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Joe",
                    LastName = "Nguyen",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Stream Realty Partners")
                },
                Password = "xI8Brg",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "hicks43@ggmail.com",
                    Email = "hicks43@ggmail.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Anthony",
                    LastName = "Hicks",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Microsoft")
                },
                Password = "s33WOz",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "orielly@foxnets.com",
                    Email = "orielly@foxnets.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Bill",
                    LastName = "O'Reilly",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Microsoft")
                },
                Password = "pS2OJh",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "louielouie@aool.com",
                    Email = "louielouie@aool.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Louis",
                    LastName = "Winthorpe",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Microsoft")
                },
                Password = "fq7yDw",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "smartinmartin.Martin@aool.com",
                    Email = "smartinmartin.Martin@aool.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Gregory",
                    LastName = "Martinez",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Capital One")
                },
                Password = "1rKkMW",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "or@aool.com",
                    Email = "or@aool.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Anka",
                    LastName = "Radkovich",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Capital One")
                },
                Password = "8K0cAh",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "tanner@ggmail.com",
                    Email = "tanner@ggmail.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Jeremy",
                    LastName = "Tanner",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Shell")
                },
                Password = "w9wPff",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "tee_frank@hootmail.com",
                    Email = "tee_frank@hootmail.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Frank",
                    LastName = "Tee",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Academy Sports & Outdoors")
                },
                Password = "1EIwbx",
                RoleName = "Recruiter"
            });

            AllUsers.Add(new AddUserModel()
            {
                User = new AppUser()
                {
                    //populate the user properties that are from the 
                    //IdentityUser base class
                    UserName = "target_spot@example.com",
                    Email = "target_spot@example.com",

                    //TODO: Add additional fields that you created on the AppUser class
                    //FirstName is included as an example
                    FirstName = "Spot",
                    LastName = "Dog",
                    Company = context.Companies.FirstOrDefault(c => c.CompanyName == "Target")
                },
                Password = "spotthedog",
                RoleName = "Recruiter"
            });

            //create flag to help with errors
            String errorFlag = "Start";

            //create an identity result
            IdentityResult result = new IdentityResult();
            //call the method to seed the user
            try
            {
                foreach (AddUserModel aum in AllUsers)
                {
                    errorFlag = aum.User.Email;
                    result = await Utilities.AddUser.AddUserWithRoleAsync(aum, userManager, context);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem adding the user with email: "
                    + errorFlag, ex);
            }

            return result;
        }
    }
}
