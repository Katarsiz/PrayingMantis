using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITracker : MonoBehaviour {
    
    public bool tracked;

    /// <summary>
    /// Place this scroll manager will try to stick to,
    /// </summary>
    public Transform target;
    // Start is called before the first frame update
    void Start() {
        if (tracked) {
            StartCoroutine(TrackTarget());
        }
    }
    
    public IEnumerator TrackTarget() {
        while (tracked) {
            yield return new WaitForFixedUpdate();
            if (!target) {
                tracked = false;
                break;
            }
            transform.position = Camera.main.WorldToScreenPoint(target.position);
        }
    }
}
