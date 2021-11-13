using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// One shot linkables only activate once
/// </summary>
public abstract class OneShotLinkable : Linkable {

    public bool activated;

    public override void OnSignalReceived() {
        if (!activated) {
            activated = true;
            Activate();
        }
    }
}
