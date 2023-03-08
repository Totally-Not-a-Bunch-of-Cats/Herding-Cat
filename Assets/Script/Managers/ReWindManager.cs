using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReWindManager : MonoBehaviour
{
    [SerializeField] Board PreviousGameBoard;
    [SerializeField] int PreviousRoundsPlayed;
    [SerializeField] int PreviousItemsUsed;
    /// <summary>
    /// saves a deep copy of the board by calling a specilized constructor
    /// </summary>
    /// <param name="currentBoard"> current game board before end turn is processed</param>
    /// <param name="currentRoundsPlayed">current rounds played before the round was processed</param>
    /// <param name="currentItemsUsed">current items used before the round was processed</param>
    /// <param name="CurrentLevelTiles">current level data </param>
    public void SaveRewind(Board currentBoard, int currentRoundsPlayed, int currentItemsUsed, PosTile[] CurrentLevelTiles)
    {
        PreviousGameBoard = new Board(currentBoard, CurrentLevelTiles);
        PreviousRoundsPlayed = currentRoundsPlayed;
        PreviousItemsUsed = currentItemsUsed;
    }
    /// <summary>
    /// called when the player presses the rewind button. Changes the position of the cats to there orriginal spots both in the 
    /// game world and in the board data structure
    /// </summary>
    public void Revert()
    {
        //moves the cats objects on the board
        for (int i = 0; i < PreviousGameBoard.Cats.Count; i++)
        {
            int TempDifferenceX = GameManager.Instance._matchManager.GameBoard.Cats[i].Position.x - PreviousGameBoard.Cats[i].Position.x;
            int TempDifferenceY = GameManager.Instance._matchManager.GameBoard.Cats[i].Position.y - PreviousGameBoard.Cats[i].Position.y;
            GameManager.Instance._matchManager.GameBoard.Cats[i].Object.localPosition -= new Vector3(TempDifferenceX, TempDifferenceY, 0);
        }
        GameManager.Instance._matchManager.GameBoard = PreviousGameBoard;
        GameManager.Instance._matchManager.RoundsPlayed = PreviousRoundsPlayed;
        GameManager.Instance._matchManager.ItemsUsed = PreviousItemsUsed;
    }
}
