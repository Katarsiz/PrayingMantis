using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : Pickable {

    /// <summary>
    /// Force this magnet attracts items to itself
    /// </summary>
    public float magnetism;

    public Collider2D magneticCollider;

    public bool activeOnStart;

    private ContactFilter2D _detectionFilter;

    public AudioClip pickAudio;

    /// <summary>
    /// If the magnet must be active or not
    /// </summary>
    private bool _active;

    private void Start() {
        _detectionFilter = new ContactFilter2D();
        _detectionFilter.useTriggers = true;
        if (activeOnStart) {
            StartCoroutine(ActivateMagnet());
        }
    }

    public void SetMagnetism(float newMagnetism) {
        magnetism = newMagnetism;
    }
    
    public override void OnPick(Interactor i) {
        base.OnPick(i);
        AudioEventManager.PlayOneShotAudioClip(pickAudio);
        StartCoroutine(ActivateMagnet());
    }

    public override void OnDrop() {
        _active = false;
        base.OnDrop();
    }

    /// <summary>
    /// Activates the magnet's magnetism
    /// </summary>
    /// <returns></returns>
    public IEnumerator ActivateMagnet() {
        _active = true;
        List<Collider2D> results = new List<Collider2D>();
        while (_active) {
            Physics2D.OverlapCollider(magneticCollider, _detectionFilter, results);
            foreach (Collider2D c in results) {
                Magnetic m = c.GetComponent<Magnetic>();
                m?.OnAttractionDetected(magnetism);             
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
