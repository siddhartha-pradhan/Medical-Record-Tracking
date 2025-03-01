using Microsoft.AspNetCore.Hosting;
using Microsoft.ML;

namespace EMR.Core.ViewModels.MLModel;

public class MLModel
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    private readonly MLContext _mlContext;
    private ITransformer? trainedModel = null;
    
    public MLModel(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
        _mlContext = new MLContext();
    }
    public void Build()
    {
        string trainDataPath = GetAbsolutePath();
        // STEP 1: Common data loading configuration
        var trainingDataView = _mlContext.Data.LoadFromTextFile<HeartData>(trainDataPath, hasHeader: true, separatorChar: ';');
        // STEP 2: Concatenate the features and set the training algorithm
        var pipeline = _mlContext.Transforms.Concatenate("Features", "Age", "Sex", "Cp", "TrestBps", "Chol", "Fbs", "RestEcg", "Thalac", "Exang", "OldPeak", "Slope", "Ca", "Thal")                          .Append(_mlContext.BinaryClassification.Trainers.FastTree(labelColumnName: "Label", featureColumnName: "Features"));
        trainedModel = pipeline.Fit(trainingDataView);
    }
    public HeartPrediction Consume(HeartData input)
    {
        var predictionEngine = _mlContext.Model.CreatePredictionEngine<HeartData, HeartPrediction>(trainedModel);
        return predictionEngine.Predict(input);
    }
    private string GetAbsolutePath()
    {
        string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "datasets", "HeartTraining.csv");
        return fullPath;
    }
    
    public DiseasePrediction TrainAndSaveModel(SymptomDataWithLabel symptomDataWithLabel)
    {
        string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "datasets", "DiseaseTraining.csv");
        // Create ML.NET context
        var mlContext = new MLContext(seed: 0);

        // Load data
        IDataView trainingDataView = mlContext.Data.LoadFromTextFile<SymptomDataWithLabel>(
            path: fullPath,
            hasHeader: true,
            separatorChar: ',');

        // Split data into training (80%) and test sets (20%)
        var dataSplit = mlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);
        var trainingData = dataSplit.TrainSet;
        var testData = dataSplit.TestSet;

        // Define the training pipeline
        var pipeline = mlContext.Transforms.Conversion.MapValueToKey(
                inputColumnName: "Disease", 
                outputColumnName: "Label")
            .Append(mlContext.Transforms.Concatenate("Features", new[]
            {
                "Fever", "Cough", "Fatigue", "ShortnessOfBreath",
                "HeadAche", "SoreThroat", "ChestPain", "Nausea",
                "BodyAches", "Diarrhea", "Rash", "JointPain"
            }))
            .Append(mlContext.Transforms.NormalizeMinMax("Features"))
            // For multiclass classification
            .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(
                labelColumnName: "Label",
                featureColumnName: "Features"))
            .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        Console.WriteLine("Training the model...");
        var model = pipeline.Fit(trainingData);
        Console.WriteLine("Model training complete.");

        // Evaluate the model on test data
        var predictions = model.Transform(testData);
        var metrics = mlContext.MulticlassClassification.Evaluate(predictions);

        Console.WriteLine($"Macro Accuracy: {metrics.MacroAccuracy:F2}");
        Console.WriteLine($"Micro Accuracy: {metrics.MicroAccuracy:F2}");
        Console.WriteLine($"Log Loss: {metrics.LogLoss:F2}");
        Console.WriteLine($"Log Loss Reduction: {metrics.LogLossReduction:F2}");

        // Create a prediction engine for a single test
        var predictionEngine = mlContext.Model.CreatePredictionEngine<SymptomDataWithLabel, DiseasePrediction>(model);
        
        var prediction = predictionEngine.Predict(symptomDataWithLabel);
       
        return prediction;
    }
}