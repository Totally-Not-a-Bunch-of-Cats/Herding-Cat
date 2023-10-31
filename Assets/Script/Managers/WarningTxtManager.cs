using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WarningTxtManager : MonoBehaviour
{
    [SerializeField] GameObject Warning;
    [SerializeField] TMP_Text WarningTxt;
    [SerializeField] List<string> WarningList = new List<string>();

    private void OnEnable()
    {
        WarningTxt = Warning.GetComponentInChildren<TMP_Text>();
    }

    public void SwitchTxt(int pos)
    {
        WarningTxt.text = WarningList[pos];
    }
}
