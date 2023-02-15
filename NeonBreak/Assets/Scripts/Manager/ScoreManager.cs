using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    /// <summary> 
    /// Static gameobject instance.
    /// </summary>
    public static ScoreManager Instance;

    /// <summary> 
    /// TMPGUI component for score control.
    /// </summary>
    public TextMeshProUGUI scoreText;

    /// <summary> 
    /// TMPGUI component for life control.
    /// </summary>
    public TextMeshProUGUI lifesText;

    /// <summary> 
    /// Lifes counter.
    /// </summary>
    private int lifes = 3;

    /// <summary> 
    /// Score counter.
    /// </summary>
    private int score = 0;

    /// <summary> 
    /// Setting instance.
    /// </summary>
    private void Awake() {
        if(Instance == null)
        {

            DontDestroyOnLoad(gameObject);

            Instance = this;
        }
    }

    /// <summary> 
    /// Getter for static gameobject instance.
    /// </summary>
    /// <returns> 
    /// Static gameobject instance.
    /// </returns>
    public static ScoreManager GetInstance() {
        return Instance;
    }

    /// <summary> 
    /// Subtract one life and check game over.
    /// </summary>
    public void LoseLife() {
        this.lifes -= 1;
        this.lifesText.text = lifes.ToString();

        if(this.lifes < 1) {
            this.gameOver();
        }
    }

    /// <summary> 
    /// Add extra life.
    /// </summary>
    /// <param name="addedLifes"> 
    /// Lifes to be added.
    /// </param>
    public void AddLifes(int addedLifes = 1) {
        this.lifes += addedLifes;
        this.lifesText.text = lifes.ToString();
    }

    /// <summary> 
    /// Count up score.
    /// </summary>
    /// <param name="scoreToAdd"> 
    /// Score to be added.
    /// </param>
    public void AddScore(int scoreToAdd) {
        this.score += scoreToAdd;
        this.scoreText.text = score.ToString().PadLeft(6, '0');

        if(GameObject.FindGameObjectsWithTag("Bricks").Length == 0) {
            GameObject.FindGameObjectWithTag("Balls").GetComponent<BallController>().Respawn(true);
            BrickFactory.GetInstance().generateBricks();
        }
    }

    /// <summary> 
    /// Display game over screen.
    /// </summary>
    private void gameOver() {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Balls");
        foreach (GameObject ball in balls) {
            Destroy(ball);
        }
        SoundManager.GetInstance().StopSinglePlayerSoundtrack();
        SoundManager.GetInstance().StartMainMenuSoundtrack();
        SceneManager.LoadScene("Scoreboard", LoadSceneMode.Single);
    }

    /// <summary> 
    /// Getter for score.
    /// </summary>
    /// <returns> 
    /// Returns current score.
    /// </returns>
    public int getScore() {
        return this.score;
    }
}