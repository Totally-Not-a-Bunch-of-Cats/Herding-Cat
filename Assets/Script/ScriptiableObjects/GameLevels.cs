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
}
