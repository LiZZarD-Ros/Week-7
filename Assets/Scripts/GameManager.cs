using System.Collections;
using System.Collections.Generic;
using UnityEditor.Advertisements;
using UnityEngine;
using UnityEngine.Advertisements;

public class GameManager : MonoBehaviour
{
    public int bestScore;
    public int score;

    public int currentStage = 0;

    public static GameManager singleton;
    
    // Start is called before the first frame update
    void Awake()
    {
        Advertisement.Initialize("5709913");
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
        currentStage++;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
        Debug.Log("Next Level start");

    }

    public void RestartLevel()
    {
        Debug.Log("Game Over");
        Advertisement.Show();
        5709913

        singleton.score = 0;
        FindObjectOfType<BallController>().ResetBall();
        FindObjectOfType<HelixController>().LoadStage(currentStage);
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
