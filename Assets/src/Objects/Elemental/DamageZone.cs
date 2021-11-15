using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour {

    public int damage;

    /// <summary>
    /// Time after which a damage zone can attack an entity once again
    /// </summary>
    public float damageCooldown;

    /// <summary>
    /// Location the damaged entity teleports to when taking damaged
    /// </summary>
    public Transform teleportLocation;

    protected DamageEffect _damageEffect;

    /// <summary>
    /// Entities and its current damage effect cooldown;
    /// </summary>
    protected HashSet<Affectable> _entityCooldowns; 

    protected virtual void Awake() {
        _entityCooldowns = new HashSet<Affectable>();
        _damageEffect = new DamageEffect(damage);
    }

    private void OnTriggerStay2D(Collider2D other) {
        Affectable a = other.GetComponent<Affectable>();
        if (a && !_entityCooldowns.Contains(a)) {
            _damageEffect.Apply(a);
            a.gameObject.transform.position = teleportLocation.position;
            StartCoroutine(CooldownAffectable(damageCooldown, a));
        }
    }

    /// <summary>
    /// Avoids the damage zone to attack the entity for the given time.
    /// </summary>
    /// <param name="seconds"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    private IEnumerator CooldownAffectable(float seconds, Affectable a) {
        _entityCooldowns.Add(a);
        yield return new WaitForSeconds(seconds);
        _entityCooldowns.Remove(a);
    }
}
