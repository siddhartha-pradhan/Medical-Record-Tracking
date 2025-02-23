using Microsoft.CodeAnalysis;

namespace Silverline.Core.ViewModels;

public class ReportViewModel
{
    public string? PatientName { get; set; }

    public string? DateOfBirth { get; set; }

    public string? Medicine { get; set; }

    public string? TestName { get; set; }

    public string? TestResult { get; set; }

    public string? TestRange { get; set; }

    public string? TimeFormat { get; set; }

    public string? TimePeriod { get; set; }

    public string? TestRemarks { get; set; }

    public string? FinalizedDate { get; set; }

    public string StaffName { get; set; }
}
