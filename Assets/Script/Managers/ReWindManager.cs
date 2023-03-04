using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReWindManager : MonoBehaviour
{
    [SerializeField] Board PreviousGameBoard;
    [SerializeField] List<PosObject> PreviousCatsLocations;
    [SerializeField] int PreviousRoundsPlayed;
    [SerializeField] int PreviousItemsUsed;
    [SerializeField] List<Vector2Int> Position;
    [SerializeField] List<Transform> Object;
    [SerializeField] List<ItemAdjPanel> ItemAdjObject;
    [SerializeField] List<string> Name;
    string bing = "chilling";
    public void SaveRewind(Board currentBoard, List<PosObject> currentCatLoc, int currentRoundsPlayed, int currentItemsUsed)
    {
        Debug.Log("we saving");
        PreviousCatsLocations.Clear();
        PreviousGameBoard = new Board(currentBoard);
        foreach(PosObject i in currentCatLoc)
        {
            ItemAdjObject.Add(i.ItemAdjObject);
            Object.Add(i.Object);
            Position.Add(i.Position);
            Name.Add(i.Name);
        }
        DeepCopy();
        PreviousRoundsPlayed = currentRoundsPlayed;
        PreviousItemsUsed = currentItemsUsed;
        Debug.Log("we saved");
    }

    public void Revert()
    {
        Debug.Log("about to revert");
        GameManager.Instance._matchManager.GameBoard = PreviousGameBoard;
        for (int j = 0; j < PreviousCatsLocations.Count; j++)
        {
            if (PreviousCatsLocations[j].Position != GameManager.Instance._matchManager.GameBoard.Cats[j].Position)
            {
                Debug.Log("we moving a cat");
                GameManager.Instance._matchManager.GameBoard.Set(PreviousCatsLocations[j].Position, GameManager.Instance._matchManager.GameBoard.At(GameManager.Instance._matchManager.GameBoard.Cats[j].Position));
                GameManager.Instance._matchManager.GameBoard.Set(GameManager.Instance._matchManager.GameBoard.Cats[j].Position, null);
            }
            else
            {
                Debug.Log("cat no move");
            }
        }

        for (int i = 0; i < PreviousCatsLocations.Count; i++)
        {
            int TempDifferenceX = GameManager.Instance._matchManager.GameBoard.Cats[i].Position.x - PreviousCatsLocations[i].Position.x;
            int TempDifferenceY = GameManager.Instance._matchManager.GameBoard.Cats[i].Position.y - PreviousCatsLocations[i].Position.y;
            GameManager.Instance._matchManager.GameBoard.Cats[i].Object.localPosition -= new Vector3(TempDifferenceX, TempDifferenceY, 0);
        }
        GameManager.Instance._matchManager.GameBoard.Cats = PreviousCatsLocations;
        GameManager.Instance._matchManager.RoundsPlayed = PreviousRoundsPlayed;
        GameManager.Instance._matchManager.ItemsUsed = PreviousItemsUsed;
        Debug.Log("Reverted");
    }

    void DeepCopy()
    {
        Debug.Log("we deepcopping");
        for (int i = 0; i < Position.Count; i++)
        {
            PreviousCatsLocations.Add(new PosObject(Position[i], Object[i], ItemAdjObject[i], Name[i]));
            Debug.Log(PreviousCatsLocations[i].Position);
        }
        Position.Clear();
        Object.Clear();
        ItemAdjObject.Clear();
        Name.Clear();
    }
}
