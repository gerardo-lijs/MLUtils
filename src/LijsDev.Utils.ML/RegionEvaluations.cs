namespace LijsDev.Utils.ML;

using System.Collections;

/// <summary>
/// Region evaluations collection.
/// </summary>
/// <typeparam name="T">Type of region object</typeparam>
public class RegionEvaluations<T> : IReadOnlyList<RegionEvaluation<T>>
{
    private readonly List<RegionEvaluation<T>> _evaluations = new();

    /// <inheritdoc/>
    public RegionEvaluation<T> this[int index] => _evaluations[index];

    /// <inheritdoc/>
    public int Count => _evaluations.Count;

    /// <inheritdoc/>
    public IEnumerator<RegionEvaluation<T>> GetEnumerator() => _evaluations.GetEnumerator();

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator() => _evaluations.GetEnumerator();

    /// <summary>
    /// Add an evaluation were the background was detected correctly.
    /// </summary>
    public void Add_TrueNegative() => _evaluations.Add(new RegionEvaluation<T>(default, default, ConfusionMatrixClasiffication.TrueNegative));

    /// <summary>
    /// Add an evaluation were ground-truth region was detected correctly.
    /// </summary>
    public void Add_TruePositive(T groundTruth, T mlInference) => _evaluations.Add(new RegionEvaluation<T>(groundTruth, mlInference, ConfusionMatrixClasiffication.TruePositive));

    /// <summary>
    /// Add an evaluation were detected region has no matching ground-truth region.
    /// </summary>
    public void Add_FalsePositive(T mlInference) => _evaluations.Add(new RegionEvaluation<T>(default, mlInference, ConfusionMatrixClasiffication.FalsePositive));

    /// <summary>
    /// Add an evaluation were ground-truth region was not detected.
    /// </summary>
    public void Add_FalseNegative(T groundTruth) => _evaluations.Add(new RegionEvaluation<T>(groundTruth, default, ConfusionMatrixClasiffication.FalseNegative));
}
