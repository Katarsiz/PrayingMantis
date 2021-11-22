using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable {

    public Linkable[] linkedObjects;

    /// <summary>
    /// Bug called when the button is pressed
    /// </summary>
    public Bug pressedBug;
    
    private bool _pressed;
    public override void OnInteractionRange() {
    }

    public override void OnOutOfInteractionRange() {
    }

    public override IEnumerator OnInteraction(Interactor i) {
        if (!_pressed) {
            _pressed = true;
            foreach (Linkable linkedObject in linkedObjects) {
                pressedBug.ApplyAllModifiers();
                linkedObject.Activate();
            }
        }
        yield return null;
    }

    public override void OnInteractionStop(Interactor i) {
    }
}
