using TMPro;
using UnityEngine;

public class UiTextPresses : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTime;
    [SerializeField] private TextMeshProUGUI _textPresses;
    [SerializeField] private TextMeshProUGUI _textHighScore;

    private void OnEnable()
    {
        GameController.OnTimeUpdated += UpdateTime;
        GameController.OnPressesUpdated += UpdatePresses;
        GameController.OnHighScoreUpdated += UpdateHighScore;
    }

    private void OnDisable()
    {
        GameController.OnTimeUpdated -= UpdateTime;
        GameController.OnPressesUpdated -= UpdatePresses;
        GameController.OnHighScoreUpdated -= UpdateHighScore;
    }

    private void UpdateTime(float time)
    {
        _textTime.text = "Tiempo: " + time.ToString("00.00");
    }

    private void UpdatePresses(int score)
    {
        _textPresses.text = score.ToString("0") + " clicks";
    }

    private void UpdateHighScore(int score)
    {
        _textHighScore.text = "High score: " + score.ToString("0");
    }
}