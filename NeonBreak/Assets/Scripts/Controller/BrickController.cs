using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    public BrickType type = BrickType.Default;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Balls") {
            if (this.type != BrickType.Default) {
                PowerupFactory.Instance.spawnPowerup(this.type, this.transform.position * 1);
            }

            Destroy(this.gameObject);
        } 
    }
}