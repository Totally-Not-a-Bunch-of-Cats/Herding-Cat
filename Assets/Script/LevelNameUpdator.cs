using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelNameUpdator : MonoBehaviour
{
    public TMP_Text LevelName;
    public TMP_Text TargetRounds;
    public TMP_Text TargetItems;
    public TMP_Text HelpText;
    public GameObject Htext;
    public GameObject PauseMenu;
    public GameObject PauseButton;
    public Button RestartButton;
    public Button RewindButton;
    public Button EndTurnButton;
    public List<string> SpecHelpText;
    public List<int> SpecHelpLevelNum;

    public void NameUpdate()
    {
        LevelName.text = "Level " + GameManager.Instance._matchManager.CurrentLevel.name;
        TargetRounds.text = "Target Rounds:  " + GameManager.Instance._matchManager.CurrentLevel.TargetRounds;
        TargetItems.text = "Target Items:  " + GameManager.Instance._matchManager.CurrentLevel.TargetItems;
    }

    public void SpecialHelpText(int TextNum)
    {
        Htext.SetActive(true);
        HelpText.text = SpecHelpText[TextNum];
    }

    public void PauseGame()
    {
        PauseMenu.SetActive(true);
        PauseButton.SetActive(false);
        RestartButton.interactable = false;
        RewindButton.interactable = false;
        EndTurnButton.interactable = false;

        GameManager.Instance._uiManager.Override = false;
    }
}
