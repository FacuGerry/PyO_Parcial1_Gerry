using System;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonsGame : MonoBehaviour
{
    public static event Action OnButtonGamePressed;
    public static event Action OnButtonCreditsPressed;
    public static event Action OnButtonAdsPressed;

    [SerializeField] private Button _btnGame;
    [SerializeField] private Button _btnCredits;
    //[SerializeField] private Button _btnAds;

    private void Start()
    {
        _btnGame.onClick.AddListener(ButtonGameClicked);
        _btnCredits.onClick.AddListener(CreditsClicked);
        //_btnAds.onClick.AddListener(AdsClicked);
    }

    private void OnDestroy()
    {
        _btnGame.onClick.RemoveAllListeners();
        _btnCredits.onClick.RemoveAllListeners();
        //_btnAds.onClick.RemoveAllListeners();
    }

    private void ButtonGameClicked()
    {
        OnButtonGamePressed?.Invoke();
    }

    private void CreditsClicked()
    {
        OnButtonCreditsPressed?.Invoke();
    }

    private void AdsClicked()
    {
        OnButtonAdsPressed?.Invoke();
    }
}
