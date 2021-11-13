using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class RockWall : MonoBehaviour {

    public float height;

    public float speed;

    /// <summary>
    /// Seconds the rock wall will last 
    /// </summary>
    public float duration;
    
    // Start is called before the first frame update
    void Start() {
        StartCoroutine(EmergeAndDestroy());
    }
    
    private IEnumerator EmergeAndDestroy() {
        yield return Emerge();
        yield return new WaitForSeconds(duration);
        yield return Submerge();
        Destroy(gameObject);
    }

    private IEnumerator Emerge() {
        float currentHeight = 0, addedHeight = 0;
        while (currentHeight<height) {
            addedHeight = speed * Time.fixedDeltaTime;
            currentHeight += addedHeight;
            transform.position += addedHeight * transform.up;
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator Submerge() {
        float currentHeight = 0, addedHeight = 0;
        while (currentHeight<height) {
            addedHeight = speed * Time.fixedDeltaTime;
            currentHeight += addedHeight;
            transform.position -= addedHeight * transform.up;
            yield return new WaitForFixedUpdate();
        }
    }
}
