using Microsoft.ML.Data;

namespace EMR.Core.ViewModels.MLModel;

public class HeartPrediction
{
    [ColumnName("PredictedLabel")]
    public bool Prediction;
    public float Probability;
    public float Score;
}