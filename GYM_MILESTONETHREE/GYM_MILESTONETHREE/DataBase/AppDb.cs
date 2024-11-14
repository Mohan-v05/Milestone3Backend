
using GYM_MILESTONETHREE.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GYM_MILESTONETHREE.DataBase
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options) { }

        public DbSet<GymPrograms> gymprograms {  get; set; }

        public DbSet<Users> users { get; set; }

        //public DbSet<Payments> payments { get; set; }

       // public DbSet<Notification> notification { get; set; }
       public DbSet<PaymentNotification> PaymentNotifications { get; set; }

        public DbSet<Enrollments>enrollments { get; set; }

        public DbSet<ImageModel> Images { get; set; }

        public DbSet<Payments> payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>().HasOne(U => U.Address).WithOne(T => T.user).HasForeignKey<Address>(A => A.userId).OnDelete(DeleteBehavior.Cascade);
            // Configure the many-to-many relationship between Users and GymPrograms through Enrollments
            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollment)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);  // Optional: Define delete behavior

            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.GymProgram)
                .WithMany(g => g.enrollments)
                .HasForeignKey(e => e.GymProgramId)
                .OnDelete(DeleteBehavior.Cascade);  // Optional: Define delete behavior

            modelBuilder.Entity<PaymentNotification>()
              .HasOne(pn => pn.User) // Relationship between PaymentNotification and User
              .WithMany() // No need for a navigation property in the User class for PaymentNotifications
              .HasForeignKey(pn => pn.UserId)
              .OnDelete(DeleteBehavior.Cascade); // Cascade delete when the related User is deleted

            modelBuilder.Entity<PaymentNotification>()
                .HasOne(pn => pn.Payment) // Relationship between PaymentNotification and Payment
                .WithMany() // No need for a navigation property in the Payment class for PaymentNotifications
                .HasForeignKey(pn => pn.PaymentId)
                .OnDelete(DeleteBehavior.Cascade); // 

            base.OnModelCreating(modelBuilder);




        }
    }


        
    
}
