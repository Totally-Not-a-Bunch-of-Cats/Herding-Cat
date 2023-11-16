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
    public GameObject PurchaseFailure1;
    public GameObject PurchaseFailure2;
    [SerializeField] GameObject PurchaseButton;

    private void OnEnable()
    {
        if(AcessoryName != "NA")
        {
            for (int i = 0; i < GameManager.Instance._catInfoManager.Accessories.Count; i++)
            {
                if (GameManager.Instance._catInfoManager.Accessories[i].Name == AcessoryName
                    && GameManager.Instance._catInfoManager.Accessories[i].AcessoryUnlock == true
                    && AcessoryColorName == "NA")
                {
                    PurchaseButton.SetActive(false);
                    break;
                }
                if(GameManager.Instance._catInfoManager.Accessories[i].Name == AcessoryName
                    && GameManager.Instance._catInfoManager.Accessories[i].AcessoryColorUnlock == true)
                {
                    PurchaseButton.SetActive(false);
                    break;
                }
            }
        }
        if(SkinName != "NA")
        {
            for (int j = 0; j < GameManager.Instance._catInfoManager.SkinList.Count; j++)
            {
                if (GameManager.Instance._catInfoManager.SkinList[j].Name == SkinName 
                    && GameManager.Instance._catInfoManager.SkinList[j].Unlocked == true)
                {
                    PurchaseButton.SetActive(false);
                    break;
                }
            }
        }
    }
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
                    PurchaseFailure1.SetActive(true);
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
            for (int i = 0; i < GameManager.Instance._catInfoManager.SkinList.Count; i++)
            {
                if (GameManager.Instance._catInfoManager.SkinList[i].Name == SkinName)
                {
                    SelectedSkin = i;
                    break;
                }
            }
            if (GameManager.Instance.StarCount >= GameManager.Instance._catInfoManager.SkinList[SelectedSkin].Cost)
            {
                GameManager.Instance._PlayerPrefsManager.SaveBool(SkinName, true);
                GameManager.Instance._catInfoManager.SkinList[SelectedSkin].Unlocked = true;
                GameManager.Instance.StarCount -= GameManager.Instance._catInfoManager.SkinList[SelectedSkin].Cost;
                GameManager.Instance._PlayerPrefsManager.SaveInt("StarCount", GameManager.Instance.StarCount);
            }
            else
            {
                //make error screen for not enough stars
                Debug.Log("oops all tears");
                PurchaseFailure1.SetActive(true);
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
            Debug.Log(AcessoryName);
            if (GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryUnlock == true)
            {
                if (GameManager.Instance.StarCount >= GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryColorCost)
                {
                    Debug.Log(GameManager.Instance.StarCount >= GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryColorCost);
                    GameManager.Instance._PlayerPrefsManager.SaveBool(AcessoryName + "Color", true);
                    GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryColorUnlock = true;
                    GameManager.Instance.StarCount -= GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryColorCost;
                    GameManager.Instance._PlayerPrefsManager.SaveInt("StarCount", GameManager.Instance.StarCount);
                }
                else
                {
                    //make error screen for not enough stars
                    Debug.Log("oops all tears");
                    PurchaseFailure1.SetActive(true);
                }
            }
            else
            {
                //make error screen for not enough stars
                Debug.Log("oops all tears");
                PurchaseFailure2.SetActive(true);
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
    public void DisableColorButton(GameObject button)
    {
        if (GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].AcessoryColorUnlock == true)
        {
            button.SetActive(false);
        }
    }
}
