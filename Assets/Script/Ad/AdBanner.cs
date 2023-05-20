using UnityEngine;
using UnityEngine.Advertisements;

public class AdBanner : MonoBehaviour
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.TOP_CENTER;
    [SerializeField] bool _testMode = true;
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string adUnitId = null;

    private void OnEnable()
    {
#if UNITY_IOS
    string adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
    string adUnitId = _androidAdUnitId;
#endif
        Advertisement.Banner.SetPosition(_bannerPosition);
        LoadBanner();
    }
    private void OnDisable()
    {
        HideBannerAd();
    }
    public void LoadBanner()
    {
        Debug.Log("banner loading");
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(adUnitId, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(adUnitId);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    public void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }
}
