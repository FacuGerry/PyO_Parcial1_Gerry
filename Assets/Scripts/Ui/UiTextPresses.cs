using TMPro;
using UnityEngine;

public class UiTextPresses : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textTime;
    [SerializeField] private TextMeshProUGUI _textPresses;
    [SerializeField] private TextMeshProUGUI _textHighScore;

    public void UpdateTime(float time)
    {
        _textTime.text = "Tiempo: " + time.ToString("00.00");
    }

    public void UpdatePresses(int score)
    {
        _textPresses.text = score.ToString("0") + " clicks";
    }

    public void UpdateHighScore(int score)
    {
        _textHighScore.text = "High score: " + score.ToString("0");
    }
}