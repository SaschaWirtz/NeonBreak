using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject singleplayerButton;
    public GameObject multiplayerButton;
    public GameObject exitButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void singleplayerMode() {
        StartCoroutine(singleplayer());
    }

    IEnumerator singleplayer() {
        singleplayerButton.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        singleplayerButton.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        singleplayerButton.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public void multiplayerMode() {
        StartCoroutine(multiplayer());
    }

    IEnumerator multiplayer() {
        multiplayerButton.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        multiplayerButton.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        multiplayerButton.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        //TODO multiplayer
        multiplayerButton.SetActive(true);
    }

    public void exitGame() {
        StartCoroutine(exit());
    }

    IEnumerator exit() {
        exitButton.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        exitButton.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        exitButton.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Application.Quit();
    }
}
