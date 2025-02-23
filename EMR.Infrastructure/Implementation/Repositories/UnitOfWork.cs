using Silverline.Infrastructure.Persistence;
using Silverline.Application.Interfaces.Repositories;
using Silverline.Core.Entities;

namespace Silverline.Infrastructure.Implementation.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        AppUser = new AppUserRepository(_dbContext);
        Appointment = new AppointmentRepository(_dbContext);
		AppointmentDetail = new AppointmentDetailRepository(_dbContext);
		Category = new CategoryRepository(_dbContext);
        Manufacturer = new ManufacturerRepository(_dbContext);
        Medicine = new MedicineRepository(_dbContext);
        MedicalRecord = new MedicalRecordRepository(_dbContext);
        Specialty = new SpecialtyRepository(_dbContext);
        Test = new TestRepository(_dbContext);
        TestType = new TestTypeRepository(_dbContext);
        Patient = new PatientRepository(_dbContext);
        Doctor = new DoctorRepository(_dbContext);
        Pharmacist = new PharmacistRepository(_dbContext);
        LabTechnician = new LabTechnicianRepository(_dbContext);
        TestCart = new TestCartRepository(_dbContext);
		LabDiagnosis = new LabDiagnosisRepository(_dbContext);
        MedicalTreatment = new MedicalTreatmentRepository(_dbContext);

	}

    public IAppUserRepository AppUser { get; set; }

    public IAppointmentRepository Appointment { get; set; }

	public IAppointmentDetailRepository AppointmentDetail { get; set; }

	public IPatientRepository Patient { get; set; }
    
    public IDoctorRepository Doctor { get; set; }
    
    public ILabTechnicianRepository LabTechnician { get; set; }

	public ILabDiagnosisRepository LabDiagnosis { get; set; }

	public IMedicalRecordRepository MedicalRecord { get; set; }

	public IMedicalTreatmentRepository MedicalTreatment { get; set; }

	public IPharmacistRepository Pharmacist { get; set; }

    public ICategoryRepository Category { get; set; }
    
    public IManufacturerRepository Manufacturer { get; set; }
    
    public IMedicineRepository Medicine { get; set; }
    
    public ISpecialtyRepository Specialty { get; set; }
    
    public ITestRepository Test { get; set; }
    
    public ITestTypeRepository TestType { get; set; }

    public ITestCartRepository TestCart { get; set; }

    public void Save()
    {
        _dbContext.SaveChanges();
    }
}
