using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PowerupFactory : MonoBehaviour {

    /// <summary> 
    /// Static gameobject reference.
    /// </summary>
    public static PowerupFactory Instance;

    /// <summary> 
    /// Reference to enlarge padel powerup.
    /// </summary>
    public GameObject EnlargePadel;

    /// <summary> 
    /// Maximal amount of enlarged powerups that can be pooled
    /// </summary>
    [SerializeField]
    private int maxEnlargePoolSize = 10;

    /// <summary> 
    /// Object powerup pool.
    /// </summary>
    private Stack<GameObject> enlargePowerupPool;

    /// <summary> 
    /// Getter for static gameobject instance.
    /// </summary>
    /// <returns> 
    /// Static gameobject instance.
    /// </returns>
    public static PowerupFactory GetInstance() {
        return Instance;
    }

    /// <summary> 
    /// Initialize factory by making static.
    /// </summary>
    private void Awake() {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;

            SceneManager.sceneLoaded += this.OnSceneLoaded;
        }
    }

    /// <summary> 
    /// Reset powerup pool.
    /// </summary>
    /// <param name="scene"> 
    /// Scene that got loaded.
    /// </param>
    /// <param name="mode"> 
    /// LoadSceneMode for this loading process.
    /// </param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if(scene.name == "Main") {
            this.enlargePowerupPool = new Stack<GameObject>();
        }
    }

    /// <summary> 
    /// Initialize powerup object pool.
    /// </summary>
    private void Start() {
        this.enlargePowerupPool = new Stack<GameObject>();
    }

    /// <summary> 
    /// Getting active enlarge powerup from pool.
    /// </summary>
    /// <returns> 
    /// Enlarge powerup gameobject.
    /// </returns>
    private GameObject GetEnlargePowerup() {
        if(this.enlargePowerupPool.Count > 0) {
            GameObject enlargePowerup = this.enlargePowerupPool.Pop();
            enlargePowerup.gameObject.SetActive(true);
            return enlargePowerup;
        }else {
            return Instantiate(this.EnlargePadel);
        }
    }

    /// <summary> 
    /// Returns powerup back to pool if pool isn't full.
    /// </summary>
    /// <param name="powerup"> 
    /// Powerup that is been returned to the pool.
    /// </param>
    public void ReturnPowerup(GameObject powerup) {
        powerup.gameObject.SetActive(false);
        switch(powerup.gameObject.tag) {
            case "EnlargePadel":
                if(this.enlargePowerupPool.Count < this.maxEnlargePoolSize) {
                    this.enlargePowerupPool.Push(powerup);
                }else {
                    Destroy(powerup);
                }
                break;
            default:
                break;
        }
    }

    /// <summary> 
    /// Method for placing powerups in gameview.
    /// </summary>
    /// <param name="type"> 
    /// Powerup type determined by brick type.
    /// </param>
    /// <param name="position"> 
    /// Location of powerup spawn.
    /// </param>
    public void spawnPowerup(BrickType type, Vector3 position) {

        GameObject newObject = this.GetEnlargePowerup();
        if(newObject != null) {
            newObject.transform.position = position;
        }
    }
}