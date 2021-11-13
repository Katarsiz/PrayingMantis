using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : FireZone {

    /// <summary>
    /// Initial radius the breath will have
    /// </summary>
    public float initialRadius;
    
    /// <summary>
    /// Final radius the breath will have
    /// </summary>
    public float finalRadius;

    /// <summary>
    /// How far the breath reaches
    /// </summary>
    public float reach;

    private void OnTriggerEnter2D(Collider2D other) {
        Affectable a = other.GetComponent<Affectable>();
        if (a) {
            burningEffect.Apply(a);
        }
    }

    public IEnumerator ExpandBreath() {
        yield break;
    }
}
