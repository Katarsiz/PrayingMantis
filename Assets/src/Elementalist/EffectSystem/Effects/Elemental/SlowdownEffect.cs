using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlowdownEffect : EntityEffect {

    /// <summary>
    /// Quantity (In percentage) that the affected entity has its movement speed reduced
    /// </summary>
    private float _intensity;

    private float _duration;

    public SlowdownEffect(float intensity, float duration) {
        _intensity = intensity;
        _duration = duration;
        resistanceType = Resistance.CROWD_CONTROL;
    }
    
    protected override IEnumerator ApplyEffectOnEntity(Affectable a, float resistance) {
        float resistedIntensity = _intensity;
        // Slowdown effect is only resisted if resistance is more than 0.
        if (resistance > 0) {
            resistedIntensity = _intensity * Mathf.Lerp(1f,0f,resistance/100f);
        }
        a.controller.Slowdown(resistedIntensity);
        yield return new WaitForSeconds(_duration);
        a.controller.ResetToFullSpeed();
    }
}
