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
        StartCoroutine(CountDown());
        
    }

    public IEnumerator CountDown()
    {
        yield return new WaitForSeconds(7);
        EnableExitButton();
    }

    void EnableExitButton()
    {
        ExitButton.SetActive(true);
    }
}
