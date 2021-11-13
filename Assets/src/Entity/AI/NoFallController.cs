using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoFallController : HitChangeController {

    /// <summary>
    /// Magnitude the raycast checking the floor will have
    /// </summary>
    public float floorCheckMagnitude;
    
    protected override void FixedUpdate() {
        CheckGrounded();
        if (!_grounded) return;
        
        if (DirectionSwapDetected()) {
            OnSwapDirection();
        }
        
        if (canMove) {
            Move();
        }
    }

    public override bool DirectionSwapDetected() {
        // Raycasts to detect if there is no more ground or if a wall is collided
        return !Physics2D.Raycast(_directionCheck.position, Vector2.down, floorCheckMagnitude, m_WhatIsGround).collider
            || base.DirectionSwapDetected();
    }
}
