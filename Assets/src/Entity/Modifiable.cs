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
    public ScrollManager modifiersContainer;

    /// <summary>
    /// Functions called when the mouse is hovered over the entity
    /// </summary>
    public virtual void OnMouseHovered() {
        animator.SetInteger("EffectId",Element.GLOW);
    }
    
    /// <summary>
    /// Functions called when the mouse has stopped being hovered over the entity
    /// </summary>
    public virtual void OnMouseExited() {
        animator.SetInteger("EffectId",Element.NONE);
    }

    public void ShowModifiers() {
        modifiersContainer.Show();
    }
    
    public void HideModifiers() {
        modifiersContainer.TryHide();
    }
}
