using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LowerEdgeController : MonoBehaviour
{

    public delegate void Respawn();
    public Respawn respawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.name == "Ball") {
            this.respawn();
        } else if (collider.gameObject.tag == "EnlargePadel") {
            collider.gameObject.GetComponent<PowerupController>().DestroyPowerup();
        }
    }
}
