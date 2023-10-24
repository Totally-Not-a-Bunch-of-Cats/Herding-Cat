using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatInfoManager : MonoBehaviour
{
    // List of Cat Skins
    //[SerializeField] private List<RuntimeAnimatorController> CatAnim = new List<RuntimeAnimatorController>();
    //public List<Sprite> CatSkins = new List<Sprite>(); //Catskins and cat anims must be same length
    public List<SkinInfo> Cats = new List<SkinInfo>();
    // List of Accessories
    [SerializeField] public List<AcessoryInfo> Accessories = new List<AcessoryInfo>();
    // Cat Prefabs
    [SerializeField] public List<Cat> Catlist = new List<Cat>();
    // list of warnings for the warning
    [SerializeField] public List<string> WarningList = new List<string>();
    //List of colors
    [SerializeField] public List<AcessoryInfo> AccessoryColors = new List<AcessoryInfo>();
    // Cat Prefab Selected
    public Cat SelectCat;
    public RuntimeAnimatorController CurrentAnim;
    public Sprite CurrentSkin;
    public Sprite CurrentAccessory;
    public string CurrentName;
    public int CurrentSelected;
    [SerializeField] int CurrentSkinIndex;
    public int CurrentAccessoryIndex;
    public GameObject Warning;
    // Opens the Customize Screen
    public void GoToCustomize(GameObject Reference)
    {
        // Sets the customizatoin screen to active
        Reference.transform.GetChild(1).gameObject.SetActive(true);
        // Gathers the prefab from the scriptable object
        SelectCat = Catlist[CurrentSelected];
        // Updates the variables that hold the selected cat's customizations and then updates the screen to match the sele
        CurrentAnim = SelectCat.AnimationController;
        CurrentSkin = SelectCat.Skin;
        Reference.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = CurrentSkin;
        CurrentAccessory = SelectCat.Acessory1;
        CurrentName = SelectCat.nameofAcessory1;
        CurrentAccessoryIndex = GetAccessoryIndex();
        Reference.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>().sprite = Catlist[CurrentSelected].Acessory1;
        Reference.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<RectTransform>().offsetMax = -Accessories[CurrentAccessoryIndex].MaxoffsetforCatButton;
        Reference.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<RectTransform>().offsetMin = Accessories[CurrentAccessoryIndex].MinoffsetforCatButton;
        CurrentSkinIndex = GetSkinIndex();
    }

    int GetSkinIndex()
    {
        for (int i = 0; i < Cats.Count; i++)
        {
            if (Cats[i].Skin == CurrentSkin)
            {
                return i;
            }
        }
        return -10;
    }

    public int GetAccessoryIndex()
    {
        for (int i = 0; i < Accessories.Count; i++)
        {
            if (Accessories[i].Name == CurrentName)
            {
                return i;
            }
        }
        return -10;
    }
    //finish me
    int GetColorIndex()
    {
        for (int i = 0; i < Cats.Count; i++)
        {
            if (Cats[i].Skin == CurrentSkin)
            {
                return i;
            }
        }
        return -10;
    }
    public void GetCurrentCatNum(int Catnum)
    {
        CurrentSelected = Catnum;
    }

    // Gets the wanted prefab from the scriptable objects
    public GameObject GetCatPrefab(int Wanted)
    {
        return Catlist[Wanted].GetPrefab();
    }

    // Goes to the next skin for the cat to be
    public void NextSkin(GameObject Reference)
    {
        // Go forwards one in the list to swap to the new choice
        CurrentSkinIndex++;

        // Loops the skins back around so that user doesn't have to go back from hitting the walls
        if(CurrentSkinIndex >= Cats.Count)
        {
            CurrentSkinIndex = 0;
        }

        // Setting the visual in the customization to fit the new skin, as well as adjusting the scriptable object to the new skin
        CurrentAnim = Cats[CurrentSkinIndex].CatAnim;
        Catlist[CurrentSelected].AnimationController = CurrentAnim;
        CurrentSkin = Cats[CurrentSkinIndex].Skin;
        Catlist[CurrentSelected].Skin = CurrentSkin;
        Reference.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = CurrentSkin;
    }

    // Goes to the previous skin for the cat to be
    public void PreviousSkin(GameObject Reference)
    {

        // Go backwards one in the list to swap to the new choice
        CurrentSkinIndex--;

        // Loops the skins back around so that user doesn't have to go back from hitting the walls
        if(CurrentSkinIndex < 0)
        {
            CurrentSkinIndex = Cats.Count - 1;
        }
        // Setting the visual in the customization to fit the new skin, as well as adjusting the scriptable object to the new skin
        CurrentAnim = Cats[CurrentSkinIndex].CatAnim;
        Catlist[CurrentSelected].AnimationController = CurrentAnim;
        CurrentSkin = Cats[CurrentSkinIndex].Skin;
        Catlist[CurrentSelected].Skin = CurrentSkin;
        Reference.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = CurrentSkin;
    }

    // Goes to the next accessory for the cat to wear
    public void NextAccessory(GameObject Reference)
    {
        // Go forward one in the list to swap to the new choice
        CurrentAccessoryIndex++;

        // Loops the accessory back around so that user doesn't have to go back from hitting the walls
        if(CurrentAccessoryIndex >= Accessories.Count)
        {
            CurrentAccessoryIndex = 0;
        }
        //actives the warning if you dont own the accessory
        if (Accessories[CurrentAccessoryIndex].Unlocked == false)
        {
            Warning.GetComponentInChildren<TMPro.TMP_Text>().text = WarningList[0];
            Debug.Log(Warning.GetComponent<Image>().color);
            Warning.GetComponent<Image>().color = new Color(0,0,1,1);
            Debug.Log(Warning.GetComponent<Image>().color);
        }
        // Setting the visual in the customization to fit the new accessory, as well as adjusting the scriptable object to the new accessory
        CurrentAccessory = Accessories[CurrentAccessoryIndex].Acessory;
        Transform ReferenceChild = Reference.transform.GetChild(1).GetChild(1).GetChild(1);
        ReferenceChild.GetComponent<Image>().sprite = CurrentAccessory;
        ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[CurrentAccessoryIndex].MaxoffsetforCatButton;
        ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[CurrentAccessoryIndex].MinoffsetforCatButton;
        Catlist[CurrentSelected].Acessory1 = CurrentAccessory;
        Catlist[CurrentSelected].nameofAcessory1 = Accessories[CurrentAccessoryIndex].Name;
        Catlist[CurrentSelected].Acessory1ListNum = CurrentAccessoryIndex;
    }

    // Goes to the previous accessory for the cat to wear
    public void PreviousAccessory(GameObject Reference)
    {
        // Go back one in the list to swap to the last choice
        CurrentAccessoryIndex--;

        // Loops the accessory back around so that user doesn't have to go back from hitting the walls
        if(CurrentAccessoryIndex < 0)
        {
            CurrentAccessoryIndex = Accessories.Count - 1;
        }
        //actives the warning if you dont own the accessory
        if (Accessories[CurrentAccessoryIndex].Unlocked == false)
        {
            Warning.SetActive(true);
            Warning.GetComponentInChildren<TMPro.TMP_Text>().text = WarningList[0];
        }
        // Setting the visual in the customization to fit the new accessory, as well as adjusting the scriptable object to the new accessory
        CurrentAccessory = Accessories[CurrentAccessoryIndex].Acessory;
        //Catlist[CurrentSelected].GetPrefab().transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = CurrentAccessory;
        //Catlist[CurrentSelected].GetPrefab().transform.GetChild(1).position = Accessories[CurrentAccessoryIndex].CatPrefabLocation;
        Transform ReferenceChild = Reference.transform.GetChild(1).GetChild(1).GetChild(1);
        ReferenceChild.GetComponent<Image>().sprite = CurrentAccessory;
        ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[CurrentAccessoryIndex].MaxoffsetforCatButton;
        ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[CurrentAccessoryIndex].MinoffsetforCatButton;
        Catlist[CurrentSelected].Acessory1 = CurrentAccessory;
        Catlist[CurrentSelected].nameofAcessory1 = Accessories[CurrentAccessoryIndex].Name;
        Catlist[CurrentSelected].Acessory1ListNum = CurrentAccessoryIndex;
    }
    public void NextColor(GameObject Reference)
    {

    }
    public void PreviousColor(GameObject Reference)
    {

    }
}
