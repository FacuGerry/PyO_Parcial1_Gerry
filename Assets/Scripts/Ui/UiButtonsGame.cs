using System;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonsGame : MonoBehaviour
{
    public event Action OnButtonGamePressed;
    public event Action OnButtonCreditsPressed;

    [SerializeField] private Button _btnGame;
    [SerializeField] private Button _btnCredits;

    private void Start()
    {
        _btnGame.onClick.AddListener(ButtonGameClicked);
        _btnCredits.onClick.AddListener(CreditsClicked);
    }

    private void OnDestroy()
    {
        _btnGame.onClick.RemoveAllListeners();
        _btnCredits.onClick.RemoveAllListeners();
    }

    private void ButtonGameClicked()
    {
        OnButtonGamePressed?.Invoke();
    }

    private void CreditsClicked()
    {
        OnButtonCreditsPressed?.Invoke();
    }
}
