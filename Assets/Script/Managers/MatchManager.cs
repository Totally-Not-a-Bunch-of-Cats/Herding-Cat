/*
 * Author: Zachary Boehm
 * Created: 08/17/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
/// <summary>
/// Manage all logic to do with a given match
/// </summary>
public class MatchManager : MonoBehaviour
{
    [Header("Board")]
    [Space]
    public Board GameBoard;
    [SerializeField] private Vector2Int BoardSize;
    private bool ActiveMatch = false;

    [Header("Gameplay Info")]
    [Space]
    [SerializeField] private int TargetRounds = 10;
    [SerializeField] private int TargetItems = 10;
    [SerializeField] private int RoundsPlayed = 0;
    [SerializeField] private int ItemsUsed = 0;


    /// <summary>
    /// Initialize the <see cref="Board"/> and all scene <see cref="GameObject"/>s for the match
    /// </summary>
    /// <param name="dimensions">The dimensions of the <see cref="Board"/></param>
    /// <returns>True if match was successfully initialized</returns>
    public bool InitMatch(LevelData currentLevel)
    {
        if (currentLevel != null && currentLevel.valid())
        {
            BoardSize = currentLevel.GetDimensions();
            TargetRounds = currentLevel.GetTargetRounds();
            TargetItems = currentLevel.GetTargetItems();

            GameBoard = new Board(BoardSize, currentLevel.GetTiles());
            RoundsPlayed = 0;
            ItemsUsed = 0;

            // TODO: Generate Grid and UI

            return true;
        }
        return false;
    }

    /// <summary>
    /// Start the match. This includes toggling flags and 
    /// </summary>
    public void StartMatch()
    {
        // Load board
        // Tell player it is strategy phase
        // Toggle flag to start match for update loop
        ActiveMatch = true;
    }

    private void Update()
    {
        if(ActiveMatch)
        {

        }
        // If match start
            // increment rounds played and update score
            // If player places item
                // Pass the item data and position on grid to board
            // Else if player ends their turn
                // Tell board to execute items and save new score
            // If game win
                // call end match on game manager and pass the total score and the needed score
    }
}