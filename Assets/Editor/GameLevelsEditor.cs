using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameLevels))]

public class GameLevelsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameLevels myScript = (GameLevels)target;

        // Inspector button to get all levels in order
        if (GUILayout.Button("Get Levels"))
        {
            string CurrentLevelName = "1-0";
            List<LevelData> PossibleLevels = GetAllInstances<LevelData>();
            List<LevelData> NewLevelsList = new List<LevelData>();

            for (int i = 0; i < PossibleLevels.Count; i++)
            {
                string NextLevel = myScript.GetNextLevelName(CurrentLevelName, PossibleLevels);
                if (NextLevel != "")
                {
                    NewLevelsList.Add(PossibleLevels.Find(level => level.name == NextLevel));
                    CurrentLevelName = NextLevel;
                }
                else
                {
                    break;
                }
            }
            myScript.GameLevel = NewLevelsList;
        }

        // Inspector button to unlock all levels
        if (GUILayout.Button("Unlock Levels"))
        {
            for (int i = 0; i < myScript.GameLevel.Count; i++)
            {
                myScript.GameLevel[i].SetUnlocked(true);
            }
        }

        // Inspector Button to Reset levels back to default(all except level 1-1 locked and no stars
        if (GUILayout.Button("Reset Levels"))
        {
            for (int i = 0; i < myScript.GameLevel.Count; i++)
            {
                myScript.GameLevel[i].StarsEarned = 0;
                if (i != 0)
                {
                    myScript.GameLevel[i].SetUnlocked(false);
                }
            }
        }
    }

    /// <summary>
    /// Get all instances of scriptable objects with given type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<T> GetAllInstances<T>() where T : ScriptableObject
    {
        return AssetDatabase.FindAssets($"t: {typeof(T).Name}").ToList()
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<T>)
                    .ToList();
    }

}
