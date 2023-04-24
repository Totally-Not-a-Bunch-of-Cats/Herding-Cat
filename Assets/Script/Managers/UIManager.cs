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
    public PosObject CurrentSelectedItem;
    public GameObject SelectedButton;
    public Sprite SelectedBoxSprite;
    public Sprite BoxSprite;

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
                    if (itemLocation.y == 0 || itemLocation.y == GameManager.Instance._matchManager.GameBoard.GetHeight() - 1)
                    {
                        clickableY += 1;
                    }
                    if (itemLocation.x == 0 || itemLocation.x == GameManager.Instance._matchManager.GameBoard.GetWidth() - 1)
                    {
                        clickableX += 1;
                    }
                    // Checks if position is within board and if the tile is empty
                    if (WorldPosition.x >= -clickableX && WorldPosition.x < clickableX &&
                        (WorldPosition.y >= -clickableY && WorldPosition.y < clickableY)
                        && GameManager.Instance._matchManager.GameBoard.At(itemLocation) == null)
                    {
                        // Creating Item for On Board
                        GameManager.Instance._matchManager.GameBoard.Set(itemLocation, SelectedItem);
                        GameObject temp = Instantiate(SelectedItem.GetPrefab(), WorldPosition, Quaternion.identity, Board.transform);
                        temp.name = SelectedItem.name + $" ({itemLocation.x}, {itemLocation.y})";
                        GameManager.Instance._matchManager.GameBoard.Items.Add(new PosObject(itemLocation, SelectedItem.name, SelectedItem, temp.transform));
                        // Adds Item to the list to delete/adjust order of items
                        GameObject NewItemEntry = Instantiate(ItemAdjPrefab, new Vector3(0, 0, 0), Quaternion.identity, ItemAdjPanel.transform.GetChild(0).GetChild(0));
                        NewItemEntry.GetComponent<ItemAdjPanel>().ItemImage.sprite = temp.GetComponent<SpriteRenderer>().sprite;
                        if(GameManager.Instance._matchManager.GameBoard.Items.Count % 2 == 0)
                        {
                            NewItemEntry.GetComponent<Image>().color = new Color(.8f, .8f, .8f);
                        }
                        GameManager.Instance._matchManager.GameBoard.Items[GameManager.Instance._matchManager.GameBoard.Items.Count - 1].ItemAdjObject = NewItemEntry.GetComponent<ItemAdjPanel>();
                    }
                    else if(WorldPosition.x >= -clickableX && WorldPosition.x < clickableX &&
                        (WorldPosition.y >= -clickableY && WorldPosition.y < clickableY)
                        && GameManager.Instance._matchManager.GameBoard.At(itemLocation).Is<Item>())
                    {
                        if(GameManager.Instance._matchManager.GameBoard.At(itemLocation) == SelectedItem)
                        {
                            //delete item sets item location to null, then remove item adj panel location and then
                            for(int i = 0; i< GameManager.Instance._matchManager.GameBoard.Items.Count; i++)
                            {
                                if(GameManager.Instance._matchManager.GameBoard.Items[i] != null)
                                {
                                    if (itemLocation == GameManager.Instance._matchManager.GameBoard.Items[i].Position)
                                    {
                                        GameManager.Instance._matchManager.GameBoard.Items[i].ItemAdjObject.DeleteItem();
                                        break;
                                    }
                                }
                            }

                        }
                        else
                        {
                            for (int i = 0; i < GameManager.Instance._matchManager.GameBoard.Items.Count; i++)
                            {
                                if (GameManager.Instance._matchManager.GameBoard.Items[i] != null)
                                {
                                    if (itemLocation == GameManager.Instance._matchManager.GameBoard.Items[i].Position)
                                    {
                                        GameManager.Instance._matchManager.GameBoard.Items[i].ItemAdjObject.ItemImage.sprite = SelectedItem.Picture;
                                        GameManager.Instance._matchManager.GameBoard.Items[i].Object.gameObject.SetActive(false);
                                        GameObject temp2 = Instantiate(SelectedItem.GetPrefab(), WorldPosition, Quaternion.identity, Board.transform);
                                        GameManager.Instance._matchManager.GameBoard.Items[i].Object = temp2.transform;
                                        GameManager.Instance._matchManager.GameBoard.Items[i].Name = SelectedItem.name;
                                        GameManager.Instance._matchManager.GameBoard.Items[i].Tile = SelectedItem;
                                        GameManager.Instance._matchManager.GameBoard.Set(itemLocation, SelectedItem);
                                        break;
                                    }
                                }
                            }
                        }
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
        //rewind button and make an event
        GameObject.Find("Rewind Button").GetComponent<Button>().onClick.AddListener(() => Rewind());
    }

    /// <summary>
    /// allows an item to be placed and is handed which item to place
    /// </summary>
    /// <param name="item"><see cref="Item"/> that will be placed when clicked on board</param>
    public void PlaceItem(Item item, GameObject selectedButton)
    {
        if (SelectedButton != null)
        {
            SelectedButton.GetComponent<Image>().sprite = BoxSprite;
        }
        SelectedButton = selectedButton;
        SelectedButton.GetComponent<Image>().sprite = SelectedBoxSprite;
        SelectedItem = item;
        CanPlaceItem = true;
    }

    /// <summary>
    /// Restarts the current round
    /// </summary>
    public void Restart()
    {
        SelectedItem = null;
        GameManager.Instance._ReWindManager.Clear();
        StartCoroutine(GameManager.Instance.StartMatch(GameManager.Instance._matchManager.CurrentLevel.name));
    }

    public void Rewind()
    {
        if(GameManager.Instance._ReWindManager.PreviousRoundsPlayed != -1)
        {
            SelectedItem = null;
            if (SelectedButton != null)
            {
                SelectedButton.GetComponent<Image>().color = new Color(.5f, .5f, .5f, 1);
            }
            Debug.Log("we rewinding");
            GameManager.Instance._ReWindManager.Revert();
        }
    }

    /// <summary>
    /// Calls the end of round
    /// </summary>
    public void EndRound()
    {
        if(GameManager.Instance._matchManager.GameBoard.Items.Count > 0)
        {
            Override = false;
            GameManager.Instance._matchManager.CatJustinCage = false;
            GameManager.Instance._matchManager.GameBoard.SecondCatList.Clear();
            //lock you out fron pressing buttons
            GameManager.Instance._ReWindManager.SaveRewind(GameManager.Instance._matchManager.GameBoard, GameManager.Instance._matchManager.RoundsPlayed,
                GameManager.Instance._matchManager.ItemsUsed, GameManager.Instance._matchManager.CurrentLevel.GetTiles());
            GameManager.Instance._matchManager.RoundsPlayed++;
            StartCoroutine(GameManager.Instance._matchManager.EndRound());
        }
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
    /// Turns on the highlight for the item adjust entry and item
    /// </summary>
    /// <param name="Index">Index of item that is selected</param>
    public void HighlightItem(int Index)
    {
        if (CurrentSelectedItem.ItemAdjObject != null)
        {
            CurrentSelectedItem.ItemAdjObject.HighLightObject.SetActive(false);
            CurrentSelectedItem.ItemAdjObject.DeleteButton.gameObject.SetActive(false);
            CurrentSelectedItem.Object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0.9166545f, 1, 0, 0.5254902f);
        }
        CurrentSelectedItem = GameManager.Instance._matchManager.GameBoard.Items[Index];
        SpriteRenderer temp = CurrentSelectedItem.Object.transform.GetChild(0).GetComponent<SpriteRenderer>();
        CurrentSelectedItem.Object.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .9f);

        // Highlight item circle
        CurrentSelectedItem.ItemAdjObject.HighLightObject.SetActive(true);
        CurrentSelectedItem.ItemAdjObject.DeleteButton.gameObject.SetActive(true);
    }
}   
