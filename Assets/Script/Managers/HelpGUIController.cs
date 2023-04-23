using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the Help GUI
/// </summary>
public class HelpGUIController : MonoBehaviour
{

    [System.Serializable] public enum HelpOption { None, Item, General, Trap }
    [SerializeField] private HelpOption SelectedOption = HelpOption.None;

    [SerializeField] private GameObject HelpOptionParent;
    [SerializeField] private GameObject HelpInfo;
    [SerializeField] private Transform DefaultBackground; 

    /// <summary>
    /// Selects the Category of help wanted
    /// </summary>
    public void SelectHelpCategory(HelpOption helpOption)
    {
        SelectedOption = helpOption;
        //Show Buttons of selected Category

    }

    public void SetHelpInfo ()
    {
        // Set Help text
        // Set Help Gif
    }
    
}
