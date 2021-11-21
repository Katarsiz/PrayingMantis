using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

#pragma warning disable 649
public class PlatformerCharacter2D : GroundedController {

        [SerializeField] private float clingSpeed = 3f;
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;  // Amount of maxSpeed applied to crouching movement. 1 = 100%
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsClingable;
        [SerializeField] private float dodgeSpeed = 15f;
        [SerializeField] private float dodgeTime = 1.5f;  // Time a dodge lasts
        [SerializeField] private float dodgeCooldown = 1f; // Time a player can't dodge just after dodging

        public Collider2D climbCollider; // Collider used to check if the player is able to climb
        
        public float dodgeXMove;                   // x move in which the player is dodging
        
        public ParallaxPlus [] parallaxGenerators;

        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        const float k_ClingCheckRadius = .2f;

        private Transform _clingCheck;
        const float _kClimbingLength = 2.33f; // Amount that the climber checks when climbing up a ledge
        const float _kClimbingHeight = 2.25f; // Amount that the climber checks when climbing up a ledge
        private Transform _climbCheck; // Transform that the player will raycast to check if it must climb
        const float _kObstacleCheckLength = 1.5f; // Amount that the obstacle checker checks when climbing up a ledge
        private Transform m_obstacleCheck;    // A position marking where to check if an obstacle is found
        private bool _clinging;

        private bool canRotate = true;             // Flag that locks the playerrotation
        private bool _canDodge = true;                     // Flag that locks the playe rdodge

        private SpriteRenderer _spriteRenderer;
        
        private bool _checkCling;

        /// <summary>
        /// Contains the functions called when a player jumps 
        /// </summary>
        public Bug jumpBug;

        /// <summary>
        /// Threshold of velocity for the flip to be applied
        /// </summary>
        private float FLIP_THRESHOLD = 0.3f;
        
        /// <summary>
        /// Time the rotation is locked when wallbouncing
        /// </summary>
        private const float ROTATION_LOCK_TIME = 0.3f;
        
        /// <summary>
        /// Time the cling check is locked when wallbouncing
        /// </summary>
        private const float CLINGCHECK_LOCK_TIME = 0.3f;

        private ContactFilter2D climbFilter = new ContactFilter2D();

        public override void Initialize() {
            base.Initialize();
            // Setting up references.
            m_CeilingCheck = transform.Find("CeilingCheck");
            _clingCheck = transform.Find("ClingCheck");
            _climbCheck = transform.Find("ClimbCheck");
            m_obstacleCheck = transform.Find("FallCheck");
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            climbFilter.layerMask = m_WhatIsGround;
            climbFilter.useLayerMask = true;

            character = GetComponent<MainCharacter>();
        }

        private void FixedUpdate() {
            CheckGrounded();
            
            animator.SetBool("Ground", _grounded);

            // Set the vertical animation
            animator.SetFloat("vSpeed", rb.velocity.y);
        }

        private void OnCollisionEnter2D(Collision2D other) {
            // Cling is only checked if the player isn't grounded and isn't already clinging
            if (!_clinging && !_grounded) {
                RaycastHit2D hit = Physics2D.Raycast(_clingCheck.position, directionVector, 
                    k_ClingCheckRadius,m_WhatIsClingable);
                // If hit detected, activate cling and deactivate cling checking
                if (hit) {
                    Cling(hit.point);
                } // If not, a raycast is done to detect if player is able to climb a corner
                else {
                    TryClimb(_kClimbingHeight,_kClimbingLength);
                }
            }
        }

