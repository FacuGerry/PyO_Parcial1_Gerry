using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static event Action<float> OnTimeUpdated;
    public static event Action<int> OnPressesUpdated;
    public static event Action<int> OnHighScoreUpdated;

    [SerializeField] private GameDataSO _data;
    private int _presses = 0;
    private float _timer = 0f;

    private IEnumerator _coroutine;

    private bool _isRunning = false;
    private bool _canBePlayed = true;

    private void Start()
    {
        OnPressesUpdated?.Invoke(_presses);
        _timer = _data.gameTime;
        OnTimeUpdated?.Invoke(_timer);
        _canBePlayed = true;
        OnHighScoreUpdated?.Invoke(_data.highScore);
    }

    private void OnEnable()
    {
        UiButtonsGame.OnButtonGamePressed += ButtonPressed;
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
            OnTimeUpdated?.Invoke(_timer);
        }
    }

    private void OnDisable()
    {
        UiButtonsGame.OnButtonGamePressed -= ButtonPressed;
    }

    private IEnumerator WaitingForNextGame()
    {
        _canBePlayed = false;

        _timer = _data.gameTime;

        if (_presses > _data.highScore)
        {
            _data.highScore = _presses;
            OnHighScoreUpdated?.Invoke(_data.highScore);
        }

        _presses = 0;

        _isRunning = false;

        yield return new WaitForSeconds(2f);
        OnPressesUpdated?.Invoke(_presses);

        _canBePlayed = true;

        yield return null;
    }

    private void ButtonPressed()
    {
        if (!_canBePlayed)
            return;

        _presses++;
        _isRunning = true;
        OnPressesUpdated?.Invoke(_presses);
    }
}
