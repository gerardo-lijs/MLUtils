namespace LijsDev.Utils.ML.UnitTests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

[TestClass]
public class ConfusionMatrixMultiClassTests
{
    [TestMethod]
    public void ConfusionMatrixMultiClass_ShouldCalculateCorrectly()
    {
        // NB: Tests based in values and results found in https://towardsdatascience.com/confusion-matrix-for-your-multi-class-machine-learning-model-ff9aa3bf7826

        var labels = new List<string>() { "apple", "orange", "mango" };
        var confusionMatrix = new ConfusionMatrixMultiClass(labels);

        // True Positives
        confusionMatrix.SetPrediction("apple", "apple", 7);
        confusionMatrix.SetPrediction("orange", "orange", 2);
        confusionMatrix.SetPrediction("mango", "mango", 1);

        // False Positives for apple
        confusionMatrix.SetPrediction("apple", "orange", 8);
        confusionMatrix.SetPrediction("apple", "mango", 9);

        // False Positives for orange
        confusionMatrix.AddPrediction("orange", "apple");       // NB: We can also use AddPrediction to add one if we are doing a loop in our data (most likely use case)
        confusionMatrix.SetPrediction("orange", "mango", 3);

        // False Positives for mango
        confusionMatrix.SetPrediction("mango", "apple", 3);
        confusionMatrix.SetPrediction("mango", "orange", 2);

        // Test results obtained for apple
        confusionMatrix.TruePositives("apple").Should().Be(7);
        confusionMatrix.TrueNegatives("apple").Should().Be(8);
        confusionMatrix.FalsePositives("apple").Should().Be(17);
        confusionMatrix.FalseNegatives("apple").Should().Be(4);

        // Test results obtained for orange
        confusionMatrix.TruePositives("orange").Should().Be(2);
        confusionMatrix.TrueNegatives("orange").Should().Be(20);
        confusionMatrix.FalsePositives("orange").Should().Be(4);
        confusionMatrix.FalseNegatives("orange").Should().Be(10);

        // Test results obtained for mango
        confusionMatrix.TruePositives("mango").Should().Be(1);
        confusionMatrix.TrueNegatives("mango").Should().Be(18);
        confusionMatrix.FalsePositives("mango").Should().Be(5);
        confusionMatrix.FalseNegatives("mango").Should().Be(12);

        // Check metric results for apple
        Math.Round(confusionMatrix.Accuracy("apple"), 2).Should().Be(0.42);
        Math.Round(confusionMatrix.F1Score("apple"), 2).Should().Be(0.40);
        Math.Round(confusionMatrix.Precision("apple"), 2).Should().Be(0.29);
        Math.Round(confusionMatrix.Recall("apple"), 2).Should().Be(0.64);
        Math.Round(confusionMatrix.Prevalence("apple"), 2).Should().Be(0.31);
        Math.Round(confusionMatrix.ErrorRate("apple"), 2).Should().Be(0.58);
        Math.Round(confusionMatrix.TruePositiveRate("apple"), 2).Should().Be(0.64);
        Math.Round(confusionMatrix.FalsePositiveRate("apple"), 2).Should().Be(0.68);
        Math.Round(confusionMatrix.TrueNegativeRate("apple"), 2).Should().Be(0.32);

        // Check metric results for orange
        Math.Round(confusionMatrix.Accuracy("orange"), 2).Should().Be(0.61);
        Math.Round(confusionMatrix.F1Score("orange"), 2).Should().Be(0.22);
        Math.Round(confusionMatrix.Precision("orange"), 2).Should().Be(0.33);
        Math.Round(confusionMatrix.Recall("orange"), 2).Should().Be(0.17);
        Math.Round(confusionMatrix.Prevalence("orange"), 2).Should().Be(0.33);
        Math.Round(confusionMatrix.ErrorRate("orange"), 2).Should().Be(0.39);
        Math.Round(confusionMatrix.TruePositiveRate("orange"), 2).Should().Be(0.17);
        Math.Round(confusionMatrix.FalsePositiveRate("orange"), 2).Should().Be(0.17);
        Math.Round(confusionMatrix.TrueNegativeRate("orange"), 2).Should().Be(0.83);

        // Check metric results for mango
        Math.Round(confusionMatrix.Accuracy("mango"), 2).Should().Be(0.53);
        Math.Round(confusionMatrix.F1Score("mango"), 2).Should().Be(0.11);
        Math.Round(confusionMatrix.Precision("mango"), 2).Should().Be(0.17);
        Math.Round(confusionMatrix.Recall("mango"), 2).Should().Be(0.08);
        Math.Round(confusionMatrix.Prevalence("mango"), 2).Should().Be(0.36);
        Math.Round(confusionMatrix.ErrorRate("mango"), 2).Should().Be(0.47);
        Math.Round(confusionMatrix.TruePositiveRate("mango"), 2).Should().Be(0.08);
        Math.Round(confusionMatrix.FalsePositiveRate("mango"), 2).Should().Be(0.22);
        Math.Round(confusionMatrix.TrueNegativeRate("mango"), 2).Should().Be(0.78);

        // Global metrics
        Math.Round(confusionMatrix.MicroF1, 2).Should().Be(0.28);
        Math.Round(confusionMatrix.MacroF1, 2).Should().Be(0.24);
        Math.Round(confusionMatrix.WeightedF1, 2).Should().Be(0.23);
        Math.Round(confusionMatrix.WeightedAccuracy, 2).Should().Be(0.52);
    }

