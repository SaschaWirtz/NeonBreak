using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject resumeButton;

    public GameObject mainMMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame() {
        this.gameObject.SetActive(true);
        resumeButton.SetActive(true);
        mainMMenuButton.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void resumeGame() {
        StartCoroutine(resume());
    }

    IEnumerator resume() {
        resumeButton.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        resumeButton.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        resumeButton.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1.0f;
        this.gameObject.SetActive(false);
    }

    public void toMainMenu() {
        StartCoroutine(mainMenu());
    }

    IEnumerator mainMenu() {
        mainMMenuButton.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        mainMMenuButton.SetActive(true);
        yield return new WaitForSecondsRealtime(0.1f);
        mainMMenuButton.SetActive(false);
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}
