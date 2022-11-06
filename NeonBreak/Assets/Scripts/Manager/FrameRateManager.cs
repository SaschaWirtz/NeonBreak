using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{

    /// <summary> 
    /// Static gameobject instance.
    /// </summary>
    private static FrameRateManager Instance;

    /// <summary> 
    /// Getter for static gameobject instance.
    /// </summary>
    /// <returns> 
    /// Static gameobject instance.
    /// </returns>
    public static FrameRateManager GetInstance() {
        return Instance;
    }

    /// <summary> 
    /// Initialize factory by making static and setting target framerate.
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }

        this.SetTargetFramerate();
    }

    /// <summary> 
    /// Setting target framerate.
    /// </summary>
    public void SetTargetFramerate() {
        Application.targetFrameRate = 120;
    }
}