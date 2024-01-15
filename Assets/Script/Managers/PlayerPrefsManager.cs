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
        //checks to see if SFXVolme has a key and if it doesnt it makes one
        ////if(!PlayerPrefs.HasKey("SFXVolume"))
        ////{
        ////    GameManager.Instance.SFXVolume = 1;
        ////    PlayerPrefs.SetFloat("SFXVolume", 1);
        ////}
        //else
        //{
        //    GameManager.Instance.SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
        //}
        //checks to see if SFXToggle has a key and if it doesnt it makes one
        //if (!PlayerPrefs.HasKey("SFXToggle"))
        //{
        //    GameManager.Instance.SFXToggle = true;
        //    SaveBool("SFXToggle", true);
        //}
        //else
        //{
        //    GameManager.Instance.SFXToggle = GetBool("SFXToggle");
        //}
        //checks to see if musicvolue has a key and if it doesnt it makes a key for it
        if (!PlayerPrefs.HasKey("MusicVolume"))
        {
            GameManager.Instance.musicVolume = 1;
            PlayerPrefs.SetFloat("MusicVolume", 1);
        }
        else
        {
            GameManager.Instance.musicVolume = PlayerPrefs.GetFloat("MusicVolume");
        }
        //checks to see if MusicToggle has a key and if it doesnt it makes one
        if (!PlayerPrefs.HasKey("MusicToggle"))
        {
            GameManager.Instance.MusicToggle = true;
            SaveBool("MusicToggle", true);
        }
        else
        {
            GameManager.Instance.MusicToggle = GetBool("MusicToggle");
        }
        //checks to see if StarCount has a key and if it doesnt it makes one
        if (!PlayerPrefs.HasKey("StarCount"))
        {
            GameManager.Instance.StarCount = 0;
            PlayerPrefs.SetFloat("StarCount", 0);
        }
        else
        {
            GameManager.Instance.StarCount = PlayerPrefs.GetInt("StarCount");
        }
        //checks to see if CatSpeed has a key and if it doesnt it makes one
        if (!PlayerPrefs.HasKey("CatSpeed"))
        {
            GameManager.Instance.CatSpeed = 1;
            PlayerPrefs.SetFloat("CatSpeed", 1);
        }
        else
        {
            GameManager.Instance.CatSpeed = PlayerPrefs.GetFloat("CatSpeed");
        }
        //checks to see if ItemIndicators has a key and if it doesnt it makes one
        if (!PlayerPrefs.HasKey("ItemIndicators"))
        {
            GameManager.Instance.ItemIndicators = false;
            SaveBool("ItemIndicators", false);
        }
        else
        {
            GameManager.Instance.ItemIndicators = GetBool("ItemIndicators");
        }
        //checks to see if FurthestLevel has a key and if it doesnt it makes one
        if (!PlayerPrefs.HasKey("FurthestLevel"))
        {
            GameManager.Instance.FurthestLevel = "1-1";
            SaveString("FurthestLevel", "1-1");
        }
        else
        {
            GameManager.Instance.FurthestLevel = PlayerPrefs.GetString("FurthestLevel");
        }
        //checks to see if ADsoff has a key and if it doesnt it makes one
        if (!PlayerPrefs.HasKey("ADsoff"))
        {
            GameManager.Instance.ADsoff = false;
            SaveBool("ADsoff", false);
        }
        else
        {
            GameManager.Instance.ADsoff = GetBool("ADsoff");
        }
        //checks to see if ItemIndicators has a key and if it doesnt it makes one
        if (!PlayerPrefs.HasKey("SkipForcedVids"))
        {
            GameManager.Instance.SkipForcedVids = false;
            SaveBool("SkipForcedVids", false);
        }
        else
        {
            GameManager.Instance.SkipForcedVids = GetBool("SkipForcedVids");
        }
        if (!PlayerPrefs.HasKey("PurchasedStarBoost"))
        {
            GameManager.Instance.PurchasedStarBoost = false;
            SaveBool("PurchasedStarBoost", false);
        }
        else
        {
            GameManager.Instance.SkipForcedVids = GetBool("SkipForcedVids");
        }



        if (!PlayerPrefs.HasKey("Cat1Acc"))
        {
            SaveString("Cat1Acc", "NA");
        }
        else
        {
            GameManager.Instance._catInfoManager.LoadCatsAcc(0, PlayerPrefs.GetString("Cat1Acc"));
        }

        if (!PlayerPrefs.HasKey("Cat1Skin"))
        {
            SaveString("Cat1Skin", "BlackCat");
        }
        else
        {
            GameManager.Instance._catInfoManager.LoadCatsSkin(0, PlayerPrefs.GetString("Cat1Skin"));
        }

        if (!PlayerPrefs.HasKey("Cat2Acc"))
        {
            SaveString("Cat2Acc", "NA");
        }
        else
        {
            GameManager.Instance._catInfoManager.LoadCatsAcc(1, PlayerPrefs.GetString("Cat2Acc"));
        }

        if (!PlayerPrefs.HasKey("Cat2Skin"))
        {
            SaveString("Cat2Skin", "BlackCat");
        }
        else
        {
            GameManager.Instance._catInfoManager.LoadCatsSkin(1, PlayerPrefs.GetString("Cat2Skin"));
        }

        if (!PlayerPrefs.HasKey("Cat3Acc"))
        {
            SaveString("Cat3Acc", "NA");
        }
        else
        {
            GameManager.Instance._catInfoManager.LoadCatsAcc(2, PlayerPrefs.GetString("Cat3Acc"));
        }

        if (!PlayerPrefs.HasKey("Cat3Skin"))
        {
            SaveString("Cat3Skin", "BlackCat");
        }
        else
        {
            GameManager.Instance._catInfoManager.LoadCatsSkin(2, PlayerPrefs.GetString("Cat3Skin"));
        }

        if (!PlayerPrefs.HasKey("Cat4Acc"))
        {
            SaveString("Cat4Acc", "NA");
        }
        else
        {
            GameManager.Instance._catInfoManager.LoadCatsAcc(3, PlayerPrefs.GetString("Cat4Acc"));
        }

        if (!PlayerPrefs.HasKey("Cat4Skin"))
        {
            SaveString("Cat4Skin", "BlackCat");
        }
        else
        {
            GameManager.Instance._catInfoManager.LoadCatsSkin(3, PlayerPrefs.GetString("Cat4Skin"));
        }

        if (!PlayerPrefs.HasKey("Cat5Acc"))
        {
            SaveString("Cat5Acc", "NA");
        }
        else
        {
            GameManager.Instance._catInfoManager.LoadCatsAcc(4, PlayerPrefs.GetString("Cat4Acc"));
        }

        if (!PlayerPrefs.HasKey("Cat5Skin"))
        {
            SaveString("Cat5Skin", "BlackCat");
        }
        else
        {
            GameManager.Instance._catInfoManager.LoadCatsSkin(4, PlayerPrefs.GetString("Cat5Skin"));
        }
    }

    /// <summary>
    /// goes through the levels on game launch to make sure the level data matches the playerprefs data
    /// </summary> //make cause a bug/ crash if you have completed all levels
    public void CheckLevels()
    {
        //loop through all levels checking for saved data and making it match as well as unlocking all levels behind the FurthestUnlockedLevel
        for (int i = 0; i < GameManager.Instance.Levels.Count; i++)
        {
            if (PlayerPrefs.HasKey(GameManager.Instance.Levels[i].name))
            {
                if (GameManager.Instance.FurthestLevel == GameManager.Instance.Levels[i].name)
                {
                    GameManager.Instance.Levels[i].SetUnlocked(true);
                    if (PlayerPrefs.GetInt(GameManager.Instance.Levels[i].name) > 0)
                    {
                        GameManager.Instance.Levels[i].StarsEarned = PlayerPrefs.GetInt(GameManager.Instance.Levels[i].name);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    GameManager.Instance.Levels[i].StarsEarned = PlayerPrefs.GetInt(GameManager.Instance.Levels[i].name);
                    GameManager.Instance.Levels[i].SetUnlocked(true);
                }
            }
            else
            {
                break;
            }
        }
    }
    public void UnlockCosmetics()
    {
        for(int i = 0; i < GameManager.Instance._catInfoManager.Accessories.Count; i++)
        {
            if (PlayerPrefs.HasKey(GameManager.Instance._catInfoManager.Accessories[i].Name))
            {
                if(GetBool(GameManager.Instance._catInfoManager.Accessories[i].Name))
                {
                    GameManager.Instance._catInfoManager.Accessories[i].AcessoryUnlock = true;
                }
            }
            if (PlayerPrefs.HasKey(GameManager.Instance._catInfoManager.Accessories[i].Name + "Color"))
            {
                if (GetBool(GameManager.Instance._catInfoManager.Accessories[i].Name + "Color"))
                {
                    GameManager.Instance._catInfoManager.Accessories[i].AcessoryColorUnlock = true;
                }
            }
        }
        for (int j = 0; j < GameManager.Instance._catInfoManager.SkinList.Count; j++)
        {
            if (PlayerPrefs.HasKey(GameManager.Instance._catInfoManager.SkinList[j].Name))
            {
                if (GetBool(GameManager.Instance._catInfoManager.SkinList[j].Name))
                {
                    GameManager.Instance._catInfoManager.SkinList[j].Unlocked = true;
                }
            }
        }
    }

    public void RemoveHelpScreens()
    {
        for (int i = 0; i < GameManager.Instance.Levels.Count; i++)
        {
            if (PlayerPrefs.HasKey(GameManager.Instance.Levels[i].name))
            {
                if (GetBool(GameManager.Instance.Levels[i].name) == true)
                {
                    GameManager.Instance.Levels[i].SpecialHelpTxt = false;
                    GameManager.Instance.Levels[i].NewThingIntroduced = false;
                }
            }
            else
            {
                break;
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
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
