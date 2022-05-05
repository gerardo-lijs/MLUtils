namespace LijsDev.Utils.ML;

/// <summary>
/// Object detect region machine learning evaluation.
/// </summary>
/// <typeparam name="T">Type of region object</typeparam>
public class RegionEvaluation<T>
{
    /// <summary>
    /// Region Ground-truth.
    /// Value is null for false positives and true negatives.
    /// </summary>
    public T? GroundTruth { get; set; }
    /// <summary>
    /// Region inference.
    /// Value is null for false negatives and true negatives.
    /// </summary>
    public T? MLInference { get; set; }

    /// <summary>
    /// Confusion Matrix clasiffication
    /// </summary>
    public ConfusionMatrixClasiffication ConfusionMatrixClasiffication { get; set; }

    /// <inheritdoc/>
    internal RegionEvaluation(T? groundTruth, T? mlInference, ConfusionMatrixClasiffication confusionMatrixClasiffication)
    {
        GroundTruth = groundTruth;
        MLInference = mlInference;
        ConfusionMatrixClasiffication = confusionMatrixClasiffication;
    }
}
