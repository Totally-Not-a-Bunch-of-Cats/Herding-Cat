using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatInfoManager : MonoBehaviour
{
    // List of Cat Skins
    [SerializeField] private List<Sprite> CatSkins = new List<Sprite>();
    // List of Accessories
    [SerializeField] private List<Sprite> Accessories = new List<Sprite>();
    // Cat Prefabs
    [SerializeField] private List<Cat> CatPrefabs = new List<Cat>();
    // Cat Prefab Selected
    public GameObject SelectCatPrefab;
    public Sprite CurrentSkin;
    public Sprite CurrentAccessory;
    public int CurrentSelected;

    // Update is called once per frame
    void Update()
    {
        
    }

    // Opens the Customize Screen
    public void GoToCustomize(int Selected, GameObject Reference)
    {
        // Sets the customizatoin screen to active
        Reference.transform.GetChild(1).gameObject.SetActive(true);
        // Gathers the prefab from the scriptable object
        SelectCatPrefab = CatPrefabs[Selected].GetPrefab();
        // Updates the variables that hold the selected cat's customizations and then updates the screen to match the selected
        CurrentSkin = SelectCatPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
        Reference.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = CurrentSkin;
        CurrentAccessory = SelectCatPrefab.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        Reference.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>().sprite = SelectCatPrefab.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite;
        Reference.transform.GetChild(0).gameObject.SetActive(false);
        GameManager.Instance._catInfoManager.CurrentSelected = Selected;
    }

    // Gets the wanted prefab from the scriptable objects
    public GameObject GetCatPrefab(int Wanted)
    {
        return CatPrefabs[Wanted].GetPrefab();
    }

    // Goes to the next skin for the cat to be
    public void NextSkin(GameObject Reference)
    {
        // Holds the current spot in the array that the skin is attached to
        int CurrentSkinIndex = 0;

        // Holds the current spot in the array that the skin is attached to
        for (int i = 0; i < CatSkins.Count; i++)
        {
            if (CatSkins[i] == GameManager.Instance._catInfoManager.CurrentSkin)
            {
                CurrentSkinIndex = i;
            }
        }

        // Go forwards one in the list to swap to the new choice
        CurrentSkinIndex++;

        // Loops the skins back around so that user doesn't have to go back from hitting the walls
        if(CurrentSkinIndex >= CatSkins.Count)
        {
            CurrentSkinIndex = 0;
        }

        // Setting the visual in the customization to fit the new skin, as well as adjusting the scriptable object to the new skin
        GameManager.Instance._catInfoManager.CurrentSkin = CatSkins[CurrentSkinIndex];
        CatPrefabs[GameManager.Instance._catInfoManager.CurrentSelected].GetPrefab().transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance._catInfoManager.CurrentSkin;
        Reference.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.CurrentSkin;
    }

    // Goes to the previous skin for the cat to be
    public void PreviousSkin(GameObject Reference)
    {
        // Holds the current spot in the array that the skin is attached to
        int CurrentSkinIndex = 0;

        // Holds the current spot in the array that the skin is attached to
        for (int i = 0; i < CatSkins.Count; i++)
        {
            if (CatSkins[i] == GameManager.Instance._catInfoManager.CurrentSkin)
            {
                CurrentSkinIndex = i;
            }
        }

        // Go backwards one in the list to swap to the new choice
        CurrentSkinIndex--;

        // Loops the skins back around so that user doesn't have to go back from hitting the walls
        if(CurrentSkinIndex < 0)
        {
            CurrentSkinIndex = CatSkins.Count - 1;
        }

        // Setting the visual in the customization to fit the new skin, as well as adjusting the scriptable object to the new skin
        GameManager.Instance._catInfoManager.CurrentSkin = CatSkins[CurrentSkinIndex];
        CatPrefabs[GameManager.Instance._catInfoManager.CurrentSelected].GetPrefab().transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GameManager.Instance._catInfoManager.CurrentSkin;
        Reference.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.CurrentSkin;
    }

    // Goes to the next accessory for the cat to wear
    public void NextAccessory(GameObject Reference)
    {
        // Holds the current spot in the array that the accessory is attached to
        int CurrentAccessoryIndex = 0;

        // Holds the current spot in the array that the accessory is attached to
        for (int i = 0; i < Accessories.Count; i++)
        {
            if (Accessories[i] == GameManager.Instance._catInfoManager.CurrentAccessory)
            {
                CurrentAccessoryIndex = i;
            }
        }

        // Go forward one in the list to swap to the new choice
        CurrentAccessoryIndex++;

        // Loops the accessory back around so that user doesn't have to go back from hitting the walls
        if(CurrentAccessoryIndex >= Accessories.Count)
        {
            CurrentAccessoryIndex = 0;
        }

        // Setting the visual in the customization to fit the new accessory, as well as adjusting the scriptable object to the new accessory
        GameManager.Instance._catInfoManager.CurrentAccessory = Accessories[CurrentAccessoryIndex];
        CatPrefabs[GameManager.Instance._catInfoManager.CurrentSelected].GetPrefab().transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameManager.Instance._catInfoManager.CurrentAccessory;
        Reference.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.CurrentAccessory;
    }

    // Goes to the previous accessory for the cat to wear
    public void PreviousAccessory(GameObject Reference)
    {
        // Holds the current spot in the array that the accessory is attached to
        int CurrentAccessoryIndex = 0;

        // Holds the current spot in the array that the accessory is attached to
        for (int i = 0; i < Accessories.Count; i++)
        {
            if (Accessories[i] == GameManager.Instance._catInfoManager.CurrentAccessory)
            {
                CurrentAccessoryIndex = i;
            }
        }

        // Go back one in the list to swap to the last choice
        CurrentAccessoryIndex--;

        // Loops the accessory back around so that user doesn't have to go back from hitting the walls
        if(CurrentAccessoryIndex < 0)
        {
            CurrentAccessoryIndex = Accessories.Count - 1;
        }

        // Setting the visual in the customization to fit the new accessory, as well as adjusting the scriptable object to the new accessory
        GameManager.Instance._catInfoManager.CurrentAccessory = Accessories[CurrentAccessoryIndex];
        CatPrefabs[GameManager.Instance._catInfoManager.CurrentSelected].GetPrefab().transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = GameManager.Instance._catInfoManager.CurrentAccessory;
        Reference.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Image>().sprite = GameManager.Instance._catInfoManager.CurrentAccessory;
    }
}