using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public static ScoreManager Instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifesText;

    private int lifes = 3;

    public static ScoreManager GetInstance() {
        return Instance;
    }

    private void Awake() {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void LoseLife() {
        this.lifes -= 1;
        this.lifesText.text = lifes.ToString();
    }

    public void AddLifes(int addedLifes = 1) {
        this.lifes += addedLifes;
        this.lifesText.text = lifes.ToString();
    }
}