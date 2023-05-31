using Microsoft.CodeAnalysis;

namespace Silverline.Core.ViewModels;

public class ReportViewModel
{
    public string? Name { get; set; }

    public string? DateOfBirth { get; set; }

    public string? Medicine { get; set; }

    public string? Test { get; set; }

    public string? Result { get; set; }

    public string? Range { get; set; }

    public string? TimeFormat { get; set; }

    public string? TimePeriod { get; set; }

    public string? Remarks { get; set; }

    public string? FinalizedDate { get; set; }

    public string StaffName { get; set; }
}
