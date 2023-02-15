using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
    /// <summary>
    /// Awake method initializing the autodestroy
    /// </summary>
    public void Awake() {
        this.StartCoroutine(this.WaitForSound());
    }

    /// <summary>
    /// Wait till sound finishes and destroy the object.
    /// </summary>
    /// <returns>
    /// IEnumerator for yielding purposes
    /// </returns>
    private IEnumerator WaitForSound() {
        yield return new WaitUntil(() => this.gameObject.GetComponent<AudioSource>().isPlaying == false);
        Destroy(this.gameObject);
    }
}