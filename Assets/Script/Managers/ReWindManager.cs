using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReWindManager : MonoBehaviour
{
    [SerializeField] Board PreviousGameBoard;
    [SerializeField] List<PosObject> PreviousCatsLocations;
    [SerializeField] int PreviousRoundsPlayed;
    [SerializeField] int PreviousItemsUsed;
    public void Start()
    {
        
    }
    public void SaveRewind(Board currentBoard, List<PosObject> currentCatLoc, int currentRoundsPlayed, int currentItemsUsed)
    {
        Debug.Log("we saving");
        PreviousGameBoard = new Board(currentBoard);
        PreviousCatsLocations = currentCatLoc;
        PreviousRoundsPlayed = currentRoundsPlayed;
        PreviousItemsUsed = currentItemsUsed;
        Debug.Log("we saved");

    }

    public void Revert()
    {
        Debug.Log("about to revert");
        GameManager.Instance._matchManager.GameBoard = PreviousGameBoard;
        GameManager.Instance._matchManager.GameBoard.Cats = PreviousCatsLocations;
        GameManager.Instance._matchManager.RoundsPlayed = PreviousRoundsPlayed;
        GameManager.Instance._matchManager.ItemsUsed = PreviousItemsUsed;
        Debug.Log("Reverted");
    }
}
