using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generation effects generate things when applied
/// </summary>
public abstract class GenerationEffect : Effect {

    /// <summary>
    /// Prefab to generate
    /// </summary>
    protected GameObject generablePrefab;

    protected GameObject generatedObject;

    public GenerationEffect(GameObject generablePrefab) {
        this.generablePrefab = generablePrefab;
    }
}
