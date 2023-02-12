using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    /// <summary> 
    /// The constant speed of the ball.
    /// </summary>
    public float speed = 5f;

    /// <summary> 
    /// Spawn position of ball every time it respawns.
    /// </summary>
    public Vector2 respawnPosition = new Vector2(0f, 0f);

    /// <summary> 
    /// Rigidbody of ball.
    /// </summary>
    private Rigidbody2D rigidBody;
    
    /// <summary> 
    /// Spawn position of initial spawn.
    /// </summary>
    private readonly Vector2 initPosition = new Vector2(0f, -3.4f);

    /// <summary> 
    /// Saves current velocity for anti softlock.
    /// </summary>
    private Vector2 firstVelocity = new Vector2(0f, 0f);

    /// <summary> 
    /// Saves past velocity for anti softlock.
    /// </summary>
    private Vector2 secondVelocity = new Vector2(0f, 0f);

    /// <summary> 
    /// Determines anti softlock method.
    /// </summary>
    private int unstuckMethod = 0;

    /// <summary> 
    /// Determines how much the direction of the ball changes on paddel collision.
    /// </summary>
    [SerializeField]
    private float ballVelocityModifier = 10;

    /// <summary> 
    /// Getting reference for rigid body.
    /// </summary>
    void Awake() {
        this.rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary> 
    /// Triggers initial respawn and adding delegate function for later respawns.
    /// </summary>
    void Start()
    {
        this.Respawn(true);
        GameObject.Find("lowerEdge").GetComponent<LowerEdgeController>().respawn += this.RespawnTrigger;
    }

    /// <summary> 
    /// Normalize ball speed and trigger softlock check.
    /// </summary>
    void FixedUpdate()
    {
        this.rigidBody.velocity = this.rigidBody.velocity.normalized * this.speed;
        this.checkForRepeatingMovements();
    }

    /// <summary> 
    /// Remove delegate function to prevent nullpointer exception.
    /// </summary>
    void OnDestroy() {
        LowerEdgeController lowerEdge = GameObject.Find("lowerEdge").GetComponent<LowerEdgeController>();
        if(lowerEdge != null) {
            lowerEdge.respawn -= this.RespawnTrigger;
        }
        
    }

    /// <summary> 
    /// Modifies ball velocity dependat on collision.
    /// </summary>
    /// <param name="collision"> 
    /// Colliding gameobject.
    /// </param>
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            float ballModifier = (this.transform.position.x - collision.transform.position.x) / collision.collider.bounds.size.x * this.ballVelocityModifier;
            this.rigidBody.velocity += (new Vector2(ballModifier, 0f));
            this.rigidBody.velocity = this.rigidBody.velocity.normalized * this.speed;
        }
    }

    /// <summary> 
    /// Please don't laugh, this is a compiler error fix. xD
    /// </summary>
    private void RespawnTrigger() {
        this.Respawn();
    }

    /// <summary> 
    /// Triggers respawn logic after life was lost.
    /// </summary>
    /// <param name="isInit">
    /// Determines if initial spawn or respawn.
    /// </param>
    /// <param name="respawnDelay">
    /// Determines the time between the ball appearing and starts moving.
    /// </param>
    public void Respawn(bool isInit = false, int respawnDelay = 1) {
        if(!isInit) {
            ScoreManager.GetInstance().LoseLife();
        }
        this.transform.position = isInit ? this.initPosition : this.respawnPosition;
        this.rigidBody.velocity = Vector2.zero * this.speed;
        this.StartCoroutine(this.SetVelocity(respawnDelay));
    }

    /// <summary> 
    /// Method to unstuck the ball if it gets softlocked.
    /// </summary>
    private void checkForRepeatingMovements() {

        // Catches cases in which no adjustment is needed
        if(this.rigidBody.velocity == Vector2.zero) {
            return;
        }else if(this.rigidBody.velocity == this.firstVelocity) {
            return;
        }else if(this.rigidBody.velocity != this.secondVelocity) {
            this.secondVelocity = this.firstVelocity;
            this.firstVelocity = this.rigidBody.velocity;
            return;
        }

        // Unstuckes ball with change of velocity vector
        if(this.unstuckMethod == 0) {
            this.rigidBody.velocity += new Vector2(-0.5f, 0.5f);
            this.unstuckMethod += 1;
        }else {
            this.rigidBody.velocity += new Vector2(0.5f, -0.5f);
            this.unstuckMethod -= 1;
        }
        this.rigidBody.velocity = this.rigidBody.velocity.normalized * this.speed;
        this.secondVelocity = this.firstVelocity;
        this.firstVelocity = this.rigidBody.velocity;
    }

    /// <summary> 
    /// Coroutine for spawn delay.
    /// </summary>
    private IEnumerator SetVelocity(int respawnDelay) {
        yield return new WaitForSeconds(respawnDelay);
        this.rigidBody.velocity = RandomHelper.GetRandomDownwardUnitVector(-0.7f, 0.7f) * this.speed;
    }
}
