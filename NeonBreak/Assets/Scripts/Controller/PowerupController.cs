using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    /// <summary> 
    /// The constant speed of the powerup.
    /// </summary>
    public float speed = 2.5f;

    /// <summary> 
    /// Powerup's rigidbody.
    /// </summary>
    private Rigidbody2D rigidBody;

    /// <summary> 
    /// Get reference of rigidbody.
    /// </summary>
    void Awake() {
        this.rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary> 
    /// Initialize velocity.
    /// </summary>
    void Start()
    {
        this.rigidBody.velocity = Vector2.down * this.speed;
    }

    /// <summary> 
    /// Normalize velocity.
    /// </summary>
    void FixedUpdate()
    {
        this.rigidBody.velocity = this.rigidBody.velocity.normalized * this.speed;
    }
    
    /// <summary> 
    /// Destroy Powerup.
    /// </summary>
    public void DestroyPowerup() {
        SoundManager.GetInstance().StartPowerupSFX();

        this.gameObject.SetActive(false);
    }
}