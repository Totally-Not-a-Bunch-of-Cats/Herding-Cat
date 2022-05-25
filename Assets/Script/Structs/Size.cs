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

    public int getWidth() { return width; }
    public int getHeight() { return height; }

    public Size(int width, int height)
    {
        this.width = width;
        this.height = height;
    }
}