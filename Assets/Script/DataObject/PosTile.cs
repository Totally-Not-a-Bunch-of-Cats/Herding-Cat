using UnityEngine;

/// <summary>
/// Stores the position/tile of a specific tile type
/// </summary>
[System.Serializable]
public class PosTile
{
    [SerializeField] public Vector2Int Position;
    [SerializeField] public Tile Slate;
    [SerializeField] public Vector2Int TubeDestination;

    public PosTile(Vector2Int TileLocation, Tile TileType)
    {
        Position = TileLocation;
        Slate = TileType;
    }
    public PosTile(Vector2Int TileLocation, Tile TileType, Vector2Int Tube)
    {
        Position = TileLocation;
        Slate = TileType;
        TubeDestination = Tube;
    }
};
