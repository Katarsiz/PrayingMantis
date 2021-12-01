using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable {
    
    public Bug pullLeverBug;

    /// <summary>
    /// On or off
    /// </summary>
    public bool activated;

    public Linkable[] linkedObjects;
    public override void OnInteractionRange() {
    }

    public override void OnOutOfInteractionRange() {
    }

    public override IEnumerator OnInteraction(Interactor i) {
        if (!activated) {
            activated = true;
            // Modifiers of the bug are applied, if any exist
            if (pullLeverBug) {
                pullLeverBug.ApplyAllModifiers();
            }
            foreach (Linkable linkedObject in linkedObjects) {
                linkedObject.Activate();
            }
        }
        yield return null;
    }

    public override void OnInteractionStop(Interactor i) {
        throw new System.NotImplementedException();
    }
}
