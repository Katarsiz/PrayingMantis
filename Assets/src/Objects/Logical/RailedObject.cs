using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for objects that go to a target through their rails
/// </summary>
public class RailedObject : MonoBehaviour {
    /// <summary>
    /// 
    /// </summary>
    public Transform target;

    public float speed;

    /// <summary>
    /// Distance at which the object will stop if close to target.
    /// </summary>
    public float stopDistance;

    public IEnumerator GoToTarget() {
        Vector3 directionToTarget = Vector3.Normalize(target.position - transform.position);
        while (Vector3.Distance(target.position,transform.position)>stopDistance) {
            directionToTarget = speed * Time.fixedDeltaTime * Vector3.Normalize(target.position - transform.position);
            transform.position += directionToTarget;
            yield return new WaitForFixedUpdate();
        }

        transform.position = target.position;
    }
}
