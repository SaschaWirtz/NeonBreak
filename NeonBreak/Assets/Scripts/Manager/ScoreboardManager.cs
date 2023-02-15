using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour {

    /// <summary> 
    /// Static gameobject instance.
    /// </summary>
    public static ScoreboardManager Instance;

    /// <summary> 
    /// All saved scores.
    /// </summary>
    private List<int> scoreEntries = new List<int> ();

    /// <summary> 
    /// Prefab for score list element.
    /// </summary>
    [SerializeField]
    private GameObject scoreElementPrefab;

    /// <summary> 
    /// Reference to wrapper for score list elements.
    /// </summary>
    [SerializeField]
    private Transform elementWrapper;

    /// <summary> 
    /// Maximum amount of scores saved.
    /// </summary>
    [SerializeField]
    private int maxCount = 10;

    /// <summary> 
    /// Name of file in storage.
    /// </summary>
    [SerializeField]
    private string filename = "";

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
    /// Triggering score logic and filling UI with it.
    /// </summary>
    void Start() {
        this.LoadScores();

        if(ScoreManager.GetInstance().getScore() > 0) {
            this.addCurrentScore();
        }

        this.FillUI();        
    }

    /// <summary> 
    /// Getter for static gameobject instance.
    /// </summary>
    /// <returns> 
    /// Static gameobject instance.
    /// </returns>
    public static ScoreboardManager GetInstance() {
        return Instance;
    }

    /// <summary> 
    /// Trigger loading scores logic and checks if max count is been followed.
    /// </summary>
    private void LoadScores() {
        this.scoreEntries = ScoreFileHandler.ReadFromJSON<int>(filename);

        this.CheckMaxCount();
    }

    /// <summary> 
    /// Go back to main menu.
    /// </summary>
    public void ToMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    /// <summary> 
    /// Trigger score save logic.
    /// </summary>
    private void SaveScores() {
        ScoreFileHandler.SaveToJSON<int>(this.scoreEntries, this.filename);
    }

    /// <summary> 
    /// Check if score entry list has less then max count of scores saved.
    /// </summary>
    private void CheckMaxCount() {
        while (this.scoreEntries.Count > this.maxCount) {
            this.scoreEntries.RemoveAt(maxCount);
        }
    }

    /// <summary> 
    /// Add current score to list.
    /// </summary>
    private void addCurrentScore() {
        int currentScore = ScoreManager.GetInstance().getScore();
        for(int entry = 0; entry < maxCount; entry++) {
            if(entry >= scoreEntries.Count || currentScore > scoreEntries[entry]) {
                scoreEntries.Insert(entry, currentScore);

                this.CheckMaxCount();
                this.SaveScores();

                break;
            }
        }
    }

    /// <summary> 
    /// Fill Scoreboard with entries.
    /// </summary>
    private void FillUI() {
        for(int scoreEntry = 0; scoreEntry < this.scoreEntries.Count; scoreEntry++) {
            var inst = Instantiate(this.scoreElementPrefab, Vector3.zero, Quaternion.identity);
            inst.transform.SetParent(this.elementWrapper, false);

            var elementTexts = inst.GetComponentsInChildren<TextMeshProUGUI>();
            elementTexts[0].text = (scoreEntry + 1).ToString() + ".";
            elementTexts[1].text = this.scoreEntries[scoreEntry].ToString();

        }
    }

}