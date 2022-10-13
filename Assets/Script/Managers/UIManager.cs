using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class UIManager : MonoBehaviour
{
    Item SelectedItem;

    bool CanPlaceItem = false;
    public GameObject Board;

    public void FindBoard(GameObject _board)
    {
        Board = _board;
    }

    void Update()
    {
        if (CanPlaceItem)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 WorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                WorldPosition.z = 3;
                WorldPosition = ItemLocationSanitization(WorldPosition);
                //santise the mouse imput and lock it to elagal spots
                Instantiate(SelectedItem, WorldPosition, Quaternion.identity, Board.transform);  //make this place where mouse is
                //*GameManager.Instance._ItemManager.AddItem(SelectedItem.getName());*//*
                //give position of item to item manager.
                CanPlaceItem = false;
            }
        }
    }


    //is triggured when a button is pressed
    public void PlaceItem(Item item)
    {
        SelectedItem = item;
        CanPlaceItem = true;
        //Debug.Log("in Place item");
    }


    public void EndRound()
    {
        /*GameManager.Instance._roundManager.EndTurn();*/
        Debug.Log("end turn");
    }

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
