using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textBest;
    
    public GameObject mainMenuPanel;
    public GameObject leaderPanel;
    GameObject ball;


    // Start is called before the first frame update
    void Start()
    {

        ball = GameObject.FindWithTag("Ball");

        mainMenuPanel.SetActive(true);
        ball.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        textBest.text = "Best: " + GameManager.singleton.bestScore;
        textScore.text = "Score: " + GameManager.singleton.score;

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            ShowMainMenu();
            ball.SetActive(false);
        }
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        
    }

    public void StartGame()
    {
        mainMenuPanel.SetActive(false);
        leaderPanel.SetActive(false);
        ball.SetActive(true);
    }
    }
