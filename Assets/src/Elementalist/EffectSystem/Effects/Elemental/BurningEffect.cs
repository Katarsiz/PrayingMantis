using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurningEffect : EntityEffect {

    private float _duration;

    private float _repetitionRate;

    private int _absoluteDamage;
    
    public BurningEffect(int damage, float duration, float repetitionRate) {
        _duration = duration;
        _repetitionRate = repetitionRate;
        resistanceType = Resistance.BURNING;
        _absoluteDamage = damage;
    }

    protected override IEnumerator ApplyEffectOnEntity(Affectable a, float resistance) {
        a.SetAnimatorEffectId(Element.FIRE);
        int reducedDamage = Mathf.RoundToInt(_absoluteDamage * GetBasicMultiplier(resistance));
        float currentTime = 0;
        while (currentTime<_duration) {
            a.TakeDamage(reducedDamage);
            yield return new WaitForSeconds(_repetitionRate);
            currentTime += _repetitionRate;
        }
        a.SetAnimatorEffectId(Element.NONE);
    }
}
