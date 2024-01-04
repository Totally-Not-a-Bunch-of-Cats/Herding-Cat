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
    [SerializeField] int CurrentAccessoryIndex;
    [SerializeField] int CurrentAccessoryColorIndex = 0;
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
        GameManager.Instance._catInfoManager.CurrentAccessory = SelectCat.Acessory1;
        CurrentName = SelectCat.nameofAcessory1;
        CurrentAccessoryIndex = GetAccessoryIndex();
        GameManager.Instance._catInfoManager.CurrentAccessoryIndex = GetAccessoryIndex();
        Reference.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>().sprite = Catlist[CurrentSelected].Acessory1;
        Reference.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<RectTransform>().offsetMax = -Accessories[CurrentAccessoryIndex].MaxoffsetforCatButton;
        Reference.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<RectTransform>().offsetMin = Accessories[CurrentAccessoryIndex].MinoffsetforCatButton;
        GameManager.Instance._catInfoManager.CurrentSkinIndex = GetSkinIndex();
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
        GameManager.Instance._catInfoManager.CurrentSkinIndex++;
        // Loops the skins back around so that user doesn't have to go back from hitting the walls
        if (GameManager.Instance._catInfoManager.CurrentSkinIndex >= SkinList.Count)
        {
            GameManager.Instance._catInfoManager.CurrentSkinIndex = 0;
        }
        //Checks to see if skin is unlocked
        if (GameManager.Instance._catInfoManager.SkinList[GameManager.Instance._catInfoManager.CurrentSkinIndex].Unlocked == false)
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtSkin(1);
        }
        else
        {
            GameManager.Instance._WarningTxtManager.HideTxtSkin();
            GameManager.Instance._WarningTxtManager.EnableConfirmButton();
        }
        // Setting the visual in the customization to fit the new skin, as well as adjusting the scriptable object to the new skin
        CurrentAnim = SkinList[GameManager.Instance._catInfoManager.CurrentSkinIndex].CatAnim;
        Catlist[CurrentSelected].AnimationController = CurrentAnim;
        CurrentSkin = SkinList[GameManager.Instance._catInfoManager.CurrentSkinIndex].Skin;
        Catlist[CurrentSelected].Skin = CurrentSkin;
        Reference.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = CurrentSkin;
    }

    // Goes to the previous skin for the cat to be
    public void PreviousSkin(GameObject Reference)
    {

        // Go backwards one in the list to swap to the new choice
        GameManager.Instance._catInfoManager.CurrentSkinIndex--;
        // Loops the skins back around so that user doesn't have to go back from hitting the walls
        if (GameManager.Instance._catInfoManager.CurrentSkinIndex < 0)
        {
            GameManager.Instance._catInfoManager.CurrentSkinIndex = SkinList.Count - 1;
        }
        //Checks to see if skin is unlocked
        if (GameManager.Instance._catInfoManager.SkinList[GameManager.Instance._catInfoManager.CurrentSkinIndex].Unlocked == false)
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtSkin(1);
        }
        else
        {
            GameManager.Instance._WarningTxtManager.HideTxtSkin();
            GameManager.Instance._WarningTxtManager.EnableConfirmButton();
        }
        // Setting the visual in the customization to fit the new skin, as well as adjusting the scriptable object to the new skin
        CurrentAnim = SkinList[GameManager.Instance._catInfoManager.CurrentSkinIndex].CatAnim;
        Catlist[CurrentSelected].AnimationController = CurrentAnim;
        CurrentSkin = SkinList[GameManager.Instance._catInfoManager.CurrentSkinIndex].Skin;
        Catlist[CurrentSelected].Skin = CurrentSkin;
        Reference.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = CurrentSkin;
    }

    // Goes to the next accessory for the cat to wear
    public void NextAccessory(GameObject Reference)
    {
        // Go forward one in the list to swap to the new choice
        GameManager.Instance._catInfoManager.CurrentAccessoryIndex++;
        GameManager.Instance._WarningTxtManager.HideTxtColor();
        // Loops the accessory back around so that user doesn't have to go back from hitting the walls
        if (GameManager.Instance._catInfoManager.CurrentAccessoryIndex >= Accessories.Count)
        {
            GameManager.Instance._catInfoManager.CurrentAccessoryIndex = 0;
        }
        // Setting the visual in the customization to fit the new accessory, as well as adjusting the scriptable object to the new accessory
        GameManager.Instance._catInfoManager.CurrentAccessory = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].Acessory;
        Transform ReferenceChild = Reference.transform.GetChild(1).GetChild(1).GetChild(1);
        ReferenceChild.GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.CurrentAccessory;
        ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
        ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
        //Checks to see if acessory is unlocked
        if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryUnlock == false)
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtAccessory(0);
        }
        else
        {
            GameManager.Instance._WarningTxtManager.HideTxtAccessory();
            GameManager.Instance._WarningTxtManager.EnableConfirmButton();
        }
        Catlist[CurrentSelected].Acessory1 = GameManager.Instance._catInfoManager.CurrentAccessory;
        Catlist[CurrentSelected].nameofAcessory1 = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].Name;
        Catlist[CurrentSelected].Acessory1ListNum = GameManager.Instance._catInfoManager.CurrentAccessoryIndex;
    }

    // Goes to the previous accessory for the cat to wear
    public void PreviousAccessory(GameObject Reference)
    {
        // Go back one in the list to swap to the last choice
        GameManager.Instance._catInfoManager.CurrentAccessoryIndex--;
        GameManager.Instance._WarningTxtManager.HideTxtColor();
        // Loops the accessory back around so that user doesn't have to go back from hitting the walls
        if (GameManager.Instance._catInfoManager.CurrentAccessoryIndex < 0)
        {
            GameManager.Instance._catInfoManager.CurrentAccessoryIndex = Accessories.Count - 1;
        }
        // Setting the visual in the customization to fit the new accessory, as well as adjusting the scriptable object to the new accessory
        GameManager.Instance._catInfoManager.CurrentAccessory = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].Acessory;
        Transform ReferenceChild = Reference.transform.GetChild(1).GetChild(1).GetChild(1);
        ReferenceChild.GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.CurrentAccessory;
        ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
        ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
        if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryUnlock == false)
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtAccessory(0);
        }
        else
        {
            GameManager.Instance._WarningTxtManager.HideTxtAccessory();
            GameManager.Instance._WarningTxtManager.EnableConfirmButton();
        }
        Catlist[CurrentSelected].Acessory1 = GameManager.Instance._catInfoManager.CurrentAccessory;
        Catlist[CurrentSelected].nameofAcessory1 = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].Name;
        Catlist[CurrentSelected].Acessory1ListNum = GameManager.Instance._catInfoManager.CurrentAccessoryIndex;
    }
    public void NextColor(GameObject Reference)
    {
        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex++;
        if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "")
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtAccColor(3);
        }
        else
        {
            if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorUnlock == false)
            {
                Transform ReferenceChild = Reference.transform.GetChild(1).GetChild(1).GetChild(1);
                GameManager.Instance._WarningTxtManager.SwitchTxtAccColor(2);
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "HatAccessoryColors")
                {
                    if (GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex >= HatAccessoryColors.Count)
                    {
                        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex = 0;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = HatAccessoryColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = HatAccessoryColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "HeadsetColors")
                {
                    if (GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex >= GameManager.Instance._catInfoManager.HeadsetColors.Count)
                    {
                        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex = 0;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.HeadsetColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = GameManager.Instance._catInfoManager.HeadsetColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "ConeColors")
                {
                    if (GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex >= ConeColors.Count)
                    {
                        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex = 0;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = ConeColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = ConeColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "FlowerColors")
                {
                    if (GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex >= FlowerColors.Count)
                    {
                        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex = 0;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = FlowerColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = FlowerColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "FlowerHatAccessoryColors")
                {
                    if (GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex >= FlowerHatColors.Count)
                    {
                        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex = 0;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = FlowerHatColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = FlowerHatColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                }
            }
            else
            {
                Transform ReferenceChild = Reference.transform.GetChild(1).GetChild(1).GetChild(1);
                GameManager.Instance._WarningTxtManager.HideTxtColor();
                GameManager.Instance._WarningTxtManager.EnableConfirmButton();
                if(GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "HatAccessoryColors")
                {
                    if (GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex >= HatAccessoryColors.Count)
                    {
                        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex = 0;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = HatAccessoryColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = HatAccessoryColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "HeadsetColors")
                {
                    if (GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex >= GameManager.Instance._catInfoManager.HeadsetColors.Count)
                    {
                        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex = 0;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.HeadsetColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = GameManager.Instance._catInfoManager.HeadsetColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "ConeColors")
                {
                    if (GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex >= ConeColors.Count)
                    {
                        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex = 0;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = ConeColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = ConeColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "FlowerColors")
                {
                    if (GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex >= FlowerColors.Count)
                    {
                        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex = 0;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = FlowerColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = FlowerColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "FlowerHatAccessoryColors")
                {
                    if (GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex >= FlowerHatColors.Count)
                    {
                        GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex = 0;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = FlowerHatColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = FlowerHatColors[GameManager.Instance._catInfoManager.CurrentAccessoryColorIndex].ColorAcessory;
                }
            }
        }
    }
    public void PreviousColor(GameObject Reference)
    {
        CurrentAccessoryColorIndex--;
        //GameManager.Instance._catInfoManager.CurrentAccessoryIndex = GetAccessoryIndex();
        if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "")
        {
            GameManager.Instance._WarningTxtManager.SwitchTxtAccColor(3);
        }
        else
        {
            if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorUnlock == false)
            {
                Transform ReferenceChild = Reference.transform.GetChild(1).GetChild(1).GetChild(1);
                GameManager.Instance._WarningTxtManager.SwitchTxtAccColor(2);
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "HatAccessoryColors")
                {
                    if (CurrentAccessoryColorIndex < 0)
                    {
                        CurrentAccessoryColorIndex = HatAccessoryColors.Count - 1;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = HatAccessoryColors[CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = HatAccessoryColors[CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "HeadsetColors")
                {
                    if (CurrentAccessoryColorIndex < 0)
                    {
                        CurrentAccessoryColorIndex = HeadsetColors.Count - 1;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = HeadsetColors[CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = HeadsetColors[CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "ConeColors")
                {
                    if (CurrentAccessoryColorIndex < 0)
                    {
                        CurrentAccessoryColorIndex = ConeColors.Count - 1;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = ConeColors[CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = ConeColors[CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "FlowerColors")
                {
                    if (CurrentAccessoryColorIndex < 0)
                    {
                        CurrentAccessoryColorIndex = FlowerColors.Count - 1;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = FlowerColors[CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = FlowerColors[CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "FlowerHatAccessoryColors")
                {
                    if (CurrentAccessoryColorIndex < 0)
                    {
                        CurrentAccessoryColorIndex = FlowerHatColors.Count - 1;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = FlowerHatColors[CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = FlowerHatColors[CurrentAccessoryColorIndex].ColorAcessory;
                }
            }
            else
            { 
                Transform ReferenceChild = Reference.transform.GetChild(1).GetChild(1).GetChild(1);
                GameManager.Instance._WarningTxtManager.HideTxtColor();
                GameManager.Instance._WarningTxtManager.EnableConfirmButton();
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "HatAccessoryColors")
                {
                    if (CurrentAccessoryColorIndex < 0)
                    {
                        CurrentAccessoryColorIndex = HatAccessoryColors.Count - 1;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = HatAccessoryColors[CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = HatAccessoryColors[CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "HeadsetColors")
                {
                    if (CurrentAccessoryColorIndex < 0)
                    {
                        CurrentAccessoryColorIndex = HeadsetColors.Count - 1;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = HeadsetColors[CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = HeadsetColors[CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "ConeColors")
                {
                    if (CurrentAccessoryColorIndex < 0)
                    {
                        CurrentAccessoryColorIndex = ConeColors.Count - 1;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = ConeColors[CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = ConeColors[CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "FlowerColors")
                {
                    if (CurrentAccessoryColorIndex < 0)
                    {
                        CurrentAccessoryColorIndex = FlowerColors.Count - 1;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = FlowerColors[CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = FlowerColors[CurrentAccessoryColorIndex].ColorAcessory;
                }
                if (GameManager.Instance._catInfoManager.Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].AcessoryColorListName == "FlowerHatAccessoryColors")
                {
                    if (CurrentAccessoryColorIndex < 0)
                    {
                        CurrentAccessoryColorIndex = FlowerHatColors.Count - 1;
                    }
                    ReferenceChild.GetComponent<Image>().sprite = FlowerHatColors[CurrentAccessoryColorIndex].ColorAcessory;
                    ReferenceChild.GetComponent<RectTransform>().offsetMax = -Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MaxoffsetforCatButton;
                    ReferenceChild.GetComponent<RectTransform>().offsetMin = Accessories[GameManager.Instance._catInfoManager.CurrentAccessoryIndex].MinoffsetforCatButton;
                    Catlist[CurrentSelected].Acessory1 = FlowerHatColors[CurrentAccessoryColorIndex].ColorAcessory;
                }
            }
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
