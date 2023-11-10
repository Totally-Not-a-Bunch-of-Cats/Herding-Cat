using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject PauseButton;
    public Button RestartButton;
    public Button RewindButton;
    public Button EndTurnButton;

    public void ClosePauseMenu()
    {
        PauseButton.SetActive(true);
        RestartButton.interactable = true;
        RewindButton.interactable = true;
        EndTurnButton.interactable = true;

        PauseMenu.SetActive(false);
        GameManager.Instance._uiManager.Override = true;
    }

    public void RestartLevel()
    {
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

    public void ReturnMainMenu()
    {
        GameManager.Instance.ChangeScene("Main Menu");
    }
    public void NavagateToLevelSeelct()
    {
        GameManager.Instance.ChangeScene("Main Menu");
        StartCoroutine(GameManager.Instance.GotoLevelSecet("Level Select"));
    }
    public void NavagateToCatCust()
    {
        GameManager.Instance.ChangeScene("Main Menu");
        StartCoroutine(GameManager.Instance.GotoLevelSecet("CatMenu"));
    }
}
