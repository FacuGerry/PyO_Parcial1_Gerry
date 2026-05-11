using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener
{
    public bool isTestMode = true;
    public BannerManager banner;
    public InterstitialManager interstitial;
    public RewardedAdManager rewardedAd;

    private string _gameId;

    void Awake()
    {
#if UNITY_ANDROID
        _gameId = "6104391";
#elif UNITY_WEBGL
        Destroy(gameObject);
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, isTestMode, this);
        }
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        banner.Show();
        interstitial.Initialize(banner);
        rewardedAd.Initialize(banner);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}
