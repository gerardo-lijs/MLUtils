namespace LijsDev.Utils.ML.UnitTests;

using Xunit;
using FluentAssertions;

public class EvaluationMetricsTests
{
    [Fact]
    public void IntersectionOverUnion_Case1_ShouldCalculateCorrectly()
    {
        var box1 = new BoundingBox(10, 10, 10, 10);
        var box2 = new BoundingBox(10, 10, 10, 10);
        var iou = EvaluationMetrics.IntersectionOverUnion(box1, box2);

        // Same size boxes give IOU = 1
        iou.Should().Be(1);
    }

    [Fact]
    public void IntersectionOverUnion_Case2_ShouldCalculateCorrectly()
    {
        var box1 = new BoundingBox(10, 10, 10, 10);
        var box2 = new BoundingBox(15, 15, 10, 10);
        var iou = EvaluationMetrics.IntersectionOverUnion(box1, box2);

        iou.Should().Be(0.1429);
    }

    [Fact]
    public void IntersectionOverUnion_Case3_ShouldCalculateCorrectly()
    {
        var box1 = new BoundingBox(10, 10, 10, 10);
        var box2 = new BoundingBox(15, 10, 10, 10);
        var iou = EvaluationMetrics.IntersectionOverUnion(box1, box2);

        iou.Should().Be(0.3333);
    }

    [Fact]
    public void IntersectionOverUnion_Case4_ShouldCalculateCorrectly()
    {
        var box1 = new BoundingBox(10, 10, 10, 10);
        var box2 = new BoundingBox(11, 10, 10, 10);
        var iou = EvaluationMetrics.IntersectionOverUnion(box1, box2);

        iou.Should().Be(0.8182);
    }

    [Fact]
    public void IntersectionOverUnion_Case5_ShouldCalculateCorrectly()
    {
        var box1 = new BoundingBox(130, 32, 320, 420);
        var box2 = new BoundingBox(140, 42, 210, 405);
        var iou = EvaluationMetrics.IntersectionOverUnion(box1, box2);

        iou.Should().Be(0.6328);
    }

    [Fact]
    public void IntersectionOverUnion_Case6_ShouldCalculateCorrectly()
    {
        var box1 = new BoundingBox(10, 10, 10, 10);
        var box2 = new BoundingBox(11, 10, 9, 10);
        var iou = EvaluationMetrics.IntersectionOverUnion(box1, box2);

        iou.Should().Be(0.9);
    }

}
