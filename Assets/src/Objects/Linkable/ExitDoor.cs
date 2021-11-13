using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : OneShotLinkable {
    public override void Activate() {
        transform.Rotate(Vector3.forward,90);
    }

    public override void Deactivate() {
    }
}
