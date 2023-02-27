using Silverline.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Silverline.Infrastructure.Persistence;

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

    public DbSet<Doctor> Doctors { get; set; }
    
    public DbSet<LaboratoryDiagnosis> LaboratoryDiagnosis { get; set; }

    public DbSet<LabTechnician> LabTechnicians { get; set; }

    public DbSet<Manufacturer> Manufacturers { get; set; }

    public DbSet<MedicationTreatment> MedicationTreatments { get; set; }

    public DbSet<Medicine> Medicines { get; set; }

    public DbSet<MedicineCart> MedicineCarts { get; set; }

    public DbSet<OrderDetail> OrderDetails { get; set; }

    public DbSet<OrderHeader> OrderHeader { get; set; }

    public DbSet<Patient> Patients { get; set; }

    public DbSet<Pharmacist> Pharmacists { get; set; }

    public DbSet<RecordDetail> RecordDetails { get; set; }

    public DbSet<RecordHeader> RecordHeaders { get; set; }

    public DbSet<Specialty> Specialties { get; set; }

    public DbSet<TestCart> TestCarts { get; set; }

    public DbSet<TestDetail> TestDetails { get; set; }

    public DbSet<TestHeader> TestHeaders { get; set; }

    public DbSet<TestType> TestTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //modelBuilder.Entity<IdentityRole>().HasData(
        //    new IdentityRole
        //    {
        //        Id = "2c5e174e-3b0e-446f-86af-483d56fd7210",
        //        Name = "ADMIN",
        //        NormalizedName = "ADMIN".ToUpper()
        //    }
        //);

        //modelBuilder.Entity<IdentityUser>().HasData(
        //    new IdentityUser
        //    {
        //        Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
        //        UserName = "admin@admin.com",
        //        NormalizedUserName = "ADMIN",
        //        Email = "admin@admin.com",
        //        NormalizedEmail = "ADMIN@ADMIN.COM",
        //        EmailConfirmed = true,
        //        PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "@ff!N1ty"),
        //        PhoneNumber = "9803364638",
        //        SecurityStamp = "C7STNXSE5EHSFNMWSFGWEWXLK6NJZRYQ",
        //        ConcurrencyStamp = "dd0a4161-07b1-4d5d-92b4-61fa51aa14bf"
        //    }
        //);

        //modelBuilder.Entity<IdentityUserRole<string>>().HasData(
        //    new IdentityUserRole<string>
        //    {
        //        RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
        //        UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
        //    }
        //);

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