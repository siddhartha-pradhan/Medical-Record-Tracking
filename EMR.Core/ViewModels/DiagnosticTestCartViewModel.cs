namespace Silverline.Core.ViewModels;

public class DiagnosticTestCartViewModel
{
	public Guid Id { get; set; }
	public string TestName { get; set; }	

	public string BookedDate { get; set; }

	public string FinalizedTest { get; set; }

	public string Range { get; set; }

	public string Value { get; set; }

	public string TechnicianRemarks { get; set; }

	public string TestType { get; set; }

	public string PaymentStatus { get; set; }
}
