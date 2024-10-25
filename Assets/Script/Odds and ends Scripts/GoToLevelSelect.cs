using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//* Contains the Function to boot up level select screen
public class GoToLevelSelect : MonoBehaviour
{
    // Passed the name of the level select in the Hierarchy
    public void SwitchToLevelSelect(string name)
    {
        // Looking for the placement of the level select in the Hierarchy
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // Looks for Main Menu object to turn it off
            if (this.transform.GetChild(i).name == "Main Menu")
            {
                // Sets the Main Menu to false
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
            // Looks for the Level Select by name
            if (this.transform.GetChild(i).name == name)
            {
                // Sets the object to true
                this.transform.GetChild(i).gameObject.SetActive(true);
                // Gets the level select object and finds the level select generation script calling the function to load the menu
                this.transform.GetChild(i).GetChild(0).gameObject.GetComponent<LevelSelectGeneration>().LoadLevelGroup();
                // Set the cat customization object to true
                if (GameManager.Instance.WorldNumber > 1)
                {
                    this.transform.GetChild(i).GetChild(4).gameObject.SetActive(true);
                }
            }
        }
    }
}
