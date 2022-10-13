/*
 * Author: Zachary Boehm
 * Created: 08/17/2022
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
            int tempx = BoardSize.x / 2;
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
                GameObject tempObj = new GameObject(CurPosTile.Slate.GetType().ToString());
                tempObj.AddComponent<SpriteRenderer>().sprite = CurPosTile.Slate.GetImage();
                tempObj.transform.SetParent(transform);
                tempObj.transform.localScale = new Vector3(0.4f, 0.4f, 1f);
                tempObj.transform.localPosition = new Vector3(CurPosTile.Position.x - tempx + 0.5f, CurPosTile.Position.y - tempy + 0.5f, 5);
            }

            for (int i = 0; i < currentLevel.GetPossibleItems().Length; i++)
            {
                Instantiate(ItemButtonPrefab, GameObject.Find("GUI").transform);
                //GameObject newButton = new GameObject(currentLevel.GetPossibleItems()[i].name);
                //newButton.transform.parent = GameObject.Find("GUI").transform;
                //newButton.transform.parent = GameObject.Find("GUI").transform;
                Debug.Log("test");
            }

            return true;
        }
        return false;
    }

    /// <summary>
    /// Start the match. This includes toggling flags and 
    /// </summary>
    public void StartMatch()
    {
        // Load board
        // Tell player it is strategy phase
        // Toggle flag to start match for update loop
        ActiveMatch = true;
    }

    private void Update()
    {
        if(ActiveMatch)
        {

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

    
}