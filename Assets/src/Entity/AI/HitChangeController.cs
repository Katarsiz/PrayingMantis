using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChangeController : DirectionalController {

    /// <summary>
    /// Radius the entity will check to have collided a wall on the direction checker.
    /// </summary>
    public float detectionRadius;
    
    public override void Move() {
        if (facingRight) {
            MoveHorizontally(1);
        } else {
            MoveHorizontally(-1);
        }
    }

    /// <summary>
    /// Entity will change direction if it hits a wall or ground object
    /// </summary>
    /// <returns></returns>
    public override bool DirectionSwapDetected() {
        return Physics2D.Raycast(_directionCheck.position, Vector2.down, detectionRadius, m_WhatIsGround);
    }
}
