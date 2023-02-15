using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickController : MonoBehaviour
{
    /// <summary>
    /// Set brick type.
    /// </summary>
    public BrickType type = BrickType.Default;

    /// <summary> 
    /// Destroys brick on impact and triggers powerup method.
    /// </summary>
    /// <param name="collision"> 
    /// Colliding collision information.
    /// </param>
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Balls") {
            SoundManager.GetInstance().StartBrickHitSFX();

            if (this.type != BrickType.Default) {
                PowerupFactory.Instance.spawnPowerup(this.type, this.transform.position * 1);
            }

            BrickFactory.GetInstance().ReturnBrick(this.gameObject);
            ScoreManager.GetInstance().AddScore(10);
        }
    }
}