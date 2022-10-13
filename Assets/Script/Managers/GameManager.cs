using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Number of stars that the player has
    private int CurrentStars = 0;

    //Holds references to the other managers
    public MatchManager _matchManager;
    public static List<LevelData> Levels;

    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static GameManager m_Instance;

    public static GameManager Instance
    {
        get
        {
            if (m_ShuttingDown)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(GameManager) +
                    "' already destroyed. Returning null.");
                return null;
            }
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (GameManager)FindObjectOfType(typeof(GameManager));
                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        m_Instance = singletonObject.AddComponent<GameManager>();
                        singletonObject.name = typeof(GameManager).ToString() + " (Singleton)";
                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }
                return m_Instance;
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

    private void Start()
    {
        Levels = GetAllInstances<LevelData>();
        StartCoroutine(StartMatch());
    }

    //IEnumerator
    IEnumerator StartMatch()
    {
        string level_name = "1-1";

        SceneManager.LoadScene("Match");
        yield return new WaitForEndOfFrame();

        //TODO: Grab match manager from scene
        GameObject _board = GameObject.Find("Board");
        if (_board != null)
        {
            _matchManager = _board.GetComponent<MatchManager>();
            LevelData _currentLevel = Levels.Find(level => level.name == level_name);

            // Init round manager / match
            if (_matchManager.InitMatch(_currentLevel))
            {
                Debug.Log($"Successfully initialized level {level_name}");
                // Start match if initialized
                //_matchManager.StartMatch();
            }
            else
            {
                Debug.LogError("Failed to initialize the match");
            }
        }
    }
}
