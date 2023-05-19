using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameLevels", menuName = "ScriptableObjects/New GameLevels")]
public class GameLevels : ScriptableObject
{
    public List<LevelData> GameLevel;

    public LevelData GetLevel(int level)
    {
        return GameLevel[level];
    }

    public LevelData GetLevelOfName(string name)
    {
        for (int i = 0; i < GameLevel.Count; i++)
        {
            if (GameLevel[i].name == name)
            {
                return GameLevel[i];
            }
        }
        return null;
    }

    public bool CheckListForName(string name)
    {
        for(int i = 0; i < GameLevel.Count; i++)
        {
            if (GameLevel[i].name == name)
            {
                return true;
            }
        }
        return false;
    }

    public string GetNextLevelName(string CurrentName, List<LevelData> LevelList)
    {
        string[] LevelNameParts = CurrentName.Split('-');
        string NextLevelName = LevelNameParts[0] + "-";
        try
        {
            NextLevelName += int.Parse(LevelNameParts[1]) + 1;
        }
        catch (System.FormatException)
        {
            Debug.LogError($"Unable to parse '{LevelNameParts[1]}'");
        }
        if (!LevelList.Exists(level => level.name == NextLevelName))
        {
            NextLevelName = "";
            try
            {
                NextLevelName += int.Parse(LevelNameParts[0]) + 1;
            }
            catch (System.FormatException)
            {
                Debug.LogError($"Unable to parse '{LevelNameParts[1]}'");
            }
            NextLevelName += "-1";
            if (!LevelList.Exists(level => level.name == NextLevelName))
            {
                Debug.LogWarning("No Next Level");
                return "";
            }
            return NextLevelName;
        } else
        {
            return NextLevelName;
        }
        
    }
}
