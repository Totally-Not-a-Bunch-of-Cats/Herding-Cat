/** @Author Zachary Boehm, Damian Link, Aaron */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/// <summary>
/// Stores Data of Level
/// </summary>
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
    [SerializeField] int StarsEarned = 0;
    [SerializeField] bool Unlocked = false;

    /// <summary>
    /// Checks if Level Data has all valid information
    /// </summary>
    /// <returns>True if valid, False if not</returns>
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

    /// <summary>
    /// Gets the dimensions of the <see cref="Board"/> from the Level
    /// </summary>
    /// <returns>Dimensions of the <see cref="Board"/> in the level</returns>
    public Vector2Int GetDimensions()
    {
        return Dimensions;
    }

    /// <summary>
    /// Gets the target number of Rounds to get all cats in the pen
    /// </summary>
    /// <returns>Number of rounds that is the goal to get all cats in the pen</returns>
    public int GetTargetRounds()
    {
        return TargetRounds;
    }

    /// <summary>
    /// Gets the target number of items to get all cats in the pen
    /// </summary>
    /// <returns>Number of items that is the goal to get all cats in the pen</returns>
    public int GetTargetItems()
    {
        return TargetItems;
    }

    /// <summary>
    /// Gets the Tiles(Cats, traps, pens) within the level
    /// </summary>
    /// <returns>Tiles(Cats, traps, pens) within the level</returns>
    public PosTile[] GetTiles()
    {
        return Tiles;
    }

    /// <summary>
    /// Gets the Image of the Tile that will be placed as the default of the tiles
    /// </summary>
    /// <returns>The tile that is default for the board cell</returns>
    public UnityEngine.Tilemaps.Tile GetBackgroundTile()
    {
        return BackgroundTile;
    }

    /// <summary>
    /// Gets all the possible items that can be used within the level
    /// </summary>
    /// <returns>Array of possible items within the level</returns>
    public Item[] GetPossibleItems()
    {
        return PossibleItems;
    }
}
