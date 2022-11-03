using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public float speed = 2.5f;
    private Rigidbody2D rigidBody;

    void Awake() {
        this.rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.rigidBody.velocity = Vector2.down * this.speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.rigidBody.velocity = this.rigidBody.velocity.normalized * this.speed;
    }
    
    public void destroyPowerup() {
        Destroy(this.gameObject);
    }
}