using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPot : Affectable {
    
    public GameObject[] prefabsToDrop;

    /// <summary>
    /// When an object pot dies, it drops items that the player can pick
    /// </summary>
    public override void OnDeath() {
        foreach (GameObject drop in prefabsToDrop) {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
        base.OnDeath();
    }

    public override void OnHeal() {
        throw new System.NotImplementedException();
    }
}
