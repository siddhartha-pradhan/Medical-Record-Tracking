using Microsoft.ML.Data;

namespace EMR.Core.ViewModels.MLModel;

public class DiseasePrediction
{
    [ColumnName("PredictedLabel")] 
    public string Disease { get; set; }
    public float[] Score { get; set; }
}