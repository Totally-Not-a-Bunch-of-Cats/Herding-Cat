using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Controls the Help GUI
/// </summary>
public class HelpGUIController : MonoBehaviour
{

    [System.Serializable] public enum HelpOption { None, Item, General, Trap }
    [SerializeField] private HelpOption SelectedOption = HelpOption.None;
    [SerializeField] private List<HelpInfo> SelectedList;

    [Header("Help Information")]
    [Space]
    public List<HelpInfo> GeneralHelpList;
    public List<HelpInfo> ItemHelpList;
    public List<HelpInfo> TrapHelpList;

    [Header("Objects for Control")]
    [Space]
    [SerializeField] private GameObject HelpOptionParent;
    [SerializeField] private GameObject HelpInfo;
    [SerializeField] private Transform DefaultBackground;
    [SerializeField] private TMP_Text HelpText;
    [SerializeField] private RawImage GIFImage;
    [SerializeField] private GameObject ButtonPrefab;

    /// <summary>
    /// Selects the Category of help wanted
    /// </summary>
    public void SelectHelpCategory(HelpOption helpOption)
    {
        SelectedOption = helpOption;
        // Shows the Default background/help text and video of the info section
        DefaultBackground.gameObject.SetActive(true);
        HelpText.gameObject.SetActive(false);
        GIFImage.gameObject.SetActive(false);

        // Show Buttons of selected Category
        SelectedList = new List<HelpInfo>();
        for (int i = 0; i < SelectedList.Count; i++)
        {
            // Destroy old Buttons
        }
        switch (helpOption)
        {
            case HelpOption.General:
                SelectedList = GeneralHelpList;
                break;
            case HelpOption.Item:
                SelectedList = ItemHelpList;
                break;
            case HelpOption.Trap:
                SelectedList = TrapHelpList;
                break;
        }
        for (int i = 0; i < SelectedList.Count; i++)
        {
            // Creates button
            GameObject temp = Instantiate(ButtonPrefab, Vector3.zero, Quaternion.identity, transform);
            // Sets Listener

        }
    }

    /// <summary>
    /// Sets the 
    /// </summary>
    public void SetHelpInfo (int i)
    {
        DefaultBackground.gameObject.SetActive(false);
        // Set Help text
        HelpText.text = SelectedList[i].Description;
        // Set Help Gif
        GIFImage.texture = SelectedList[i].Video;
    }

}
