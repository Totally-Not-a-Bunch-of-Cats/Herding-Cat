using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticUnlockSaver : MonoBehaviour
{
    public string AcessoryName = "NA";
    int SelectedAccessory;
    int SelectedSkin;
    public string SkinName = "NA";
    public string AcessoryColorName = "NA";
    public GameObject PurchaseFailure;
    [SerializeField] GameObject PurchaseButton;

    private void OnEnable()
    {
        for (int i = 0; i < GameManager.Instance._catInfoManager.Accessories.Count; i++)
        {
            if (GameManager.Instance._catInfoManager.Accessories[i].Name == AcessoryName 
                && GameManager.Instance._catInfoManager.Accessories[i].AcessoryUnlock == true)
            {
                PurchaseButton.SetActive(false);
                break;
            }
        }
    }
    // && GameManager.Instance._catInfoManager.Accessories[i].Unlocked == true
    public void UnlockAccessory()
    {
        if (GameManager.Instance.PlayerPrefsTrue)
        {
            for(int i = 0; i < GameManager.Instance._catInfoManager.Accessories.Count; i++)
            {
                if (GameManager.Instance._catInfoManager.Accessories[i].Name == AcessoryName)
                {
                    SelectedAccessory = i;
                    break;
                }
            }
            if(GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryUnlock == false)
            {
                if (GameManager.Instance.StarCount >= GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryCost)
                {
                    GameManager.Instance._PlayerPrefsManager.SaveBool(AcessoryName, true);
                    GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryUnlock = true;
                    GameManager.Instance.StarCount -= GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryCost;
                    GameManager.Instance._PlayerPrefsManager.SaveInt("StarCount", GameManager.Instance.StarCount);
                }
                else
                {
                    //make error screen for not enough stars
                    Debug.Log("oops all tears");
                    PurchaseFailure.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("playerpref false");
        }
    }
    public void UnlockSkin()
    {
        if (GameManager.Instance.PlayerPrefsTrue)
        {
            for (int i = 0; i < GameManager.Instance._catInfoManager.Cats.Count; i++)
            {
                if (GameManager.Instance._catInfoManager.Cats[i].Name == SkinName)
                {
                    SelectedSkin = i;
                    break;
                }
            }
            if (GameManager.Instance.StarCount >= GameManager.Instance._catInfoManager.Cats[SelectedSkin].Cost)
            {
                GameManager.Instance._PlayerPrefsManager.SaveBool(SkinName, true);
                GameManager.Instance._catInfoManager.Cats[SelectedSkin].Unlocked = true;
                GameManager.Instance.StarCount -= GameManager.Instance._catInfoManager.Cats[SelectedSkin].Cost;
                GameManager.Instance._PlayerPrefsManager.SaveInt("StarCount", GameManager.Instance.StarCount);
            }
            else
            {
                //make error screen for not enough stars
                Debug.Log("oops all tears");
                PurchaseFailure.SetActive(true);
            }
        }
        else
        {
            Debug.Log("playerpref false");
        }
    }

    public void UnlockAccessoryColor()
    {
        if (GameManager.Instance.PlayerPrefsTrue)
        {
            for (int i = 0; i < GameManager.Instance._catInfoManager.Accessories.Count; i++)
            {
                if (GameManager.Instance._catInfoManager.Accessories[i].Name == AcessoryName)
                {
                    SelectedAccessory = i;
                    break;
                }
            }
            if (GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryUnlock == false)
            {
                if (GameManager.Instance.StarCount >= GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryCost)
                {
                    GameManager.Instance._PlayerPrefsManager.SaveBool(AcessoryName, true);
                    GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryUnlock = true;
                    GameManager.Instance.StarCount -= GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryCost;
                    GameManager.Instance._PlayerPrefsManager.SaveInt("StarCount", GameManager.Instance.StarCount);
                }
                else
                {
                    //make error screen for not enough stars
                    Debug.Log("oops all tears");
                    PurchaseFailure.SetActive(true);
                }
            }
        }
        else
        {
            Debug.Log("playerpref false");
        }
    }

    public void DisableButton(GameObject button)
    {
        if(GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryUnlock == true)
        {
            button.SetActive(false);
        }
    }
}
