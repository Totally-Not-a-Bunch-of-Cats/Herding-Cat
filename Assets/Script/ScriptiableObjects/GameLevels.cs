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
}
