/** @Author Damian Link, Aaron */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine.SceneManagement;


/// <summary>
/// Controls the Match UI
/// </summary>
public class UIManager : MonoBehaviour
{
    public Item SelectedItem;

    public bool CanPlaceItem = false;
    public bool Override = true;
    public GameObject Board;
    public GameObject GUI;
    public GameObject ItemAdjPrefab;
    public GameObject ItemAdjPanel;

    /// <summary>
    /// finds the Board game object and GUI object
    /// </summary>
    /// <param name="_board"></param>
    public void FindBoard(GameObject _board)
    {
        Board = _board;
        GUI = GameObject.Find("GUI");
        ItemAdjPanel = GameObject.Find("ItemAdjPanel");
    }

    /// <summary>
    /// governs if you can place and item and 
    /// when you can place an item it places it both on the baord and as a gameobject
    /// then prevents you from placing it
    /// </summary>
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Match")
        {
            //checks to see if you can place an item
            if (CanPlaceItem && Override && GameManager.Instance._matchManager.ActiveMatch == true)
            {
                //checks to see if the mouse button was pressed (update for mobile maybe use unity buttons)
                if (Input.GetMouseButtonDown(0) && SelectedItem != null)
                {
                    //gets world position and translates it to a vec2int
                    Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    WorldPosition.z = 3;
                    WorldPosition = ItemLocationSanitization(WorldPosition);
                    float clickableX = GameManager.Instance._matchManager.GameBoard.GetWidth() / 2;
                    float clickableY = GameManager.Instance._matchManager.GameBoard.GetHeight() / 2;
                    if(GameManager.Instance._matchManager.BoardOffset.y == 0.5f)
                    {
                        clickableY -= 0.5f;
                    }
                    if (GameManager.Instance._matchManager.BoardOffset.x == 0.5f)
                    {
                        clickableX -= 0.5f;
                    }

                    Vector2Int itemLocation = new Vector2Int((int)(WorldPosition.x - 0.5 + clickableX),
                        (int)(WorldPosition.y - 0.5 + clickableY));

                    if (GameManager.Instance._matchManager.BoardOffset.x == 0.5f)
                    {
                        itemLocation.x += 1;
                    }
                    if (GameManager.Instance._matchManager.BoardOffset.y == 0.5f)
                    {
                        itemLocation.y += 1;
                    }
                    //checs for edge casts hehe and adjusts accordingly
                    if(itemLocation.y == 0 || itemLocation.y == GameManager.Instance._matchManager.GameBoard.GetHeight() - 1)
                    {
                        clickableY += 1;
                    }
                    if (itemLocation.x == 0 || itemLocation.x == GameManager.Instance._matchManager.GameBoard.GetWidth() - 1)
                    {
                        clickableX += 1;
                    }
                    // Checks if position is within board and if the tile is empty
                    if ((WorldPosition.x >= -clickableX && WorldPosition.x < clickableX) && 
                        (WorldPosition.y >= -clickableY && WorldPosition.y < clickableY)
                        && GameManager.Instance._matchManager.GameBoard.At(itemLocation) == null)
                    {
                        GameManager.Instance._matchManager.GameBoard.Set(itemLocation, SelectedItem);
                        GameObject temp = Instantiate(SelectedItem.GetPrefab(), WorldPosition, Quaternion.identity, Board.transform);
                        temp.name = SelectedItem.name + $" ({itemLocation.x}, {itemLocation.y})";
                        GameManager.Instance._matchManager.GameBoard.Items.Add(new PosObject(itemLocation, SelectedItem.name, temp.transform));
                        // Adds Item to the list to delete/adjust order of items
                        GameObject NewItemEntry = Instantiate(ItemAdjPrefab, new Vector3(0, 0, 0), Quaternion.identity, ItemAdjPanel.transform.GetChild(0).GetChild(0));
                        NewItemEntry.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
                        GameManager.Instance._matchManager.GameBoard.Items[GameManager.Instance._matchManager.GameBoard.Items.Count - 1].ItemAdjObject = NewItemEntry;
                        int num = GameManager.Instance._matchManager.GameBoard.Items.Count - 1;
                        NewItemEntry.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => DeleteItem(num));

                    }
                    CanPlaceItem = false;
                }
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
        GameObject.Find("End Turn Button").GetComponent<Button>().onClick.AddListener(() => EndRound());
        //get the restart button and make an event 
        GameObject.Find("Restart Button").GetComponent<Button>().onClick.AddListener(() => Restart());
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
        SelectedItem = null;
        StartCoroutine(GameManager.Instance.StartMatch(GameManager.Instance._matchManager.CurrentLevel.name));
    }
    
    /// <summary>
    /// Calls the end of round
    /// </summary>
    public void EndRound()
    {
        Override = false;
        //lock you out fron pressing buttons
        GameManager.Instance._matchManager.RoundsPlayed++;
        StartCoroutine(GameManager.Instance._matchManager.EndRound());
    }


    /// <summary>
    /// Sanitizes the items location to line up on the cell by getting our vec3 ready to be changed to a vec2int
    /// </summary>
    /// <param name="Location">Location that needs to be Sanitized</param>
    /// <returns>Location that is centered on a 0.5 to line up with the cell</returns>
    public Vector3 ItemLocationSanitization(Vector3 Location)
    {
        if(GameManager.Instance._matchManager.BoardOffset.x == 0.0f)
        {
            Location.x += 0.5f;
        }
        if (GameManager.Instance._matchManager.BoardOffset.y == 0.0f)
        {
            Location.y += 0.5f;
        }

        Location.x = Mathf.Round(Location.x);
        Location.y = Mathf.Round(Location.y);

        if (GameManager.Instance._matchManager.BoardOffset.x == 0.0f)
        {
            Location.x -= .5f;
        }
        if (GameManager.Instance._matchManager.BoardOffset.y == 0.0f)
        {
            Location.y -= .5f;
        }

        return Location;
    }

    /// <summary>
    /// Deletes Item on board
    /// </summary>
    /// <param name="Index">Index of item to delete from the board from the item list</param>
    void DeleteItem(int Index)
    {
        if (Index >= 0 && Index <= GameManager.Instance._matchManager.GameBoard.Items.Count)
        {
            Destroy(GameManager.Instance._matchManager.GameBoard.Items[Index].Object.gameObject);
            Destroy(GameManager.Instance._matchManager.GameBoard.Items[Index].ItemAdjObject);
            GameManager.Instance._matchManager.GameBoard.Set(GameManager.Instance._matchManager.GameBoard.Items[Index].Position, null);
            GameManager.Instance._matchManager.GameBoard.Items[Index] = null;
            GameManager.Instance._matchManager.ItemsUsed -= 1;
        } 
        else
        {
            Debug.LogError($"Index must be between 0 and ({GameManager.Instance._matchManager.GameBoard.Items.Count}");
            throw new ArgumentOutOfRangeException($"Index must be between 0 and ({GameManager.Instance._matchManager.GameBoard.Items.Count}");
        }
    }
}
