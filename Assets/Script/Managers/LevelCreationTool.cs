using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TMPro;

[ExecuteInEditMode]
public class LevelCreationTool : MonoBehaviour
{
    [Header("New Board Info")]
    public UnityEngine.Tilemaps.Tile BackgroundTile;
    public Vector2Int BoardSize;
    public List<Item> SelectedItems;
    public int GoalItems;
    public int GoalRounds;
    public List<PosTile> Tiles;
    public string LevelName;

    [Header("UI Objects")]
    public GameObject ItemBoardButtons;
    public GameObject LevelCreateMenuObject;
    public GameObject LevelEditNameObject;
    public GameObject LevelDesignStartupObject;
    public GameObject ConfirmationWindowObject;
    public List<Toggle> ItemToggles;

    [Header("Other Info")]
    public List<UnityEngine.Tilemaps.Tile> TileImages;
    public Tile SelectedBoardTile;
    public bool Override = true;
    public bool CanPlaceBoardTile = false;
    public GameObject Board;
    Board GameBoard;
    Vector3 BoardOffset = new Vector3();
    public GameLevels GamelevelList;
    public bool Remove;
    public List<Vector2Int> TileLocations;
    public List<GameObject> ItemReferences;
    public List<Item> PossibleItems;
    public enum Mode{Edit, Create};
    public Mode CurrentMode = Mode.Create;
    public Redirection redirection = new Redirection();
    List<Vector2Int> redirections = new List<Vector2Int>() {Vector2Int.up,Vector2Int.down,Vector2Int.left,Vector2Int.right};

    //then activates buttons to begin working on the level

    private void Start()
    {
        BackgroundTile = TileImages[0];
    }

    /// <summary>
    /// takes input for sizeX
    /// </summary>
    /// <param name="X"></param>
    public void SizeXInput(string X)
    {
        if (int.Parse(X) > 0)
        {
            BoardSize.x = int.Parse(X);
        }
    }
    /// <summary>
    /// takes the input for the num of turns to beat the level
    /// </summary>
    /// <param name="turn"></param>
    public void GoalTurnInput(string turn)
    {
        if (int.Parse(turn) > 0)
        {
            GoalRounds = int.Parse(turn);
        }
    }
    /// <summary>
    /// takes the input for the num of items to beat the level
    /// </summary>
    /// <param name="items"></param>
    public void GoalItemsInput(string items)
    {
        if (int.Parse(items) > 0)
        {
            GoalItems = int.Parse(items);
        }
    }
    /// <summary>
    /// takes input for sizeY
    /// </summary>
    /// <param name="X"></param>
    public void SizeYInput(string Y)
    {
        if (int.Parse(Y) > 0)
        {
            BoardSize.y = int.Parse(Y);
        }
    }

    /// <summary>
    /// lets the desginer input the name of the level
    /// </summary>
    /// <param name="name"></param>
    public void LevelNameInput(string name)
    {
        LevelName = name;
    }

    public void TileSelect(int ListTileNum)
    {
        BackgroundTile = TileImages[ListTileNum];
    }

    /// <summary>
    /// check if a level with the name exists and pulls up if the level layout editor
    /// </summary>
    public void EditLevelLayout()
    {
        if (GamelevelList.CheckListForName(LevelName))
        {
            CurrentMode = Mode.Edit;
            //navigates to be able to edit an existing layout
            GameObject.Find("Error").GetComponent<TextMeshProUGUI>().text = "";
            LevelEditNameObject.SetActive(false);
            GetLevelData(LevelName);
            GenerateExistingBoard();
            //then turn on buttons with items
            ItemBoardButtons.SetActive(true);
        } 
        else
        {
            //error that level does not exist
            GameObject.Find("Error").GetComponent<TextMeshProUGUI>().text = $"* {LevelName}: Level Does not Exist";
        } 
    }

