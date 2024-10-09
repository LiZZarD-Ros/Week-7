using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int bestScore;
    public int score;

    public int currentStage = 0;

    public static GameManager singleton;

    [SerializeField] private TextMeshProUGUI scoreBest;

    // Start is called before the first frame update
    void Awake()
    {
        //Advertisement.Initialize("5709913");
        if (singleton == null)
        {
            singleton = this;
        }else if (singleton != this)
        {
            Destroy(gameObject);
        }

        bestScore = PlayerPrefs.GetInt("Highscore");

        //Managing Ads
       
        StartCoroutine(DisplayBannerWithDelay());

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
        //Advertisement.Show("5709913");
        //5709913
        AdsManager.Instance.interstitialAds.ShowInterstitialAd();

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

    private IEnumerator DisplayBannerWithDelay()
    {
        yield return new WaitForSeconds(1f);
        AdsManager.Instance.bannerAds.ShowBannerAd();
    }

    private void Update()
    {
        scoreBest.text = bestScore.ToString();
    }

}
