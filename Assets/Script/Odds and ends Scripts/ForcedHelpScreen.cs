using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ForcedHelpScreen : MonoBehaviour
{
    public GameObject ExitButton;
    public GameObject ViewPort;
    public VideoPlayer VideoPlayer;
    public GameObject HelpGui;

    private void OnEnable()
    {
        transform.SetAsLastSibling();
        //GameManager.Instance._matchManager.CurrentLevel.TileName
        for(int i = 0; i < HelpGui.GetComponent<HelpGUIController>().GeneralHelpList.Count; i++)
        {
            if(GameManager.Instance._matchManager.CurrentLevel.TileName == HelpGui.GetComponent<HelpGUIController>().GeneralHelpList[i].name)
            {
                VideoPlayer.clip = HelpGui.GetComponent<HelpGUIController>().GeneralHelpList[i].Video;
                break;
            }
        }
        for (int i = 0; i < HelpGui.GetComponent<HelpGUIController>().ItemHelpList.Count; i++)
        {
            if (GameManager.Instance._matchManager.CurrentLevel.TileName == HelpGui.GetComponent<HelpGUIController>().ItemHelpList[i].name)
            {
                VideoPlayer.clip = HelpGui.GetComponent<HelpGUIController>().ItemHelpList[i].Video;
                break;
            }
        }
        for (int i = 0; i < HelpGui.GetComponent<HelpGUIController>().TrapHelpList.Count; i++)
        {
            if (GameManager.Instance._matchManager.CurrentLevel.TileName == HelpGui.GetComponent<HelpGUIController>().TrapHelpList[i].name)
            {
                VideoPlayer.clip = HelpGui.GetComponent<HelpGUIController>().TrapHelpList[i].Video;
                break;
            }
        }
        if(GameManager.Instance.SkipForcedVids == true)
        {
            EnableExitButton();
        }
        else
        {
            StartCoroutine(CountDown());
        }
        
    }
    /// <summary>
    /// Forces player to look at help screen before enabling teh exit button
    /// </summary>
    /// <returns></returns>
    public IEnumerator CountDown()
    {
        yield return new WaitForSeconds(6);
        StartCoroutine(AutoClose());
        EnableExitButton();
    }

    void EnableExitButton()
    {
        ExitButton.SetActive(true);
    }

    public IEnumerator AutoClose()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}
