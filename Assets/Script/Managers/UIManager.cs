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
                if((WorldPosition.x >= -5 && WorldPosition.x < GameManager.Instance._matchManager.GameBoard.GetWidth()/2) && 
                    (WorldPosition.y >= -5 && WorldPosition.y < GameManager.Instance._matchManager.GameBoard.GetHeight()/2))
                {
                    Instantiate(SelectedItem.GetPrefab(), WorldPosition, Quaternion.identity, Board.transform);  //make this place where mouse is
                    GameManager.Instance._matchManager.GameBoard.Set((int)WorldPosition.x, (int)WorldPosition.y, SelectedItem);
                    Vector2Int itemLocation = new Vector2Int((int)WorldPosition.x, (int)WorldPosition.y);
                    GameManager.Instance._matchManager.ItemLocations.Add(itemLocation);
                }
                CanPlaceItem = false;
                Debug.Log("cant palce now");
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
