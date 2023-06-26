using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

/// <summary>
/// Controls the Help GUI
/// </summary>
public class HelpGUIController : MonoBehaviour
{

    //None = 0, General = 1, Item = 2, Trap = 3
    [SerializeField] private int SelectedOption = 0;
    [SerializeField] private List<HelpInfo> SelectedList;

    [SerializeField] private int HelpPages = 0;
    [SerializeField] private int ButtonsPerPage = 0;
    [SerializeField] private int CurrentPage = 0;
    private bool IsDirect = false;

    [Header("Help Information")]
    [Space]
    public List<HelpInfo> GeneralHelpList;
    public List<HelpInfo> ItemHelpList;
    public List<HelpInfo> TrapHelpList;

    [Header("Objects for Control")]
    [Space]
    [SerializeField] public VideoPlayer VideoPlayer;
    public List<GameObject> ViewPorts;
    [SerializeField] private int ActivePort;
    [SerializeField] private GameObject HelpOptionParent;
    [SerializeField] private GameObject HelpInfo;
    [SerializeField] private Transform DefaultBackground;
    [SerializeField] private TMP_Text HelpText;
    [SerializeField] private GameObject ButtonPrefab;
    [SerializeField] private int ScreenWidth;
    [SerializeField] private int ScreenHight;

    [Header("Object Button selection")]
    [SerializeField] private GameObject DownButtonObject;
    [SerializeField] private GameObject UpButtonObject;
    [SerializeField] private Transform HelpButtonsParent;
    [SerializeField] private GameObject Content;

    /// <summary>
    /// Turns off old info if not direct
    /// </summary>
    private void OnEnable()
    {
        // Shows the Default background/help text and video of the info section
        if (!IsDirect)
        {
            HelpText.gameObject.SetActive(false);
            ViewPorts[ActivePort].SetActive(false);
            DefaultBackground.gameObject.SetActive(true);
        } else
        {
            IsDirect = false;
        }
        DetermineScrenSize();
    }

    private void DetermineScrenSize()
    {
        float ScreenWidth = Screen.width;
        float ScreenHight = Screen.height;
        float ScreenDiv = ScreenWidth / ScreenHight;
        if(ScreenDiv > 2)
        {
            ActivePort = 1;
        }
        else if (ScreenDiv > 1.59f)
        {
            ActivePort = 0;
        }
        else
        {
            ActivePort = 2;
        }
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

        //None = 0, General = 1, Item = 2, Trap = 3
        switch (helpOption)
        {
            case 1:
                SelectedList = GeneralHelpList;
                break;
            case 2:
                SelectedList = ItemHelpList;
                break;
            case 3:
                SelectedList = TrapHelpList;
                break;
        }

        ButtonsPerPage = Mathf.FloorToInt((HelpButtonsParent.GetComponent<RectTransform>().rect.height - 30) / (90 + 15));
        CurrentPage = 1;
        
        // Creates Buttons of the selected Category
        for (int i = 0; i < SelectedList.Count; i++)
        {
            // Creates button
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
        if (HelpPages != 1)
        {
            float Bottom = HelpButtonsParent.GetComponent<RectTransform>().rect.height - (90 + 15) * ButtonsPerPage;
            Content.transform.parent.GetComponent<RectTransform>().offsetMin = new Vector2(0, Bottom);
        }
        UpButtonObject.SetActive(false);
        Content.transform.parent.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        Content.transform.localPosition = Vector3.zero;
    }


    /// <summary>
    /// Sets the Help info to the object of the given name
    /// </summary>
    /// <param name="TileName">Name of object to give info about</param>
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
            ViewPorts[ActivePort].SetActive(true);
            VideoPlayer.targetTexture = (RenderTexture)ViewPorts[ActivePort].GetComponent<RawImage>().texture;
            // Set Help text
            HelpText.text = SelectedList[pos].Description.Replace("%", "\n");
            // Set Help Gif
            VideoPlayer.clip = SelectedList[pos].Video;
        }
    }

    /// <summary>
    /// Shifts to the next page up on button option
    /// </summary>
    public void ScrollUp ()
    {
        CurrentPage--;
        DownButtonObject.SetActive(true);
        float ContentY = Content.transform.localPosition.y - ButtonsPerPage * 105;
        Content.transform.localPosition = new Vector3(Content.transform.localPosition.x,
           ContentY, Content.transform.localPosition.z);
        float Bottom = HelpButtonsParent.GetComponent<RectTransform>().rect.height - (90 + 15) * ButtonsPerPage;
        Content.transform.parent.GetComponent<RectTransform>().offsetMin = new Vector2(0, Bottom - 50);
        if (CurrentPage == 1)
        {
            UpButtonObject.SetActive(false);
            Content.transform.parent.GetComponent<RectTransform>().offsetMin = new Vector2(0, Bottom);
            Content.transform.parent.GetComponent<RectTransform>().offsetMax = Vector2.zero;
        }
    }

    /// <summary>
    /// Shifts to the next page down on button option
    /// </summary>
    public void ScrollDown ()
    {
        CurrentPage++;
        UpButtonObject.SetActive(true);
        float ContentY = Content.transform.localPosition.y + ButtonsPerPage  * 105;
        Content.transform.localPosition = new Vector3(Content.transform.localPosition.x,
            ContentY, Content.transform.localPosition.z);
        float Bottom = HelpButtonsParent.GetComponent<RectTransform>().rect.height - (90 + 15) * ButtonsPerPage;
        Content.transform.parent.GetComponent<RectTransform>().offsetMin = new Vector2(0, Bottom-50);
        Content.transform.parent.GetComponent<RectTransform>().offsetMax = new Vector2(0, -50);
        if (CurrentPage == HelpPages)
        {
            DownButtonObject.SetActive(false);
            Content.transform.parent.GetComponent<RectTransform>().offsetMin = Vector2.zero;
        }
    }

    /// <summary>
    /// Loads the help screen to the selected catagory and object
    /// </summary>
    /// <param name="Cat">Catagory of the help entryparam>
    /// <param name="TileName">Entry to load on help screen</param>
    public void JumpToHelpScreen (int Cat, string TileName)
    {
        IsDirect = true;
        // Turn on Help GUI
        gameObject.SetActive(true);
        GameObject.Find("GUI").SetActive(false);
        // Set Catagry
        SelectHelpCategory(Cat);
        // Turn on help screen entry
        SetHelpInfo(TileName);
    }
}
