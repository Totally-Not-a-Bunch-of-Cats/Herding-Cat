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
    [Header("Saved Values")]
    [Tooltip("Number of stars that the player has")]
    public int StarCount;
    [Tooltip("Furthest level player has unlocked")]
    public string FurthestLevel;

    [Header("Sub Managers")]
    public MatchManager _matchManager;
    public UIManager _uiManager;
    public ScreenResizeManager _screenResizeManager;
    public ReWindManager _ReWindManager;
    public PlayerPrefsManager _PlayerPrefsManager;
    public CatInfoManager _catInfoManager;
    public MusicManager _musicManager;
    public WarningTxtManager _WarningTxtManager;

    [Header("Misc")]
    //list of all level data
    //public static List<LevelData> Levels;
    public List<LevelData> Levels = new List<LevelData>();
    public GameLevels GamelevelList;
    public int LevelPosition = 0;
    public int WorldNumber = 1;
    //public bool ActivateItemIndicators = false;
    public bool ClearStartHelpScreen = false;
    public bool PurchasedStarBoost = false;
    public bool SkipForcedVids = false;

    [Header("Ad Varables")]
    public int GamesTillRewardAd = 4;
    public int GamesTillMandatoryAd = 10;
    public bool ADsoff = false;

    // Option Varables(Can be changed in options menu)
    [Header("Option Varables")]
    public float CatSpeed = 1;
    public bool ItemIndicators = false;
    public float sfxVolume;
    public bool SFXToggle;
    public float musicVolume;
    public bool MusicToggle;

    // Used for testing to determine if star count for a level should be changed or outputed in console.
    [Header("Toggles to update info")]
    public bool UpdateLevelData = false;
    public bool PlayerPrefsTrue = false;

    // Singleton Varables
    //Check to see if we're about to be destroyed.
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
    /// Runs on start to start the first level
    /// </summary>
    private void Start()
    {
        Application.targetFrameRate = Screen.currentResolution.refreshRate;
        Levels = GamelevelList.GameLevel;
        if (PlayerPrefsTrue)
        {
            _PlayerPrefsManager.LoadSettings();
            _PlayerPrefsManager.CheckLevels();
            _PlayerPrefsManager.RemoveHelpScreens();
        }
        StartCoroutine(SwitchScene("Main Menu"));
        StartCoroutine(StartMenuMusic());
    }

    /// <summary>
    /// Changes a current scene to a scene with a given name
    /// </summary>
    /// <param name="Name">Name of scene to go to</param>
    public void ChangeScene(string Name)
    {
        Instance.StartCoroutine(SwitchScene(Name));
    }

    /// <summary>
    /// Switches scenes
    /// </summary>
    /// <param name="Name">Name of scene that you want to switch to</param>
    public IEnumerator SwitchScene(string Name)
    {
        SceneManager.LoadScene(Name, LoadSceneMode.Single);
        yield return new WaitForEndOfFrame();
        if(Name == "Main Menu")
        {
            yield return new WaitForEndOfFrame();
            GameObject WaringTxt = GameObject.Find("Canvas");
            Instance._WarningTxtManager = WaringTxt.GetComponent<WarningTxtManager>();
        }
    }
    public IEnumerator GotoLevelSecet(string name)
    {
        StartCoroutine(GoingtoLevelSecet(name));
        yield return new WaitForEndOfFrame();
    }
    public IEnumerator GoingtoLevelSecet(string name)
    {
        yield return new WaitForSeconds(.1f);
        GameObject.Find("Canvas").GetComponent<GoToLevelSelect>().SwitchToLevelSelect(name);
    }
    /// <summary>
    /// Starts the level
    /// </summary>
    public void LevelSelected(string level_name)
    {
        Instance.StartCoroutine(StartMatch(level_name));
    }

    /// <summary>
    /// Sets current level position
    /// </summary>
    /// <param name="buttonPressed">Level position that was pressed</param>
    public void ButtonOfSelectedNum(int buttonPressed)
    {
        Instance.LevelPosition = buttonPressed;
    }

    public IEnumerator StartMenuMusic()
    {
        yield return new WaitForSeconds(0);
        Instance._musicManager.PlayMenuSong();
    }

    public void StartMatchMusic()
    {
       Instance._musicManager.RandomTrack();
       Instance._musicManager.PlayTrack();
    }
    /// <summary>
    /// gives the player their bonus stars for levels already completed, when they purchase the +1 stars pack
    /// </summary>
    public void GrantBonusStars()
    {
        for(int i = 0; i < Levels.Count; i++)
        {
            if(Levels[i].StarsEarned < 1)
            {
                Levels[i].StarsEarned += 1;
                Instance._PlayerPrefsManager.SaveInt(Levels[i].name, Levels[i].StarsEarned);
            }
        }
    }

    /// <summary>
    /// starts the match of level 1-1, also checks if the level is already loaded,
    /// so it handles restarting
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartMatch(string level_name)
    {
        //string level_name = "Test";
        //checks to see what current level is and if so it reload the level (update later for better functinality)
        if (SceneManager.GetActiveScene().name != "Match")
        {
            SceneManager.LoadScene("Match");
            yield return new WaitForEndOfFrame();
        }
        else
        {
            yield return new WaitForEndOfFrame();
            SceneManager.LoadScene("Match");
            yield return new WaitForEndOfFrame();
        }

        //loads the board and starts the level by generating a match using the match info and matchmanager
        yield return new WaitForEndOfFrame();

        yield return new WaitForSeconds(.5f);
        GameObject _board = GameObject.Find("Board");
        Instance._uiManager.FindBoard(_board);
        _uiManager.SelectedItem = null;
        if (_board != null)
        {
            Instance._matchManager = _board.GetComponent<MatchManager>();
            LevelData _currentLevel = null;
            //LevelData _currentLevel = Levels.Find(level => level.name == level_name);
            for (int i =0; i < Instance.Levels.Count; i++)
            {
                if(level_name == Instance.Levels[i].name)
                {
                    _currentLevel = Instance.Levels[i];
                    break;
                }
            }

            //LevelPosition = Instance.Levels.IndexOf(_currentLevel) + 1;
            // Init round manager / match
            if (Instance._matchManager.InitMatch(_currentLevel))
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
    public void Purchasemade()
    {
        ADsoff = true;
        _PlayerPrefsManager.SaveBool("Adsoff", true);
    }

    public int GetWorldNumber(){return WorldNumber;}
    public void SetWorldNumber(int SelectedWorld){WorldNumber = SelectedWorld;}
}
