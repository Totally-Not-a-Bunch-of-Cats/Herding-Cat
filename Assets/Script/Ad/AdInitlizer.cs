using UnityEngine.Advertisements;
using UnityEngine;

public class AdInitlizer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId = "5257935";
    [SerializeField] string _iOSGameId = "5257934";
    [SerializeField] bool _testMode = true;
    private string gameId;

    private void Start()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            gameId = _iOSGameId;
#elif UNITY_ANDROID
            gameId = _androidGameId;
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, _testMode, this);
        }
    }


    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
