using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DirectionalController : GroundedController {

    protected Transform _directionCheck;

    public override void Initialize() {
        base.Initialize();
        _directionCheck = transform.Find("DirectionCheck");
    }

    protected virtual void FixedUpdate() {
        if (DirectionSwapDetected()) {
            OnSwapDirection();
        }
        Move();
    }

    public abstract void Move();

    public abstract bool DirectionSwapDetected();

    public void OnSwapDirection() {
        rb.velocity = new Vector2(0, rb.velocity.y);
        Flip();
    }
}
