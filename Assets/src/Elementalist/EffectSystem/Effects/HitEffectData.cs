using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Container class with all the required data to apply environmental effects 
/// </summary>
public class HitEffectData{
    
    /// <summary>
    /// Position of the environment where the 
    /// </summary>
    public Vector3 impactPosition;

    public Vector3 impactDirection;

    public Vector3 impactNormal;

    /// <summary>
    /// Object the effect is being casted upon
    /// </summary>
    public GameObject hitObject;

    public HitEffectData(Vector3 impactPosition, Vector3 impactDirection, Vector3 impactNormal, GameObject hitObject) {
        this.hitObject = hitObject;
        this.impactPosition = impactPosition;
        this.impactDirection = impactDirection;
        this.impactNormal = impactNormal;
    }
}