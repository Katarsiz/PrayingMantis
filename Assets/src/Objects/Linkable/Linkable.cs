using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Linkable : MonoBehaviour {
    public virtual void OnSignalReceived() {
        Activate();
    }
    
    public virtual void OnSignalStop() {
        Deactivate();
    }

    public abstract void Activate();
    
    public abstract void Deactivate();
}
