using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFactory {

    /// <summary>
    /// Entity using this factory
    /// </summary>
    public Affectable sourceEntity;
    
    private const int BURNING_DAMAGE = 3;
    
    private const float BURNING_DURATION = 3f;
    
    private const float BURNING_RATE = 0.5f;
    
    private float FREEZE_DURATION = 2f;
    
    private int ROCK_DAMAGE = 5;
    public ElementFactory(Affectable sourceEntity) {
        this.sourceEntity = sourceEntity;
        GetEffectData();
    }

    private void GetEffectData() {
    }

    /// <summary>
    /// Creates a new element with the given element Id and links it to the entity 
    /// </summary>
    /// <param name="usingEntity"></param>
    /// <param name="elementId"></param>
    /// <returns></returns>
    public Element CreateNewElement(int elementId) {
        Element result = null;
        
        Effect [] enviromentalEffects = GetEnvironmentalEffects(elementId), entityEffects = GetEntityEffects(elementId);
        switch (elementId) {
            default:
            case Element.FIRE:
                result = new FireElement(entityEffects,enviromentalEffects);
                break;
            case Element.ICE:
                result = new IceElement(entityEffects,enviromentalEffects);
                break;
            case Element.ROCK:
                result = new RockElement(entityEffects,enviromentalEffects);
                break;
            case Element.WIND:
                result = new WindElement(entityEffects,enviromentalEffects);
                break;
            case Element.PHANTOM:
                result = new PhantomElement(entityEffects,enviromentalEffects);
                break;
        }

        return result;
    }
    
    /// <summary>
    /// Gets the effect's that affect an entity based on the element id
    /// </summary>
    /// <param name="elementId"></param>
    /// <returns></returns>
    public Effect[] GetEntityEffects(int elementId) {
        Effect[] result;
        switch (elementId) {
            default:
            case Element.FIRE:
                result = new Effect[] { new FlamethrowEffect(Resources.Load ("prefab/Elements/EffectObjects/FireBreath") as GameObject) };
                break;
            case Element.ICE:
                result = new Effect[] { new FreezeEffect(FREEZE_DURATION) };
                break;
            case Element.ROCK:
                result = new Effect[] { new DamageEffect(ROCK_DAMAGE) };
                break;
            case Element.WIND:
                result = new Effect[] { new FlurryEffect(Resources.Load ("prefab/Elements/EffectObjects/WindFlurry") as GameObject) };
                break;
            case Element.PHANTOM:
                result = new Effect[] { new SwapEffect(sourceEntity) };
                break;
        }
        return result;
    }
    
    /// <summary>
    /// Gets the effect's that affect an entity based on the element id
    /// </summary>
    /// <param name="elementId"></param>
    /// <returns></returns>
    public Effect[] GetEnvironmentalEffects(int elementId) {
        Effect[] result;
        switch (elementId) {
            default:
            case Element.FIRE:
                result = new Effect[] { new FlamethrowEffect(Resources.Load ("prefab/Elements/EffectObjects/FireBreath") as GameObject) };
                break;
            case Element.ICE:
                result = new Effect[] { new FlurryEffect(Resources.Load ("prefab/Elements/EffectObjects/WindFlurry") as GameObject) };
                break;
            case Element.ROCK:
                result = new Effect[] { new WallEffect(Resources.Load ("prefab/Elements/EffectObjects/RockWall") as GameObject) };
                break;
            case Element.WIND:
                result = new Effect[] { new FlurryEffect(Resources.Load ("prefab/Elements/EffectObjects/WindFlurry") as GameObject) };
                break;
            case Element.PHANTOM:
                result = new Effect[] { new TeleportEffect(sourceEntity) };
                break;
        }
        return result;
    }
}
