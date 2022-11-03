using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{

    public float speed = 5f;
    public Vector2 respawnPosition = new Vector2(0f, 0f);
    private Rigidbody2D rigidBody;
    private readonly Vector2 initPosition = new Vector2(0f, -3.4f);

    void Awake() {
        this.rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.Respawn(true);
        GameObject.Find("lowerEdge").GetComponent<LowerEdgeController>().respawn += this.RespawnTrigger;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.rigidBody.velocity = this.rigidBody.velocity.normalized * this.speed;
    }

    void OnDestroy() {
        LowerEdgeController lowerEdge = GameObject.Find("lowerEdge").GetComponent<LowerEdgeController>();
        if(lowerEdge != null) {
            lowerEdge.respawn -= this.RespawnTrigger;
        }
        
    }

    // Please don't laugh, this is a compiler error fix. xD
    private void RespawnTrigger() {
        this.Respawn();
    }

    private void Respawn(bool isInit = false, int respawnDelay = 1) {
        if(!isInit) {
            ScoreManager.GetInstance().LoseLife();
        }
        this.transform.position = isInit ? this.initPosition : this.respawnPosition;
        this.rigidBody.velocity = Vector2.zero * this.speed;
        this.StartCoroutine(this.SetVelocity(respawnDelay));
    }

    private IEnumerator SetVelocity(int respawnDelay) {
        yield return new WaitForSeconds(respawnDelay);
        this.rigidBody.velocity = RandomHelper.GetRandomDownwardUnitVector(-0.7f, 0.7f) * this.speed;
    }
}
