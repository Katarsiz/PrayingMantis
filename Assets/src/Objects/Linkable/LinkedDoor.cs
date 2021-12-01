using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LinkedDoor : Linkable {
    public override void Activate() {
        transform.Rotate(Vector3.forward,90);
    }

    public override void Deactivate() {
        transform.Rotate(Vector3.forward,-90);
    }
}
