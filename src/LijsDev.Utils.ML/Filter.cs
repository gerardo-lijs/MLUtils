namespace LijsDev.Utils.ML;

/// <summary>
/// Utilities for filtering predictions
/// </summary>
public static class Filter
{
    /// <summary>
    /// Applies non-maximum suppression (NMS) algorithm to a list of predictions (bounding boxes with corresponding confidence scores and label).
    /// </summary>
    /// <param name="predictions">List of prediction bounding boxes with corresponding confidence scores and label</param>
    /// <param name="iouThreshold">The overlap threshold for suppressing unnecessary boxes.</param>
    public static List<Prediction> ApplyNMS(in List<Prediction> predictions, float iouThreshold = 0.5f)
    {
        var resultsNMS = new List<Prediction>();

        // Sort by confidence score
        var inputPredictions = predictions.OrderByDescending(x => x.Confidence).Cast<Prediction?>().ToList();

        var f = 0;
        while (f < inputPredictions.Count)
        {
            var res = inputPredictions[f];
            if (res is null)
            {
                f++;
                continue;
            }

            resultsNMS.Add(new Prediction(res.Box, res.Label, res.Confidence));
            inputPredictions[f] = null;

            var iou = inputPredictions.Select(x => x is null ? double.NaN : EvaluationMetrics.IntersectionOverUnion(res.Box, x.Box)).ToList();
            for (var i = 0; i < iou.Count; i++)
            {
                if (double.IsNaN(iou[i]))
                {
                    continue;
                }

                if (iou[i] > iouThreshold)
                {
                    inputPredictions[i] = null;
                }
            }
            f++;
        }

        return resultsNMS;
    }
}
