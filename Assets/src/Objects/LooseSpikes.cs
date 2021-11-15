using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseSpikes : MonoBehaviour, Magnetic {
    
    public int damage;
    
    /// <summary>
    /// Speed at which spikes will move once detached
    /// </summary>
    public float speed;
    
    /// <summary>
    /// Location the damaged entity teleports to when taking damaged
    /// </summary>
    public Transform teleportLocation;

    /// <summary>
    /// Direction the spikes will follow once loosen
    /// </summary>
    public GenMath.Direction2D direction;
    
    protected DamageEffect _damageEffect;

    private Vector3 _directionVector;

    private Rigidbody2D rb;

    private bool _attached;

    void Awake() {
        _damageEffect = new DamageEffect(damage);
        _directionVector = GenMath.EnumToVector(direction);
        rb = GetComponent<Rigidbody2D>();
        _attached = true;
    }

    public void Detach() {
        if (_attached) {
            _attached = false;
            StartCoroutine(Loose());
        }
    }

    public IEnumerator Loose() {
        rb.bodyType = RigidbodyType2D.Dynamic;
        while (true) {
            yield return new WaitForFixedUpdate();
            rb.velocity = _directionVector * speed;
        }
    }

    public void OnAttractionDetected(float magneticForce) {
        speed = magneticForce;
        Detach();
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        Affectable a = other.GetComponent<Affectable>();
        if (a) {
            _damageEffect.Apply(a);
            a.gameObject.transform.position = teleportLocation.position;
            Destroy(gameObject);
        }
    }
}