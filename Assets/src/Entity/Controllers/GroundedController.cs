using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for controlling grounded entities
/// </summary>
public class GroundedController : MovingController {

    [SerializeField] public bool _grounded;            // Whether or not the entity is grounded.
    
    [SerializeField] protected LayerMask m_WhatIsGround;  // A mask determining what is ground to the character

    [SerializeField] protected float groundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    
    protected Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    
    const float _kFallCheckHeight = 3f; // Amount that the fall checker checks when climbing up a ledge
    private Transform m_fallCheck;        // A position marking where to check if no floor is next

    public AudioClip jumpAudio;

    public override void Initialize() {
        base.Initialize();
        m_GroundCheck = transform.Find("GroundCheck");
        m_fallCheck = transform.Find("FallCheck");
    }

    protected virtual void CheckGrounded() {
        _grounded = false;
        if (Physics2D.OverlapCircle(m_GroundCheck.position, groundedRadius, m_WhatIsGround)) {
            _grounded = true;
        }
    }

    public bool GetGrounded() {
        return _grounded;
    }
    
    public bool FallDetected() {
        return !Physics2D.Raycast(m_fallCheck.position, Vector2.down, _kFallCheckHeight, m_WhatIsGround);
    }
}