        public void Move(float xMove, float yMove, bool crouch, bool jump) {

            // If crouch is tried and the character is grounded, a crouch is tried
            if (crouch && canRotate && _canDodge) {
                StartCoroutine(Dodge(directionVector.x));
            }

            //only control the player if grounded or airControl is turned on
            if ((_grounded || m_AirControl) && canRotate) {
                // Reduce the speed if crouching by the crouchSpeed multiplier
                xMove = crouch ? xMove*m_CrouchSpeed : xMove;

                // The Speed animator parameter is set to the absolute value of the horizontal input.
                animator.SetFloat("Speed", Mathf.Abs(xMove));

                // Move the character
                if (xMove != 0 && !_clinging && canMove) {
                    MoveHorizontally(xMove);
                }
            }

            // Player can jump if he hasn't double jumped or he is grounded
            if (jump) {
                if (_clinging) {
                    WallBounce();
                } else if(_grounded) {
                    _grounded = false;
                    animator.SetBool("Ground", false);
                    Jump();
                }
            }

            // If clinging, check the same raycast to determine if dropping the player or making it climb a corner
            if (_clinging) {
                // Otherwise, the player is checked to be still clinging 
                RaycastHit2D hit = Physics2D.Raycast(_clingCheck.position, -_clingCheck.right, k_ClingCheckRadius,m_WhatIsClingable);
                if (hit) {
                    RaycastHit2D climbHit = Physics2D.Raycast(_climbCheck.position, directionVector, 
                        _kClimbingLength,m_WhatIsClingable);
                    // If climb hit collided nothing, the player must climb
                    if (!climbHit) {
                        // Clinging nullification
                        _spriteRenderer.flipX = !_spriteRenderer.flipX;
                        animator.SetBool("Clinging",false);
                        _clinging = false;
                        float heightToClimb = _kClimbingHeight;
                        StartCoroutine(Climb(heightToClimb, 2.33f * Mathf.Abs(transform.localScale.x)));
                    } // Otherwise, move it in the input direction
                    else {
                        Vector3 movementVector = yMove * clingSpeed * Time.fixedDeltaTime * Vector3.up;
                        transform.Translate( movementVector);
                    }
                } else { // If not hit, and not something down, the player is dropped
                    _clinging = false;
                    Drop();
                }
            }
            DampVelocity();
            SetParallaxMovingFactor(-rb.velocity.x);
        }

        private void SetParallaxMovingFactor(float xMove) {
            foreach (ParallaxPlus parallaxGenerator in parallaxGenerators) {
                parallaxGenerator.movingFactor = Math.Sign(xMove);
                parallaxGenerator.currentSpeed = Mathf.Lerp(0, parallaxGenerator.maxSpeed, Mathf.Abs(rb.velocity.x) 
                    / horizontalMaxSpeed);
            }
        }

        public void Jump() {
            jumpBug.ApplyAllModifiers();
            // First, the vertical rigid body movement is stopped
            rb.velocity = new Vector2(rb.velocity.x,0);
            // Then, the force is added
            rb.AddForce(new Vector2(0f, m_JumpForce));
        }

        /// <summary>
        /// Tries to climb on this character with the given height and width
        /// </summary>
        /// <returns></returns>
        public int TryClimb(float height, float width) {
            // Climb collider is used to check if player is able to climb
            List<Collider2D> results = new List<Collider2D>();
            Physics2D.OverlapCollider(climbCollider, climbFilter, results);
            if (results.Count>0) {
                // First, a raycast to up the player is tried
                RaycastHit2D verticalHit = Physics2D.Raycast(transform.position, transform.up, 
                        height,m_WhatIsGround),
                    horizontalHit = Physics2D.Raycast(_climbCheck.position + transform.up*0.8f, directionVector, 
                        width,m_WhatIsGround);
                // If climb hits collided nothing, the player climbs
                if (!verticalHit && !horizontalHit) {
                    StartCoroutine(Climb(height,width));
                }
            }
            return 0;
        }

        public IEnumerator Climb(float height, float width) {
            Freeze();
            Vector3 cornerPosition = transform.position + _climbCheck.up * height, 
                landingPosition = cornerPosition + (Vector3)(directionVector * width);
            yield return new WaitForSeconds(0.3f);
            transform.position = cornerPosition;
            yield return new WaitForSeconds(0.3f);
            transform.position = landingPosition;
            Unfreeze();
        }

