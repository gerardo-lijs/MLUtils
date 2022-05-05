namespace LijsDev.Utils.ML;

/// <summary>
/// Confusion Matrix clasiffication
/// </summary>
public enum ConfusionMatrixClasiffication
{
    /// <summary>
    /// Ground-truth region detected correctly.
    /// </summary>
    TruePositive,
    /// <summary>
    /// Background detected correctly.
    /// </summary>
    TrueNegative,
    /// <summary>
    /// Detected region with no matching ground-truth region.
    /// </summary>
    FalsePositive,
    /// <summary>
    /// Ground-truth region not detected.
    /// </summary>
    FalseNegative
}
