using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using log4net.Core;
using System.Diagnostics;

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
    /// <param name="CurrentLevelGroup"> The current level group that will be displayed </param>
    public void CreateWorldButtons(int CurrentLevelGroup, int CurrentWorld)
    {
        // Destorying previous buttons that will no longer be used
        foreach (Transform buttonTransform in this.transform)
        {
            Destroy(buttonTransform.gameObject);
        }

        // Clears the level list to create the new buttons
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
            // Creates a button in the level select using the level button prefab
            Transform levelButtonTransform = Instantiate(LevelButtonPrefab, this.transform);
            // Sets the text of the button to the respective level (EX: "1-1, 1-2, 1-3")
            levelButtonTransform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Level: " + (CurrentLevelGroup + ((CurrentWorld - 1) * 10)) + "-" + (i + 1);
            // Sets the button to active or inactive depending on if the level has been unlocked
            levelButtonTransform.GetComponent<Button>().enabled = GameManager.Instance.Levels[i + ((CurrentLevelGroup - 1) * 10 + (CurrentWorld - 1) * 100)].GetUnlocked();

            // Creates the action on the button that will load the level associated with the button
            int currentLevel = i + 1;
            levelButtonTransform.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.LevelSelected(CurrentLevelGroup + (CurrentWorld - 1) * 10 + "-" + currentLevel));
            // Creates the action that updates the current level position to fit with what level you are on
            levelButtonTransform.GetComponent<Button>().onClick.AddListener(() => GameManager.Instance.ButtonOfSelectedNum(currentLevel + ((CurrentLevelGroup - 1) * 10) + (CurrentWorld - 1) * 100));

            // Colors the stars according to starts earned on the level by the player
            for (int j = 0; j < 3; j++)
            {
                // Looks for if level currently being created has stars already unlocked
                if (GameManager.Instance.Levels[i + ((CurrentLevelGroup - 1) * 10) + (CurrentWorld - 1) * 100].StarsEarned > j)
                {
                    // Gets the star and changes its color to be the correct for finishing it
                    levelButtonTransform.GetChild(0).GetChild(j).GetComponent<Image>().color = Color.yellow;
                }
            }

            // Adds it to the list keeping track of the worlds incase they need to be accessed
            LevelList.Add(levelButtonTransform.gameObject);
        }

        // Updates the level number to the level number the player is going to
        if (CurrentLevelGroup < LevelNumber || LevelNumber > CurrentLevelGroup)
        {
            // The level number is updated to new level number value
            GameManager.Instance.SetWorldNumber(LevelNumber + (CurrentWorld - 1) * 10);
        }

        // Sets the two transfer level buttons to true so they can be turned off if necessary
        PreviousLevelGroupButton.gameObject.SetActive(true);
        NextLevelGroupButton.gameObject.SetActive(true);

        // Checks to see if when the levels have been created if its the first level group
        if (CurrentLevelGroup == 1)
        {
            // Set the previous level button to false since its at the start of the world
            PreviousLevelGroupButton.gameObject.SetActive(false);

        }
        // Checks to see if the current level group is at the end of the world so it doesn't have to create the next button
        if (CurrentLevelGroup + (WorldNumber - 1) * 10 == Math.Ceiling((double)(GameManager.Instance.Levels.Count) / 10))
        {
            // Makes the next level group button inactive
            NextLevelGroupButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Reloads the level to current level group
    /// </summary>
    public void LoadLevelGroup()
    {
        // Gets the current level that the world is on
        switch (GameManager.Instance.WorldNumber)
        {
            case 1:
                LevelNumber = GameManager.Instance.LevelGroupWorld1;
                break;
            case 2:
                LevelNumber = GameManager.Instance.LevelGroupWorld2;
                break;
        }

        // Sets the World number to match the game manager value
        WorldNumber = GameManager.Instance.WorldNumber;

        // Edit the title to be the correct world number
        WorldTitle.text = "World " + WorldNumber;
        // Creates the level buttons based on the current level number and world number
        CreateWorldButtons(LevelNumber, WorldNumber);
    }

    /// <summary>
    /// Moves the player to the next group of levels in the level select
    /// </summary>
    public void NextLevelGroup()
    {
        // Increases the level up to produce the next group of levels
        LevelNumber++;
        switch (GameManager.Instance.WorldNumber)
        {
            case 1:
                GameManager.Instance.LevelGroupWorld1++;
                break;
            case 2:
                GameManager.Instance.LevelGroupWorld2++;
                break;
        }
        // Sets the current level tracker of the world in the gamemanger
        //GameManager.Instance.SetWorldNumber(WorldNumber);

        // Generates the Buttons based current level group and world the player is on
        CreateWorldButtons(LevelNumber, WorldNumber);

        // Check to see if the user is on the last level group by seeing how many level gropus there are then cutting it off based on number of worlds(each world is 100 levels at max)
        double NumberOfLevelGroups = ((double)(GameManager.Instance.Levels.Count) / 10) - (WorldNumber - 1) * 10;

        // If more than 10 level groups reduce to 10 since max 10 level groups a world can have
        if (NumberOfLevelGroups > 10 * WorldNumber)
        {
            NumberOfLevelGroups = 10;
        }

        // if the level number is at the end of the number of levels accessible then doesn't allow moving to the next level group
        if (LevelNumber >= Math.Ceiling(NumberOfLevelGroups))
        {
            // The next level button is made inactive
            NextLevelGroupButton.gameObject.SetActive(false);
        }
        else
        {
            // The next level button is made active since there is a level group to go to
            NextLevelGroupButton.gameObject.SetActive(true);
        }

        // Since the player moved forward a level group it means they have a level group to go back to
        PreviousLevelGroupButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Moves the player to the previous level group in the level select
    /// </summary>
    public void PreviousLevelGroup()
    {
        // Reduces the level down to produce the previous group of levels
        LevelNumber--;

        switch (GameManager.Instance.WorldNumber)
        {
            case 1:
                GameManager.Instance.LevelGroupWorld1--;
                break;
            case 2:
                GameManager.Instance.LevelGroupWorld2--;
                break;
        }
        // Sets the current level tracker of the world in the gamemanger
        //GameManager.Instance.SetWorldNumber(LevelNumber);

        // Generates the Buttons based current level group and world the player is on
        CreateWorldButtons(LevelNumber, WorldNumber);

        // Check to see if the user is on the first group of levels
        if (LevelNumber == 1)
        {
            // Sets the previous level group button to false since the player is at the start of the world
            PreviousLevelGroupButton.gameObject.SetActive(false);
        }
        else
        {
            // Sets the previous level group button to true since the player is not at the start of the world
            PreviousLevelGroupButton.gameObject.SetActive(true);
        }

        // The player is going down a level group so that means there is future levels the player can go to
        NextLevelGroupButton.gameObject.SetActive(true);
    }


    /// <summary>
    /// Goes the world that is passed into the button and searches for what level group the player left that level on
    /// </summary>
    /// <param name="WorldSelected"> The level that the player wishes to travel to
    public void GoToWorld(int WorldSelected)
    {
        // Sets the current world number to that of the world selected
        WorldNumber = WorldSelected;
        GameManager.Instance.WorldNumber = WorldNumber;

        // Depending the World it will pull what level they last left off on
        switch (WorldNumber)
        {
            // If world 1
            case 1:
                // Creates the world according to where the world left off on when the player switched world
                LevelNumber = GameManager.Instance.LevelGroupWorld1;
                CreateWorldButtons(LevelNumber, WorldNumber);
                break;
            // If world 2
            case 2:
                // Creates the world according to where the world left off on when the player switched world
                LevelNumber = GameManager.Instance.LevelGroupWorld2;
                CreateWorldButtons(LevelNumber, WorldNumber);
                break;
        }

        // Edit the title to be the correct world number
        WorldTitle.text = "World " + WorldNumber;
    }
}