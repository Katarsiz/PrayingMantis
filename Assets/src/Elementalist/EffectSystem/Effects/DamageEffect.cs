using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Effect that damages an entity
/// </summary>
public class DamageEffect : EntityEffect {

    private int _absoluteDamage;

    public DamageEffect(int damage) {
        _absoluteDamage = damage;
        resistanceType = Resistance.PHYSICAL;
    }

    protected override IEnumerator ApplyEffectOnEntity(Affectable a, float resistance) {
        a.TakeDamage(Mathf.RoundToInt(_absoluteDamage * GetBasicMultiplier(resistance)));
        yield return null;
    }
}
