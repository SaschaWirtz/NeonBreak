using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LowerEdgeController : MonoBehaviour
{

    /// <summary> 
    /// Respawn delegate method container.
    /// </summary>
    public delegate void Respawn();

    /// <summary> 
    /// Instance of delegate container
    /// </summary>
    public Respawn respawn;

    /// <summary> 
    /// Triggers respawn and powerups on contact.
    /// </summary>
    /// <param name="collider"> 
    /// Colliding collider information.
    /// </param>
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.name == "Ball") {
            this.respawn();
        } else if (collider.gameObject.tag == "EnlargePadel") {
            collider.gameObject.GetComponent<PowerupController>().DestroyPowerup();
        }
    }
}
