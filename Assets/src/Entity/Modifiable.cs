using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that serves as a container for the logic controllers to request their needed bugs
/// </summary>
public class Modifiable : MonoBehaviour {

    /// <summary>
    /// Game object containing the object modifiers
    /// </summary>
    public ScrollManager modifiersContainer;

    public Animator animator;

    /// <summary>
    /// Bugs this entity can be affected by. Logic components request bugs from the modifiable and assign them to
    /// their corresponding components that need them
    /// </summary>
    public List<Bug> bugs;

    /// <summary>
    /// Functions called when the mouse is hovered over the entity
    /// </summary>
    public virtual void OnMouseHovered() {
        if (animator) {
            animator.SetInteger("EffectId",Element.GLOW);
        }
    }
    
    /// <summary>
    /// Functions called when the mouse has stopped being hovered over the entity
    /// </summary>
    public virtual void OnMouseExited() {
        if (animator) {
            animator.SetInteger("EffectId",Element.NONE);
        }
    }

    public void Correct() {
        for (int i = 0; i < bugs.Count; i++) {
            if (bugs[i].Correct()) {
                break;
            }
        }
    }

    public void ShowModifiers() {
        modifiersContainer.Show();
    }
    
    public void HideModifiers() {
        modifiersContainer.TryHide();
    }
}
