using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    /// TMPGUI component for final message control.
    /// </summary>
    public TextMeshProUGUI finalText;

    /// <summary> 
    /// Lifes counter.
    /// </summary>
    private int lifes = 3;

    /// <summary> 
    /// Score counter.
    /// </summary>
    private int score = 0;

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
    /// Setting instance.
    /// </summary>
    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    /// <summary> 
    /// Subtract one life and check game over
    /// </summary>
    public void LoseLife() {
        this.lifes -= 1;
        this.lifesText.text = lifes.ToString();

        if(this.lifes < 1) {
            this.finalText.text = "Game Over";
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
            this.finalText.text = "You Won";
            this.gameOver();
        }
    }

    /// <summary> 
    /// Display game over screen.
    /// </summary>
    private void gameOver() {
        this.finalText.enabled = true;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Balls");
        foreach (GameObject ball in balls) {
            Destroy(ball);
        }
    }
}