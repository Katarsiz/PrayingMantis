using System;
using System.Collections;
using UnityEngine;

public class FragileFloor : MonoBehaviour {
    
    /// <summary>
    /// Max weight this floor can hold before being destroyed
    /// </summary>
    public float maxWeight;

    /// <summary>
    /// Time it takes for the floor to completely break;
    /// </summary>
    public float breakTime;

    /// <summary>
    /// Flag that locks the fall function
    /// </summary>
    private bool _breaking;
    
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent(out PlatformerCharacter2D character)) {
            if (character.GetWeight() > maxWeight) {
                StartCoroutine(FallIn());
            }
        }
    }

    public void Fall() {
        Destroy(gameObject);
    }

    public IEnumerator FallIn() {
        _breaking = true;
        yield return new WaitForSeconds(breakTime);
        Fall();
    }
}
