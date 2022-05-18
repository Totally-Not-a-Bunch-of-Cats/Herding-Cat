using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Cat : MonoBehaviour
{
    //---------------------------------------
    // Varables
    //---------------------------------------

    // 
    Vector2Int Size;
    // 
    Vector2Int Location;
    // If cat can move/is asleep(if true cat cant move)
    private bool Asleep = false;
    // Cat moves extra space next round if true
    private bool Fuzzed = false;

    //----------------------------------------
    // Functions
    //----------------------------------------

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
