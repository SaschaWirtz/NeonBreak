using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// A array holding the information which AudioSource contains
    /// which sound.
    /// </summary>
    public string[] AudioClips;

    /// <summary>
    /// Holding the prefab for BrickHitSFX instantiation.
    /// </summary>
    public GameObject BrickHitSFX;

    /// <summary>
    /// Holding the prefab for PowerupSFX instantiation.
    /// </summary>
    public GameObject PowerupSFX;

    /// <summary>
    /// A array holding all the initial AudioSource components
    /// </summary>
    private AudioSource[] MainAudioSources;

    /// <summary> 
    /// Static gameobject instance.
    /// </summary>
    private static SoundManager Instance;

    /// <summary> 
    /// Getter for static gameobject instance.
    /// </summary>
    /// <returns> 
    /// Static gameobject instance.
    /// </returns>
    public static SoundManager GetInstance() {
        return Instance;
    }

    /// <summary> 
    /// Initialize factory by making static and starting main menu soundtrack.
    /// </summary>
    private void Awake()
    {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }

        this.MainAudioSources = this.gameObject.GetComponents<AudioSource>();

        this.StartMainMenuSoundtrack();
    }

    /// <summary> 
    /// Starting main menu soundtrack.
    /// </summary>
    public void StartMainMenuSoundtrack() {
        this.MainAudioSources[Array.IndexOf(AudioClips, "MainMenu_Soundtrack")].Play();
    }

    /// <summary> 
    /// Stopping main menu soundtrack.
    /// </summary>
    public void StopMainMenuSoundtrack() {
        this.MainAudioSources[Array.IndexOf(AudioClips, "MainMenu_Soundtrack")].Stop();
    }

    /// <summary> 
    /// Starting button click SFX.
    /// </summary>
    public void StartButtonClickSFX() {
        this.MainAudioSources[Array.IndexOf(AudioClips, "ButtonClick_SFX")].Play();
    }

    /// <summary> 
    /// Starting single player soundtrack.
    /// </summary>
    public void StartSinglePlayerSoundtrack() {
        this.MainAudioSources[Array.IndexOf(AudioClips, "Singleplayer_Soundtrack")].Play();
    }

    /// <summary> 
    /// Switching single player soundtrack.
    /// </summary>
    public void SwitchSinglePlayerSoundtrack() {
        if (this.MainAudioSources[Array.IndexOf(AudioClips, "Singleplayer_Soundtrack")].isPlaying) {
            //float timestamp = this.MainAudioSources[Array.IndexOf(AudioClips, "Singleplayer_Soundtrack")].Play();
        } else {

        }
        
    }

    /// <summary> 
    /// Stopping single player soundtrack.
    /// </summary>
    public void StopSinglePlayerSoundtrack() {
        this.MainAudioSources[Array.IndexOf(AudioClips, "Singleplayer_Soundtrack")].Stop();
    }

    /// <summary> 
    /// Starting brick hit SFX.
    /// </summary>
    public void StartBrickHitSFX() {
        Instantiate(this.BrickHitSFX, new Vector2(0,0), Quaternion.identity);
    }

    /// <summary> 
    /// Starting powerup SFX.
    /// </summary>
    public void StartPowerupSFX() {
        Instantiate(this.PowerupSFX, new Vector2(0,0), Quaternion.identity);
    }
}