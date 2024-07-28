using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using log4net.Core;

/**
* Author: Declin Anderson
* Version: 1.76.0
* Unity Version: 2021.3.2f1 
*/

//* Creates the Level Select Grid dynamically
public class LevelSelectGeneration : MonoBehaviour
{
    // List of Buttons that will hold all of the levels for the current world
    private List<GameObject> LevelList = new List<GameObject>();

    [SerializeField] private TextMeshProUGUI WorldTitle;
    // Prefab for what the buttons will look like
    [SerializeField] private Transform LevelButtonPrefab;
    // References to the next world and previous Level Group button in the scene
    [SerializeField] private Transform NextLevelGroupButton;
    [SerializeField] private Transform PreviousLevelGroupButton;
    // References to the next world and previous world button in the scene
    [SerializeField] private Transform NextWorldButton;
    [SerializeField] private Transform PreviousWorldButton;
    // Int holding the current world the user is on
    public int LevelNumber = 1;
    public int WorldNumber = 1;

    // Start is called before the first frame update
    private void Start()
    {
        LoadLevelGroup();
    }

    /// <summary>
    /// Creates the level select buttons according to the current world the player is on
    /// </summary>
    /// <param name="CurrentWorld"> The current world that the player is going to </param>
    /// <param name="NumberOfWorlds"> The number of worlds that will be displayed </param>
    public void CreateWorldButtons(int CurrentLevelGroup, int CurrentWorld)
    {
        // Destorying previous buttons that will no longer be used
        foreach (Transform buttonTransform in this.transform)
        {
            Destroy(buttonTransform.gameObject);
        }

        LevelList.Clear();

        // Checking to make sure that there is 10 levels to create and if not reducing the amount of levels created
        int AmountOfButtons = 10;
        bool NotTen = false;
        if (GameManager.Instance.Levels.Count - (100 * (CurrentWorld - 1) + (CurrentLevelGroup * 10)) < 0)
        {
            NotTen = true;
        }
        if (NotTen) { AmountOfButtons = GameManager.Instance.Levels.Count % 10; }

        // Creating 10 buttons for the current world that the player has entered
        for (int i = 0; i < AmountOfButtons; i++)
        {
            // Creates a button in the level select
            Transform levelButtonTransform = Instantiate(LevelButtonPrefab, this.transform);
            // Sets the text of the button to the respective level
            levelButtonTransform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Level: " + CurrentLevelGroup + "-" + (i + 1);
            // Sets the button to active or inactive depending on if the level has been unlocked
            levelButtonTransform.GetComponent<Button>().enabled = GameManager.Instance.Levels[i + ((CurrentLevelGroup - 1) * 10)].GetUnlocked();

            // Creates the action on the button that will load the level associated with the button
            int currentLevel = i + 1;
            levelButtonTransform.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.LevelSelected(CurrentLevelGroup + "-" + currentLevel));
            // Creates the action that updates the current level position to fit with what level you are on
            levelButtonTransform.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.ButtonOfSelectedNum(currentLevel + ((CurrentLevelGroup - 1) * 10)));

            // Colors the stars according to starts earned on the level by the player
            for (int j = 0; j < 3; j++)
            {
                if (GameManager.Instance.Levels[i + ((CurrentLevelGroup - 1) * 10)].StarsEarned > j)
                {
                    levelButtonTransform.GetChild(0).GetChild(j).GetComponent<Image>().color = Color.yellow;
                }
            }

