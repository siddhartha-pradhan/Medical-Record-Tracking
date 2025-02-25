using EMR.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EMR.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<AppointmentDetail> AppointmentDetails { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<DiagnosticTest> DiagnosticTests { get; set; }

    public DbSet<MedicalOfficer> Doctors { get; set; }
    
    public DbSet<LaboratoryDiagnosis> LaboratoryDiagnosis { get; set; }

    public DbSet<LabTechnician> LabTechnicians { get; set; }

    public DbSet<Manufacturer> Manufacturers { get; set; }

    public DbSet<MedicationTreatment> MedicationTreatments { get; set; }

    public DbSet<MedicalRecord> MedicalRecords { get; set; }

    public DbSet<Medicine> Medicines { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Pharmacist> Pharmacists { get; set; }

    public DbSet<Specialty> Specialties { get; set; }

    public DbSet<TestCart> TestCarts { get; set; }

    public DbSet<TestType> TestTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Patient)
            .WithMany(x => x.Appointments)
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(x => x.Doctor)
            .WithMany(x => x.Appointments)
            .HasForeignKey(p => p.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicationTreatment>()
            .HasOne(x => x.AppointmentDetail)
            .WithMany(x => x.MedicalTreatments)
            .HasForeignKey(p => p.ReferralId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicationTreatment>()
            .HasOne(x => x.Medicine)
            .WithMany(x => x.MedicationTreatments)
            .HasForeignKey(p => p.MedicineId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(x => x.OrderHeader)
            .WithMany(x => x.OrderDetails)
            .HasForeignKey(p => p.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(x => x.Medicine)
            .WithMany(x => x.OrderDetails)
            .HasForeignKey(p => p.MedicineId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TestDetail>()
            .HasOne(x => x.TestHeader)
            .WithMany(x => x.TestDetails)
            .HasForeignKey(p => p.TestHeaderId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TestDetail>()
            .HasOne(x => x.DiagnosticTest)
            .WithMany(x => x.TestDetails)
            .HasForeignKey(p => p.TestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicineCart>()
            .HasOne(x => x.Patient)
            .WithMany(x => x.MedicineCarts)
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MedicineCart>()
            .HasOne(x => x.Medicine)
            .WithMany(x => x.MedicineCarts)
            .HasForeignKey(p => p.MedicineId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TestCart>()
           .HasOne(x => x.Patient)
           .WithMany(x => x.TestCarts)
           .HasForeignKey(p => p.PatientId)
           .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<TestCart>()
            .HasOne(x => x.DiagnosticTest)
            .WithMany(x => x.TestCarts)
            .HasForeignKey(p => p.TestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<IdentityUser>().ToTable("Users");
        modelBuilder.Entity<IdentityRole>().ToTable("Roles");
        modelBuilder.Entity<IdentityUserToken<string>>().ToTable("Tokens");
        modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
        modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("LoginAttempts");
    }
}