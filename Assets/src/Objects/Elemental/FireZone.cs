using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireZone : MonoBehaviour {

    public int damage;
    
    /// <summary>
    /// Duration of the burn effect once the entity is out of the zone
    /// </summary>
    public float duration;

    /// <summary>
    /// Rate at which the damage is dealt to the player
    /// </summary>
    public float burningRate;

    protected BurningEffect burningEffect;

    /// <summary>
    /// Entities currently in contact with the fire zone
    /// </summary>
    protected List<Affectable> contactEntities;

    // Start is called before the first frame update
    void Start() {
        contactEntities = new List<Affectable>();
        burningEffect = new BurningEffect(damage, duration, burningRate);
    }

    private void OnCollisionExit2D(Collision2D other) {
        Affectable a = other.collider.GetComponent<Affectable>();
        if (a) {
            burningEffect.Apply(a);
        }
    }
}
