using FitnessTracker.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data
{
    public class FitnessTrackerContext : DbContext
    {
        public FitnessTrackerContext(DbContextOptions<FitnessTrackerContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<RunActivity> RunActivities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User")
                .HasIndex(u => u.Email)
                .IsUnique(); 
            modelBuilder.Entity<RunActivity>().ToTable("RunActivity");
               

        }
    }
}
