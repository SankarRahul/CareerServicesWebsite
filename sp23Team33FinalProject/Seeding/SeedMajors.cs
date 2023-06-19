using sp23Team33FinalProject.Models;
using sp23Team33FinalProject.DAL;
using Microsoft.SqlServer.Server;

using System.Text;

namespace sp23Team33FinalProject.Seeding
{
    public static class SeedMajors
    {
        public static async Task SeedAllMajors(AppDbContext db)
        {
            // check to see if all the genres have already been added
            if (db.Majors.Count() == 9)
            {
                //exit the program - we don't need to do any of this
                NotSupportedException ex = new NotSupportedException("Major record count is already 9!");
                throw ex;
            }
            Int32 intMajorsAdded = 0;
            try
            {
                //Create a list of languages
                List<Major> Majors = new List<Major>();

                Major m1 = new Major() { MajorName = "MIS" };
                Majors.Add(m1);

                Major m2 = new Major() { MajorName = "International Business" };
                Majors.Add(m2);

                Major m3 = new Major() { MajorName = "Finance" };
                Majors.Add(m3);

                Major m4 = new Major() { MajorName = "Accounting" };
                Majors.Add(m4);

                Major m5 = new Major() { MajorName = "Marketing" };
                Majors.Add(m5);

                Major m6 = new Major() { MajorName = "Science and Technology Management" };
                Majors.Add(m6);

                Major m7 = new Major() { MajorName = "Business Honors" };
                Majors.Add(m7);

                Major m8 = new Major() { MajorName = "Supply Chain Management" };
                Majors.Add(m8);

                Major m9 = new Major() { MajorName = "Management" };
                Majors.Add(m9);

                foreach (Major majorToAdd in Majors)
                {
                    //test if each genre exists
                    Major dbMajor = db.Majors.FirstOrDefault(g => g.MajorName == majorToAdd.MajorName);
                    if (dbMajor == null)
                    {
                        db.Majors.Add(majorToAdd);
                        db.SaveChanges();
                        intMajorsAdded += 1;
                    }
                }
            }
            catch
            {
                String msg = "Majors Added: " + intMajorsAdded.ToString();
                throw new InvalidOperationException(msg);
            }
        }
    }
}
