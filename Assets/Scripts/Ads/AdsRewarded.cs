using System;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsRewarded : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public event Action OnAdPlayed;

    [SerializeField] private Button _showAdButton;
    [SerializeField] private string _androidAdUnitId = "Rewarded_Android";
    private string _adUnitId = null;
    private bool _adLoaded = false;
    private bool _hasUsedAd = false;

    private void Awake()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        _adUnitId = _androidAdUnitId;
#else
        _adUnitId = null;
#endif
    }

    private void Start()
    {
        _showAdButton.onClick.AddListener(ShowAd);
        _showAdButton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _showAdButton.onClick.RemoveAllListeners();
    }

    public void LoadAd()
    {
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);
        _adLoaded = true;
        if (!_hasUsedAd)
            ChangeButton(true);
    }

    public void ShowAd()
    {
        _showAdButton.gameObject.SetActive(false);

        if (_adLoaded)
            Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            ChangeButton(false);
            _hasUsedAd = true;
            OnAdPlayed?.Invoke();
            LoadAd();
        }
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error} - {message}");
        _adLoaded = false;
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error} - {message}");
        _adLoaded = false;
        ChangeButton(true);
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    public void OnNewGame_ShowAddButton()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        ChangeButton(true);
        _hasUsedAd = false;
#endif
    }

    public void ChangeButton(bool isOn)
    {
        _showAdButton.gameObject.SetActive(isOn);
    }
}