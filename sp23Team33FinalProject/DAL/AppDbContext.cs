using System;
using Microsoft.EntityFrameworkCore;
using sp23Team33FinalProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace sp23Team33FinalProject.DAL
{
    //NOTE: This class definition references the user class for this project.  
    //If your User class is called something other than AppUser, you will need
    //to change it in the line below
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //this code makes sure the database is re-created on the $5/month Azure tier
            builder.HasPerformanceLevel("Basic");
            builder.HasServiceTier("Basic");
            base.OnModelCreating(builder);

            // tells migration that app is parent of intereview in one-to-one relationship
            builder.Entity<Application>()
            .HasOne(b => b.Interview)
            .WithOne(i => i.Application)
            .HasForeignKey<Interview>(b => b.AppForeignKey);
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<Major> Majors { get; set; }
        public DbSet<Position> Positions { get; set; }

    }
}
