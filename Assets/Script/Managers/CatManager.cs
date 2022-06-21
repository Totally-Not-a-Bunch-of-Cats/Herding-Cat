/*
 * Author:
 * Version:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    /// calls for all cats on the board to me moved
    /// </summary>
    /// <param name="Distance">the distance each cat should move</param>
    public void MoveCat(Vector2Int Distance)
    { 
        for(int i = 0; i < CatList.Length; i++)
        {
            CatList[i].Move(Distance);
        }
    }
    /// <summary>
    /// calls one specific cat to move
    /// </summary>
    /// <param name="CurCat">the cat we want to move</param>
    /// <param name="Distance">the distance we want the cat to move</param>
    public void MoveCat (Cat CurCat, Vector2Int Distance)
    {
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
}
