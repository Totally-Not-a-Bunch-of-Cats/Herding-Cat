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

    //None = 0, Item = 1, General = 2, Trap = 3
    [SerializeField] private int SelectedOption = 0;
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
    [SerializeField] private Transform HelpButtonsParent;
    [SerializeField] private Transform DefaultBackground;
    [SerializeField] private TMP_Text HelpText;
    [SerializeField] private RawImage GIFImage;
    [SerializeField] private GameObject ButtonPrefab;


    private void OnEnable()
    {
        // Shows the Default background/help text and video of the info section
        HelpText.gameObject.SetActive(false);
        GIFImage.gameObject.SetActive(false);
        DefaultBackground.gameObject.SetActive(true);
    }

    /// <summary>
    /// Selects the Category of help wanted
    /// </summary>
    public void SelectHelpCategory(int helpOption)
    {
        if (helpOption < 0 || helpOption > 3)
        {
            Debug.LogWarning("Help Button Not in range: " + helpOption);
        }
        SelectedOption = helpOption;

        // Show Buttons of selected Category
        SelectedList = new List<HelpInfo>();
        for (int i = 0; i < HelpButtonsParent.childCount; i++)
        {
            // Destroy old Buttons
            Destroy(HelpButtonsParent.GetChild(i).gameObject);
        }

        //None = 0, Item = 1, General = 2, Trap = 3
        switch (helpOption)
        {
            case 2:
                SelectedList = GeneralHelpList;
                break;
            case 1:
                SelectedList = ItemHelpList;
                break;
            case 3:
                SelectedList = TrapHelpList;
                break;
        }

        for (int i = 0; i < SelectedList.Count; i++)
        {
            // Creates button
            GameObject temp = Instantiate(ButtonPrefab, Vector3.zero, Quaternion.identity, HelpButtonsParent);
            // Places the icon of the button
            temp.transform.GetChild(0).GetComponent<Image>().sprite = SelectedList[i].Icon;
            //temp.name = SelectedList[i].name + " Button";
            // Sets Listener
            
            string test = SelectedList[i].name;
            Debug.Log(test);
            temp.GetComponent<Button>().onClick.AddListener(() => SetHelpInfo(test));
        }
    }

    /// <summary>
    /// Sets the 
    /// </summary>
    public void SetHelpInfo (string TileName)
    {
        int pos = -1;
        for (int i = 0; i < SelectedList.Count; i++)
        {
            if (SelectedList[i].name == TileName)
            {
                pos = i;
                break;
            }
        }
        
        if (pos != -1)
        {
            DefaultBackground.gameObject.SetActive(false);
            HelpText.gameObject.SetActive(true);
            GIFImage.gameObject.SetActive(true);
            // Set Help text
            HelpText.text = SelectedList[pos].Description.Replace("%", "\n");
            // Set Help Gif
            GIFImage.texture = SelectedList[pos].Video;
        }
        
    }

}
