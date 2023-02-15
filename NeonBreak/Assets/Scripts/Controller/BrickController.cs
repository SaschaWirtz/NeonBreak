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

            StartCoroutine(this.disableBrick());
        }
    }

    /// <summary> 
    /// Coroutine for brick destroy delay.
    /// </summary>
    IEnumerator disableBrick() {
        yield return new WaitForSeconds(0.1f);
        this.gameObject.SetActive(false);
        ScoreManager.GetInstance().AddScore(10);
    }
}