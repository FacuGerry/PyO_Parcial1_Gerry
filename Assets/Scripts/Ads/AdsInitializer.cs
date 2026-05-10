using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] private string _androidGameId;
    [SerializeField] private bool _testMode = true;
    private string _gameId;

    private AdsBanner _banner;
    private AdsInterstitial _interstitial;
    private AdsRewarded _rewarded;

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        InitializeAds();
#endif

        _banner = GetComponent<AdsBanner>();
        _interstitial = GetComponent<AdsInterstitial>();
        _rewarded = GetComponent<AdsRewarded>();
    }

    public void InitializeAds()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        _gameId = _androidGameId;
#endif

        if (!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        _banner.LoadBanner();
        _interstitial.LoadAd();
        _rewarded.LoadAd();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error} - {message}");
    }
}