    /// <summary>
    /// check if a level with the name exists and pulls up if the level info editor
    /// </summary>
    public void EditLevelInfo()
    {
        if (GamelevelList.CheckListForName(LevelName))
        {
            CurrentMode = Mode.Edit;
            //navigates to be able to edit an existing layout
            GameObject.Find("Error").GetComponent<TextMeshProUGUI>().text = "";
            LevelEditNameObject.SetActive(false);
            GetLevelData(LevelName);
            LevelCreateMenuObject.SetActive(true);
            FillLevelInfoForm();
        }
        else
        {
            //error that level does not exist
            GameObject.Find("Error").GetComponent<TextMeshProUGUI>().text = $"* {LevelName}: Level Does not Exist";
        }
    }

    public void Continue()
    {
        if (LevelName != null && BoardSize.x != 0 && BoardSize.y != 0)
        {
            if (GamelevelList.CheckListForName(LevelName))
            {
                if (CurrentMode == Mode.Create)
                {
                    Debug.LogWarning("Name already Taken please change name");
                    return;
                }
                if (CurrentMode == Mode.Edit)
                {
                    // Adds the selected items to the newest values of selected items of the created level
                    SelectedItems.Clear();
                    for (int i = 0; i < ItemToggles.Count; i++)
                    {
                        if (ItemToggles[i].isOn)
                        {
                            SelectedItems.Add(PossibleItems[i]);
                        }
                    }
                    //turn off level info
                    LevelCreateMenuObject.SetActive(false);
                    //then make blank of proper size
                    GenerateExistingBoard();
                    //then turn on buttons with items
                    ItemBoardButtons.SetActive(true);
                    return;
                }
            } else {
                // Adds the selected items to the selected items of the created level
                for (int i = 0; i < ItemToggles.Count; i++)
                {
                    if (ItemToggles[i].isOn)
                    {
                        SelectedItems.Add(PossibleItems[i]);
                    }
                }
                //turn off level info
                LevelCreateMenuObject.SetActive(false);
                //then make blank of proper size
                GenerateBlankBoard();
                //then turn on buttons with items
                ItemBoardButtons.SetActive(true);
            }
        } else {
            Debug.LogWarning("Level Name or Board size invalid");
        }
    }

    /// <summary>
    /// Resets star counts to 0 and locks all levels except first one
    /// </summary>
    public void ResetLevels()
    {
        for (int i = 0; i < GamelevelList.GameLevel.Count; i++)
        {
            GamelevelList.GameLevel[i].StarsEarned = 0;
            if (i != 0)
            {
                GamelevelList.GameLevel[i].SetUnlocked(false);
            }    
        }
    }

