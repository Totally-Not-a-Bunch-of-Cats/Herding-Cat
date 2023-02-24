using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves item adjust panel out(Move out/flips icon arrow)
/// </summary>
public class MovingButton : MonoBehaviour
{
    public GameObject Image;
    bool Fliped = false;
    bool Dragging = false;
    Vector3 WorldPos;
    Vector2 ScreenPos;
    ItemAdjPanel LastDragged;

    /// <summary>
    /// Moves item adjust panel out(Move out/flips icon arrow)
    /// </summary>
    public void Click()
    {
        if(!Fliped)
        {
            gameObject.transform.localPosition += new Vector3(210, 0, 0);
            Image.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            Fliped = true;
        }
        else
        {
            gameObject.transform.localPosition += new Vector3(-210, 0, 0);
            Image.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Fliped = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void Update()
    {
        if (Fliped && Input.mousePosition.x < transform.position.x)
        {
            if (Dragging)
            {
                Debug.Log("we drag");
                if (Input.GetMouseButtonDown(0))
                {
                    // Drop
                    Debug.Log("drag");
                    Dragging = false;
                    return;
                }
            }

           if (Input.GetMouseButton(0))
           {
                Debug.Log("drop");
                Vector3 mousePos = Input.mousePosition;
                ScreenPos = new Vector2(mousePos.x, mousePos.y);
           } 
           else
           {
                return;
           }

           WorldPos = Camera.main.ScreenToWorldPoint(ScreenPos);

           if(Dragging)
           {
                //Drag
                LastDragged.transform.position = new Vector2(WorldPos.x, WorldPos.y);
                Debug.Log(LastDragged);
           } 
           else
           {
                Debug.Log("not dragging");
                RaycastHit2D hit = Physics2D.Raycast(WorldPos, Vector2.zero);
                Debug.Log(hit.collider);
                if (hit.collider != null)
                {
                    Debug.Log("really not dragging");
                    Debug.Log(hit.transform.gameObject.name);
                    ItemAdjPanel ItemAdj = hit.transform.gameObject.GetComponent<ItemAdjPanel>();
                    if (ItemAdj != null)
                    {
                        LastDragged = ItemAdj;
                        Debug.Log("Hit");
                        // initiate drag
                        Dragging = true;
                    }
                }
           }
        }
    }
}
