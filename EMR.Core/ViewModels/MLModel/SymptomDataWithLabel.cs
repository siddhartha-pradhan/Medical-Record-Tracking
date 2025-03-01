using Microsoft.ML.Data;

namespace EMR.Core.ViewModels.MLModel;

public class SymptomDataWithLabel
{
    [LoadColumn(0)] public string Disease { get; set; }
    [LoadColumn(1)] public float Fever { get; set; }
    [LoadColumn(2)] public float Cough { get; set; }
    [LoadColumn(3)] public float Fatigue { get; set; }
    [LoadColumn(4)] public float ShortnessOfBreath { get; set; }
    [LoadColumn(5)] public float HeadAche { get; set; }
    [LoadColumn(6)] public float SoreThroat { get; set; }
    [LoadColumn(7)] public float ChestPain { get; set; }
    [LoadColumn(8)] public float Nausea { get; set; }
    [LoadColumn(9)] public float BodyAches { get; set; }
    [LoadColumn(10)] public float Diarrhea { get; set; }
    [LoadColumn(11)] public float Rash { get; set; }
    [LoadColumn(12)] public float JointPain { get; set; }
}