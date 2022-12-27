using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<DiagnosticTest> DiagnosticTests { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

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
        modelBuilder.Entity<Appointment>().HasNoKey();
        modelBuilder.Entity<MedicationTreatment>().HasNoKey();
        modelBuilder.Entity<OrderDetail>().HasNoKey();
        modelBuilder.Entity<TestDetail>().HasNoKey();

    }
}