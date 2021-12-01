using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Element {
    
    /// <summary>
    /// Effects that this element will apply when an entity is hit
    /// </summary>
    protected Effect[] entityEffects;

    /// <summary>
    /// Effects that this element will cast upon hitting an environment object
    /// </summary>
    protected Effect[] environmentalEffects;
    
    public const int NONE = 0, FIRE = 1, ICE = 2, ROCK = 3, WIND = 4, PHANTOM = 5, GLOW = 6;

    public int type;

    public Element(Effect[] entityEffects, Effect[] environmentalEffects) {
        this.entityEffects = entityEffects;
        this.environmentalEffects = environmentalEffects;
    }

    /// <summary>
    /// Applies the element effect on the given affectable
    /// </summary>
    /// <param name="data"></param>
    public void ApplyEntityEffects(HitEffectData data) {
        Affectable a = data.hitObject.GetComponent<Affectable>();
        a.ApplyAllEffects(entityEffects, data);
    }

    public void ApplyEnvironmentalEffects(HitEffectData data) {
        foreach (Effect effect in environmentalEffects) {
            effect.Apply(data);
        }
    }

    /// <summary>
    /// Gets the color asscociated to the given elementId
    /// </summary>
    /// <param name="elemetId"></param>
    /// <returns></returns>
    public static Color GetColorById(int elemetId) {
        switch (elemetId) {
            default:
            case Element.FIRE:
                return Color.red;
            case Element.ICE:
                return Color.cyan;
            case Element.ROCK:
                return Color.yellow;
            case Element.WIND:
                return Color.gray;
            case Element.PHANTOM:
                return Color.magenta;
        }
    }
}