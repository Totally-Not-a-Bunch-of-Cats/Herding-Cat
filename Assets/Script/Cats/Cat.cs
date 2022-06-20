/*
 * Author: Damian Link
 * Version: 5/21/22
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for cats
/// </summary>
[System.Serializable]
public class Cat : MonoBehaviour
{
    //---------------------------------------
    // Varables
    //---------------------------------------

    // stores the size of the cat
    Size CatSize = new Size(1,1);
    // stores the location of the cat 
    public Vector2Int Location = new Vector2Int(1,1);   //this isnt nessarly world space careful 
    // If cat can move/is asleep(if true cat cant move)
    private bool Asleep = false;
    // Cat moves extra space next round if true
    private bool Fuzzed = false;

    //----------------------------------------
    // Functions
    //----------------------------------------

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public void Move(Vector2Int Distance)
    {
        //call round manager to check movement and send adjustment movement back 
        // then call wall check if movement isnt zero
        Location.x = Distance.x;
        Location.y = Distance.y;
        
        UpdatePosition();
    }
    /// <summary>
    /// updates the position of the cat after movment with the round manager
    /// </summary>
    public void UpdatePosition()
    {
        //updates the position of the cat with the round manager after movement
    }

   

    /// <summary>
    /// Sets the fuzz value of the cat
    /// </summary>
    /// <param name="FuzzVal">Value of fuzz to be set to</param>
    public void SetFuzz (bool FuzzVal)
    {
        Fuzzed = FuzzVal;
    }

    /// <summary>
    /// Sets the asleep value of the cat
    /// </summary>
    /// <param name="SleepVal">Value of asleep to be set to</param>
    public void SetAsleep (bool SleepVal)
    {
        Asleep = SleepVal;
    }

}
