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

    private ContactFilter2D _detectionFilter;

    public void SetMagnetism(float newMagnetism) {
        magnetism = newMagnetism;
    }
    
    public override void OnPick(Interactor i) {
        base.OnPick(i);
        _detectionFilter = new ContactFilter2D();
        _detectionFilter.useTriggers = true;
        StartCoroutine(ActivateMagnet());
    }
    
    /// <summary>
    /// Activates the magnet's magnetism
    /// </summary>
    /// <returns></returns>
    public IEnumerator ActivateMagnet() {
        List<Collider2D> results = new List<Collider2D>();
        while (holded) {
            Physics2D.OverlapCollider(magneticCollider, _detectionFilter, results);
            foreach (Collider2D c in results) {
                Magnetic m = c.GetComponent<Magnetic>();
                m?.OnAttractionDetected(magnetism);             
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