    /// <summary>
    /// Fills the Info into the Info Setup screen
    /// </summary>
    public void FillLevelInfoForm ()
    {
        GameObject.Find("InputField for size X").GetComponent<TMP_InputField>().text = $"{BoardSize.x}";
        GameObject.Find("InputField for size Y").GetComponent<TMP_InputField>().text = $"{BoardSize.y}";
        GameObject.Find("Turn goal input feild").GetComponent<TMP_InputField>().text = $"{GoalRounds}";
        GameObject.Find("Item goal input feild").GetComponent<TMP_InputField>().text = $"{GoalItems}";
        TMP_InputField LevelNameInputObject = GameObject.Find("InputField for Name").GetComponent<TMP_InputField>();
        LevelNameInputObject.text = $"{LevelName}";
        LevelNameInputObject.interactable = false;

        for (int i = 0; i < SelectedItems.Count; i++)
        {
            for (int j = 0; j < ItemToggles.Count; j++)
            {
                if (ItemToggles[j].isOn == false && ItemToggles[j].name.Contains(SelectedItems[i].name))
                {
                    ItemToggles[j].isOn = true;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// saves the level youre working on as a real level and gives it to the game manager list to use as a real level
    /// </summary>
    public void Save()
    {
        #if UNITY_EDITOR
            LevelData Level = ScriptableObject.CreateInstance<LevelData>();
            Level.BackgroundTile = BackgroundTile;
            Level.Dimensions = BoardSize;
            Level.PossibleItems = SelectedItems.ToArray();
            Level.TargetItems = GoalItems;
            Level.TargetRounds = GoalRounds;
            Level.Tiles = Tiles.ToArray();
            AssetDatabase.CreateAsset(Level, "Assets/Script/ScriptiableObjects/Levels/" + LevelName + ".asset");
            GamelevelList.GameLevel.Add(Level);
        #endif
    }

    /// <summary>
    /// if the Level exists, it gets the data from the scriptable object
    /// </summary>
    /// <param name="LevelName">Name of the level to get info of</param>
    public void GetLevelData(string LevelName)
    {
        if (GamelevelList.CheckListForName(LevelName))
        {
            LevelData Level = GamelevelList.GetLevelOfName(LevelName);
            BackgroundTile = Level.BackgroundTile;
            BoardSize = Level.Dimensions;
            SelectedItems = new List<Item>(Level.PossibleItems);
            GoalItems = Level.TargetItems;
            GoalRounds = Level.TargetRounds;
            Tiles = new List<PosTile>(Level.Tiles);
            this.LevelName = Level.name;
        }
    }

    /// <summary>
    /// Generates a blank tilemap of the new board size
    /// </summary>
    public void GenerateBlankBoard()
    {
        GameBoard = new Board(BoardSize);
        Tilemap BoardTileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        GameObject.Find("OutlineSquare").transform.localScale = new Vector3(BoardSize.x, BoardSize.y, 1);
        // Generates grid (Odd numbered sizes will break)
        int tempx = BoardSize.x / 2;
        int tempy = BoardSize.y / 2;
        bool oddX = BoardSize.x % 2 != 0;
        bool oddY = BoardSize.y % 2 != 0;

        if (oddX)
        {
            BoardOffset.x = 0.5f;
        }
        if (oddY)
        {
            BoardOffset.y = 0.5f;
        }

        if (oddX || oddY)
        {
            BoardTileMap.transform.localPosition += BoardOffset;
        }

        // setup background(tilemap)
        for (int x = (int)(-tempx - (0.5f + BoardOffset.x)); x < tempx; x++)
        {
            for (int y = (int)(-tempy - (0.5f + BoardOffset.y)); y < tempy; y++)
            {
                BoardTileMap.SetTile(new Vector3Int(x, y, 0), BackgroundTile);
            }
        }
    }

    /// <summary>
    /// Generates a existing board
    /// </summary>
    public void GenerateExistingBoard()
    {
        GameBoard = new Board(BoardSize);
        Tilemap BoardTileMap = GameObject.Find("Tilemap").GetComponent<Tilemap>();
        GameObject.Find("OutlineSquare").transform.localScale = new Vector3(BoardSize.x, BoardSize.y, 1);
        // Generates grid (Odd numbered sizes will break)
        int tempx = BoardSize.x / 2;
        int tempy = BoardSize.y / 2;
        bool oddX = BoardSize.x % 2 != 0;
        bool oddY = BoardSize.y % 2 != 0;

        if (oddX)
        {
            BoardOffset.x = 0.5f;
        }
        if (oddY)
        {
            BoardOffset.y = 0.5f;
        }

        if (oddX || oddY)
        {
            BoardTileMap.transform.localPosition += BoardOffset;
        }

        // setup background(tilemap)
        for (int x = (int)(-tempx - (0.5f + BoardOffset.x)); x < tempx; x++)
        {
            for (int y = (int)(-tempy - (0.5f + BoardOffset.y)); y < tempy; y++)
            {
                BoardTileMap.SetTile(new Vector3Int(x, y, 0), BackgroundTile);
            }
        }

        foreach (PosTile CurPosTile in Tiles)
        {
            Vector3 pos = new Vector3(CurPosTile.Position.x - tempx + 0.5f - BoardOffset.x,
            CurPosTile.Position.y - tempy + 0.5f - BoardOffset.y, 5);
            Transform temp = Instantiate(CurPosTile.Slate.GetPrefab(), pos, Quaternion.identity, transform).transform;
            temp.gameObject.name = CurPosTile.Slate.name + $" ({CurPosTile.Position.x}, {CurPosTile.Position.y})";
        }
    }

    /// <summary>
    /// passes the select item to some code that will place it
    /// </summary>
    /// <param name="item"></param>
    public void PlaceBoardItem(Tile tile)
    {
        SelectedBoardTile = tile;
        CanPlaceBoardTile = true;
    }

    /// <summary>
    /// Turns on Delete mode, to delete an object
    /// </summary>
    public void Delete()
    {
        CanPlaceBoardTile = true;
        Remove = true;
    }

    void Update()
    {
        //checks to see if you can place an item
        if (CanPlaceBoardTile && Override)
        {
            //checks to see if the mouse button was pressed (update for mobile maybe use unity buttons)
            if (Input.GetMouseButtonDown(0))
            {
                //gets world position and translates it to a vec2int
                Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                WorldPosition.z = 3;
                WorldPosition = TileLocationSanitization(WorldPosition);
                float clickableX = BoardSize.x / 2;
                float clickableY = BoardSize.y / 2;
                if (BoardOffset.y == 0.5f)
                {
                    clickableY -= 0.5f;
                }
                if (BoardOffset.x == 0.5f)
                {
                    clickableX -= 0.5f;
                }
                Vector2Int tileLocation = new Vector2Int((int)(WorldPosition.x - 0.5 + clickableX),
                    (int)(WorldPosition.y - 0.5 + clickableY));
                if (BoardOffset.x == 0.5f)
                {
                    tileLocation.x += 1;
                }
                if (BoardOffset.y == 0.5f)
                {
                    tileLocation.y += 1;
                }
                if (tileLocation.x == 0 || tileLocation.x == BoardSize.x - 1)
                {
                    clickableX += 1;
                }
                if (tileLocation.y == 0 || tileLocation.y == BoardSize.y - 1)
                {
                    clickableY += 1;
                }
                if ((WorldPosition.x >= -clickableX && WorldPosition.x < clickableX)
                    && (WorldPosition.y >= -clickableY && WorldPosition.y < clickableY))
                {
                    if(Remove)
                    {
                        GameBoard.Set(tileLocation, null);
                        for(int i = 0; i < Tiles.Count; i++)
                        {
                            if(Tiles[i].Position == tileLocation)
                            {
                                Tiles.RemoveAt(i);
                                TileLocations.Remove(tileLocation);
                                Destroy(ItemReferences[i]);
                                ItemReferences.RemoveAt(i);
                                break;
                            }
                        }
                        //remove in game object 
                        Remove = false;
                    }
                    else
                    {
                        if (GameBoard.At(tileLocation) == null)
                        {
                            TileLocations.Add(tileLocation);
                            GameBoard.Set(tileLocation, SelectedBoardTile);
                            GameObject temp = Instantiate(SelectedBoardTile.GetPrefab(), WorldPosition, Quaternion.identity, Board.transform);
                            temp.name = SelectedBoardTile.name + $" ({tileLocation.x}, {tileLocation.y})";
                            ItemReferences.Add(temp);
                            Tiles.Add(new PosTile(tileLocation, SelectedBoardTile, redirections[(int)redirection],""));
                        }
                    }
                }
                CanPlaceBoardTile = false;
            }
        }
        else
        {
            CanPlaceBoardTile = true;
        }
    }


    /// <summary>
    /// Sanitizes the location to make the object centered in the cell
    /// </summary>
    /// <param name="Location">Location to be Sanitizationed</param>
    /// <returns>Location after being Sanitizationed</returns>
    public Vector3 TileLocationSanitization(Vector3 Location)
    {
        if (BoardOffset.x == 0.0f)
        {
            Location.x += 0.5f;
        }
        if (BoardOffset.y == 0.0f)
        {
            Location.y += 0.5f;
        }

        Location.x = Mathf.Round(Location.x);
        Location.y = Mathf.Round(Location.y);

        if (BoardOffset.x == 0.0f)
        {
            Location.x -= 0.5f;
        }
        if (BoardOffset.y == 0.0f)
        {
            Location.y -= 0.5f;
        }

        return Location;
    }
}
