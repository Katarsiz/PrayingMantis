using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : Door {
    public override IEnumerator OnInteraction(Interactor i) {
        if (i.canOpenLockedDoors) {
            Open();
        }
        yield return null;
    }
}
