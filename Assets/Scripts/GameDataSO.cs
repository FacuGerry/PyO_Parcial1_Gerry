using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Settings/GameData")]

public class GameDataSO : ScriptableObject
{
    public int highScore;
    public int gameTime;
}