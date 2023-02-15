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
    }

    public void singleplayerMode() {
        SoundManager.GetInstance().StopMainMenuSoundtrack();
        SoundManager.GetInstance().StartButtonClickSFX();
        StartCoroutine(singleplayer());
    }

    IEnumerator singleplayer() {
        singleplayerButton.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        singleplayerButton.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        singleplayerButton.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        SoundManager.GetInstance().StartSinglePlayerSoundtrack();
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
                    if (btPermissionManager.Call<bool>("IsPermissionRequired", currentActivity) 
                        && (!Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_CONNECT")
                        || !Permission.HasUserAuthorizedPermission("android.permission.BLUETOOTH_SCAN")
                        || !Permission.HasUserAuthorizedPermission("android.permission.ACCESS_FINE_LOCATION"))) {
                        var callbacks = new PermissionCallbacks();
                        callbacks.PermissionDenied += (string permissionName) => {
                            if (permissionName == "android.permission.BLUETOOTH_CONNECT") {
                                //Error
                            }
                        };

                        callbacks.PermissionGranted += (string permissionName) => {
                            if (permissionName == "android.permission.BLUETOOTH_CONNECT") {
                                Permission.RequestUserPermission("android.permission.BLUETOOTH_SCAN", callbacks);
                            } else if (permissionName == "android.permission.ACCESS_FINE_LOCATION") {
                                if (this.activateBluetooth()) {
                                    this.discoverDevices();
                                } else {
                                    //Error
                                }
                            } else if (permissionName == "android.permission.BLUETOOTH_SCAN") {
                                Permission.RequestUserPermission("android.permission.ACCESS_FINE_LOCATION", callbacks);
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
                    while (btDiscoveryAgent.Call<string>("getMacForDeviceName", "HUAWEI P20 lite") == null) {
                        Debug.Log(btDiscoveryAgent.Call<string>("getMacForDeviceName", "HUAWEI P20 lite"));    
                    }
                    Debug.Log(btDiscoveryAgent.Call<string>("getMacForDeviceName", "HUAWEI P20 lite"));
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
