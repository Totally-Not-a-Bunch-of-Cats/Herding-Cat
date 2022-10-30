/*
 * Author: Zachary Boehm
 * Created: 08/17/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using UnityEngine.UI;

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

    [SerializeField] private Tilemap BoardTileMap;
    [SerializeField] private GameObject ItemButtonPrefab;
    [SerializeField] public List<Vector2Int> ItemLocations;



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

            BoardTileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
            // TODO: Generate Grid and UI
            int tempx = BoardSize.x / 2;  //odd numbers will break
            int tempy = BoardSize.y / 2;

            // setup background(tilemap)
            for (int x = -tempx; x < tempx; x++) {
                for (int y = -tempy; y < tempy; y++) {
                    BoardTileMap.SetTile(new Vector3Int(x,y,0), currentLevel.GetBackgroundTile());
                }
            }
            // place tiles associated to level
            foreach (PosTile CurPosTile in currentLevel.GetTiles())
            {
                CurPosTile.Slate.TileObject = Instantiate(CurPosTile.Slate.GetPrefab(), new Vector3(CurPosTile.Position.x - tempx + 0.5f, CurPosTile.Position.y - tempy + 0.5f, 5), 
                    Quaternion.identity, transform).transform;
            }
            //places items 
            for (int i = 0; i < currentLevel.GetPossibleItems().Length; i++)
            {
                Item item = currentLevel.GetPossibleItems()[i];
                GameObject button = Instantiate(ItemButtonPrefab, new Vector3(10, -5, 4) ,Quaternion.identity, GameObject.Find("GUI").transform);
                button.GetComponentInChildren<TextMeshProUGUI>().SetText(item.name);
                button.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance._uiManager.PlaceItem(item));
            }
            GameManager.Instance._uiManager.GetUI();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Start the match. This includes toggling flags and 
    /// </summary>
    public void StartMatch()
    {
        // Toggle flag to start match for update loop
        ActiveMatch = true;
    }

    private void Update()
    {
        if(ActiveMatch)
        {
            //end turn function triggures here
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

    public void EndRound()
    {
        if (ItemLocations.Count != 0)
        {
            ItemsUsed += ItemLocations.Count;
        }
        //go through all the items
        for (int i = 0; i < ItemLocations.Count; i++)
        {
            Item CurrentItem = GameBoard.At(ItemLocations[i]) as Item;
            for (int j = 0; j < GameBoard.Cats.Count; j++)
            {
                if (CurrentItem.Radius == -1)
                {
                    //effects all cats 
                }
                else
                {
                    Debug.Log(CurrentItem.name);
                    //find if the cat is in range and if so moves said cat
                    int deltaX = GameBoard.Cats[j].x - ItemLocations[i].x;
                    int deltaY = GameBoard.Cats[j].y - ItemLocations[i].y;
                    //if (GameBoard.Cats[j].x - CurrentItem.Radius >= ItemLocations[i].x)
                    if (deltaX <= CurrentItem.Radius && deltaX > 0 && deltaY == 0)
                    {
                        GameBoard.CheckMovement(GameBoard.Cats[j], CurrentItem.MoveDistance,
                            GameBoard.Cats[j] += new Vector2Int(CurrentItem.MoveDistance, 0));
                    }
                    //if(GameBoard.Cats[j].x + CurrentItem.Radius <= ItemLocations[i].x)
                    if (deltaX >= -CurrentItem.Radius && deltaX < 0 && deltaY == 0)
                    {
                        GameBoard.CheckMovement(GameBoard.Cats[j], CurrentItem.MoveDistance,
                            GameBoard.Cats[j] += new Vector2Int(-CurrentItem.MoveDistance, 0));
                    }
                    //if(GameBoard.Cats[j].y - CurrentItem.Radius >= ItemLocations[i].y)
                    if (deltaY <= CurrentItem.Radius && deltaY > 0 && deltaX == 0)
                    {
                        GameBoard.CheckMovement(GameBoard.Cats[j], CurrentItem.MoveDistance,
                            GameBoard.Cats[j] += new Vector2Int(0, CurrentItem.MoveDistance));
                    }
                    //if(GameBoard.Cats[j].y + CurrentItem.Radius <= ItemLocations[i].y)
                    if (deltaY >= -CurrentItem.Radius && deltaY < 0 && deltaX == 0)
                    {
                        GameBoard.CheckMovement(GameBoard.Cats[j], CurrentItem.MoveDistance,
                                GameBoard.Cats[j] += new Vector2Int(0, -CurrentItem.MoveDistance));
                    }
                }
            }

        }
        Debug.Log(ItemLocations.Count);
        for (int k = 0; k < ItemLocations.Count; k++)
        {
            GameBoard.At(ItemLocations[k]).TileObject.gameObject.SetActive(false);
            GameBoard.Set(ItemLocations[k], null);
        }
        ItemLocations.Clear();
    }
}