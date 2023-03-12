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
            Debug.Log(PreviousGameBoard.Cats[i]);
            Debug.Log(GameManager.Instance._matchManager.GameBoard.Cats[i]);
            if (GameManager.Instance._matchManager.GameBoard.Cats[i] != null && PreviousGameBoard.Cats[i] != null)
            {
                int TempDifferenceX = GameManager.Instance._matchManager.GameBoard.Cats[i].Position.x - PreviousGameBoard.Cats[i].Position.x;
                int TempDifferenceY = GameManager.Instance._matchManager.GameBoard.Cats[i].Position.y - PreviousGameBoard.Cats[i].Position.y;
                GameManager.Instance._matchManager.GameBoard.Cats[i].Object.localPosition -= new Vector3(TempDifferenceX, TempDifferenceY, 0);
            }
        }
        for(int i = 0; i < GameManager.Instance._matchManager.GameBoard.SecondCatList.Count; i++)
        {
            Vector2Int boardsize = GameManager.Instance._matchManager.BoardSize;
            int tempx = boardsize.x / 2;
            int tempy = boardsize.x / 2;
            Vector3 boardoffset = GameManager.Instance._matchManager.BoardOffset;
            Debug.Log(PreviousGameBoard.SecondCatPos);
            Debug.Log(PreviousGameBoard.SecondCatPos[0]);
            Vector2Int Catposition = PreviousGameBoard.Cats[PreviousGameBoard.SecondCatPos[i]].Position;
            GameManager.Instance._matchManager.GameBoard.SecondCatList[i].Object.localPosition = new Vector3(Catposition.x - tempx + 0.5f - boardoffset.x, Catposition.y - tempy + 0.5f - boardoffset.y, 5);
        }
        GameManager.Instance._matchManager.GameBoard = PreviousGameBoard;
        GameManager.Instance._matchManager.RoundsPlayed = PreviousRoundsPlayed;
        GameManager.Instance._matchManager.ItemsUsed = PreviousItemsUsed;
    }
}
