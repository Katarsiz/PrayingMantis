using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Interactor : MonoBehaviour {

    public Interactable lastInteractableObject;

    /// <summary>
    /// Current object the interactor is interacting with
    /// </summary>
    public Interactable currentlyInteractingObject;
    
    private void OnTriggerEnter2D(Collider2D other) {
        Interactable i;
        if (i = other.GetComponent<Interactable>()) {
            if (lastInteractableObject) {
                lastInteractableObject.OnOutOfInteractionRange();
            }
            lastInteractableObject = i;
            lastInteractableObject.OnInteractionRange();
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        Interactable i;
        if (i = other.GetComponent<Interactable>()) {
            if (i.Equals(lastInteractableObject)) {
                i.OnOutOfInteractionRange();
                lastInteractableObject = null;
            }
        }
    }

    public void TryInteract() {
        StartCoroutine(InteractWithObject(lastInteractableObject));
    }
    
    /// <summary>
    /// Stopping the interaction with an object only makes sense if there is an object being interacted with
    /// </summary>
    public virtual void OnInteractionStopTry() {
        if (currentlyInteractingObject) {
            currentlyInteractingObject.OnInteractionStop(this);
            currentlyInteractingObject = null;
        }
    }

    public virtual IEnumerator InteractWithObject(Interactable i) {
        if (i) {
            currentlyInteractingObject = i;
            yield return i.OnInteraction(this);
        }
    }
}
