
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

        public DbSet<Payments> payments { get; set; }

       public DbSet<Notification> notification { get; set; }
       
        //public DbSet<PaymentNotification> PaymentNotifications { get; set; }

        public DbSet<Enrollments>enrollments { get; set; }

        public DbSet<ImageModel> Images { get; set; }

       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>()
                .HasOne(U => U.Address)
                .WithOne(T => T.user)
                .HasForeignKey<Address>(A => A.userId)
                .OnDelete(DeleteBehavior.Cascade);


            // Configure the relationship between Payments and Users for Payer
            //modelBuilder.Entity<Payments>()
            //    .HasOne(p => p.Payer)
            //    .WithMany(u => u.Payments)
            //    .HasForeignKey(p => p.PayerId)
            //    .OnDelete(DeleteBehavior.Restrict); // No cascading delete for Payer

            
            //modelBuilder.Entity<Payments>()
            //    .HasOne(p => p.Payee)
            //    .WithMany() // Payee does not need a collection of Payments in the Users model
            //    .HasForeignKey(p => p.PayeeId)
            //    .OnDelete(DeleteBehavior.Restrict);




            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.User)
                .WithMany(u => u.Enrollment)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Enrollments>()
                .HasOne(e => e.GymProgram)
                .WithMany(g => g.enrollments)
                .HasForeignKey(e => e.GymProgramId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payments>()
             .HasOne(p => p.Payer)
             .WithMany(u => u.Payments)
             .HasForeignKey(p => p.PayerId)
             .OnDelete(DeleteBehavior.Restrict);  // Avoid cascade for Payer

            modelBuilder.Entity<Payments>()
                .HasOne(p => p.Payee)
                .WithMany() // No need for a collection of Payments in Users
                .HasForeignKey(p => p.PayeeId)
                .OnDelete(DeleteBehavior.Restrict); // Avoid cascade for Payee

            base.OnModelCreating(modelBuilder);




        }
    }


        
    
}
