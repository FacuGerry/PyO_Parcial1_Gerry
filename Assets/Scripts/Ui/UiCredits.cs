using UnityEngine;
using UnityEngine.UI;

public class UiCredits : MonoBehaviour
{
    [SerializeField] private UiButtonsGame _buttons;
    [SerializeField] private Button _btnBack;
    private CanvasGroup _canvas;

    private void Awake()
    {
        _canvas = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _btnBack.onClick.AddListener(OnXClicked_CloseCredits);
        ChangeCanvas(false);
    }

    private void OnEnable()
    {
        _buttons.OnButtonCreditsPressed += OnCreditsClicked_OpenCredits;
    }

    private void OnDisable()
    {
        _buttons.OnButtonCreditsPressed -= OnCreditsClicked_OpenCredits;
    }

    private void OnDestroy()
    {
        _btnBack.onClick.RemoveAllListeners();
    }

    private void OnCreditsClicked_OpenCredits()
    {
        ChangeCanvas(true);
    }

    private void OnXClicked_CloseCredits()
    {
        ChangeCanvas(false);
    }

    private void ChangeCanvas(bool isOn)
    {
        _canvas.alpha = isOn ? 1 : 0;
        _canvas.interactable = isOn;
        _canvas.blocksRaycasts = isOn;
    }
}
