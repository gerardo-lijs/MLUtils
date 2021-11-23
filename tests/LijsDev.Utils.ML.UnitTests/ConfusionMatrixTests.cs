namespace LijsDev.Utils.ML.UnitTests;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;

[TestClass]
public class ConfusionMatrixTests
{
    [TestMethod]
    public void ConfusionMatrix_ShouldCalculateCorrectly()
    {
        // NB: Tests based in values and results found in https://www.dataschool.io/simple-guide-to-confusion-matrix-terminology/

        var confusionMatrix = new ConfusionMatrix(truePositives: 100, trueNegatives: 50, falsePositives: 10, falseNegatives: 5);

        confusionMatrix.TruePositives.Should().Be(100);
        confusionMatrix.TrueNegatives.Should().Be(50);
        confusionMatrix.FalsePositives.Should().Be(10);
        confusionMatrix.FalseNegatives.Should().Be(5);

        Math.Round(confusionMatrix.Accuracy, 2).Should().Be(0.91);
        Math.Round(confusionMatrix.F1Score, 2).Should().Be(0.93);
        Math.Round(confusionMatrix.Precision, 2).Should().Be(0.91);
        Math.Round(confusionMatrix.Recall, 2).Should().Be(0.95);
        Math.Round(confusionMatrix.Prevalence, 2).Should().Be(0.64);
        Math.Round(confusionMatrix.ErrorRate, 2).Should().Be(0.09);
        Math.Round(confusionMatrix.TruePositiveRate, 2).Should().Be(0.95);
        Math.Round(confusionMatrix.FalsePositiveRate, 2).Should().Be(0.17);
        Math.Round(confusionMatrix.TrueNegativeRate, 2).Should().Be(0.83);
    }
}
