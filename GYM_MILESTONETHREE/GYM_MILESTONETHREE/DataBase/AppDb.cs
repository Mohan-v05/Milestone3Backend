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

        public DbSet<Enrollments>enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity< Users >().HasOne(U => U.Address).WithOne(T => T.user).HasForeignKey<Address>(A => A.userId).OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);



            
        }
    }


        
    
}
