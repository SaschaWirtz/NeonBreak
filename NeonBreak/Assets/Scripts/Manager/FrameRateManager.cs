using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    private static FrameRateManager Instance;

    public static FrameRateManager GetInstance() {
        return Instance;
    }

    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }

        this.SetTargetFramerate();
    }

    public void SetTargetFramerate() {
        Application.targetFrameRate = 120;
    }
}