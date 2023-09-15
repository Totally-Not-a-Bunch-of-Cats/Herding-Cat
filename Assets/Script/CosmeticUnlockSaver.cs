using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CosmeticUnlockSaver : MonoBehaviour
{
    public string Name;
    int SelectedAccessory;

    public void UnlockAccessory()
    {
        if (GameManager.Instance.PlayerPrefsTrue)
        {
            for(int i = 0; i < GameManager.Instance._catInfoManager.Accessories.Count; i++)
            {
                if (GameManager.Instance._catInfoManager.Accessories[i].Name == Name)
                {
                    SelectedAccessory = i;
                    break;
                }
            }
            if (GameManager.Instance.StarCount >= GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].Cost)
            {
                GameManager.Instance._PlayerPrefsManager.SaveBool(Name, true);
                GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].Unlocked = true;
                GameManager.Instance.StarCount -= GameManager.Instance._catInfoManager.Accessories[SelectedAccessory].Cost;
                GameManager.Instance._PlayerPrefsManager.SaveInt("StarCount", GameManager.Instance.StarCount);
            }
            else
            {
                //make error screen for not enough stars
                Debug.Log("oops all tears");
            }

        }
        else
        {
            Debug.Log("playerpref false");
        }
    }
    public void UnlockSkin()
    {

    }
}
