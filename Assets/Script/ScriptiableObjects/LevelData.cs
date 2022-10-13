using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/New LevelData", order = 1)]
public class LevelData: ScriptableObject
{
    [SerializeField] Vector2Int Dimensions;
    [SerializeField] int TargetRounds;
    [SerializeField] int TargetItems;

    [Header("Tile Data")]
    // Tile that is set for the backgound of play area
    [SerializeField] UnityEngine.Tilemaps.Tile BackgroundTile;
    [SerializeField] PosTile[] Tiles; // cats/traps(obsticles)
    [SerializeField] Item[] PossibleItems;
    // TODO: Add Traps

    public bool valid()
    {
        int numCats = 0;
        foreach (PosTile _tile in Tiles)
        {
            if (_tile.Slate.Is<Cat>())
            {
                numCats++;
            }
        }

        return Dimensions != Vector2Int.zero
            && TargetRounds > 0
            && TargetItems > 0
            && numCats > 0
            && BackgroundTile != null;
    }

    public Vector2Int GetDimensions()
    {
        return Dimensions;
    }

    public int GetTargetRounds()
    {
        return TargetRounds;
    }

    public int GetTargetItems()
    {
        return TargetItems;
    }

    public PosTile[] GetTiles()
    {
        return Tiles;
    }

    public UnityEngine.Tilemaps.Tile GetBackgroundTile()
    {
        return BackgroundTile;
    }

    public Item[] GetPossibleItems()
    {
        return PossibleItems;
    }
}
