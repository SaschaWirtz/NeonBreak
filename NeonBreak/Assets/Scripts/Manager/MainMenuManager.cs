using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;

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
        
        multiplayerButton.SetActive(true);

        this.prepareAndStartMultiplayer();
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

    private bool activateBluetooth() {
        using (AndroidJavaObject btStateManager = new AndroidJavaObject("de.MGD.NeonBreak.transports.bluetoothTransport.BTStateManager")) {
            int result = btStateManager.Call<int>("TurnOnBluetooth");
            
            return result==0;
        }
    }

    private void prepareAndStartMultiplayer() {
        if (!Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT")) {
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionDenied += (string permissionName) => {
                if (permissionName == "android.permission.BLUETOOTH_CONNECT") {
                    //Error
                }
            };

            callbacks.PermissionGranted += (string permissionName) => {
                if (permissionName == "android.permission.BLUETOOTH_CONNECT") {
                    if (this.activateBluetooth()) {
                        //Multiplayer
                    } else {
                        //Error
                    }
                }
            };

            Permission.RequestUserPermission("android.permission.BLUETOOTH_CONNECT", callbacks);
        } else {
            if (this.activateBluetooth()) {
                //Multiplayer
            } else {
                //Error
            }
        }
    }
}
