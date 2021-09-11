using System;

namespace LijsDev.Utils.ML
{
    /// <summary>
    /// Utilities for evaluation metrics
    /// </summary>
    public static class EvaluationMetrics
    {
        /// <summary>
        /// IoU (Intersection over union) measures the overlap between 2 boundaries. We use that to
        /// measure how much our predicted boundary overlaps with the ground truth (the real object
        /// boundary). In some datasets, we predefine an IoU threshold (say 0.5) in classifying
        /// whether the prediction is a true positive or a false positive. This method filters
        /// overlapping bounding boxes with lower probabilities.
        /// </summary>
        /// <param name="boundingBoxA">First bouding box</param>
        /// <param name="boundingBoxB">Second bouding box</param>
        /// <returns></returns>
        public static double IntersectionOverUnion(BoundingBox boundingBoxA, BoundingBox boundingBoxB)
        {
            var areaA = (boundingBoxA.Width) * (boundingBoxA.Height);
            var areaB = (boundingBoxB.Width) * (boundingBoxB.Height);

            if (areaA <= 0 || areaB <= 0) return 0;

            var dx = Math.Max(0, Math.Min(boundingBoxA.Left + boundingBoxA.Width, boundingBoxB.Left + boundingBoxB.Width) - Math.Max(boundingBoxA.Left, boundingBoxB.Left));
            var dy = Math.Max(0, Math.Min(boundingBoxA.Top + boundingBoxA.Height, boundingBoxB.Top + boundingBoxB.Height) - Math.Max(boundingBoxA.Top, boundingBoxB.Top));
            var intersectionArea = dx * dy;

            var iou = intersectionArea / (areaA + areaB - intersectionArea);
            return iou < 0 ? 0 : Math.Min(Math.Round(iou, 4), 1);
        }
    }
}
