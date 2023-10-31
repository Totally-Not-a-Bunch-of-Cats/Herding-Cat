using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// saving info.
/// When a level is completed. The following info is saved.
/// update StarCount.
/// Update per Level Star Count.
/// FurthestUnlockedLevel is updated
/// unlocked cosmetics
/// </summary>

public class PlayerPrefsManager : MonoBehaviour
{
    /// <summary>
    /// loads important setting quick at game start
    /// </summary>
    public void LoadSettings()
    {
        GameManager.Instance.sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
        GameManager.Instance.SFXToggle = GetBool("SFXToggle");
        GameManager.Instance.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        GameManager.Instance.MusicToggle = GetBool("MusicToggle");
        GameManager.Instance.StarCount = PlayerPrefs.GetInt("StarCount");
        GameManager.Instance.CatSpeed = PlayerPrefs.GetFloat("CatSpeed");
        GameManager.Instance.ItemIndicators = GetBool("ItemIndicators");
        GameManager.Instance.FurthestLevel = PlayerPrefs.GetString("FurthestLevel");
        GameManager.Instance.ADsoff = GetBool("Adsoff");
        GameManager.Instance.ADsoff = GetBool("SkipForcedVids");
    }

    /// <summary>
    /// goes through the levels on game launch to make sure the level data matches the playerprefs data
    /// </summary> //make cause a bug/ crash if you have completed all levels
    public void CheckLevels()
    {
        //loop through all levels checking for saved data and making it match as well as unlocking all levels behind the FurthestUnlockedLevel
        for (int i = 0; i < GameManager.Instance.Levels.Count; i++)
        {
            if (PlayerPrefs.GetInt(GameManager.Instance.Levels[i].name) > 3)
            {
                SaveInt(GameManager.Instance.Levels[i].name, 0);
            }
            else if(GameManager.Instance.FurthestLevel == GameManager.Instance.Levels[i].name)
            {
                if(PlayerPrefs.GetInt(GameManager.Instance.Levels[i].name) > 0)
                {
                    GameManager.Instance.Levels[i].StarsEarned = PlayerPrefs.GetInt(GameManager.Instance.Levels[i].name);
                }
                break;
            }
            else
            {
                GameManager.Instance.Levels[i].StarsEarned = PlayerPrefs.GetInt(GameManager.Instance.Levels[i].name);
            }
        }
    }

    public void RemoveHelpScreens()
    {
       for(int i = 0; i < GameManager.Instance.Levels.Count; i++)
       {
            if(GetBool(GameManager.Instance.Levels[i].name) == true)
            {
                GameManager.Instance.Levels[i].SpecialHelpTxt = false;
                GameManager.Instance.Levels[i].NewThingIntroduced = false;
            }
        }
    }

    //saves float information
    public void SaveFloat(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }

    ////gets float info with key
    //public float GetFloat(string KeyName)
    //{
    //    return PlayerPrefs.GetFloat(KeyName , 0);
    //}

    //saves int with a key
    public void SaveInt(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
    //public int GetInt(string KeyName)
    //{
    //    return PlayerPrefs.GetInt(KeyName, 0);
    //}

    //saves string
    public void SaveString(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
    }

    //public string GetString(string KeyName)
    //{
    //    return PlayerPrefs.GetString(KeyName,"");
    //}

    public void SaveBool(string name, bool value)
    {
        int boolVal;
        if(value == true)
        {
            boolVal = 1;
            PlayerPrefs.SetInt(name, boolVal);
        }
        else
        {
            boolVal = 0;
            PlayerPrefs.SetInt(name, boolVal);
        }
    }

    public bool GetBool(string KeyName)
    {
        if(PlayerPrefs.GetInt(KeyName) == 1)
        {
            return true;
        }
        return false;
    }
}
