using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace sp23Team33FinalProject.Seeding
{
    public class SeedRoles
    {
        public static async Task AddAllRoles(RoleManager<IdentityRole> roleManager)
        {
            //if the student role doesn't exist, add it
            if (await roleManager.RoleExistsAsync("Student") == false)
            {
                //this code uses the role manager object to create the student role
                await roleManager.CreateAsync(new IdentityRole("Student"));
            }

            //if the Recruiter role doesn't exist, add it
            if (await roleManager.RoleExistsAsync("Recruiter") == false)
            {
                //this code uses the role manager object to create the recruiter role
                await roleManager.CreateAsync(new IdentityRole("Recruiter"));
            }

            //if the Recruiter role doesn't exist, add it
            if (await roleManager.RoleExistsAsync("CSO") == false)
            {
                //this code uses the role manager object to create the recruiter role
                await roleManager.CreateAsync(new IdentityRole("CSO"));
            }
        }
    }
}
