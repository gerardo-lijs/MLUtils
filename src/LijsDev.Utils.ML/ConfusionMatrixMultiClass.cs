using System.Collections.Generic;
using System.Linq;

namespace LijsDev.Utils.ML
{
    /// <summary>
    /// Confusion matrix is a table that is often used to describe the performance of a classification model (or "classifier") on a set of test data for which the true values are known
    /// </summary>
    public class ConfusionMatrixMultiClass
    {
        private readonly int[,] _matrix;

        /// <summary>
        /// Labels used in multi class confusion matrix
        /// </summary>
        public List<string> Labels { get; }

        /// <summary>
        /// Predicted yes cases.
        /// </summary>
        public int TruePositives(string classLabel)
        {
            var index = GetMatrixClassLabelIndex(classLabel);
            return _matrix[index, index];
        }

        /// <summary>
        /// Predicted no cases.
        /// </summary>
        public int TrueNegatives(string classLabel)
        {
            var index = GetMatrixClassLabelIndex(classLabel);
            var trueNegatives = 0;
            for (var row = 0; row < Labels.Count; row++)
            {
                if (row == index) continue;
                for (var col = 0; col < Labels.Count; col++)
                {
                    if (col == index) continue;
                    trueNegatives += _matrix[row, col];
                }
            }
            return trueNegatives;
        }

        /// <summary>
        /// Actual no cases.
        /// </summary>
        public int FalsePositives(string classLabel)
        {
            var index = GetMatrixClassLabelIndex(classLabel);
            var falsePositives = 0;
            for (var col = 0; col < Labels.Count; col++)
            {
                if (col == index) continue;
                falsePositives += _matrix[index, col];
            }
            return falsePositives;
        }

        /// <summary>
        /// Actual yes cases.
        /// </summary>
        public int FalseNegatives(string classLabel)
        {
            var index = GetMatrixClassLabelIndex(classLabel);
            var falseNegatives = 0;
            for (var row = 0; row < Labels.Count; row++)
            {
                if (row == index) continue;
                falseNegatives += _matrix[row, index];
            }
            return falseNegatives;
        }

        ///<inheritdoc/>
        public ConfusionMatrixMultiClass(List<string> labels)
        {
            Labels = labels;

            // Init matrix array
            _matrix = new int[labels.Count, labels.Count];
        }

        private int GetMatrixClassLabelIndex(string classLabel)
        {
            var index = Labels.IndexOf(classLabel);
            if (index == -1) throw new System.ArgumentOutOfRangeException($"Class label: {classLabel} does not exists in confusion matrix multi class.");
            return index;
        }

        /// <summary>
        /// Add prediction result to confusion matrix (adds one).
        /// </summary>
        public void AddPrediction(string label, string predictedLabel)
        {
            var trueLabelIndex = GetMatrixClassLabelIndex(label);
            var predictedLabelIndex = GetMatrixClassLabelIndex(predictedLabel);

            _matrix[trueLabelIndex, predictedLabelIndex]++;
        }

        /// <summary>
        /// Set prediction result to confusion matrix.
        /// </summary>
        public void SetPrediction(string label, string predictedLabel, int value)
        {
            var trueLabelIndex = GetMatrixClassLabelIndex(label);
            var predictedLabelIndex = GetMatrixClassLabelIndex(predictedLabel);

            _matrix[trueLabelIndex, predictedLabelIndex] = value;
        }

        /// <summary>
        /// Total cases.
        /// </summary>
        public int Total(string classLabel) => TruePositives(classLabel) + TrueNegatives(classLabel) + FalsePositives(classLabel) + FalseNegatives(classLabel);

        /// <summary>
        /// Accuracy - Overall, how often is the classifier correct?
        /// </summary>
        public double Accuracy(string classLabel) => Total(classLabel) == 0 ? 0 : (TruePositives(classLabel) + TrueNegatives(classLabel)) / (double)Total(classLabel);

        /// <summary>
        /// F1Score is the harmonic mean of precision and recall and is a better measure than accuracy
        /// </summary>
        public double F1Score(string classLabel) => Precision(classLabel) + Recall(classLabel) == 0 ? 0 : 2 * ((Precision(classLabel) * Recall(classLabel)) / (Precision(classLabel) + Recall(classLabel)));

