using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour {
    
    public abstract void OnInteractionRange();

    public abstract void OnOutOfInteractionRange();

    public abstract IEnumerator OnInteraction(Interactor i);
    
    public abstract void OnInteractionStop(Interactor i);
}