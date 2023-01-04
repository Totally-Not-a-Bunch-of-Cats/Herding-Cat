using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCreationTool: MonoBehaviour
{
    public int SizeX;
    public int SizeY;
    public string LevelName;
        
    //then activates buttons to begin working on the level

    /// <summary>
    /// takes input for sizeX
    /// </summary>
    /// <param name="X"></param>
    public void SizeXInput(string X)
    {
        if (int.Parse(X) > 0)
        {
            SizeX = int.Parse(X);
        }
    }
    /// <summary>
    /// takes input for sizeY
    /// </summary>
    /// <param name="X"></param>
    public void SizeYInput(string Y)
    {
        if (int.Parse(Y) > 0)
        {
            SizeY = int.Parse(Y);
        }
    }
    /// <summary>
    /// lets the desginer input the name of the level
    /// </summary>
    /// <param name="name"></param>
    public void LevelNameInput(string name)
    {
        LevelName = name;
        Debug.Log(name);
    }

    public void Continue()
    {
        if(LevelName != null && SizeX != 0 && SizeY != 0)
        {
            Debug.Log("herlp");
        }
        //turn off level info thing 
        //then make blank of proper size
        //then turn on buttons with items
    }

    /// <summary>
    /// saves the level youre working on as a real level and gives it to the game manager list to use as a real level
    /// </summary>
    public void Save()
    {

    }
}
