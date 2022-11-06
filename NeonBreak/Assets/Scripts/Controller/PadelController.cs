using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadelController : MonoBehaviour
{
    /// <summary> 
    /// New destination position of padel.
    /// </summary>
    private Vector2 newPosition;

    /// <summary> 
    /// Padel's rigidbody.
    /// </summary>
    private Rigidbody2D rb;

    /// <summary> 
    /// Coroutine for enlarge powerup.
    /// </summary>
    private IEnumerator enlargePadelCoroutine;

    /// <summary> 
    /// Max speed of padel.
    /// </summary>
    public float speed = 10f;

    /// <summary> 
    /// Tolerance for movement detection.
    /// </summary>
    public float movementTolerance = 0.1f;

    /// <summary> 
    /// Initializing gameobject.
    /// </summary>
    void Awake() {
        this.newPosition = this.transform.position;
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    /// <summary> 
    /// Controlling padel logic.
    /// </summary>
    void FixedUpdate()
    {
        if (Input.touchCount > 0) {
            var touch = Input.GetTouch(0);
            var pos = Camera.main.ScreenToWorldPoint(touch.position);
            pos.z = 0.0f;
            pos.y = this.transform.position.y;
            this.newPosition = pos;
        }

        Vector2 moveDirection;

        if (this.transform.position.x - this.movementTolerance > this.newPosition.x) {
            moveDirection = Vector2.left;
        } else if (this.transform.position.x + this.movementTolerance < this.newPosition.x) {
            moveDirection = Vector2.right;
        } else {
            moveDirection = Vector2.zero;
        }

        this.rb.velocity = moveDirection * this.speed;
    }

    /// <summary> 
    /// Trigger powerups and store them for repetition. 
    /// </summary>
    /// <param name="collider"> 
    /// Colliding collider information.
    /// </param>
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "EnlargePadel") {
            ScoreManager.GetInstance().AddScore(50);
            collider.gameObject.GetComponent<PowerupController>().DestroyPowerup();
            if(this.enlargePadelCoroutine != null) {
                this.StopCoroutine(this.enlargePadelCoroutine);
            }
            this.enlargePadelCoroutine = this.enlargePadel();
            this.StartCoroutine(this.enlargePadelCoroutine);
        }
    }

    /// <summary> 
    /// Coroutine for enlarge padel powerup.
    /// </summary>
    private IEnumerator enlargePadel() {
        this.transform.localScale = new Vector3(0.7f, 0.5f, 1f);
        yield return new WaitForSeconds(10);
        this.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        this.enlargePadelCoroutine = null;
    }
}