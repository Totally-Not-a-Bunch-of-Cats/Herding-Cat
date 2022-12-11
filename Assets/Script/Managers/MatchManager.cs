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
using System;

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
    [SerializeField] public Vector2Int BoardSize;
    private bool ActiveMatch = false;
    private bool Won = false;
    private bool CatMoving = false;

    //stores the items used, rounds passed, and targets for starts gained
    [Header("Gameplay Info")]
    [Space]
    [SerializeField] private int TargetRounds = 10;
    [SerializeField] private int TargetItems = 10;
    public int RoundsPlayed = 0;
    [SerializeField] public int ItemsUsed = 0;

    [SerializeField] public Tilemap BoardTileMap;
    [SerializeField] private GameObject ItemButtonPrefab;
    [SerializeField] public LevelData CurrentLevel;
    [SerializeField] public GameObject GameWonUI;

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
            CurrentLevel = currentLevel;


            BoardTileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
            GameObject.Find("OutlineSquare").transform.localScale = new Vector3(BoardSize.x, BoardSize.y, 1);
            // Generates grid (Odd numbered sizes will break)
            int tempx = BoardSize.x / 2;
            int tempy = BoardSize.y / 2;

            // setup background(tilemap)
            for (int x = -tempx; x < tempx; x++) {
                for (int y = -tempy; y < tempy; y++) {
                    BoardTileMap.SetTile(new Vector3Int(x,y,0), currentLevel.GetBackgroundTile());
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
                float ScreenScale = (float)Screen.width / 1000;
                Transform EndturnButton = GameObject.Find("End Turn Button").transform;
                GameObject button = Instantiate(item.ButtonPrefab, new Vector3(0, EndturnButton.position.y + 2f + (2f * i), 4) ,Quaternion.identity, GameObject.Find("GUI").transform);
                button.transform.localPosition += new Vector3(EndturnButton.localPosition.x, 0, 0);
                button.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance._uiManager.PlaceItem(item));
                GameManager.Instance._screenResizeManager.RescaleItem(button);
            }
            GameManager.Instance._uiManager.GetUI();
            ActiveMatch = true;
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (Won)
        {
            GameWonUI.SetActive(true);
            ActivateStars();
        }
    }

    void ActivateStars()
    {
        //get references to stars and activate them
        List<Image> Stars = new List<Image>();
        Stars.Add(GameObject.Find("Star1").GetComponent<Image>());
        Stars.Add(GameObject.Find("Star2").GetComponent<Image>());
        Stars.Add(GameObject.Find("Star3").GetComponent<Image>());

        for(int i = 0; i < CurrentLevel.StarsEarned; i++)
        {
            Stars[i].color = Color.white;
        }
        //GameObject.Find("Star3").GetComponent<Image>().color = Color.white;
    }

    /// <summary>
    /// Finds closest cat to an item and starts the movement 
    /// </summary>
    public IEnumerator EndRound()
    {
        // Adds number of placed items to the total used items
        if (GameBoard.Items.Count != 0)
        {
            ItemsUsed += GameBoard.Items.Count;
        }
        //go through all the items that were placed
        for (int i = 0; i < GameBoard.Items.Count; i++)
        {
            //makes sure the item slot isnt null, we make it null to delete it, unity bad
            if(GameBoard.Items[i] != null)
            {
                yield return new WaitWhile(() => CatMoving);
                Item CurrentItem = GameBoard.At(GameBoard.Items[i].Position) as Item;
                int ClosestDistance = -1;
                Vector2Int CurDestination = Vector2Int.zero;
                List<CatMovementInfo> CatMoveInfo = new List<CatMovementInfo>();

                List<Vector2Int> CurDestinationList = new List<Vector2Int>();
                List<int> CatListPositions = new List<int>();

                // loops through cats to find the closest one to the item to move
                for (int j = 0; j < GameBoard.Cats.Count; j++)
                {
                    if (GameBoard.Cats[j] != null)
                    {
                        if (CurrentItem.AllCatsinRadius == true)
                        {
                            //moves all cats in radius of the item
                            int deltaX = GameBoard.Cats[j].Position.x - GameBoard.Items[i].Position.x;
                            int deltaY = GameBoard.Cats[j].Position.y - GameBoard.Items[i].Position.y;

                            int Dist = System.Math.Abs(deltaX) + System.Math.Abs(deltaY);
                            //adds distance to the list 
                            CatMoveInfo.Add(new CatMovementInfo(j, Dist));
                            Vector2Int test = DestinationAll(deltaX, deltaY, CurrentItem, j);
                            if (test != new Vector2Int(-100, -100))
                            {
                                CatMoveInfo[CatMoveInfo.Count-1].Destination = test;
                            }
                        }
                        else
                        {
                            //find if the cat is in range and if so moves said cat
                            int deltaX = GameBoard.Cats[j].Position.x - GameBoard.Items[i].Position.x;
                            int deltaY = GameBoard.Cats[j].Position.y - GameBoard.Items[i].Position.y;

                            int Dist = System.Math.Abs(deltaX) + System.Math.Abs(deltaY);
                            // Checks if cat is closer than current cat and within radius
                            if (Dist <= ClosestDistance && (deltaY == 0 || deltaX == 0) || ClosestDistance < 0 && Dist <= CurrentItem.Radius
                                && (deltaY == 0 || deltaX == 0))
                            {
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

                                // Gets the farthest that the cat will move of item (Right) is revered for items that pull cats
                                if (deltaX <= CurrentItem.Radius && deltaX > 0 && deltaY == 0)
                                {
                                    CurDestination = GameBoard.Cats[j].Position + new Vector2Int(CurrentItem.MoveDistance, 0);
                                    CurDestinationList.Add(CurDestination);
                                }
                                // Gets the farthest that the cat will move of item (Left)
                                if (deltaX >= -CurrentItem.Radius && deltaX < 0 && deltaY == 0)
                                {
                                    CurDestination = GameBoard.Cats[j].Position + new Vector2Int(-CurrentItem.MoveDistance, 0);
                                    CurDestinationList.Add(CurDestination);
                                }
                                // Gets the farthest that the cat will move of item (Up)
                                if (deltaY <= CurrentItem.Radius && deltaY > 0 && deltaX == 0)
                                {
                                    CurDestination = GameBoard.Cats[j].Position + new Vector2Int(0, CurrentItem.MoveDistance);
                                    CurDestinationList.Add(CurDestination);
                                }
                                // Gets the farthest that the cat will move of item (Down)
                                if (deltaY >= -CurrentItem.Radius && deltaY < 0 && deltaX == 0)
                                {
                                    CurDestination = GameBoard.Cats[j].Position + new Vector2Int(0, -CurrentItem.MoveDistance);
                                    CurDestinationList.Add(CurDestination);
                                }
                            }
                        }
                    }
                }
                //checks to run for loop
                if (CurrentItem.AllCatsinRadius == true)
                {
                    List<CatMovementInfo> Temps = new List<CatMovementInfo>();
                    //loops through all caps
                    for (int zz = 0; zz < CatMoveInfo.Count; zz++)
                    {
                        int small = 100;
                        int smallIndex = 0;
                        for (int y = 0; y < CatMoveInfo.Count; y++)
                        {
                            //checks to see if the cats is actually in range  
                            if (CatMoveInfo[y] != null && CurrentItem.Radius >= CatMoveInfo[y].Distance && CatMoveInfo[y].Used == false)
                            {
                                //says there are cats with in range
                                ClosestDistance = 1;
                                if (zz != 0)
                                {
                                    if (small >= CatMoveInfo[y].Distance && Temps[zz - 1].Distance <= CatMoveInfo[y].Distance)
                                    {
                                        small = CatMoveInfo[y].Distance;
                                        smallIndex = y;
                                    }
                                }
                                else
                                {
                                    if (small >= CatMoveInfo[y].Distance)
                                    {
                                        small = CatMoveInfo[y].Distance;
                                        smallIndex = y;
                                    }
                                }
                            }
                        }
                        if (CurrentItem.Radius < small)
                        {
                            break;
                        }
                        CatMoveInfo[smallIndex].Used = true;
                        Temps.Add(CatMoveInfo[smallIndex]);
                    }
                    CatMoveInfo = Temps;
                }
                // Checks to see if a cat is actually in range
                if (ClosestDistance > -1)
                {
                    if (!CurrentItem.AllCatsinRadius)
                    {
                        for (int c = 0; c < CurDestinationList.Count; c++)
                        {
                            GameBoard.CheckMovement(CurrentItem.MoveDistance, CurDestinationList[c], CatListPositions[c]);
                        }
                        CurDestinationList.Clear();
                        CatListPositions.Clear();
                    }
                    else
                    {
                        // Checks movement/Moves cat of effected cats
                        for (int c = 0; c < CatMoveInfo.Count; c++)
                        {
                            GameBoard.CheckMovement(CurrentItem.MoveDistance, (Vector2Int)CatMoveInfo[c].Destination, CatMoveInfo[c].Index);
                        }
                        CatMoveInfo.Clear();
                    }
                }
                //turns the item game object off and sets its position to null/empty
                GameBoard.Items[i].Object.gameObject.SetActive(false);
                GameBoard.Set(GameBoard.Items[i].Position, null);
            }
        }
        
        // Removes entries in item adjust window
        for (int i = 0; i < GameBoard.Items.Count; i++)
        {
            if (GameBoard.Items[i] != null)
            {
                Destroy(GameBoard.Items[i].ItemAdjObject.gameObject);
            }
        }
        GameBoard.Items.Clear();
        // Determines if level has been won
        if (GameBoard.NumberofCats == GameBoard.NumCatinPen)
        {
            ActiveMatch = false;
            Won = true;
            // Calculates star count earned from level(1 for finshing, 1 for items used, 1 for rounds)
            int StarCount = 1;
            // checking/adding star for items used
            if(TargetItems >= ItemsUsed)
            {
                StarCount++;
            }
            // Checking/Adding star for round count
            if (TargetRounds >= RoundsPlayed)
            {
                StarCount++;
            }


            // Finds next level name 
            string[] LevelNameParts = CurrentLevel.name.Split('-');
            string NextLevelName = LevelNameParts[0] + "-";
            try
            {
                NextLevelName += int.Parse(LevelNameParts[1]) + 1; 
            }
            catch (FormatException)
            {
                Debug.LogError($"Unable to parse '{LevelNameParts[1]}'");
            }
            if (!GameManager.Instance.Levels.Exists(level => level.name == NextLevelName))
            {
                NextLevelName = "";
                try
                {
                    NextLevelName += int.Parse(LevelNameParts[0]) + 1;
                }
                catch (FormatException)
                {
                    Debug.LogError($"Unable to parse '{LevelNameParts[1]}'");
                }
                NextLevelName = NextLevelName + "-" + LevelNameParts[1];
                if (!GameManager.Instance.Levels.Exists(level => level.name == NextLevelName))
                {
                    Debug.LogWarning("No Next Level");
                }
            }
            // Checks if wants to update leveldata info
            if (GameManager.Instance.UpdateLevelData == true)
            {
                // Updates level data info for current/next level
                CurrentLevel.SetStarsEarned(StarCount);
                GameManager.Instance.Levels.Find(level => level.name == NextLevelName).SetUnlocked(true);
            } 
            else
            {
                // Logs in console that next level would be unlocked and value that current levels star count would be set to
                Debug.Log($"Level {NextLevelName} Unlocked");
                Debug.Log($"Level {CurrentLevel.name}: {StarCount}");
            }
        }
        yield return null;
    }


    Vector2Int DestinationList(int deltaX, int deltaY, Item CurrentItem, int index)
    {
        Vector2Int CurDestination;
        // Gets the farthest that the cat will move of item (Right)
        if (deltaX <= CurrentItem.Radius && deltaX > 0 && deltaY == 0)
        {
            CurDestination = GameBoard.Cats[index].Position + new Vector2Int(CurrentItem.MoveDistance, 0);
            return CurDestination;
        }
        // Gets the farthest that the cat will move of item (Left)
        if (deltaX >= -CurrentItem.Radius && deltaX < 0 && deltaY == 0)
        {
            CurDestination = GameBoard.Cats[index].Position + new Vector2Int(-CurrentItem.MoveDistance, 0);
            return CurDestination;
        }
        // Gets the farthest that the cat will move of item (Up)
        if (deltaY <= CurrentItem.Radius && deltaY > 0 && deltaX == 0)
        {
            CurDestination = GameBoard.Cats[index].Position + new Vector2Int(0, CurrentItem.MoveDistance);
            return CurDestination;
        }
        // Gets the farthest that the cat will move of item (Down)
        if (deltaY >= -CurrentItem.Radius && deltaY < 0 && deltaX == 0)
        {
            CurDestination = GameBoard.Cats[index].Position + new Vector2Int(0, -CurrentItem.MoveDistance);
            return CurDestination;
        }
        return new Vector2Int(-100, -100);
    }

    /// <summary>
    /// Gets the destination of a cat after movement
    /// </summary>
    /// <param name="deltaX">Distance in X between cat and item</param>
    /// <param name="deltaY">Distance in Y between cat and item</param>
    /// <param name="CurrentItem">Item that is afecting the cat</param>
    /// <param name="index">Index of the cat to move</param>
    /// <returns></returns>
    Vector2Int DestinationAll(int deltaX, int deltaY, Item CurrentItem, int index)
    {
        Vector2Int CurDestination;
        // Gets the farthest that the cat will move of item (Right) is reversed for items that pull cats
        if (deltaX <= CurrentItem.Radius && deltaX > 0 && deltaY < deltaX && (-deltaY < -deltaX || -deltaY < deltaX))
        {                                            
            CurDestination = GameBoard.Cats[index].Position + new Vector2Int(CurrentItem.MoveDistance, 0);
            return CurDestination;
        }
        // Gets the farthest that the cat will move of item (Left)
        if (deltaX >= -CurrentItem.Radius && deltaX < 0 && deltaY < -deltaX && -deltaY < -deltaX)
        {                                                  
            CurDestination = GameBoard.Cats[index].Position + new Vector2Int(-CurrentItem.MoveDistance, 0);
            return CurDestination;
        }
        // Gets the farthest that the cat will move of item (Up)
        if (deltaY <= CurrentItem.Radius && deltaY > 0)
        {
            CurDestination = GameBoard.Cats[index].Position + new Vector2Int(0, CurrentItem.MoveDistance);
            return CurDestination;
        }
        // Gets the farthest that the cat will move of item (Down)
        if (deltaY >= -CurrentItem.Radius && deltaY < 0)
        {
            CurDestination = GameBoard.Cats[index].Position + new Vector2Int(0, -CurrentItem.MoveDistance);
            return CurDestination;
        }
        Debug.Log("fucked up");
        return new Vector2Int(-100, -100);
    }
    /// <summary>
    /// Moves the cat object to the visualy in cell on board
    /// </summary>
    /// <param name="Direction">Unit vector of direction that cat is moving</param>
    /// <param name="Cat">tile of Cat the is being moved</param>
    /// <param name="FinalDestination">End location on board for the cat</param>
    /// <param name="ListPos">Location in list that cat is stored</param>
    public void MoveCat(Vector2Int Direction, Tile Cat, Vector2Int FinalDestination, int ListPos)
    {
        Vector2Int CatPos = GameBoard.Cats[ListPos].Position;
        //moves the cat the correct the direction
        if (Direction.x > 0)
        {                                                       
            Vector3 Goalpos = new Vector3(((Math.Abs(GameBoard.Cats[ListPos].Position.x - FinalDestination.x))), 0f, 0f);

            Vector3 TempDestination = GameBoard.Cats[ListPos].Object.localPosition + new Vector3(Direction.x * Goalpos.x, Direction.y * Goalpos.y, 0);

            StartCoroutine(MoveObject(GameBoard.Cats[ListPos].Object.localPosition, TempDestination,  .5f,  ListPos,  FinalDestination));
        }
        else if (Direction.y > 0)
        {
            Vector3 Goalpos = new Vector3(0f, ((Math.Abs(GameBoard.Cats[ListPos].Position.y - FinalDestination.y))), 0f);

            Vector3 TempDestination = GameBoard.Cats[ListPos].Object.localPosition + new Vector3(Direction.x * Goalpos.x, Direction.y * Goalpos.y, 0);

            StartCoroutine(MoveObject(GameBoard.Cats[ListPos].Object.localPosition, TempDestination, .5f, ListPos, FinalDestination));
        }
        else if (Direction.x < 0)
        {
            Vector3 Goalpos = new Vector3(((Math.Abs(GameBoard.Cats[ListPos].Position.x - FinalDestination.x))), 0f, 0f);

            Vector3 TempDestination = GameBoard.Cats[ListPos].Object.localPosition + new Vector3(Direction.x * Goalpos.x, Direction.y * Goalpos.y, 0);

            StartCoroutine(MoveObject(GameBoard.Cats[ListPos].Object.localPosition, TempDestination, .5f, ListPos, FinalDestination));
        }
        else
        {
            Vector3 Goalpos = new Vector3(0f, ((Math.Abs(GameBoard.Cats[ListPos].Position.y - FinalDestination.y))), 0f);

            Vector3 TempDestination = GameBoard.Cats[ListPos].Object.localPosition + new Vector3(Direction.x * Goalpos.x, Direction.y * Goalpos.y, 0);

            StartCoroutine(MoveObject(GameBoard.Cats[ListPos].Object.localPosition, TempDestination, .5f, ListPos, FinalDestination));

            //for (int i = CatPos.y; i > FinalDestination.y; i--)
            //{
            //    GameBoard.Cats[ListPos].Object.localPosition += new Vector3(Direction.x, Direction.y, 0);
            //}
        }
        //move cat in data structure all at once
        if (GameBoard.At(FinalDestination) != null)
        {
            //adds cats to the pen count when they move in
            if (GameBoard.At(FinalDestination).Is<CatPen>())
            {
                GameBoard.Set(CatPos, null);
                GameBoard.NumCatinPen++;
            }
        }
        else
        {
            //moves the cat to the new position in the board data
            GameBoard.Set(CatPos, null);
            GameBoard.Set(FinalDestination, Cat);
        }
    }
    IEnumerator MoveObject(Vector3 source, Vector3 target, float overTime, int ListPos, Vector2Int FinalDestination)
    {
        CatMoving = true;
        float startTime = Time.time;
        while (Time.time < startTime + overTime)
        {
            GameBoard.Cats[ListPos].Object.localPosition = Vector3.Lerp(source, target, (Time.time - startTime) / overTime);

            yield return null;
        }
        GameBoard.Cats[ListPos].Object.localPosition = target;
        if(GameBoard.At(FinalDestination).Is<CatPen>())
        {
            GameBoard.Cats[ListPos] = null;
        }
        CatMoving = false;
    }
 }