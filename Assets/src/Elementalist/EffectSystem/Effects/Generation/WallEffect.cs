using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEffect : GenerationEffect {
    
    public WallEffect(GameObject prefab) : base(prefab) {
    }

    public override void Apply(HitEffectData hitEffectData) {
        generatedObject = GameObject.Instantiate(generablePrefab, hitEffectData.impactPosition , Quaternion.identity);
        generatedObject.transform.rotation = Quaternion.LookRotation(Vector3.forward,hitEffectData.impactNormal);
    }
}
