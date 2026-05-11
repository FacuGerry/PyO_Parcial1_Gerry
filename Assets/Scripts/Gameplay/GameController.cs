using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameDataSO _data;
    [SerializeField] private float _timeToAdd;

    [Header("UI")]
    [SerializeField] private UiButtonsGame _buttons;
    [SerializeField] private UiTextPresses _text;

    [Header("ADS")]
    [SerializeField] private RewardedAdManager _rewarded;
    [SerializeField] private InterstitialManager _interstitial;

    private int _presses = 0;
    private float _timer = 0f;

    private IEnumerator _coroutine;

    private bool _isRunning = false;
    private bool _canBePlayed = true;

    private void Start()
    {
        _text.UpdatePresses(_presses);
        _timer = _data.gameTime;
        _text.UpdateTime(_timer);
        _canBePlayed = true;
        _text.UpdateHighScore(_data.highScore);
    }

    private void OnEnable()
    {
        _buttons.OnButtonGamePressed += ButtonPressed;

        _rewarded.OnAdPlayed += OnAdPlayed_AddTime;
    }

    private void Update()
    {
        if (_isRunning)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                if (_coroutine != null)
                    StopCoroutine(_coroutine);

                _coroutine = WaitingForNextGame();
                StartCoroutine(_coroutine);
            }
            _text.UpdateTime(_timer);
        }
    }

    private void OnDisable()
    {
        _buttons.OnButtonGamePressed -= ButtonPressed;

        _rewarded.OnAdPlayed -= OnAdPlayed_AddTime;
    }

    private IEnumerator WaitingForNextGame()
    {
        _canBePlayed = false;

        _timer = _data.gameTime;

        if (_presses > _data.highScore)
        {
            _data.highScore = _presses;
            _text.UpdateHighScore(_data.highScore);
        }
        else
            _interstitial.ShowInterstitial();

        _presses = 0;

        _isRunning = false;

        yield return new WaitForSeconds(2f);
        _text.UpdatePresses(_presses);
        _canBePlayed = true;
        _rewarded.ShowAdButton();

        yield return null;
    }

    private void ButtonPressed()
    {
        if (!_canBePlayed)
            return;

        _presses++;
        _isRunning = true;
        _text.UpdatePresses(_presses);
    }

    private void OnAdPlayed_AddTime()
    {
        _timer += _timeToAdd;
        _text.UpdateTime(_timer);
    }
}
