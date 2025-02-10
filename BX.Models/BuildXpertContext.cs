using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BX.Models
{
    public class BuildXpertContext : IdentityDbContext<User>, IBuildXpertContext
    {
        public DbSet<User> Users { get; set; }
        //public DbSet<UserEmail> UserEmails { get; set; }
        //public DbSet<UserGoogleID> UserGoogleIDs { get; set; }
        public DbSet<UserPhone> UserPhones { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectState> ProjectStates { get; set; }
        //public DbSet<test> test { get; set; }

        public BuildXpertContext() : base()
        {

        }

        public BuildXpertContext(DbContextOptions<BuildXpertContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure composite primary key for UserEmail
            modelBuilder.Entity<UserEmail>()
                .HasKey(ue => new { ue.UserId, ue.Email });

            // Configure composite primary key for UserPhone
            modelBuilder.Entity<UserPhone>()
                .HasKey(up => new { up.UserId, up.PhoneNumber });

            // Configure one-to-one relationship for UserGoogleID
            //modelBuilder.Entity<User>()
            //    .HasOne(u => u.UserGoogleID)
            //    .WithOne(ug => ug.User)
            //    .HasForeignKey<UserGoogleID>(ug => ug.UserId);

            // Configure foreign key relationships for Project
            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectOwner)
                .WithMany()
                .HasForeignKey(p => p.ProjectOwnerId)
                .OnDelete(DeleteBehavior.Restrict); // No cascade delete

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Customer)
                .WithMany()
                .HasForeignKey(p => p.CustomerId)
                .OnDelete(DeleteBehavior.Restrict); // No cascade delete

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectState)
                .WithMany(ps => ps.Projects)
                .HasForeignKey(p => p.ProjectStateId)
                .OnDelete(DeleteBehavior.Restrict); // Cascade delete
        }

        public async Task<bool> CanConnectAsync()
        {
            return await Database.CanConnectAsync();
        }
    }
}

