/*
 * Author: Zachary Boehm
 * Created: 08/17/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
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
    public bool ActiveMatch = false;
    private bool CatMoving = false;
    public Vector3 BoardOffset;
    [SerializeField] public bool CatJustinCage = false;

    //stores the items used, rounds passed, and targets for starts gained
    [Header("Gameplay Info")]
    [Space]
    [SerializeField] private int TargetRounds = 10;
    [SerializeField] private int TargetItems = 10;
    [SerializeField] public int RoundsPlayed = 0;
    [SerializeField] public int ItemsUsed = 0;
    [SerializeField] public LevelNameUpdator LevNameUpdator;
    [SerializeField] private float SpeedAdjustment = 1;

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
        if (currentLevel != null && currentLevel.Valid())
        {
            BoardSize = currentLevel.GetDimensions();
            TargetRounds = currentLevel.GetTargetRounds();
            TargetItems = currentLevel.GetTargetItems();

            GameBoard = new Board(BoardSize, currentLevel.GetTiles());
            RoundsPlayed = 0;
            ItemsUsed = 0;
            CurrentLevel = currentLevel;

            GameObject HelpGUI = GameObject.Find("initial Help Text");

            BoardTileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
            GameObject.Find("OutlineSquare").transform.localScale = new Vector3(BoardSize.x, BoardSize.y, 1);
            // Generates grid (Odd numbered sizes will break)
            int tempx = BoardSize.x / 2;
            int tempy = BoardSize.y / 2;
            bool oddX = BoardSize.x % 2 != 0;
            bool oddY = BoardSize.y % 2 != 0;

            if (oddX || oddY)
            {
                if (oddX)
                {
                    BoardOffset.x = 0.5f;
                }
                if (oddY)
                {
                    BoardOffset.y = 0.5f;
                }
                BoardTileMap.transform.localPosition += BoardOffset;
            }

            // setup background(tilemap)
            for (int x = (int)(-tempx - (0.5f + BoardOffset.x)); x < tempx; x++)
            {
                for (int y = (int)(-tempy - (0.5f + BoardOffset.y)) ; y < tempy; y++)
                {
                    BoardTileMap.SetTile(new Vector3Int(x, y, 0), currentLevel.GetBackgroundTile());
                }
            }

            // place tiles(cat pens/cats/traps) associated to level
            int count = 0;
            for (int i = 0; i < currentLevel.GetTiles().Length; i++)
            {
                Vector3 pos = new Vector3(currentLevel.GetTiles()[i].Position.x - tempx + 0.5f - BoardOffset.x,
                    currentLevel.GetTiles()[i].Position.y - tempy + 0.5f - BoardOffset.y, 5);
                Transform temp = Instantiate(currentLevel.GetTiles()[i].Slate.GetPrefab(), pos, Quaternion.identity, transform).transform;
                // Faces arrow towards direction that being redirected to
                if (currentLevel.GetTiles()[i].Slate.name == "Redirection Pad")
                {
                    if (currentLevel.GetTiles()[i].Redirection == Vector2Int.down)
                    {
                        temp.localRotation *= Quaternion.Euler(0, 0, 90f);
                    }
                    else if (currentLevel.GetTiles()[i].Redirection == Vector2Int.up)
                    {
                        temp.localRotation *= Quaternion.Euler(0, 0, -90f);
                    }
                    else if (currentLevel.GetTiles()[i].Redirection == Vector2Int.right)
                    {
                        temp.localRotation *= Quaternion.Euler(0, 0, 180f);
                    }
                }
                temp.gameObject.name = currentLevel.GetTiles()[i].Slate.name + $" ({currentLevel.GetTiles()[i].Position.x}, {currentLevel.GetTiles()[i].Position.y})";
                if (currentLevel.GetTiles()[i].Slate.Is<Cat>())
                {
                    GameBoard.Cats[count].Object = temp;
                    count++;
                }
            }

            //places items 
            for (int i = 0; i < currentLevel.GetPossibleItems().Length; i++)
            {
                //Screen.height
                int buffer = 85;
                Item item = currentLevel.GetPossibleItems()[i];
                Transform EndturnButton = GameObject.Find("End Turn Button").transform;
                GameObject button = Instantiate(item.ButtonPrefab, new Vector3(0, 0, 4), Quaternion.identity, GameObject.Find("GUI").transform);
                //button.transform.localPosition = EndturnButton.localPosition;
                button.transform.localPosition = new Vector3(EndturnButton.localPosition.x, 0, 0);
                button.transform.localPosition += new Vector3(0, buffer + (145 * (i + 1)), 0);
                button.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
                button.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
                button.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance._uiManager.PlaceItem(item, button));
            }
            GameManager.Instance._uiManager.GetUI();
            GameManager.Instance._uiManager.Override = true;
            ActiveMatch = true;
            LevNameUpdator.NameUpdate();
            if (currentLevel.NewThingIntroduced == true && currentLevel.name == "1-1")
            {
                //HelpGUI.transform.GetChild(0).gameObject.SetActive(true);
            }
            if (currentLevel.NewThingIntroduced == true && currentLevel.name == "1-6")
            {
                //HelpGUI.transform.GetChild(1).gameObject.SetActive(true);
            }
            return true;
        }
        return false;
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
            if (GameBoard.Items[i] != null)
            {
                yield return new WaitWhile(() => CatMoving);
                Item CurrentItem = GameBoard.At(GameBoard.Items[i].Position) as Item;
                //int ClosestDistance = -1;
                List<CatMovementInfo> CatMoveInfo = new List<CatMovementInfo>();

                // loops through cats to find the closest one to the item to move
                for (int j = 0; j < GameBoard.Cats.Count; j++)
                {
                    if (GameBoard.Cats[j] != null && GameBoard.Cats[j].Sleeping == false)
                    {
                        //moves all cats in radius of the item
                        int deltaX = GameBoard.Cats[j].Position.x - GameBoard.Items[i].Position.x;
                        int deltaY = GameBoard.Cats[j].Position.y - GameBoard.Items[i].Position.y;
                        int Dist = System.Math.Abs(deltaX) + System.Math.Abs(deltaY);

                        if (CurrentItem.AllCatsinRadius == true)
                        {
                            //moves all cats in radius of the item
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
                            CatMoveInfo.Add(new CatMovementInfo(j));
                            Vector2Int test = DestinationList(deltaX, deltaY, CurrentItem, j);
                            if (test != new Vector2Int(-100, -100))
                            {
                                CatMoveInfo[CatMoveInfo.Count - 1].Destination = test;
                            }
                            else
                            {
                                CatMoveInfo.RemoveAt(CatMoveInfo.Count - 1);
                            }
                        }
                    }
                }
                //checks to run for loop
                if (CurrentItem.AllCatsinRadius == true)
                {
                    List<CatMovementInfo> Temps = new List<CatMovementInfo>();
                    //loops through all cats
                    for (int j = 0; j < CatMoveInfo.Count; j++)
                    {
                        int small = 100;
                        int smallIndex = 0;
                        for (int k = 0; k < CatMoveInfo.Count; k++)
                        {
                            //checks to see if the cats is actually in range  
                            if (CatMoveInfo[k] != null && CurrentItem.Radius >= CatMoveInfo[k].Distance && CatMoveInfo[k].Used == false)
                            {
                                //says there are cats with in range
                                if (j != 0)
                                {
                                    if (small >= CatMoveInfo[k].Distance && Temps[j - 1].Distance <= CatMoveInfo[k].Distance)
                                    {
                                        small = CatMoveInfo[k].Distance;
                                        smallIndex = k;
                                    }
                                }
                                else
                                {
                                    if (small >= CatMoveInfo[k].Distance)
                                    {
                                        small = CatMoveInfo[k].Distance;
                                        smallIndex = k;
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
                else //reorder list of cats effected by other type of items
                {
                    List<CatMovementInfo> Temps = new List<CatMovementInfo>();
                    //loops through all cats
                    for (int j = 0; j < CatMoveInfo.Count; j++)
                    {
                        int small = 100;
                        int smallIndex = 0;
                        for (int k = 0; k < CatMoveInfo.Count; k++)
                        {
                            //checks to see if the cats is actually in range  
                            if (CatMoveInfo[k] != null && CurrentItem.Radius >= CatMoveInfo[k].Distance && CatMoveInfo[k].Used == false)
                            {
                                //says there are cats with in range
                                //ClosestDistance = 1;
                                if (j != 0)
                                {
                                    if (small >= CatMoveInfo[k].Distance && Temps[j - 1].Distance <= CatMoveInfo[k].Distance)
                                    {
                                        small = CatMoveInfo[k].Distance;
                                        smallIndex = k;
                                    }
                                }
                                else
                                {
                                    if (small >= CatMoveInfo[k].Distance)
                                    {
                                        small = CatMoveInfo[k].Distance;
                                        smallIndex = k;
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
                if (CatMoveInfo.Count > 0)
                {
                    for (int c = 0; c < CatMoveInfo.Count; c++)
                    {
                        GameBoard.CheckMovement(CurrentItem.MoveDistance, (Vector2Int)CatMoveInfo[c].Destination, CatMoveInfo[c].Index, GameBoard.Items[i]);
                    }
                    CatMoveInfo.Clear();
                }
                //turns the item game object off and sets its position to null/empty
                GameBoard.Items[i].Object.gameObject.SetActive(false);
                if(GameBoard.At(GameBoard.Items[i].Position) != null)
                {
                    if (GameBoard.At(GameBoard.Items[i].Position).Is<Item>())
                    {
                        GameBoard.Set(GameBoard.Items[i].Position, null);
                    }
                }
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
        //makes sure all the cats are sleeping wont be bigger than 5
        for(int i = 0; i < GameBoard.Cats.Count; i++)
        {
            if(GameBoard.Cats[i] != null)
            {
                if(GameBoard.Cats[i].Sleeping == true)
                {
                    StartCoroutine(DecaySleep(i));
                }
                GameBoard.Cats[i].Sleeping = false;
            }
        }
        GameBoard.Items.Clear();
        yield return new WaitWhile(() => CatMoving);

        // Determines if level has been won
        if (GameBoard.NumberofCats == GameBoard.NumCatinPen)
        {
            ActiveMatch = false;
            CurrentLevel.CalculateStars(RoundsPlayed, ItemsUsed, GameManager.Instance.UpdateLevelData);

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
            StartCoroutine(VictoryPause());

            // Checks if wants to update leveldata info
            if (GameManager.Instance.UpdateLevelData == true)
            {
                // Updates level data info for current/next level
                GameManager.Instance.Levels.Find(level => level.name == NextLevelName).SetUnlocked(true);
            } 
            else
            {
                // Logs in console that next level would be unlocked and value that current levels star count would be set to
                Debug.Log($"Level {NextLevelName} Unlocked");
            }
        }
        GameManager.Instance._uiManager.Override = true;
        yield return null;
    }

    /// <summary>
    /// Gets the destination of a cat after movement
    /// </summary>
    /// <param name="deltaX">Distance in X between cat and item</param>
    /// <param name="deltaY">Distance in Y between cat and item</param>
    /// <param name="CurrentItem">Item that is afecting the cat</param>
    /// <param name="index">Index of the cat to move</param>
    /// <returns>Destination on board for cat, -100,-100 is default</returns>
    Vector2Int DestinationList(int deltaX, int deltaY, Item CurrentItem, int index)
    {
        // Gets the farthest that the cat will move of item (Right)
        if (deltaX <= CurrentItem.Radius && deltaX > 0 && deltaY == 0)
        {
            return GameBoard.Cats[index].Position + new Vector2Int(CurrentItem.MoveDistance, 0);
        }
        // Gets the farthest that the cat will move of item (Left)
        if (deltaX >= -CurrentItem.Radius && deltaX < 0 && deltaY == 0)
        {
            return GameBoard.Cats[index].Position + new Vector2Int(-CurrentItem.MoveDistance, 0);
        }
        // Gets the farthest that the cat will move of item (Up)
        if (deltaY <= CurrentItem.Radius && deltaY > 0 && deltaX == 0)
        {
            return GameBoard.Cats[index].Position + new Vector2Int(0, CurrentItem.MoveDistance);
        }
        // Gets the farthest that the cat will move of item (Down)
        if (deltaY >= -CurrentItem.Radius && deltaY < 0 && deltaX == 0)
        {
            return GameBoard.Cats[index].Position + new Vector2Int(0, -CurrentItem.MoveDistance);
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
    /// <returns>Destination on board for cat, -100,-100 is default</returns>
    Vector2Int DestinationAll(int deltaX, int deltaY, Item CurrentItem, int index)
    {
        // Gets the farthest that the cat will move of item (Right) is reversed for items that pull cats
        if (deltaX <= CurrentItem.Radius && deltaX > 0 && deltaY < deltaX && (-deltaY < -deltaX || -deltaY < deltaX))
        {
            return GameBoard.Cats[index].Position + new Vector2Int(CurrentItem.MoveDistance, 0);
        }
        // Gets the farthest that the cat will move of item (Left)
        if (deltaX >= -CurrentItem.Radius && deltaX < 0 && deltaY < -deltaX && -deltaY < -deltaX)
        {
            return GameBoard.Cats[index].Position + new Vector2Int(-CurrentItem.MoveDistance, 0);
        }
        // Gets the farthest that the cat will move of item (Up)
        if (deltaY <= CurrentItem.Radius && deltaY > 0)
        {
            return GameBoard.Cats[index].Position + new Vector2Int(0, CurrentItem.MoveDistance);
        }
        // Gets the farthest that the cat will move of item (Down)
        if (deltaY >= -CurrentItem.Radius && deltaY < 0)
        {
            return GameBoard.Cats[index].Position + new Vector2Int(0, -CurrentItem.MoveDistance);
        }
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
            if (GameBoard.Cats[ListPos].Position.x != FinalDestination.x)
            {
                Vector3 Goalpos = new Vector3(((Math.Abs(GameBoard.Cats[ListPos].Position.x - FinalDestination.x))), 0f, 0f);
                Vector3 TempDestination = GameBoard.Cats[ListPos].Object.localPosition + new Vector3(Direction.x * Goalpos.x, Direction.y * Goalpos.y, 0);

                StartCoroutine(MoveObject(GameBoard.Cats[ListPos].Object.localPosition, TempDestination, 0.5f, ListPos,  FinalDestination));
            }
        }
        else if (Direction.y > 0)
        {
            Vector3 Goalpos = new Vector3(0f, ((Math.Abs(GameBoard.Cats[ListPos].Position.y - FinalDestination.y))), 0f);
            Vector3 TempDestination = GameBoard.Cats[ListPos].Object.localPosition + new Vector3(Direction.x * Goalpos.x, Direction.y * Goalpos.y, 0);
            if (TempDestination != GameBoard.Cats[ListPos].Object.localPosition)
            {
                StartCoroutine(MoveObject(GameBoard.Cats[ListPos].Object.localPosition, TempDestination, .5f, ListPos, FinalDestination));
            }
        }
        else if (Direction.x < 0)
        {
            if (GameBoard.Cats[ListPos].Position.x != FinalDestination.x)
            {
                Vector3 Goalpos = new Vector3((Math.Abs(GameBoard.Cats[ListPos].Position.x - FinalDestination.x)), 0f, 0f);
                Vector3 TempDestination = GameBoard.Cats[ListPos].Object.localPosition + new Vector3(Direction.x * Goalpos.x, Direction.y * Goalpos.y, 0);

                StartCoroutine(MoveObject(GameBoard.Cats[ListPos].Object.localPosition, TempDestination, .5f, ListPos, FinalDestination));
            }
        }
        else
        {
            Vector3 Goalpos = new Vector3(0f, ((Math.Abs(GameBoard.Cats[ListPos].Position.y - FinalDestination.y))), 0f);
            Vector3 TempDestination = GameBoard.Cats[ListPos].Object.localPosition + new Vector3(Direction.x * Goalpos.x, Direction.y * Goalpos.y, 0);

            if (TempDestination != GameBoard.Cats[ListPos].Object.localPosition)
            {
                StartCoroutine(MoveObject(GameBoard.Cats[ListPos].Object.localPosition, TempDestination, .5f, ListPos, FinalDestination));
            }
        }
        //move cat in data structure all at once
        if (GameBoard.At(FinalDestination) != null)
        {
            //adds cats to the pen count when they move in
            if (GameBoard.At(FinalDestination).Is<CatPen>())
            {   
                Debug.Log("Is Pen");
                CatJustinCage = true;
                GameBoard.SecondCatList.Add(new PosObject(GameBoard.Cats[ListPos].Position, GameBoard.Cats[ListPos].Object, GameBoard.Cats[ListPos].ItemAdjObject, GameBoard.Cats[ListPos].Name, GameBoard.Cats[ListPos].Tile));
                GameBoard.Set(CatPos, null);
                GameBoard.NumCatinPen++;
            }
            if (GameBoard.At(FinalDestination).name == "Toy")
            {
                GameBoard.Set(CatPos, null);
                GameBoard.Set(FinalDestination, Cat);
            }
            if (GameBoard.At(FinalDestination).name == "Cat Tree")
            {
                GameBoard.Set(CatPos, null);
                GameBoard.SaveTile(FinalDestination, GameBoard.At(FinalDestination));
                GameBoard.Set(FinalDestination, Cat);
            }
            if (GameBoard.At(FinalDestination).name == "Bed")
            {
                GameBoard.Set(CatPos, null);
                GameBoard.SaveTile(FinalDestination, GameBoard.At(FinalDestination));
                GameBoard.Set(FinalDestination, Cat);
            }
            if (GameBoard.At(FinalDestination).name == "Cat Tube")
            {
                GameBoard.Set(CatPos, null);
                //GameBoard.SaveTile(FinalDestination, GameBoard.At(FinalDestination));
            }
            if (GameBoard.At(FinalDestination).name == "Redirection Pad")
            {
                GameBoard.Set(CatPos, null);
                GameBoard.SaveTile(FinalDestination, GameBoard.At(FinalDestination));
            }
        }
        else
        {
            //moves the cat to the new position in the board data
            GameBoard.Set(CatPos, null);
            GameBoard.Set(FinalDestination, Cat);
        }

        //loops through the saved tiles to check if they are no longer occupied by a cat 
        for(int i = 0; i < GameBoard.SavedTiles.Count; i++)
        {
            if (GameBoard.At(GameBoard.SavedTiles[i].Position) == null)
            {
                GameBoard.Set(GameBoard.SavedTiles[i].Position, GameBoard.SavedTiles[i].Slate);
                GameBoard.SavedTiles.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Lerps the cat to its target destination in world space
    /// </summary>
    /// <param name="source">Starting World postion of cat</param>
    /// <param name="target">Final World Postition of cat to be at</param>
    /// <param name="overTime">time that it takes to get to the target location</param>
    /// <param name="ListPos">Index that cat is in the cats list</param>
    /// <param name="FinalDestination">Final Destination of cat on Board</param>
    /// <returns></returns>
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
        if(GameBoard.At(FinalDestination) != null)
        {
            if (GameBoard.At(FinalDestination).Is<CatPen>())
            {
                GameBoard.Cats[ListPos] = null;
            }
            if (GameBoard.At(FinalDestination).name == "Cat Tube")
            {
                TileCatTubeMove(GameBoard.Cats[ListPos], ListPos);
            }
            if (GameBoard.At(FinalDestination).name == "Redirection Pad")
            {
                TileCatRedirection(GameBoard.Cats[ListPos], ListPos);
                Debug.Log("Redirection Done " + FinalDestination);
            }
        }
        if (GameBoard.At(FinalDestination).name != "Redirection Pad")
        {
            CatMoving = false;
        }
    }

    /// <summary>
    /// handles the movement of cats that occure becuse they are on tiles, like the redirection 
    /// </summary>
    /// <param name="cat"></param>
    /// <param name="ListPos"></param>
    public void TileCatTubeMove(PosObject cat, int ListPos)
    {
        //cycles through all of the tubes looking for the right one to move the cat tube
        for (int i = 0; i < GameBoard.Tubes.Count; i++)
        {
            //checks to see if there is room for the cat to move before movings
            if (GameBoard.Tubes[i].Position == cat.Position && GameBoard.At(GameBoard.Tubes[i].TubeDestination).name == "Cat Tube")
            {
                //moves the cat game object in world.
                Vector2Int TubeDestination = cat.Position - GameBoard.Tubes[i].TubeDestination; 
                cat.Object.localPosition = new Vector3(cat.Object.localPosition.x - TubeDestination.x,
                    cat.Object.localPosition.y - TubeDestination.y, cat.Object.localPosition.z);
                //adds the tube to the save tile list to be changed back to a tube and sets the new position of the cat
                GameBoard.SaveTile(GameBoard.Tubes[i].TubeDestination, GameBoard.At(GameBoard.Tubes[i].TubeDestination));
                GameBoard.Cats[ListPos].Position = GameBoard.Tubes[i].TubeDestination;
                GameBoard.Set(GameBoard.Tubes[i].TubeDestination, cat.Tile);
                break;
            }
            //handles if that cat cant mvoe through the tube due to a traffic jam
            if(GameBoard.Tubes[i].Position == cat.Position)
            {
                GameBoard.SaveTile(GameBoard.Tubes[i].Position, GameBoard.At(GameBoard.Tubes[i].Position));
                GameBoard.Set(GameBoard.Tubes[i].Position, cat.Tile);
                break;
            }
        }
    }

    /// <summary>
    /// handles the cat redirection pad 
    /// </summary>
    /// <param name="cat"></param>
    /// <param name="ListPos"></param>
    public void TileCatRedirection(PosObject cat, int ListPos)
    {
        GameBoard.Set(GameBoard.Cats[ListPos].Position, null);
        Vector2Int Destination;
        Vector2Int addition = Vector2Int.zero;
        for (int i = 0; i < GameBoard.RedirectionPads.Count; i++)
        {
            if (GameBoard.RedirectionPads[i].Position == cat.Position)
            {
                addition = GameBoard.RedirectionPads[i].Redirection;
                break;
            }
        }
        Destination = cat.Position + addition;
        GameBoard.Set(cat.Position, cat.Tile);
        GameBoard.CheckMovement(1, Destination, ListPos, null);
    }

    /// <summary>
    /// Waits till cats are finished moving to load Game won UI
    /// </summary>
    /// <returns></returns>
    IEnumerator VictoryPause()
    {
        yield return new WaitWhile(() => CatMoving);
        yield return new WaitForSeconds(.15f);
        GameWonUI.SetActive(true);
        ActivateStars();
    }

    /// <summary>
    /// Sets the stars on the Win UI to the won amount
    /// </summary>
    private void ActivateStars()
    {
        //get references to stars and activate them
        List<Image> Stars = new List<Image>();
        Stars.Add(GameObject.Find("Star1").GetComponent<Image>());
        Stars.Add(GameObject.Find("Star2").GetComponent<Image>());
        Stars.Add(GameObject.Find("Star3").GetComponent<Image>());

        for (int i = 0; i < CurrentLevel.StarsEarned; i++)
        {
            Stars[i].color = Color.white;
        }
    }
    public IEnumerator DecaySleep(int i)
    {
        Color FullAlpha = GameBoard.Cats[i].Object.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
        FullAlpha.a = 1;
        Color itemColor = GameBoard.Cats[i].Object.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color;
        while (GameBoard.Cats[i].Object.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color.a > .4)
        {
            yield return new WaitForSeconds(.2f);
            itemColor.a -= 0.1f;
            GameBoard.Cats[i].Object.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = itemColor;
        }
        GameBoard.Cats[i].Object.GetChild(0).GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = FullAlpha;
        GameBoard.Cats[i].Object.GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
}