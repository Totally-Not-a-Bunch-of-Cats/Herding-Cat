using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatInfoManager : MonoBehaviour
{
    // List of Cat Skins
    //[SerializeField] private List<RuntimeAnimatorController> CatAnim = new List<RuntimeAnimatorController>();
    //public List<Sprite> CatSkins = new List<Sprite>(); //Catskins and cat anims must be same length
    public List<SkinInfo> SkinList = new List<SkinInfo>();
    //List of Accessories
    [SerializeField] public List<AcessoryInfo> Accessories = new List<AcessoryInfo>();
    //Cat Prefabs
    [SerializeField] public List<Cat> Catlist = new List<Cat>();
    //List of colors for the Hat acessory
    [SerializeField] public List<AcessoryColorInfo> HatAccessoryColors = new List<AcessoryColorInfo>();
    //List of colors for the Headset acessory
    [SerializeField] public List<AcessoryColorInfo> HeadsetColors = new List<AcessoryColorInfo>();
    //List of colors for the Cone acessory
    [SerializeField] public List<AcessoryColorInfo> ConeColors = new List<AcessoryColorInfo>();
    //List of colors for the Flower acessory
    [SerializeField] public List<AcessoryColorInfo> FlowerColors = new List<AcessoryColorInfo>();
    //List of colors for the Flower Hat acessory
    [SerializeField] public List<AcessoryColorInfo> FlowerHatColors = new List<AcessoryColorInfo>();
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
        for (int i = 0; i < SkinList.Count; i++)
        {
            if (SkinList[i].Skin == CurrentSkin)
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
        for (int i = 0; i < SkinList.Count; i++)
        {
            if (SkinList[i].Skin == CurrentSkin)
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
        if(CurrentSkinIndex >= SkinList.Count)
        {
            CurrentSkinIndex = 0;
        }
        //Checks to see if skin is unlocked
        if (SkinList[CurrentSkinIndex].Unlocked == false)
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtSkin(1);
        }
        else
        {
            GameManager.Instance._WarningTxtManager.HideTxtSkin();
            GameManager.Instance._WarningTxtManager.EnableConfirmButton();
        }
        // Setting the visual in the customization to fit the new skin, as well as adjusting the scriptable object to the new skin
        CurrentAnim = SkinList[CurrentSkinIndex].CatAnim;
        Catlist[CurrentSelected].AnimationController = CurrentAnim;
        CurrentSkin = SkinList[CurrentSkinIndex].Skin;
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
            CurrentSkinIndex = SkinList.Count - 1;
        }
        //Checks to see if skin is unlocked
        if (SkinList[CurrentSkinIndex].Unlocked == false)
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtSkin(1);
        }
        else
        {
            GameManager.Instance._WarningTxtManager.HideTxtSkin();
            GameManager.Instance._WarningTxtManager.EnableConfirmButton();
        }
        // Setting the visual in the customization to fit the new skin, as well as adjusting the scriptable object to the new skin
        CurrentAnim = SkinList[CurrentSkinIndex].CatAnim;
        Catlist[CurrentSelected].AnimationController = CurrentAnim;
        CurrentSkin = SkinList[CurrentSkinIndex].Skin;
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
        // Setting the visual in the customization to fit the new accessory, as well as adjusting the scriptable object to the new accessory
        CurrentAccessory = Accessories[CurrentAccessoryIndex].Acessory;
        Transform ReferenceChild = Reference.transform.GetChild(1).GetChild(1).GetChild(1);
        ReferenceChild.GetComponent<Image>().sprite = CurrentAccessory;
        ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[CurrentAccessoryIndex].MaxoffsetforCatButton;
        ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[CurrentAccessoryIndex].MinoffsetforCatButton;
        //Checks to see if acessory is unlocked
        if (Accessories[CurrentAccessoryIndex].AcessoryUnlock == false)
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtAccessory(0);
        }
        else
        {
            GameManager.Instance._WarningTxtManager.HideTxtAccessory();
            GameManager.Instance._WarningTxtManager.EnableConfirmButton();
        }
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
        // Setting the visual in the customization to fit the new accessory, as well as adjusting the scriptable object to the new accessory
        CurrentAccessory = Accessories[CurrentAccessoryIndex].Acessory;
        Transform ReferenceChild = Reference.transform.GetChild(1).GetChild(1).GetChild(1);
        ReferenceChild.GetComponent<Image>().sprite = CurrentAccessory;
        ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[CurrentAccessoryIndex].MaxoffsetforCatButton;
        ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[CurrentAccessoryIndex].MinoffsetforCatButton;
        if (Accessories[CurrentAccessoryIndex].AcessoryUnlock == false)
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtAccessory(0);
        }
        else
        {
            GameManager.Instance._WarningTxtManager.HideTxtAccessory();
            GameManager.Instance._WarningTxtManager.EnableConfirmButton();
        }
        Catlist[CurrentSelected].Acessory1 = CurrentAccessory;
        Catlist[CurrentSelected].nameofAcessory1 = Accessories[CurrentAccessoryIndex].Name;
        Catlist[CurrentSelected].Acessory1ListNum = CurrentAccessoryIndex;
    }
    public void NextColor(GameObject Reference)
    {
        CurrentAccessoryIndex = GetAccessoryIndex();
        if (Accessories[CurrentAccessoryIndex].AcessoryColorCost == 0)
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtAccColor(3);
        }
        else
        {
            if (Accessories[CurrentAccessoryIndex].AcessoryUnlock == false)
            {
                GameManager.Instance._WarningTxtManager.SwitchTxtAccColor(2);
            }
            else
            {
                GameManager.Instance._WarningTxtManager.HideTxtColor();
                GameManager.Instance._WarningTxtManager.EnableConfirmButton();



            }
        }
    }
    public void PreviousColor(GameObject Reference)
    {

        if (Accessories[CurrentAccessoryIndex].AcessoryUnlock == false)
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtAccColor(2);
        }
        else
        {
            GameManager.Instance._WarningTxtManager.HideTxtColor();
            GameManager.Instance._WarningTxtManager.EnableConfirmButton();
        }
    }

    public void Clear()
    {
        Catlist[CurrentSelected].Acessory1 = Accessories[0].Acessory;
        Catlist[CurrentSelected].nameofAcessory1 = Accessories[0].Name;
        Catlist[CurrentSelected].Acessory1ListNum = 0;
        Catlist[CurrentSelected].Skin = SkinList[0].Skin;

    }
}
