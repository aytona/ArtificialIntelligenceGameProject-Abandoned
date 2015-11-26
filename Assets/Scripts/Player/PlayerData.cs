using UnityEngine;
using System.Collections;

public class PlayerData : Singleton<PlayerData> {

    private int score = 0;
    private int highScore = 0;
    private int gold = 0;
    private int waveCount = 0;

    private PlayerData()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        gold = PlayerPrefs.GetInt("Gold");
        waveCount = PlayerPrefs.GetInt("WaveCount");
    }

    public int Score
    {
        get
        {
            return this.score;
        }
        set
        {
            score = value;
            if (score > highScore)
            {
                this.highScore = score;
                PlayerPrefs.SetInt("HighScore", highScore);
            }
        }
    }

    public int HighScore
    {
        get
        {
            return this.highScore;
        }
    }

    // Might want to set gold and wavecount when a save button is pressed instead
    public int Gold
    {
        get
        {
            return this.gold;
        }
        set
        {
            gold = value;
            PlayerPrefs.SetInt("Gold", gold);
        }
    }

    public int WaveCount
    {
        get
        {
            return this.waveCount;
        }
        set
        {
            waveCount = value;
            PlayerPrefs.SetInt("WaveCount", waveCount);
        }
    }
}
