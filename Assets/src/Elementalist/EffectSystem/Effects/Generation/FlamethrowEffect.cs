using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowEffect : GenerationEffect {

    public FlamethrowEffect(GameObject generablePrefab) : base(generablePrefab) {
    }

    public override void Apply(HitEffectData hitEffectData) {
        Vector3 bounceDirection = Vector3.Reflect(hitEffectData.impactDirection, hitEffectData.impactNormal);
        generatedObject = GameObject.Instantiate(generablePrefab, hitEffectData.impactPosition , Quaternion.identity);
        Vector3 displacementVector = bounceDirection * (generatedObject.GetComponent<CircleCollider2D>().radius);
        generatedObject.transform.position += displacementVector;
        generatedObject.transform.right = bounceDirection;
    }
}
