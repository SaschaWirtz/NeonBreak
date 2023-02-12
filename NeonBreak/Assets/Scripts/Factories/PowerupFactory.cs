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
    /// Object powerup pool.
    /// </summary>
    private List<GameObject> powerupPool;

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
            this.powerupPool = new List<GameObject>();
        }
    }

    /// <summary> 
    /// Initialize powerup object pool.
    /// </summary>
    private void Start() {
        this.powerupPool = new List<GameObject>();
    }

    /// <summary> 
    /// Returns a unused powerup gameobject from objectpool.
    /// </summary>
    /// <param name="type"> 
    /// Powerup type determined by brick type.
    /// </param>
    /// <returns> 
    /// Unused powerup gameobject from objectpool.
    /// </returns>
    private GameObject GetActivePooledPowerup(BrickType type) {        

        // Iterate through objectpool and search for unused powerup gameobject.
        for(int pooledPowerup = 0; pooledPowerup < this.powerupPool.Count; pooledPowerup++) {
            if(!this.powerupPool[pooledPowerup].activeInHierarchy && this.powerupPool[pooledPowerup].tag == Enum.GetName(typeof(BrickType), type)) {
                this.powerupPool[pooledPowerup].SetActive(true);
                return this.powerupPool[pooledPowerup];
            }
        }

        // Get correct prefab reference by reflection with bricktype name
        GameObject toInstantiate = this.GetType().GetField(Enum.GetName(typeof(BrickType), type)).GetValue(this) as GameObject;

        if (toInstantiate != null) {

            // Create new powerup gameobject if no unused is available.
            GameObject newPowerup = Instantiate(toInstantiate);
            this.powerupPool.Add(newPowerup);
            return newPowerup;
        }

        return null;
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

        GameObject newObject = this.GetActivePooledPowerup(type);
        if(newObject != null) {
            newObject.transform.position = position;
        }
    }
}