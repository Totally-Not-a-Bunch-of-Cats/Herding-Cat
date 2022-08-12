/*
 * Author:
 * Version:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

/// <summary>
/// manages all of the behaviors of the cats 
/// </summary>
public class CatManager : MonoBehaviour
{ 

    //----------------------------------------------
    // Varables
    //----------------------------------------------

    // Array of Cats on the level
    [SerializeField]
    private Cat[] CatList;

    //----------------------------------------------
    // Functions
    //----------------------------------------------
    /// <summary>
    /// Pass in an item, then it goes through all the cats passing the item to GetMovementAmount in the itemManager to determine how much each cat should move.
    /// </summary>
    /// <param name="Distance">the distance each cat should move</param>
    public void MoveCats(Item item)
    {
        for (int i = 0; i < CatList.Length; i++)
        {
            CatList[i].Move(GameManager.Instance._ItemManager.GetMovementAmount(item, CatList[i]));
        }
    }
    /// <summary>
    /// calls one specific cat to move
    /// </summary>
    /// <param name="CurCat">the cat we want to move</param>
    /// <param name="Distance">the distance we want the cat to move</param>
    public void MoveCat(Cat CurCat, Vector2Int Distance)
    {
        Debug.Log("moving the cat");
        int WhichCat = FindCat(CurCat);
        CatList[WhichCat].Move(Distance);
    }


    /// <summary>
    /// finds a cat that has been passed to it
    /// </summary>
    /// <param name="CurCat">the current cat, the reference that is used to find its match in the list</param>
    /// <returns></returns>
    private int FindCat(Cat CurCat)
    {
        int Location = 0;
        for (int i = 0; i < CatList.Length; i++)
        {
            if(CatList[i] = CurCat)
            {
                return Location;
            }
        }
        return 0;
    }
    public Cat FindCat(Vector2 Location)
    {
        Vector2 difference;
        float closestLocation = 0;
        float squareDifference;
        Cat closestCat = null;
        for (int i = 0; i < CatList.Length; i++)
        {
            difference = CatList[i].WorldLocation;
            difference -= Location;
            squareDifference = Vector2.SqrMagnitude(difference);
            if(squareDifference <= closestLocation || closestCat == null)
            {
                closestLocation = squareDifference;
                closestCat = CatList[i];
            }
        }
        return closestCat;
    }
}
