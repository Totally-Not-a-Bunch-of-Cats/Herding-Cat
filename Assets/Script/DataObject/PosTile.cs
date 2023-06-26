using UnityEngine;

/// <summary>
/// Stores the position/tile of a specific tile type
/// </summary>
[System.Serializable]
public class PosTile
{
    public string name;
    [SerializeField] public Vector2Int Position;
    [SerializeField] public Tile Slate;
    [SerializeField] public Vector2Int TubeDestination;
    [SerializeField] public Vector2Int Redirection;
    string extra; // does nothing but allow us to have use the constcture we want 
    public PosTile(Vector2Int TileLocation, Tile TileType)
    {
        name = $"{TileType.name} ({TileLocation.x},{TileLocation.y})";
        Position = TileLocation;
        Slate = TileType;
        Redirection = Vector2Int.zero;
    }
    public PosTile(Vector2Int TileLocation, Tile TileType, Vector2Int Dir, string ext)
    {
        name = $"{TileType.name} ({TileLocation.x},{TileLocation.y})";
        Position = TileLocation;
        Slate = TileType;
        Redirection = Dir;
    }
    public PosTile(Vector2Int TileLocation, Tile TileType, Vector2Int Tube)
    {
        name = $"{TileType.name} ({TileLocation.x},{TileLocation.y})";
        Position = TileLocation;
        Slate = TileType;
        TubeDestination = Tube;
        Redirection = Vector2Int.zero; //only the best coding alloweed
    }
};
