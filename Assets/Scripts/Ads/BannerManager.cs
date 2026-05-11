using UnityEngine;
using UnityEngine.Advertisements;

public class BannerManager : MonoBehaviour
{
    [SerializeField] string _androidAdUnitId = "Banner_Android";
    string _adUnitId = null;

    void Start()
    {
#if  UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif
    }

    public void Show()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Load(_adUnitId, options);
    }

    void OnBannerLoaded()
    {
        ShowBanner();
    }

    public void HideBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void ShowBanner()
    {
        Advertisement.Banner.Show(_adUnitId);
    }

    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
    }
}