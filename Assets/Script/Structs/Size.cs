/// <summary>
/// Struct that holds the size information of a tile object. Sizing is grid based.
/// 
/// <list type="bullet">
///     <item>width - The width of the tile</item>
///     <item>height - The height of the tile</item>
/// </list>
/// </summary>
public struct Size
{
    private int width;
    private int height;

    /// <summary>
    /// Gets Width of stored in size
    /// </summary>
    /// <returns>Width stored in size</returns>
    public int getWidth() { return width; }

    /// <summary>
    /// Gets the height stored in size
    /// </summary>
    /// <returns>height stored in size</returns>
    public int getHeight() { return height; }

    /// <summary>
    /// Creates a new Size object
    /// </summary>
    /// <param name="width">New Width of size</param>
    /// <param name="height">New Height of size</param>
    public Size(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
}