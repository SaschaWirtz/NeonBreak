using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupFactory : MonoBehaviour {
    public static PowerupFactory Instance;

    public static PowerupFactory GetInstance() {
        return Instance;
    }

    public GameObject EnlargePadel;

    private void Awake() {
        if(Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }
    }

    public void spawnPowerup(BrickType type, Vector3 position) {
        GameObject toInstantiate = this.GetType().GetField(Enum.GetName(typeof(BrickType), type)).GetValue(this) as GameObject;

        if (toInstantiate != null) {
            GameObject newObject = Instantiate(toInstantiate);
            toInstantiate = null;
            newObject.transform.position = position;
        }
    }
}