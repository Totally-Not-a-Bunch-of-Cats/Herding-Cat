/*
 * Author:
 * Version:
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
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

    public void MoveCat(Vector2 Distance)
    {
        for(int i = 0; i < CatList.Length; i++)
        {
            CatList[i].Move();
        }
    }

    public void MoveCat (Cat CurCat, Vector2 Distance)
    {
        int WhichCat = FindCat(CurCat);
        CatList[WhichCat].Move();
    }



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