    [TestMethod]
    public void ConfusionMatrixMultiClass_sklearn_ShouldCalculateCorrectly()
    {
        // NB: Tests based in values and results found in https://towardsdatascience.com/confusion-matrix-for-your-multi-class-machine-learning-model-ff9aa3bf7826

        var labels = new List<string>() { "Class 1", "Class 2", "Class 3" };
        var confusionMatrix = new ConfusionMatrixMultiClass(labels);

        // True Positives
        confusionMatrix.SetPrediction("Class 1", "Class 1", 15);
        confusionMatrix.SetPrediction("Class 2", "Class 2", 17);
        confusionMatrix.SetPrediction("Class 3", "Class 3", 5);

        // False Positives for Class 1
        confusionMatrix.SetPrediction("Class 1", "Class 2", 0);
        confusionMatrix.SetPrediction("Class 1", "Class 3", 0);

        // False Positives for Class 2
        confusionMatrix.SetPrediction("Class 2", "Class 1", 0);
        confusionMatrix.SetPrediction("Class 2", "Class 3", 3);

        // False Positives for Class 3
        confusionMatrix.SetPrediction("Class 3", "Class 1", 1);
        confusionMatrix.SetPrediction("Class 3", "Class 2", 4);

        // Check metric results for Class 1
        Math.Round(confusionMatrix.Accuracy("Class 1"), 2).Should().Be(0.98);
        Math.Round(confusionMatrix.F1Score("Class 1"), 2).Should().Be(0.97);
        Math.Round(confusionMatrix.Precision("Class 1"), 2).Should().Be(1);
        Math.Round(confusionMatrix.Recall("Class 1"), 2).Should().Be(0.94);

        // Check metric results for Class 2
        Math.Round(confusionMatrix.Accuracy("Class 2"), 2).Should().Be(0.84);
        Math.Round(confusionMatrix.F1Score("Class 2"), 2).Should().Be(0.83);
        Math.Round(confusionMatrix.Precision("Class 2"), 2).Should().Be(0.85);
        Math.Round(confusionMatrix.Recall("Class 2"), 2).Should().Be(0.81);

        // Check metric results for Class 3
        Math.Round(confusionMatrix.Accuracy("Class 3"), 2).Should().Be(0.82);
        Math.Round(confusionMatrix.F1Score("Class 3"), 2).Should().Be(0.56);
        Math.Round(confusionMatrix.Precision("Class 3"), 2).Should().Be(0.50);
        Math.Round(confusionMatrix.Recall("Class 3"), 2).Should().Be(0.62);

        // Global metrics
        Math.Round(confusionMatrix.MicroF1, 2).Should().Be(0.82);
        Math.Round(confusionMatrix.MacroF1, 2).Should().Be(0.78);
        Math.Round(confusionMatrix.WeightedF1, 2).Should().Be(0.83);
        Math.Round(confusionMatrix.WeightedAccuracy, 2).Should().Be(0.89);
    }
}
