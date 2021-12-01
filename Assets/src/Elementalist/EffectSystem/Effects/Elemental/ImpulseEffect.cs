using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpulseEffect : EntityEffect {
    public ImpulseEffect() {
        resistanceType = Resistance.CROWD_CONTROL;
    }

    protected override IEnumerator ApplyEffectOnEntity(Affectable a, float resistance) {
        throw new System.NotImplementedException();
    }
}
