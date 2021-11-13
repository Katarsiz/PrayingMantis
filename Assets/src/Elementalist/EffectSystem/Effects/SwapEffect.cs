using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapEffect : EntityEffect {

    /// <summary>
    /// Entity that is casting this effect
    /// </summary>
    public Movable sourceEntity;

    public SwapEffect(Movable entity) {
        sourceEntity = entity;
    }

    protected override IEnumerator ApplyEffectOnEntity(Affectable a, float resistance) {
        Vector3 sourcePosition = sourceEntity.transform.position;
        sourceEntity.TeleportTo(a.transform.position);
        a.TeleportTo(sourcePosition);
        yield return null;
    }
}
