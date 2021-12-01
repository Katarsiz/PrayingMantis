using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEffect : Effect {

    public Affectable sourceEntity;

    public TeleportEffect(Affectable sourceEntity) {
        this.sourceEntity = sourceEntity;
    }

    public override void Apply(HitEffectData hitEffectData) {
        sourceEntity.TeleportTo(hitEffectData.impactPosition);
    }
}