            // Adds it to the list keeping track of the worlds incase they need to be accessed
            LevelList.Add(levelButtonTransform.gameObject);
        }

        // Updates the world value to the world that player is moving towards
        if (CurrentLevelGroup < LevelNumber)
        {
            GameManager.Instance.SetWorldNumber(LevelNumber + (WorldNumber - 1) * 10);
        }
        else if (LevelNumber > CurrentLevelGroup)
        {
            GameManager.Instance.SetWorldNumber(LevelNumber + (WorldNumber - 1) * 10);
        }

        if(CurrentLevelGroup == 1)
        {     
            PreviousLevelGroupButton.gameObject.SetActive(false);
        }
        if(CurrentLevelGroup + (WorldNumber - 1) * 10 == Math.Ceiling((double)(GameManager.Instance.Levels.Count) / 10))
        {
            NextLevelGroupButton.gameObject.SetActive(false);
        }

        if(CurrentWorld == 1)
        {     
            PreviousWorldButton.gameObject.SetActive(false);
        }
        if(CurrentWorld == Math.Ceiling((double)(GameManager.Instance.Levels.Count)/100))
        {
            NextWorldButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Reloads the level to current gen
    /// </summary>
    public void LoadLevelGroup()
    {
        LevelNumber = GameManager.Instance.WorldNumber;
        CreateWorldButtons(LevelNumber, WorldNumber);
    }

    /// <summary>
    /// Moves the player to the next world in the level select
    /// </summary>
    public void NextLevelGroup()
    {
        LevelNumber++;
        GameManager.Instance.SetWorldNumber(LevelNumber);
        CreateWorldButtons(LevelNumber, WorldNumber);

        // Check to see if the user is on the last world
        double NumberOfLevelGroups = ((double)(GameManager.Instance.Levels.Count) / 10) - (WorldNumber - 1) * 10;
        if(NumberOfLevelGroups > 10 * WorldNumber)
        {
            NumberOfLevelGroups = 10;
        }

        if (LevelNumber >= Math.Ceiling(NumberOfLevelGroups))
        {
            NextLevelGroupButton.gameObject.SetActive(false);
        }
        else
        {
            NextLevelGroupButton.gameObject.SetActive(true);
        }

        PreviousLevelGroupButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Moves the player to the previous world in the level select
    /// </summary>
    public void PreviousLevelGroup()
    {
        LevelNumber--;
        GameManager.Instance.SetWorldNumber(LevelNumber);
        CreateWorldButtons(LevelNumber, WorldNumber);

        // Check to see if the user is on the first world
        if (LevelNumber == 1)
        {
            PreviousLevelGroupButton.gameObject.SetActive(false);
        }
        else
        {
            PreviousLevelGroupButton.gameObject.SetActive(true);
        }


        NextLevelGroupButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Moves the player to the next world in the level select
    /// </summary>
    public void NextWorld()
    {
        WorldNumber++;
        LevelNumber = 1;
        CreateWorldButtons(LevelNumber, WorldNumber);

        // Check to see if the user is on the last world
        double NumberOfWorlds = (double)(GameManager.Instance.WorldNumber / 100);

        if (WorldNumber >= Math.Ceiling(NumberOfWorlds))
        {
            NextWorldButton.gameObject.SetActive(false);
          
        }
        else
        {
            NextWorldButton.gameObject.SetActive(true);
        }

        if(GameManager.Instance.Levels.Count > ((WorldNumber - 1) * 100) + 10)
        {
            NextLevelGroupButton.gameObject.SetActive(true);
        }
        else
        {
            NextLevelGroupButton.gameObject.SetActive(false);
        }

        PreviousWorldButton.gameObject.SetActive(true);
        WorldTitle.text = "World " + WorldNumber;
    }

    /// <summary>
    /// Moves the player to the previous world in the level select
    /// </summary>
    public void PreviousWorld()
    {
        WorldNumber--;
        LevelNumber = 1;
        CreateWorldButtons(LevelNumber, WorldNumber);

        // Check to see if the user is on the first world
        if (WorldNumber == 1)
        {
            PreviousWorldButton.gameObject.SetActive(false);
        }
        else
        {
            PreviousWorldButton.gameObject.SetActive(true);
        }

        NextLevelGroupButton.gameObject.SetActive(true);
        NextWorldButton.gameObject.SetActive(true);
        WorldTitle.text = "World " + WorldNumber;
    }
}