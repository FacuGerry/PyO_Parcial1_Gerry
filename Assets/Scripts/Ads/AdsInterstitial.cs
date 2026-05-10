using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInterstitial : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string _androidAdUnitId = "Interstitial_Android";
    private string _adUnitId;
    private bool _adLoaded = false;

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        _adUnitId = _androidAdUnitId;
#endif
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void ShowAd()
    {
        Debug.Log("Showing Ad: " + _adUnitId);
        if (_adLoaded)
            Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        _adLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error} - {message}");
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error} - {message}");
    }

    public void OnUnityAdsShowStart(string _adUnitId) { }
    public void OnUnityAdsShowClick(string _adUnitId) { }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd();

        if (this._adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Ad completed: " + _adUnitId);
            LoadAd();
        }
    }
}