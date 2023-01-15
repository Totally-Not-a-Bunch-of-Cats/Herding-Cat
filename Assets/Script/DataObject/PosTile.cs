using UnityEngine;

/// <summary>
/// Stores the position/tile of a specific tile type
/// </summary>
[System.Serializable]
public class PosTile
{
    [SerializeField] public Vector2Int Position;
    [SerializeField] public Tile Slate;

    public PosTile(Vector2Int TileLocation, Tile TileType)
    {
        Position = TileLocation;
        Slate = TileType;
    }
    };
