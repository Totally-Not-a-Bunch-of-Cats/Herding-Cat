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
        if (GameManager.Instance.LevelPosition != GameManager.Instance.Levels.Count)
        {
            LevelData NextLevel = GameManager.Instance.Levels[GameManager.Instance.LevelPosition++];
            GameManager.Instance.StartCoroutine(GameManager.Instance.StartMatch(NextLevel.name));
        }
    }

    public void ReturnMainMenu ()
    {
        Debug.Log("MainMenu");
        StartCoroutine(GameManager.Instance.SwitchScene("Main Menu"));
    }

}
