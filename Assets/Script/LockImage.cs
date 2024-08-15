using TMPro;
using UnityEngine;

//* Handles displaying the lock image on the levels in level select
public class LockImage : MonoBehaviour
{
    // Reference the game object that is the lock symbol
    public GameObject Lock;

    // The string that is the name of the level
    public TMP_Text levelString;

    // The name of the level
    private string LevelName;

    private void Start()
    {
        //Looks through the levels list for the level of the same name
        for (int i = 0; i < GameManager.Instance.Levels.Count; i++)
        {
            // Creates the string that the lock is comparing to
            LevelName = "Level: " + GameManager.Instance.Levels[i].name;
            // Compares the name of the string and if the level is unlocked
            if (LevelName == levelString.text && GameManager.Instance.Levels[i].GetUnlocked())
            {
                // Set the lock image to false if the level is unlocked
                Lock.SetActive(false);
                break;
            }
        }
    }
}
