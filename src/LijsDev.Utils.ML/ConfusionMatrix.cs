namespace LijsDev.Utils.ML
{
    /// <summary>
    /// Confusion matrix is a table that is often used to describe the performance of a classification model (or "classifier") on a set of test data for which the true values are known
    /// </summary>
    public class ConfusionMatrix
    {
        // NB: https://www.dataschool.io/simple-guide-to-confusion-matrix-terminology/

        /// <summary>
        /// Predicted yes cases.
        /// </summary>
        public int TruePositives { get; }
        /// <summary>
        /// Predicted no cases.
        /// </summary>
        public int TrueNegatives { get; }
        /// <summary>
        /// Actual no cases.
        /// </summary>
        public int FalsePositives { get; }
        /// <summary>
        /// Actual yes cases.
        /// </summary>
        public int FalseNegatives { get; }

        ///<inheritdoc/>
        public ConfusionMatrix(int truePositives, int trueNegatives, int falsePositives, int falseNegatives)
        {
            TruePositives = truePositives;
            TrueNegatives = trueNegatives;
            FalsePositives = falsePositives;
            FalseNegatives = falseNegatives;
        }

        /// <summary>
        /// Total cases.
        /// </summary>
        public int Total => TruePositives + TrueNegatives + FalsePositives + FalseNegatives;

        /// <summary>
        /// Accuracy - Overall, how often is the classifier correct?
        /// </summary>
        public double Accuracy => Total == 0 ? 0 : (TruePositives + TrueNegatives) / (double)Total;

        /// <summary>
        /// F1Score is the harmonic mean of precision and recall and is a better measure than accuracy
        /// </summary>
        public double F1Score => Precision + Recall == 0 ? 0 : 2 * ((Precision * Recall) / (Precision + Recall));

        /// <summary>
        /// Precision - When it predicts yes, how often is it correct
        /// </summary>
        public double Precision => TruePositives + FalsePositives == 0 ? 1 : (double)TruePositives / (TruePositives + FalsePositives);

        /// <summary>
        /// Recall - When it's actually yes, how often does it predict yes?
        /// </summary>
        public double Recall => TruePositives + FalseNegatives == 0 ? 1 : (double)TruePositives / (TruePositives + FalseNegatives);

        /// <summary>
        /// Prevalence - How often does the yes condition actually occur in our sample?
        /// </summary>
        public double Prevalence => Total == 0 ? 0 : (TruePositives + FalseNegatives) / (double)Total;

        /// <summary>
        /// Error Rate - Overall, how often is it wrong?
        /// </summary>
        public double ErrorRate => Total == 0 ? 0 : (FalsePositives + FalseNegatives) / (double)Total;

        /// <summary>
        /// True Positive Rate - When it's actually yes, how often does it predict yes?
        /// </summary>
        public double TruePositiveRate => FalseNegatives + TruePositives == 0 ? 1 : (double)TruePositives / (FalseNegatives + TruePositives);

        /// <summary>
        /// False Positive Rate - When it's actually no, how often does it predict yes?
        /// </summary>
        public double FalsePositiveRate => TrueNegatives + FalsePositives == 0 ? 1 : (double)FalsePositives / (TrueNegatives + FalsePositives);

        /// <summary>
        /// True Negative Rate - When it's actually no, how often does it predict no?
        /// </summary>
        public double TrueNegativeRate => TrueNegatives + FalsePositives == 0 ? 1 : (double)TrueNegatives / (TrueNegatives + FalsePositives);
    }
}
