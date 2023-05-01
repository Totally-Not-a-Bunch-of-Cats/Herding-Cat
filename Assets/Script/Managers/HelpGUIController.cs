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

    [SerializeField] private int HelpPages = 0;
    [SerializeField] private int ButtonsPerPage = 0;
    [SerializeField] private int CurrentPage = 0;

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

    [Header("Object Button selection")]
    [SerializeField] private GameObject DownButtonObject;
    [SerializeField] private GameObject UpButtonObject;
    [SerializeField] private Transform HelpButtonsParent;
    [SerializeField] private GameObject Content;

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
        for (int i = 0; i < Content.transform.childCount; i++)
        {
            // Destroy old Buttons
            Destroy(Content.transform.GetChild(i).gameObject);
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

        ButtonsPerPage = (int)((HelpButtonsParent.GetComponent<RectTransform>().rect.height - 30) / (90 + 15));
        CurrentPage = 1;
        
        // Creates Buttons of the selected Category
        for (int i = 0; i < SelectedList.Count; i++)
        {
            // Creates button
            /*if (i % ButtonsPerPage == 0 && i != 0)
            {
                Transform NewBlankObject = new GameObject().transform;
                NewBlankObject.SetParent(Content.transform);
                NewBlankObject.gameObject.AddComponent<Image>().enabled = false;
            }*/
            GameObject temp = Instantiate(ButtonPrefab, Vector3.zero, Quaternion.identity, Content.transform);
            // Places the icon of the button
            temp.transform.GetChild(0).GetComponent<Image>().sprite = SelectedList[i].Icon;
            if (SelectedList[i].IconScale != Vector3.zero)
            {
                temp.transform.GetChild(0).localScale = SelectedList[i].IconScale;
            }
            temp.name = SelectedList[i].name + " Button";
            // Sets Listener
            
            string test = SelectedList[i].name;
            temp.GetComponent<Button>().onClick.AddListener(() => SetHelpInfo(test));
        }

        HelpPages = SelectedList.Count / ButtonsPerPage;
        if (SelectedList.Count % ButtonsPerPage != 0)
        {
            HelpPages++;
        }

        DownButtonObject.SetActive(HelpPages != 1);
        UpButtonObject.SetActive(false);
        Content.transform.localPosition = Vector3.zero;
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

    public void ScrollUp ()
    {
        CurrentPage--;
        DownButtonObject.SetActive(true);
        float ContentY = Content.transform.localPosition.y - ButtonsPerPage * 105;
        Content.transform.localPosition = new Vector3(Content.transform.localPosition.x,
           ContentY, Content.transform.localPosition.z);
        if (CurrentPage == 1)
        {
            UpButtonObject.SetActive(false);
        }
    }

    public void ScrollDown ()
    {
        CurrentPage++;
        UpButtonObject.SetActive(true);
        float ContentY = Content.transform.localPosition.y + ButtonsPerPage  * 105;
        Content.transform.localPosition = new Vector3(Content.transform.localPosition.x,
            ContentY, Content.transform.localPosition.z);
        if (CurrentPage == HelpPages)
        {
            DownButtonObject.SetActive(false);
        }
    }

    public void JumpToHelpScreen ()
    {
        // Turn on Help GUI
        // Set Catagry
        // Turn on 
        // 
    }
}
