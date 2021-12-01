using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float destroyTime = 3f;

    public bool mustBeDestroyed;

    public void Initialize() {
        StartCoroutine(DestroyAfter(destroyTime));
    }

    private void OnTriggerEnter2D(Collider2D other) {
        MarkToDestroy();
    }

    private void OnDestroy() {
        StopAllCoroutines();
    }

    public void MarkToDestroy() {
        mustBeDestroyed = true;
    }

    /// <summary>
    /// Destroys the projectile after s seconds
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public IEnumerator DestroyAfter(float s) {
        yield return new WaitForSeconds(s);
        MarkToDestroy();
    }
}
