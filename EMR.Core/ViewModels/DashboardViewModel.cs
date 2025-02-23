namespace Silverline.Core.ViewModels;

public class PatientDashboardViewModel
{
    public int Appointment { get; set; }

    public int BookedAppointment { get; set; }

    public int VisitedDoctors { get; set; }

    public int Medications { get; set; }    
}

public class DoctorDashboardViewModel
{
    public int TotalAppointments { get; set; }

    public int CompletedAppointments { get; set; }

    public int TodaysAppointments { get; set; }

    public int PatientCount { get; set; }
}

public class StaffDashboardViewModel
{
    public int TotalDiagnosis { get; set; }

    public int CompletedDiagnosis { get; set; }

    public int RemainingDiagnosis { get; set; }

    public int DiagnosisCount { get; set; }
}
