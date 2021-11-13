using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickable : Interactable {
    
    /// <summary>
    /// Color to change the sprite to when in range of interaction
    /// </summary>
    public Color interactableColor;

    public Color idleColor;

    /// <summary>
    /// Flag that determinates if the object is being holded
    /// </summary>
    protected bool holded;
    
    public override void OnInteractionRange() {
        GetComponent<SpriteRenderer>().color = interactableColor;
    }

    public override void OnOutOfInteractionRange() {
        GetComponent<SpriteRenderer>().color = idleColor;
    }

    public override IEnumerator OnInteraction(Interactor i) {
        OnPick(i);
        yield return null;
    }
    
    public override void OnInteractionStop(Interactor i) {
        OnDrop();
    }

    public virtual void OnDrop() {
        transform.parent = null;
        GetComponent<Rigidbody2D>().simulated = true;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        holded = false;
    }

    public virtual void OnPick(Interactor i) {
        GetComponent<Collider2D>().enabled = false;
        transform.position = i.transform.position;
        transform.parent = i.transform;
        holded = true;
    }
}
