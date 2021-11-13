using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : Interactor {
    public MainCharacter character;
    
    public override  void OnInteractionStopTry() {
        if (currentlyInteractingObject) {
            character.OnDropObject();
            currentlyInteractingObject.OnInteractionStop(this);
            currentlyInteractingObject = null;
        }
    }

    public override IEnumerator InteractWithObject(Interactable i) {
        if (i) {
            character.OnHoldObject();
            currentlyInteractingObject = i;
            yield return i.OnInteraction(this);
        }
    }
}
