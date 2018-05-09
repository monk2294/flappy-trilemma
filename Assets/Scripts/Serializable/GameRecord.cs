using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameRecord
{
    public string PlayerName { get; private set; }

    public int Score { get; private set; }

    public GameRecord(string playerName, int score)
    {
        PlayerName = playerName;
        Score = score;
    }
}
