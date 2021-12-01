using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationButton : MonoBehaviour {
    
    public Linkable linkedObject;

    public int touchingObjects;

    public void OnTriggerEnter2D(Collider2D other) {
        ActivateLinked();
        touchingObjects++;
    }
    
    public void OnTriggerExit2D(Collider2D other) {
        touchingObjects--;
        DeactivateLinked();
    }

    public void ActivateLinked() {
        if (touchingObjects <= 0) {
            if (linkedObject) {
                linkedObject.OnSignalReceived();
            }
        }
    }
    
    public void DeactivateLinked() {
        if (touchingObjects <= 0) {
            if (linkedObject) {
                linkedObject.OnSignalStop();
            }
        }
    }
}