        /// <summary>
        /// MicroF1 is calculated by considering the total TP, total FP and total FN of the model. It does not consider each class individually, It calculates the metrics globally.
        /// </summary>
        public double MicroF1
        {
            get
            {
                var totalTP = 0;
                foreach (var label in Labels)
                {
                    totalTP += TruePositives(label);
                }

                var totalFP = 0;
                foreach (var label in Labels)
                {
                    totalFP += FalsePositives(label);
                }

                var totalFN = 0;
                foreach (var label in Labels)
                {
                    totalFN += FalseNegatives(label);
                }

                var precision = totalTP + totalFP == 0 ? 1 : (double)totalTP / (totalTP + totalFP);
                var recall = totalTP + totalFN == 0 ? 1 : (double)totalTP / (totalTP + totalFN);

                return precision + recall == 0 ? 0 : 2 * ((precision * recall) / (precision + recall));
            }
        }

        /// <summary>
        /// WeightedF1 is weighted-averaged F1-score.
        /// </summary>
        public double WeightedF1
        {
            get
            {
                var classF1Score = new List<double>();
                var numberOfCases = new List<double>();
                foreach (var label in Labels)
                {
                    classF1Score.Add(F1Score(label));
                    numberOfCases.Add(TruePositives(label) + FalseNegatives(label));
                }

                var sumWeightedF1Score = classF1Score.Zip(numberOfCases, (classF1Score, numberOfCases) => classF1Score * numberOfCases).Sum();
                return numberOfCases.Sum() == 0 ? 0 : sumWeightedF1Score / numberOfCases.Sum();
            }
        }

        /// <summary>
        /// MacroF1 calculates metrics for each class individually and then takes unweighted mean of the measures.
        /// </summary>
        public double MacroF1
        {
            get
            {
                var totalF1Score = 0d;
                foreach (var label in Labels)
                {
                    totalF1Score += F1Score(label);
                }

                return Labels.Count == 0 ? 0 : totalF1Score / Labels.Count;
            }
        }

/// <summary>
        /// Precision - When it predicts yes, how often is it correct
        /// </summary>
        public double Precision(string classLabel) => TruePositives(classLabel) + FalsePositives(classLabel) == 0 ? 1 : (double)TruePositives(classLabel) / (TruePositives(classLabel) + FalsePositives(classLabel));

        /// <summary>
        /// Recall - When it's actually yes, how often does it predict yes?
        /// </summary>
        public double Recall(string classLabel) => TruePositives(classLabel) + FalseNegatives(classLabel) == 0 ? 1 : (double)TruePositives(classLabel) / (TruePositives(classLabel) + FalseNegatives(classLabel));

        /// <summary>
        /// Prevalence - How often does the yes condition actually occur in our sample?
        /// </summary>
        public double Prevalence(string classLabel) => Total(classLabel) == 0 ? 0 : (TruePositives(classLabel) + FalseNegatives(classLabel)) / (double)Total(classLabel);

        /// <summary>
        /// Error Rate - Overall, how often is it wrong?
        /// </summary>
        public double ErrorRate(string classLabel) => Total(classLabel) == 0 ? 0 : (FalsePositives(classLabel) + FalseNegatives(classLabel)) / (double)Total(classLabel);

        /// <summary>
        /// True Positive Rate - When it's actually yes, how often does it predict yes?
        /// </summary>
        public double TruePositiveRate(string classLabel) => FalseNegatives(classLabel) + TruePositives(classLabel) == 0 ? 1 : (double)TruePositives(classLabel) / (FalseNegatives(classLabel) + TruePositives(classLabel));

        /// <summary>
        /// False Positive Rate - When it's actually no, how often does it predict yes?
        /// </summary>
        public double FalsePositiveRate(string classLabel) => TrueNegatives(classLabel) + FalsePositives(classLabel) == 0 ? 1 : (double)FalsePositives(classLabel) / (TrueNegatives(classLabel) + FalsePositives(classLabel));

        /// <summary>
        /// True Negative Rate - When it's actually no, how often does it predict no?
        /// </summary>
        public double TrueNegativeRate(string classLabel) => TrueNegatives(classLabel) + FalsePositives(classLabel) == 0 ? 1 : (double)TrueNegatives(classLabel) / (TrueNegatives(classLabel) + FalsePositives(classLabel));
    }
}
