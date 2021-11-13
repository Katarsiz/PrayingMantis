using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityEffect : Effect {
    
    /// <summary>
    /// Type of resistance this effect's functions will be affected by
    /// </summary>
    public int resistanceType;

    public float GetBasicMultiplier(float resistance) {
        if (resistance < 0) {
            return Mathf.Lerp(1f,2f,Mathf.Abs(resistance)/100f);
        } else if (resistance > 0) {
            return Mathf.Lerp(1f,0f,resistance/100f);
        }
        return 1f;
    }
    
    public virtual void Apply(Affectable a) {
        a.StartCoroutine(ApplyEffectOnEntity(a, a.GetResistanceTo(resistanceType)));
    }

    public override void Apply(HitEffectData hitEffectData) {
        Affectable a = hitEffectData.hitObject.GetComponent<Affectable>();
        a.StartCoroutine(ApplyEffectOnEntity(a, a.GetResistanceTo(resistanceType)));
    }

    protected abstract IEnumerator ApplyEffectOnEntity(Affectable a, float resistance);
}
