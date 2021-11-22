using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnExitTriggerActivator : Activator {
    
    private bool activated;

    public float touchingObjects;

    private void OnTriggerEnter2D(Collider2D other) {
        touchingObjects++;
    }

    private void OnTriggerExit2D(Collider2D other) {
        touchingObjects--;
        if (!activated && touchingObjects==0) {
            activated = true;
            Activate();
        }
    }
}
