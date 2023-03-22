using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReWindManager : MonoBehaviour
{
    [SerializeField] Board PreviousGameBoard;
    [SerializeField] int PreviousRoundsPlayed;
    [SerializeField] int PreviousItemsUsed;
    [SerializeField] List<int> SecondCatPos;
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
        SecondCatPos.Clear();
        int j = 0;
        if(GameManager.Instance._matchManager.CatJustinCage)
        {
            for (int i = 0; i < PreviousGameBoard.Cats.Count; i++)
            {
                if (GameManager.Instance._matchManager.GameBoard.SecondCatList.Count > 0 && GameManager.Instance._matchManager.GameBoard.Cats[i] == null)
                {
                    SecondCatPos.Add(i);
                    PreviousGameBoard.Cats[i] = (new PosObject(PreviousGameBoard.CatVec2[i], GameManager.Instance._matchManager.GameBoard.SecondCatList[j].Object, 
                        GameManager.Instance._matchManager.GameBoard.SecondCatList[j].ItemAdjObject, GameManager.Instance._matchManager.GameBoard.SecondCatList[j].Name));
                    PreviousGameBoard.Set(PreviousGameBoard.CatVec2[i], GameManager.Instance._matchManager.GameBoard.At(GameManager.Instance._matchManager.GameBoard.SecondCatList[j].Position));
                    j++;
                }
            }
        }
        //moves the cats objects on the board
        for (int i = 0; i < PreviousGameBoard.Cats.Count; i++)
        {
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
            int tempy = boardsize.y / 2;
            Vector3 boardoffset = GameManager.Instance._matchManager.BoardOffset;
            Vector2Int Catposition = PreviousGameBoard.Cats[SecondCatPos[i]].Position;                                                                              
            GameManager.Instance._matchManager.GameBoard.SecondCatList[i].Object.localPosition = new Vector3(Catposition.x - tempx + 0.5f - boardoffset.x,
                Catposition.y - tempy + 0.5f - boardoffset.y, 5);
        }
        GameManager.Instance._matchManager.GameBoard = PreviousGameBoard;
        GameManager.Instance._matchManager.RoundsPlayed = PreviousRoundsPlayed;
        GameManager.Instance._matchManager.ItemsUsed = PreviousItemsUsed;
    }
}
