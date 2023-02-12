using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject singleplayerButton;
    public GameObject multiplayerButton;
    public GameObject exitButton;

    // Update is called once per frame
    void Update()
    {
        using (AndroidJavaClass btDiscoveryAgent = new AndroidJavaClass("de.MGD.NeonBreak.transports.bluetoothTransport.BTDiscoveryAgent")) {
            AndroidJavaObject companion = btDiscoveryAgent.GetStatic<AndroidJavaObject>("Companion");
            Debug.Log("Name: " + companion.Call<int>("getSize"));
            for (int i = 0; i < companion.Call<int>("getSize"); i++)
            {
                Debug.Log("Name: " + companion.Call<string>("getNameById", i));
                Debug.Log("MAC: " + companion.Call<string>("getMacById", i));
            }
        }

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
        // Application.Quit();
        this.startHost();
    }

    private bool activateBluetooth() {
        using (AndroidJavaObject btStateManager = new AndroidJavaObject("de.MGD.NeonBreak.transports.bluetoothTransport.BTStateManager")) {
            int result = btStateManager.Call<int>("TurnOnBluetooth");
            
            return result==0;
        }
    }

    private void prepareAndStartMultiplayer() {
        using (AndroidJavaObject btPermissionManager = new AndroidJavaObject("de.MGD.NeonBreak.transports.bluetoothTransport.BTPermissionManager")) {
            using (AndroidJavaClass javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (AndroidJavaObject currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                    if (btPermissionManager.Call<bool>("IsPermissionRequired", currentActivity) && !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT")) {
                        var callbacks = new PermissionCallbacks();
                        callbacks.PermissionDenied += (string permissionName) => {
                            if (permissionName == "android.permission.BLUETOOTH_CONNECT") {
                                //Error
                            }
                        };

                        callbacks.PermissionGranted += (string permissionName) => {
                            if (permissionName == "android.permission.BLUETOOTH_CONNECT") {
                                if (this.activateBluetooth()) {
                                    this.discoverDevices();
                                } else {
                                    //Error
                                }
                            }
                        };

                        Permission.RequestUserPermission("android.permission.BLUETOOTH_CONNECT", callbacks);
                    } else {
                        if (this.activateBluetooth()) {
                            this.discoverDevices();
                        } else {
                            //Error
                        }
                    }
                }
            }
        }
    }

    private void discoverDevices() {
        using (AndroidJavaObject btDiscoveryAgent = new AndroidJavaObject("de.MGD.NeonBreak.transports.bluetoothTransport.BTDiscoveryAgent")) {
            using (AndroidJavaClass javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
                using (AndroidJavaObject currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                    btDiscoveryAgent.Call("registerReceiver", currentActivity);
                    btDiscoveryAgent.Call("startDiscovery");
                }
            }
        }
    }

    private void startHost() {
        using (AndroidJavaClass javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer")) {
            using (AndroidJavaObject currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
                using (AndroidJavaObject btServerController = new AndroidJavaObject("de.MGD.NeonBreak.transports.bluetoothTransport.BTServerController", currentActivity)) {
                    btServerController.Call("start");
                    using (AndroidJavaObject btDiscoverStateManager = new AndroidJavaObject("de.MGD.NeonBreak.transports.bluetoothTransport.BTDiscoverStateManager")) {
                        btDiscoverStateManager.Call("makeDiscoverable", currentActivity);
                    }
                }
            }
        }
    }
}
