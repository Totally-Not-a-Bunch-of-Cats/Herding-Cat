using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuControl : MonoBehaviour
{

    public void RestartLevel ()
    {
        Debug.Log("Restart Initiated");
        StartCoroutine(GameManager.Instance.StartMatch(GameManager.Instance._matchManager.CurrentLevel.name));
    }

    public void ReturnMainMenu ()
    {
        Debug.Log("MainMenu");
        StartCoroutine(GameManager.Instance.SwitchScene("Main Menu"));
    }

}
