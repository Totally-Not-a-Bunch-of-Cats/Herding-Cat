using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class LevelCreationTool : MonoBehaviour
{
    public Vector2Int BoardSize;
    public string LevelName;
    public UnityEngine.Tilemaps.Tile BackgroundTile;
    public List<UnityEngine.Tilemaps.Tile> TileImages;
    public GameObject ItemBoardButtons;
    public Tile SelectedBoardTile;
    public bool Override = true;
    public bool CanPlaceBoardTile = false;
    public GameObject Board;
    Board GameBoard;
    public List<PosObject> Tiles;
    Vector3 BoardOffset = new Vector3();


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

    public void Continue()
    {
        if (LevelName != null && BoardSize.x != 0 && BoardSize.y != 0)
        {
            //turn off level info
            GameObject.Find("Level Set Up").SetActive(false);
            //then make blank of proper size
            GenerateBlankBoard();
            //then turn on buttons with items
            ItemBoardButtons.SetActive(true);
        }
    }

    /// <summary>
    /// saves the level youre working on as a real level and gives it to the game manager list to use as a real level
    /// </summary>
    public void Save()
    {

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
        for (int x = (int)(-tempx - (0.5f + BoardOffset.y)); x < tempx; x++)
        {
            for (int y = (int)(-tempy - (0.5f + BoardOffset.y)); y < tempy; y++)
            {
                BoardTileMap.SetTile(new Vector3Int(x, y, 0), BackgroundTile);
            }
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
                Debug.Log($"Item Pos X: {(WorldPosition.x - 0.5 + clickableX)}");
                // Need to add 1 to this when odd
                Debug.Log($"Item Pos X int: {(int)(WorldPosition.x - 0.5 + clickableX)}");

                Vector2Int tileLocation = new Vector2Int((int)(WorldPosition.x - 0.5 + clickableX),
                    (int)(WorldPosition.y - 0.5 + clickableY));

                // Checks if position is within board and if the tile is empty
                if ((WorldPosition.x >= -clickableX && WorldPosition.x < clickableX)
                    && (WorldPosition.y >= -clickableY && WorldPosition.y < clickableY)
                    && GameBoard.At(tileLocation) == null) //&& GameBoard.At(tileLocation) == null
                {
                    GameBoard.Set(tileLocation, SelectedBoardTile);
                    Debug.Log(SelectedBoardTile.GetPrefab());
                    GameObject temp = Instantiate(SelectedBoardTile.GetPrefab(), WorldPosition, Quaternion.identity, Board.transform);
                    temp.name = SelectedBoardTile.name + $" ({tileLocation.x}, {tileLocation.y})";

                    Tiles.Add(new PosObject(tileLocation, SelectedBoardTile.name, temp.transform));
                }
                CanPlaceBoardTile = false;
            }
        }
        else
        {
            CanPlaceBoardTile = true;
        }
    }


    public Vector3 TileLocationSanitization(Vector3 Location)
    {
        Location.x += 0.5f;
        Location.y += 0.5f;

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
