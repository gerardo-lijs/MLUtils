namespace LijsDev.Utils.ML;

/// <summary>
/// Region detected with corresponding confidence score and label
/// </summary>
/// <param name="Box">Rectangular border defined around the region</param>
/// <param name="Label">Label detected by the ML model for the region</param>
/// <param name="Confidence">Confidence score of the detection by the ML model for the region</param>
public record Prediction(BoundingBox Box, string Label, float Confidence);
