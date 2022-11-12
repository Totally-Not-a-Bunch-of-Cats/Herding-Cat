/** @Author Damian Link, Aaron */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;

/// <summary>
/// Controls the Match UI
/// </summary>
public class UIManager : MonoBehaviour
{
    Item SelectedItem;

    bool CanPlaceItem = false;
    public GameObject Board;
    public GameObject GUI;

    /// <summary>
    /// finds the Board game object and GUI object
    /// </summary>
    /// <param name="_board"></param>
    public void FindBoard(GameObject _board)
    {
        Board = _board;
        GUI = GameObject.Find("GUI");
    }

    /// <summary>
    /// governs if you can place and item and 
    /// when you can place an item it places it both on the baord and as a gameobject
    /// then prevents you from placing it
    /// </summary>
    void Update()
    {
        //checks to see if you can place an item
        if (CanPlaceItem)
        {
            //checks to see if the mouse button was pressed (update for mobile maybe use unity buttons)
            if (Input.GetMouseButtonDown(0))
            {
                //gets world position and translates it to a vec2int
                Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                WorldPosition.z = 3;
                WorldPosition = ItemLocationSanitization(WorldPosition);
                float clickableX = GameManager.Instance._matchManager.GameBoard.GetWidth() / 2;
                float clickableY = GameManager.Instance._matchManager.GameBoard.GetHeight() / 2;
                Vector2Int itemLocation = new Vector2Int((int)(WorldPosition.x - 0.5 + clickableX), (int)(WorldPosition.y - 0.5 + clickableY));

                // Checks if position is within board and if the tile is empty
                if ((WorldPosition.x >= -clickableX && WorldPosition.x < clickableX) 
                    && (WorldPosition.y >= -clickableY && WorldPosition.y < clickableY) 
                    && GameManager.Instance._matchManager.GameBoard.At(itemLocation) == null)
                {
                    GameManager.Instance._matchManager.GameBoard.Set(itemLocation, SelectedItem);
                    GameObject temp = Instantiate(SelectedItem.GetPrefab(), WorldPosition, Quaternion.identity, Board.transform);
                    temp.name = SelectedItem.name + GameManager.Instance._matchManager.GameBoard.Items.Count;
                    GameManager.Instance._matchManager.GameBoard.Items.Add(new PosObject(itemLocation, SelectedItem.name, temp.transform));
                }
                CanPlaceItem = false;
                Debug.Log("cant palce now");
            } 
            else
            {
                CanPlaceItem = true;
            }
        }
    }

    
    /// <summary>
    /// Adds the Listeners to the End round, Restart, and Main Menu Buttons, generated at ui start
    /// </summary>
    public void GetUI()
    {
        //get the end turn button and make an event
        GameObject.Find("End Round Button").GetComponent<Button>().onClick.AddListener(() => EndRound());
        //get the restart button and make an event 
        GameObject.Find("Restart Button").GetComponent<Button>().onClick.AddListener(() => Restart());
        //get main menu button and make an event (need a real menu)
        //GameObject.Find("Main Menu Button").GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.SwitchScene("Menu"));

    }
    /// <summary>
    /// allows an item to be placed and is handed which item to place
    /// </summary>
    /// <param name="item"><see cref="Item"/> that will be placed when clicked on board</param>
    public void PlaceItem(Item item)
    {
        SelectedItem = item;
        CanPlaceItem = true;
    }


    /// <summary>
    /// Restarts the current round
    /// </summary>
    public void Restart()
    {
        Debug.Log("Restarted");
        StartCoroutine(GameManager.Instance.StartMatch());
    }
    
    /// <summary>
    /// Calls the end of round
    /// </summary>
    public void EndRound()
    {
        //lock you out fron pressing buttons
        GameManager.Instance._matchManager.RoundsPlayed++;
        GameManager.Instance._matchManager.EndRound(); 
    }


    /// <summary>
    /// Sanitizes the items location to line up on the cell by turning it from a vec3 to a vec2int
    /// </summary>
    /// <param name="Location">Location that needs to be Sanitized</param>
    /// <returns>Location that is centered on a 0.5 to line up with the cell</returns>
    Vector3 ItemLocationSanitization(Vector3 Location)
    {
        Location.x += 0.5f;
        Location.y += 0.5f;

        Location.x = Mathf.Round(Location.x);
        Location.y = Mathf.Round(Location.y);

        Location.x -= 0.5f;
        Location.y -= 0.5f;

        return Location;
    }

}
