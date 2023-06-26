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
    [SerializeField] public Vector2Int Dimensions;
    [SerializeField] public int TargetRounds;
    [SerializeField] public int TargetItems;
    [SerializeField] public bool NewThingIntroduced = false;

    [Header("Tile Data")]
    // Tile that is set for the backgound of play area
    [SerializeField] public UnityEngine.Tilemaps.Tile BackgroundTile;
    [SerializeField] public PosTile[] Tiles; // cats/traps(obsticles)
    [SerializeField] public Item[] PossibleItems;
    [SerializeField] public int StarsEarned = 0;
    [SerializeField] bool Unlocked = false;

    /// <summary>
    /// Checks if Level Data has all valid information
    /// </summary>
    /// <returns>True if valid, False if not</returns>
    public bool Valid()
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

    /// <summary>
    /// Sets the number of stars for this level
    /// </summary>
    /// <param name="StarsEarned">Number of stars for the level</param>
    public void SetStarsEarned(int StarsEarned) 
    {
        this.StarsEarned = StarsEarned;
    }

    /// <summary>
    /// Sets if level has been unlocked
    /// </summary>
    /// <param name="NewUnlocked">If the level has been unlocked or not</param>
    public void SetUnlocked(bool NewUnlocked)
    {
        this.Unlocked = NewUnlocked;
    }

    /// <summary>
    /// Gets the value if the level has been unlocked or not
    /// </summary>
    /// <returns>If the level has been unlocked or not</returns>
    public bool GetUnlocked()
    {
        return Unlocked;
    }

    /// <summary>
    /// Calculates star count earned from level(1 for finshing, 1 for items used, 1 for rounds)
    /// </summary>
    /// <param name="roundCount">Number of rounds to complete level</param>
    /// <param name="itemCount">Number of Items to complete level</param>
    /// <param name="AdjustStarCount">If should change the level data</param>
    public void CalculateStars(int roundCount = 0, int itemCount = 0, bool AdjustStarCount = false)
    {
        int NewStarsEarned = 1;
        // checking/adding star for items used
        if (TargetItems >= itemCount)
        {
            NewStarsEarned++;
        }
        // Checking/Adding star for round count
        if (TargetRounds >= roundCount)
        {
            NewStarsEarned++;
        }

        // Checks if should be updating level data (Log in console if not)
        if (AdjustStarCount)
        {
            StarsEarned = NewStarsEarned;
        } else
        {
            Debug.Log($"Level {name}: {NewStarsEarned}");
        }
    }
}
