using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadelController : MonoBehaviour
{
    private Vector2 newPosition;
    private Rigidbody2D rb;
    public float speed = 10f;
    public float movementTolerance = 0.1f;

    void Awake() {
        this.newPosition = this.transform.position;
        this.rb = this.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
}