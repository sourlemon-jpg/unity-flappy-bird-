using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player playerScript;
    public Spawner spawnerScript;
    private int spawnrate;
    
    public Text scoreSaver;
    public GameObject tap1;
    public GameObject tap2;
    public Player player;
    public bool oyuncu = false;
    
    public Text scoreText;

    public GameObject startButton;
    public GameObject stopButton;
    public GameObject playButton;
    public GameObject gameOver;

    public bool stoponbutton = false;
    private int score;
    public int highestScore;
    public int prevScore;

    
    private void Awake()
    {
        
        Pause();
        if (PlayerPrefs.HasKey("HighScore"))
        {
           highestScore = PlayerPrefs.GetInt("HighScore");
           scoreSaver.text = highestScore.ToString(); 
        }
        
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();
        
        startButton.SetActive(false);
        stopButton.SetActive(true);
        tap1.SetActive(false);
        tap2.SetActive(false);
        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
        

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        
    }
    
    public void StopOnButton()
    {
        if (oyuncu == true)
        {
            Time.timeScale = 0f;
            stopButton.SetActive(false);
            startButton.SetActive(true);
            oyuncu = false;
        }
        else
        {   
            startButton.SetActive(false);
            stopButton.SetActive(true);
            Time.timeScale = 1f;
            
            oyuncu = true;


        }
    }

    public void GameOver()
    {
        scoreSaver.text = highestScore.ToString();
        PlayerPrefs.SetInt("HighScore", highestScore);
        
        stopButton.SetActive(false);
        gameOver.SetActive(true);
        playButton.SetActive(true);
        tap1.SetActive(true);
        tap2.SetActive(true);
        Pause();
    }

    public void IncreaseScore()
    {
        
        score++;
        prevScore = score;
        if (prevScore > highestScore)
        {
            highestScore = prevScore;
        }
        
        scoreText.text = score.ToString();
    }
    // Start is called before the first frame update
    public void GettingHarder()
    {
        if ( score > 15 && score < 25)
        {
            spawnerScript.spawnRate = 1.0f;
        }

        if (score > 25 && score < 32)
        {
            spawnerScript.maxHeight = 1.2f;
        }

        if (score > 32)
        {
            spawnerScript.minHeight = -1.2f;
        }
    }

}
