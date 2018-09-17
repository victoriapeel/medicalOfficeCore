using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using medicalOfficeCore.Models;

namespace medicalOfficeCore.Data
{
    public class medicalOfficeCoreContext : DbContext
    {
        public medicalOfficeCoreContext (DbContextOptions<medicalOfficeCoreContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MO");

            //Prevent Cascade Delete from Doctor to Patient
            //so we are prevented from deleting a Doctor with
            //Patients assigned
            modelBuilder.Entity<Doctor>()
                .HasMany<Patient>(d => d.Patients)
                .WithOne(p => p.Doctor)
                .HasForeignKey(p => p.DoctorID)
                .OnDelete(DeleteBehavior.Restrict);

            //Add a unique index to the OHIP Number
            modelBuilder.Entity<Patient>()
            .HasIndex(p => p.OHIP)
            .IsUnique();
        }
    }
}
