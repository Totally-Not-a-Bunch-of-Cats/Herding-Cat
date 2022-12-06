using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour
{

    public void RestartLevel ()
    {
        Debug.Log("Restart Initiated");
        GameManager.Instance.StartCoroutine(GameManager.Instance.StartMatch(GameManager.Instance._matchManager.CurrentLevel.name));
    }

    public void NextLevel()
    {
        Debug.Log("Next Level");
        LevelData NextLevel = GameManager.Instance.Levels[GameManager.Instance.LevelPosition++];
        Debug.Log("LevelPosition is equal to: " + GameManager.Instance.LevelPosition);
        Debug.Log("The Next Level should be 1-2 but its: " + NextLevel.name);
        GameManager.Instance.StartCoroutine(GameManager.Instance.StartMatch(NextLevel.name));
    }

    public void ReturnMainMenu ()
    {
        Debug.Log("MainMenu");
        StartCoroutine(GameManager.Instance.SwitchScene("Main Menu"));
    }

}
