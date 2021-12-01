using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkableRailedObject : Linkable {
    
    /// <summary>
    /// 
    /// </summary>
    public Transform target;

    public float speed;

    /// <summary>
    /// Distance at which the object will stop if close to target.
    /// </summary>
    public float stopDistance;

    /// <summary>
    /// REPATED : BAD
    /// </summary>
    /// <returns></returns>
    public IEnumerator GoToTarget() {
        Vector3 directionToTarget = Vector3.Normalize(target.position - transform.position);
        float temporal = 0;
        while (Vector3.Distance(target.position,transform.position)>stopDistance) {
            temporal = speed * Time.fixedDeltaTime; 
            transform.position += temporal * directionToTarget;
            yield return new WaitForFixedUpdate();
        }

        transform.position = target.position;
    }
    
    public override void Activate() {
        StartCoroutine(GoToTarget());
    }

    public override void Deactivate() {
        throw new System.NotImplementedException();
    }
}
