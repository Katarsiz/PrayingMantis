using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeEffect : EntityEffect {

    private float _duration;

    public FreezeEffect(float duration) {
        _duration = duration;
        resistanceType = Resistance.FREEZING;
    }

    protected override IEnumerator ApplyEffectOnEntity(Affectable a, float resistance) {
        a.SetAnimatorEffectId(Element.ICE);
        a.FreezeMovement();
        float reducedDuration = Mathf.RoundToInt(_duration * GetBasicMultiplier(resistance));
        yield return new WaitForSeconds(reducedDuration);
        a.SetAnimatorEffectId(Element.NONE);
        a.UnFreezeMovement();
    }
}
