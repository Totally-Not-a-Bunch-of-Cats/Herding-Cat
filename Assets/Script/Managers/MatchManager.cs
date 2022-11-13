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
    //general useful level info such as the board size and the board data type
    [Header("Board")]
    [Space]
    public Board GameBoard;
    [SerializeField] private Vector2Int BoardSize;
    private bool ActiveMatch = false;
    private bool Won = false;

    //stores the items used, rounds passed, and targets for starts gained
    [Header("Gameplay Info")]
    [Space]
    [SerializeField] private int TargetRounds = 10;
    [SerializeField] private int TargetItems = 10;
    public int RoundsPlayed = 0;
    [SerializeField] private int ItemsUsed = 0;

    [SerializeField] private Tilemap BoardTileMap;
    [SerializeField] private GameObject ItemButtonPrefab;


    


    /// <summary>
    /// Initialize the <see cref="Board"/> and all scene <see cref="GameObject"/>s for the match
    /// </summary>
    /// <param name="dimensions">The dimensions of the <see cref="Board"/></param>
    /// <returns>True if match was successfully initialized</returns>
    public bool InitMatch(LevelData currentLevel)
    {
        Debug.Log("im out");
        if (currentLevel != null && currentLevel.valid())
        {
            Debug.Log(currentLevel.name);   
            BoardSize = currentLevel.GetDimensions();
            TargetRounds = currentLevel.GetTargetRounds();
            TargetItems = currentLevel.GetTargetItems();

            GameBoard = new Board(BoardSize, currentLevel.GetTiles());
            RoundsPlayed = 0;
            ItemsUsed = 0;

            

            BoardTileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
            Debug.Log(BoardTileMap);
            // Generates grid (Odd numbered sizes will break)
            int tempx = BoardSize.x / 2;
            int tempy = BoardSize.y / 2;

            // setup background(tilemap)
            for (int x = -tempx; x < tempx; x++) {
                for (int y = -tempy; y < tempy; y++) {
                    BoardTileMap.SetTile(new Vector3Int(x,y,0), currentLevel.GetBackgroundTile());
                    Debug.Log("im making a tile stew");
                }
            }
            // place tiles(cat pens/cats/traps) associated to level
            int count = 0;
            foreach (PosTile CurPosTile in currentLevel.GetTiles())
            {

                Transform temp = Instantiate(CurPosTile.Slate.GetPrefab(), new Vector3(CurPosTile.Position.x - tempx + 0.5f, CurPosTile.Position.y - tempy + 0.5f, 5),
                    Quaternion.identity, transform).transform;
                if (CurPosTile.Slate.Is<Cat>())
                {
                    GameBoard.Cats[count].Object = temp;
                    count++;
                }
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
            ActiveMatch = true;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if(ActiveMatch)
        {

        }
        if(Won)
        {
            Debug.Log("You win");     
            // call end match on game manager and pass the total score and the needed score
        }
    
    }

    /// <summary>
    /// Finds closest cat to an item and starts the movement 
    /// </summary>
    public void EndRound()
    {
        // Adds number of placed items to the total used items
        if (GameBoard.Items.Count != 0)
        {
            ItemsUsed += GameBoard.Items.Count;
        }
        //go through all the items that were placed
        for (int i = 0; i < GameBoard.Items.Count; i++)
        {
            Item CurrentItem = GameBoard.At(GameBoard.Items[i].Position) as Item;
            //int CurCatPos = -1;
            int ClosestDistance = -1;
            Vector2Int CurDestination = Vector2Int.zero;

            List<Vector2Int> CurDestinationList = new List<Vector2Int>();
            List<int> CatListPositions = new List<int>();

            // loops through cats to find the closest one to the item to move
            for (int j = 0; j < GameBoard.Cats.Count; j++)
            {
                if (GameBoard.Cats[j] != null)
                {
                    if (CurrentItem.Radius == -1)
                    {
                        //effects all cats help
                    }
                    else
                    {

                        //find if the cat is in range and if so moves said cat
                        int deltaX = GameBoard.Cats[j].Position.x - GameBoard.Items[i].Position.x;
                        int deltaY = GameBoard.Cats[j].Position.y - GameBoard.Items[i].Position.y;

                        int Dist = System.Math.Abs(deltaX) + System.Math.Abs(deltaY);
                        // Checks if cat is closer than current cat and within radius
                        if (Dist <= ClosestDistance || ClosestDistance < 0 && Dist <= CurrentItem.Radius)
                        {
                            //CurCatPos = j;
                            if (ClosestDistance == Dist)
                            {
                                // Adds postioin of cat in list to cat distance CurDestinationList if cat distance from item is same distance
                                CatListPositions.Add(j);
                            } 
                            else
                            {
                                // Clears the lists of the farther cat information and adds the new one
                                CurDestinationList.Clear();
                                CatListPositions.Clear();
                                CatListPositions.Add(j);
                            }

                            ClosestDistance = Dist;

                            // Gets the farthest that the cat will move of item (Right)
                            if (deltaX <= CurrentItem.Radius && deltaX > 0 && deltaY == 0)
                            {

                                CurDestination = GameBoard.Cats[j].Position + new Vector2Int(CurrentItem.MoveDistance, 0);
                                CurDestinationList.Add(CurDestination);
                                //GameBoard.CheckMovement(CurrentItem.MoveDistance,
                                //    GameBoard.Cats[j].Position + new Vector2Int(CurrentItem.MoveDistance, 0), j);
                            }
                            // Gets the farthest that the cat will move of item (Left)
                            if (deltaX >= -CurrentItem.Radius && deltaX < 0 && deltaY == 0)
                            {
                                CurDestination = GameBoard.Cats[j].Position + new Vector2Int(-CurrentItem.MoveDistance, 0);
                                CurDestinationList.Add(CurDestination);
                                //GameBoard.CheckMovement(CurrentItem.MoveDistance,
                                //    GameBoard.Cats[j].Position + new Vector2Int(-CurrentItem.MoveDistance, 0), j);
                            }
                            // Gets the farthest that the cat will move of item (Up)
                            if (deltaY <= CurrentItem.Radius && deltaY > 0 && deltaX == 0)
                            {
                                CurDestination = GameBoard.Cats[j].Position + new Vector2Int(0, CurrentItem.MoveDistance);
                                CurDestinationList.Add(CurDestination);
                                //GameBoard.CheckMovement(CurrentItem.MoveDistance,
                                //    GameBoard.Cats[j].Position + new Vector2Int(0, CurrentItem.MoveDistance), j);
                            }
                            // Gets the farthest that the cat will move of item (Down)
                            if (deltaY >= -CurrentItem.Radius && deltaY < 0 && deltaX == 0)
                            {
                                CurDestination = GameBoard.Cats[j].Position + new Vector2Int(0, -CurrentItem.MoveDistance);
                                CurDestinationList.Add(CurDestination);
                                //GameBoard.CheckMovement(CurrentItem.MoveDistance,
                                //        GameBoard.Cats[j].Position + new Vector2Int(0, -CurrentItem.MoveDistance), j);
                            }
                        }
                    }
                }
                
            }
            // Checks if cat was in range
            if (ClosestDistance > -1)
            {
                // Checks movement/Moves cat of effected cats
                for(int c = 0; c < CurDestinationList.Count; c++)
                {
                    GameBoard.CheckMovement(CurrentItem.MoveDistance, CurDestinationList[c], CatListPositions[c]);
                    //GameBoard.CheckMovement(CurrentItem.MoveDistance, CurDestination, CurCatPos);
                }
                CurDestinationList.Clear();
                CatListPositions.Clear();
            }
                GameBoard.Items[i].Object.gameObject.SetActive(false);
                GameBoard.Set(GameBoard.Items[i].Position, null);
        }
        GameBoard.Items.Clear();

        int catsinPens = 0;
        for (int z = 0; z < GameBoard.CatPenLocation.Count; z++)
        {
            catsinPens += ((CatPen)GameBoard.At(GameBoard.CatPenLocation[z])).NumCatinPen;
        }
        Debug.Log(catsinPens);
        if(GameBoard.NumberofCats == catsinPens)
        {
            ActiveMatch = false;
            Won = true;
        }

    }
}