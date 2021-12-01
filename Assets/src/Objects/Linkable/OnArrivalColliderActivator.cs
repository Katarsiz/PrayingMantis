using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnArrivalColliderActivator : MonoBehaviour{

    private bool activated;
    
    public void Activate() {
        if (!activated) {
            activated = true;
            GetComponent<Collider2D>().isTrigger = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.transform.position.y > transform.position.y) {
            Activate();
        }
    }
}
