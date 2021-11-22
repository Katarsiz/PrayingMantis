using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : DamageZone, Magnetic {

    /// <summary>
    /// Magnetic for it would take for the spike ball to break
    /// </summary>
    public float breakingMagneticForce;

    private Rigidbody2D _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Affectable a = other.collider.GetComponent<Affectable>();
        if (a) {
            _damageEffect.Apply(a);
            Destroy(gameObject);
        }
    }

    public void OnAttractionDetected(float magneticForce) {
        if (magneticForce > breakingMagneticForce) {
            Detach(magneticForce);
        }
    }

    public void Detach(float initialSpeed) {
        _rb.bodyType = RigidbodyType2D.Dynamic;
    }
}
