using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for managing stat effects from elements
/// </summary>
public abstract class Affectable : Movable {

    public List<EntityEffect> activeEffects;

    /// <summary>
    /// Resistances from this entity to each element
    /// </summary>
    public Resistances resistances;

    private readonly int elementIdHash = Animator.StringToHash("EffectId");

    public void ApplyEffect(Effect effect, HitEffectData data) {
        effect.Apply(data);
    }

    public void ApplyAllEffects(Effect[] effectsToApply, HitEffectData data) {
        for (int i = 0; i < effectsToApply.Length; i++) {
            ApplyEffect(effectsToApply[i], data);
        }
    }
    
    /// <summary>
    /// Sets the id of the active effect that the affectable entity will display
    /// </summary>
    /// <param name="newId"></param>
    public void SetAnimatorEffectId(int elementId) {
        // First, animator is tried to execute the effect animation
        if (animator) {
            animator.SetInteger(elementIdHash, elementId);
        }
        // Otherwise, the sprite renderer's color is set to the color the id is assocciated with
        if (TryGetComponent(out SpriteRenderer sr)) {
            sr.color = Element.GetColorById(elementId);
        }
    }

    public float GetResistanceTo(int resistanceIndex) {
        // If no resistance is defined, return the default ones
        if (resistances) {
            return resistances.GetResistanceTo(resistanceIndex);
        }

        return Resistances.GetDefaultResistanceTo(resistanceIndex);
    }
}
