using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for activating objects
/// </summary>
public class Activator : MonoBehaviour {

    public Linkable[] activableObjects;

    public void Activate() {
        foreach (Linkable linkable in activableObjects) {
            linkable.Activate();
        }
    }
}