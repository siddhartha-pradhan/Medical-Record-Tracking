namespace EMR.Core.ViewModels;

public class AppointmentFinalizedViewModel
{
    public Guid AppointmentId { get; set; }

    public string RequestTitle { get; set; }    

    public string DiagnosticTitle { get; set; } 

    public string DiagnosticDescription { get; set; }   

    public string BookedDate { get; set; }

    public string AppointedDate { get; set;}
}
