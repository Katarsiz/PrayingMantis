using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movable : Damageable {

    /// <summary>
    /// Scripts that controls this entity's movement
    /// </summary>
    public MovingController controller;
    
    protected Animator animator;

    protected virtual void Awake() {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<MovingController>();
        if (controller) {
            controller.Initialize();
            controller.SetAnimator(animator);
        }
    }
    
    public Animator GetAnimator() {
        return animator;
    }

    /// <summary>
    /// Stops the entity's movement
    /// </summary>
    public void FreezeMovement() {
        controller.Freeze();
    }
    
    /// <summary>
    /// Allows the entity to move again
    /// </summary>
    public void UnFreezeMovement() {
        controller.Unfreeze();
    }

    /// <summary>
    /// Teleports the entity to the given position
    /// </summary>
    /// <param name="position"></param>
    /// <returns> Id that tells if the entity teleported successfully or not</returns>
    public virtual int TeleportTo(Vector3 position) {
        controller.Freeze();
        // Complete
        transform.position = position;
        controller.Unfreeze();
        return 0;
    }
}
