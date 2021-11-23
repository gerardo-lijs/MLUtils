namespace LijsDev.Utils.ML;

/// <summary>
/// Rectangular border defined around the region.
/// </summary>
public class BoundingBox
{
    /// <summary>
    /// Left coordinate
    /// </summary>
    public double Left { get; set; }
    /// <summary>
    /// Top coordinate
    /// </summary>
    public double Top { get; set; }
    /// <summary>
    /// Box width
    /// </summary>
    public double Width { get; set; }
    /// <summary>
    /// Box height
    /// </summary>
    public double Height { get; set; }

    ///<inheritdoc/>
    public BoundingBox() { }

    ///<inheritdoc/>
    public BoundingBox(double left, double top, double width, double height)
    {
        Left = left;
        Top = top;
        Width = width;
        Height = height;
    }
}
