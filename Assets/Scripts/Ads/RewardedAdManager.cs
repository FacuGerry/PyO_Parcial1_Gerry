using System;
using UnityEngine;
using UnityEngine.Advertisements;

public class RewardedAdManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public event Action OnAdPlayed;

    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    private string _adUnitId = null;
    public bool adLoaded { get; private set; }
    public bool hasToLoadButton { get; private set; }
    private BannerManager _theBanner;

    private bool _isFirstLoad;

    private void Start()
    {
#if UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        _isFirstLoad = true;
        hasToLoadButton = false;
    }

    internal void Initialize(BannerManager banner)
    {
        _theBanner = banner;
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowRewardedAd()
    {
        if (adLoaded)
        {
            _theBanner.HideBanner();
            Advertisement.Show(_adUnitId, this);
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Rewarded Ad: Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        Debug.Log("mostrando interstitial");
    }

    public void OnUnityAdsShowClick(string _adUnitId)
    {
        Debug.Log("clickearon el ad");
    }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (_adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            OnAdPlayed?.Invoke();
        }
        _theBanner.ShowBanner();

        adLoaded = false;

        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        adLoaded = true;
        if (_isFirstLoad)
        {
            hasToLoadButton = true;
            _isFirstLoad = false;
        }
        else
            hasToLoadButton = false;
    }

    public void ShowAdButton()
    {
        hasToLoadButton = true;
    }
}
