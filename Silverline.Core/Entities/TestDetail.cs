using System.ComponentModel.DataAnnotations.Schema;

namespace Silverline.Core.Entities;

public class TestDetail
{
    public Guid TestHeaderId { get; set; }

    public Guid TestId { get; set; }

    public float Value { get; set; }

    public string Status { get; set; }

    public float Price { get; set; }

    [ForeignKey("TestHeaderId")]
    public TestHeader TestHeader { get; set; }

    [ForeignKey("TestId")]
    public DiagnosticTest DiagnosticTest { get; set; }

   
}
