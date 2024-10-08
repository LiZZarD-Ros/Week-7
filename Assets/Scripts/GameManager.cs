using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int bestScore;
    public int score;

    public int currentStage = 0;

    public static GameManager singleton;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }else if (singleton != this)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("Highscore");
    }

   public void NextLevel()
    {
        Debug.Log("Next Level start");

    }

    public void RestartLevel()
    {
        Debug.Log("Game Over");
        // show ads
        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        // Reload stage
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
}
