namespace EMR.Core.ViewModels;

public class AppointmentHistoryViewModel
{
    public List<BookedAppointmentViewModel> BookedAppointments { get; set; }

    public List<FinalizedAppointmentViewModel> FinalizedAppointments { get; set; }
}
