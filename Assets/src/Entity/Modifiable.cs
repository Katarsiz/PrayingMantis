using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contains entities capable of being affected by buggs
/// </summary>
public abstract class Modifiable : Affectable {

    /// <summary>
    /// Game object containing the object modifiers
    /// </summary>
    public GameObject modifiersContainer;

    /// <summary>
    /// Collider activated when the player is hovering over the entity
    /// </summary>
    public Collider2D containerCollider;

    /// <summary>
    /// Functions called when the mouse is hovered over the entity
    /// </summary>
    public virtual void OnMouseHovered() {
        animator.SetInteger("EffectId",Element.GLOW);
        containerCollider.enabled = true;
    }
    
    /// <summary>
    /// Functions called when the mouse has stopped being hovered over the entity
    /// </summary>
    public virtual void OnMouseExited() {
        animator.SetInteger("EffectId",Element.NONE);
        containerCollider.enabled = false;
    }

    public void ShowModifiers() {
        modifiersContainer.SetActive(true);
    }
    
    public void HideModifiers() {
        modifiersContainer.SetActive(false);
    }
}
