using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WarningTxtManager : MonoBehaviour
{
    [SerializeField] GameObject ConfirmButton;
    [SerializeField] GameObject Warning1;
    [SerializeField] GameObject Warning2;
    [SerializeField] GameObject Warning3;
    [SerializeField] TMP_Text WarningTMP1;
    [SerializeField] TMP_Text WarningTMP2;
    [SerializeField] TMP_Text WarningTMP3;
    [SerializeField] string WarningTxt1 = "";
    [SerializeField] string WarningTxt2 = "";
    [SerializeField] string WarningTxt3 = "";
    [SerializeField] List<string> WarningList = new List<string>();

    private void OnEnable()
    {
        WarningTMP1 = Warning1.GetComponentInChildren<TMP_Text>();
        WarningTMP2 = Warning2.GetComponentInChildren<TMP_Text>();
        WarningTMP3 = Warning3.GetComponentInChildren<TMP_Text>();
    }

    public void SwitchTxtAccessory(int pos)
    {
        if(WarningTxt1 != WarningList[pos] && WarningTxt1 == "")
        {
            WarningTxt1 = WarningList[pos];
            DisplayTxt(WarningTxt1, WarningTMP1, Warning1);
            DisableConfirmButton();
        }
    }
    public void SwitchTxtSkin(int pos)
    {
        if (WarningTxt2 != WarningList[pos] && WarningTxt2 == "")
        {
            WarningTxt2 = WarningList[pos];
            DisplayTxt(WarningTxt2, WarningTMP2, Warning2);
            DisableConfirmButton();
            return;
        }
    }
    public void SwitchTxtAccColor(int pos)
    {
        if (WarningTxt3 != WarningList[pos] && WarningTxt3 == "")
        {
            WarningTxt3 = WarningList[pos];
            DisplayTxt(WarningTxt3, WarningTMP3, Warning3);
            DisableConfirmButton();
            return;
        }
    }

    /// <summary>
    /// updates the warning txt and activates it
    /// </summary>
    void DisplayTxt(string Warningtxt, TMP_Text WarningTMP, GameObject Warning)
    {
        Warning.SetActive(true);
        WarningTMP.text = Warningtxt;
    }
    /// <summary>
    /// deactivates the warning txt for the accessory
    /// </summary>
    public void HideTxtAccessory()
    {
        Warning1.SetActive(false);
        WarningTxt1 = "";
        WarningTMP1.text = WarningTxt1;
    }
    /// <summary>
    /// deactivates the warning txt for the accessory
    /// </summary>
    public void HideTxtSkin()
    {
        Warning2.SetActive(false);
        WarningTxt2 = "";
        WarningTMP2.text = WarningTxt2;
    }
    /// <summary>
    /// deactivates the warning txt for the accessory
    /// </summary>
    public void HideTxtColor()
    {
        Warning3.SetActive(false);
        WarningTxt3 = "";
        WarningTMP3.text = WarningTxt3;
    }
    /// <summary>
    /// disables teh confirm button preventing the player from locking in something they down own,
    /// </summary>
    void DisableConfirmButton()
    {
        ConfirmButton.GetComponent<Button>().interactable = false;
        ConfirmButton.GetComponent<Image>().color = new Color(0.238341f, 0.3383688f, 0.490566f, 1);
    }

    public void EnableConfirmButton()
    {
        if(Warning1.activeInHierarchy == false && Warning2.activeInHierarchy == false && Warning3.activeInHierarchy == false)
        {
            ConfirmButton.GetComponent<Button>().interactable = true;
            ConfirmButton.GetComponent<Image>().color = new Color(0.4481132f, 0.663694f, 1, 1);
        }
    }
}
