using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


[CustomEditor(typeof(GameLevels))]

public class GameLevelsEditor : Editor
{
    public bool ActiveEditor = false;

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
                if (myScript.GameLevel[i].name == "1-1" || myScript.GameLevel[i].name == "1-6" ||  myScript.GameLevel[i].name == "2-6" || myScript.GameLevel[i].name == "3-1" || myScript.GameLevel[i].name == "4-1" || myScript.GameLevel[i].name == "4-6")
                {
                    myScript.GameLevel[i].ResetVids();
                }
            }
        }

        // Inspector Button to enter playmode on the level design GUI
        if (GUILayout.Button("Level Design"))
        {
            //EditorApplication.EnterPlaymode();
            //ActiveEditor = true;
            if (EditorApplication.isPlaying)
            {
                SceneManager.LoadScene("Level Creation", LoadSceneMode.Single);
            } else
            {
                Debug.LogWarning("Must be in play Mode");
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
