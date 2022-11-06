using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Get correct prefab reference by reflection with bricktype name
        GameObject toInstantiate = this.GetType().GetField(Enum.GetName(typeof(BrickType), type)).GetValue(this) as GameObject;

        if (toInstantiate != null) {
            GameObject newObject = Instantiate(toInstantiate);
            toInstantiate = null;
            newObject.transform.position = position;
        }
    }
}