using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : Affectable {

    /// <summary>
    /// Damage the explosion causes to entities
    /// </summary>
    public float damage;

    /// <summary>
    /// Collider that the explosion will use to detect entities
    /// </summary>
    public Collider2D explosionCollider;

    /// <summary>
    /// Objects in this layers will be affected by the explosion
    /// </summary>
    public LayerMask collidableLayers;

    private ContactFilter2D _explosionCF;

    private DamageEffect _damageEffect;

    private AudioSource _explosionSFX;
    // Start is called before the first frame update
    void Start() {
        _damageEffect = new DamageEffect(10);
        _explosionCF = new ContactFilter2D();
        _explosionCF.layerMask = collidableLayers;
        _explosionCF.useLayerMask = true;
        _explosionSFX = GetComponent<AudioSource>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            OnDeath();
        }
    }

    public override void OnDeath() {
        StartCoroutine(Explode());
    }

    public IEnumerator Explode() {
        List<Collider2D> collidedEntities = new List<Collider2D>();
        // Overlap the circle detects all entities that fall inside it
        Physics2D.OverlapCollider(explosionCollider, _explosionCF, collidedEntities);
        foreach (Collider2D entityCollider in collidedEntities) {
            Affectable a = entityCollider.GetComponent<Affectable>();
            if (a && a!=this) {
                _damageEffect.Apply(a);
                continue;
            }
            DestroyableObstacle d = entityCollider.GetComponent<DestroyableObstacle>();
            if (d) {
                Destroy(d.gameObject);
            }
        }
        // Colliders are deactivated and entity becomes invulnerable
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        explosionCollider.enabled = false;
        ApplyInvulnerability(-1);
        // Wait for the audio source to stop playing
        _explosionSFX.Play();
        yield return new WaitForSeconds(_explosionSFX.clip.length);
        // Destroy the object
        Destroy(gameObject);
    }

    public override void OnHeal() {
        throw new System.NotImplementedException();
    }
}