        public IEnumerator Dodge(float initialXMove) {
            CapsuleCollider2D characterCollider = GetComponent<CapsuleCollider2D>();
            animator.SetBool("Sliding", true);
            characterCollider.size = new Vector2(characterCollider.size.x, characterCollider.size.y / 2);
            characterCollider.offset += Vector2.down * characterCollider.size.y/2;
            // Current max speed is set to dodge speed, and dampening factor is reduced
            currentHorizontalMaxSpeed = dodgeSpeed;
            dodgeXMove = initialXMove;
            float currentTime = 0;
            canMove = false;
            // xMove can't be 0 while dodging
            while(currentTime<dodgeTime) {
                currentTime += Time.fixedDeltaTime;
                rb.velocity = new Vector2(dodgeXMove * dodgeSpeed,rb.velocity.y);
                yield return new WaitForFixedUpdate();
            }
            animator.SetBool("Sliding", false);
            // Max speed, force apply and horizontal damp are set to normal
            canMove = true;
            currentHorizontalMaxSpeed = horizontalMaxSpeed;
            // Collider is also returned to normal
            characterCollider.offset += Vector2.up * characterCollider.size.y/2;
            characterCollider.size = new Vector2(characterCollider.size.x, characterCollider.size.y * 2);
            canRotate = true;
            StartCoroutine(DodgeCooldown());
        }

        private IEnumerator DodgeCooldown() {
            _canDodge = false;
            yield return new WaitForSeconds(dodgeCooldown);
            _canDodge = true;
        }
        
        /// <summary>
        /// Clings to a wall in the specified position
        /// </summary>
        /// <param name="position"></param>
        /// <param name="newClingType"></param>
        public void Cling(Vector3 position) {
            _clinging = true;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            animator.SetBool("Clinging",true);
            Freeze();
        }

        /// <summary>
        /// Diagonal impulse when jumping off a clinged wall
        /// </summary>
        public void WallBounce() {
            Drop();
            if (facingRight) {
                rb.AddForce((Vector2.up + Vector2.right ) * m_JumpForce);
            } else {
                rb.AddForce((Vector2.up + Vector2.left) * m_JumpForce);
            }
        }
        
        public void Drop() {
            _clinging = false;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            Unfreeze();
            animator.SetBool("Clinging",false);
            animator.SetBool("Ground", false);
            Flip();
            StartCoroutine(LockClingDetection(ROTATION_LOCK_TIME));
        }

        public void OnDeath() {
            Freeze();
        }

        public void SetGravity(float g) {
            rb.gravityScale = g;
        }
        
        public void SetWeight(float m) {
            rb.mass = m;
        }

        public void SetHorizontalMaxSpeed(float newMaxSpeed){
            currentHorizontalMaxSpeed = newMaxSpeed;
            horizontalMaxSpeed = newMaxSpeed;
        }
        
        /// <summary>
        /// Increases the gravity by n
        /// </summary>
        /// <param name="n"></param>
        public void IncreaseGravity(float n) {
            rb.gravityScale += n;
        }

        public override void Freeze() {
            rb.bodyType = RigidbodyType2D.Static;
        }

        public  override void Unfreeze() {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }

        public float GetWeight() {
            return rb.mass;
            return rb.mass;
        }

        /// <summary>
        /// Locks the player rotation untiy the player starts falling
        /// </summary>
        /// <param name="lockingTime"></param>
        /// <returns></returns>
        public IEnumerator LockClingDetection(float lockingTime) {
            canRotate = false;
            yield return new WaitForSeconds(lockingTime);
            canRotate = true;
        }
        
        public bool ObstacleDetected() {
            return Physics2D.Raycast(m_obstacleCheck.position, directionVector, 
                _kObstacleCheckLength, m_WhatIsGround);
        }
    }