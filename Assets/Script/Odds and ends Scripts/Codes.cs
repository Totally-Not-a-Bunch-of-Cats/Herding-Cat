using TMPro;
using UnityEngine;

public class Codes : MonoBehaviour
{
    //codes to do dev shit 
    private string AdsBeGone = "CatAdsGone";
    private string GiveMeStars = "CatStars50";
    private string GiveMeMoreStars = "CatStars100";
    private string PurchaseBack = "";

    public GameObject InputField;

    public void CheckInput()
    {
        if(InputField.GetComponent<TMP_InputField>().text == AdsBeGone)
        {
            if (!PlayerPrefs.HasKey("CatAdsGone"))
            {
                GameManager.Instance.Purchasemade();
                Debug.Log("mep");
            }
        }
        if(InputField.GetComponent<TMP_InputField>().text == GiveMeStars)
        {
            if(!PlayerPrefs.HasKey("CatStars50"))
            {
                GameManager.Instance.StarCount += 50;
                GameManager.Instance._PlayerPrefsManager.SaveInt("StarCount", GameManager.Instance.StarCount);
                Debug.Log("mep");
            }
        }
        if (InputField.GetComponent<TMP_InputField>().text == GiveMeMoreStars)
        {
            if (!PlayerPrefs.HasKey("CatStars100"))
            {
                GameManager.Instance.StarCount += 100;
                GameManager.Instance._PlayerPrefsManager.SaveInt("StarCount", GameManager.Instance.StarCount);
                Debug.Log("mep");
            }
        }
    }
}
