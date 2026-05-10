using UnityEngine;
using UnityEngine.Advertisements;

public class AdsBanner : MonoBehaviour
{
    [SerializeField] private BannerPosition _bannerPosition = BannerPosition.TOP_CENTER;
    [SerializeField] private string _androidAdUnitId = "Banner_Android";
    private string _adUnitId = null;

    private void Start()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        _adUnitId = _androidAdUnitId;
#endif

        Advertisement.Banner.SetPosition(_bannerPosition);
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adUnitId, options);
    }

    private void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        ShowBannerAd();
    }

    private void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }

    private void ShowBannerAd()
    {
        Advertisement.Banner.Show(_adUnitId);
    }
}
