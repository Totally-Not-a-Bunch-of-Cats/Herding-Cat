using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WarningTxtManager : MonoBehaviour
{
    [SerializeField] GameObject ConfirmButton;
    [SerializeField] GameObject Warning;
    [SerializeField] TMP_Text WarningTMP;
    [SerializeField] string WarningTxt;
    [SerializeField] List<string> WarningList = new List<string>();

    private void OnEnable()
    {
        WarningTMP = Warning.GetComponentInChildren<TMP_Text>();
    }

    public void SwitchTxt(int pos)
    {
        if(WarningTxt != WarningList[pos])
        {
            WarningTxt += WarningList[pos];
        }
        DisplayTxt();
        DisableConfirmButton();
    }

    void DisplayTxt()
    {
        Warning.SetActive(true);
        WarningTMP.text = WarningTxt;
    }

    void DisableConfirmButton()
    {
        ConfirmButton.GetComponent<Button>().interactable = false;
        ConfirmButton.GetComponent<Image>().color = new Color(0.238341f, 0.3383688f, 0.490566f, 1);
    }
}
