/** @Author Zachary Boehm, Damian Link, Aaron */
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Runs the Game
/// </summary>
public class GameManager : MonoBehaviour
{
    // Number of stars that the player has
    private int CurrentStars = 0;

    //Holds references to the other managers
    public MatchManager _matchManager;
    public UIManager _uiManager;
    public ScreenResizeManager _screenResizeManager;
    //list of all level data
    //public static List<LevelData> Levels;
    public List<LevelData> Levels = new List<LevelData>();
    // Check to see if we're about to be destroyed.
    private static bool m_ShuttingDown = false;
    private static object m_Lock = new object();
    private static GameManager m_Instance;

    // Used for testing to determine if star count for a level should be changed or outputed in console.
    [SerializeField] public bool UpdateLevelData = false;

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
    //public static List<T> GetAllInstances<T>() where T : ScriptableObject
    //{
    //    return AssetDatabase.FindAssets($"t: {typeof(T).Name}").ToList()
    //                .Select(AssetDatabase.GUIDToAssetPath)
    //                .Select(AssetDatabase.LoadAssetAtPath<T>)
    //                .ToList();
    //}

    /// <summary>
    /// Runs on start to start the first level
    /// </summary>
    private void Start()
    {
        //Levels = GetAllInstances<LevelData>();
        //StartCoroutine(SwitchScene("Menu"));
        StartCoroutine(StartMatch());
    }


    public void ClicktoStart()
    {
        StartCoroutine(StartMatch());
    }

    /// <summary>
    /// Switches scenes
    /// </summary>
    /// <param name="Name">Name of scene that you want to switch to</param>
    public IEnumerator SwitchScene(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Single);
        yield return new WaitForEndOfFrame();
    }

    /// <summary>
    /// Starts the level
    /// </summary>
    public void LevelSelected()
    {
        SceneManager.LoadScene("Match");
    }

    /// <summary>
    /// starts the match of level 1-1, also checks if the level is already loaded,
    /// so it handles restarting
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartMatch()
    {
        string level_name = "Test";
        //checks to see what current level is and if so it reload the level (update later for better functinality)
        if (SceneManager.GetActiveScene().name != "Match")
        {
            SceneManager.LoadScene("Match");
            yield return new WaitForEndOfFrame();
            Debug.Log("im at match");
        }
        else
        {
            yield return new WaitForEndOfFrame();
            SceneManager.LoadScene("Match");
            yield return new WaitForEndOfFrame();
            Debug.Log("restarted");
        }

        //loads the board and starts the level by generating a match using the match info and matchmanager
        yield return new WaitForEndOfFrame();

        GameObject _board = GameObject.Find("Board");
        _uiManager.FindBoard(_board);
        if (_board != null)
        {
            _matchManager = _board.GetComponent<MatchManager>();
            LevelData _currentLevel = Levels.Find(level => level.name == level_name);
            // Init round manager / match
            if (_matchManager.InitMatch(_currentLevel))
            {
                _screenResizeManager.ScaleBoard();
                Debug.Log($"Successfully initialized level {level_name}");
            }
            else
            {
                Debug.LogError("Failed to initialize the match");
            }
        }
    }

}
