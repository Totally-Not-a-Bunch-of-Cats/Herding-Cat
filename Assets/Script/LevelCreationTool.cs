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

    //then activates buttons to begin working on the level

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
        Debug.Log(name);
    }

    public void Continue()
    {
        if (LevelName != null && BoardSize.x != 0 && BoardSize.y != 0)
        {
            Debug.Log("herlp");
            //turn off level info thing 
            GameObject.Find("Level Set Up").SetActive(false);
            //then make blank of proper size
            GenerateBlankBoard();
            //then turn on buttons with items

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
        Vector3 BoardOffset = new Vector3();
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
}
