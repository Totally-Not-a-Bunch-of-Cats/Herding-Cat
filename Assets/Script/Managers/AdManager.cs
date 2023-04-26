using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
#if UNITY_IOS
    string GameID = "5257934";
#else
    string GameID = "5257935";
#endif
    void Start()
    {
        //InitializeAds();
    }
}
