using UnityEngine;
using UnityEngine.Advertisements;
public class AdManager : MonoBehaviour
{
#if UNITY_IOS
    string GameID = "5257934";
#elif UNITY_ANDROID
    string GameID = "5257935";
#endif
    void Start()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS || UNITY_ANDROID
        //if (!Advertisement.isInitialized && Advertisement.isSupported)
        //{
        //    Advertisement.Initialize(GameID);
        //}
#endif
    }
}
