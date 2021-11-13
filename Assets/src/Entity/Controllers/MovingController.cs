using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingController : MonoBehaviour {
    
    /// <summary>
    /// Force this entity will apply when trying to move horizontally
    /// </summary>
    [SerializeField] protected float horizontalForce = 10f;
    
    protected float currentHorizontalForce = 10f;
    
    /// <summary>
    /// Force applied to the rigidbody in the opposite direction when velocity exceeds max velocity
    /// </summary>
    [SerializeField] protected float horizontalDamp = 10f;
    
    [SerializeField] protected float horizontalMaxSpeed = 10f;

    protected float currentHorizontalMaxSpeed;
    
    /// <summary>
    ///  Direction which this character is looking at
    /// </summary>
    protected Vector2 directionVector;
    
    /// <summary>
    /// Script that contains the logic of the character
    /// </summary>
    protected Movable character;
    
    protected Animator animator;

    /// <summary>
    /// Rigidbody that controls this character's movement
    /// </summary>
    protected Rigidbody2D rb;

    protected bool facingRight = true;  // For determining which way the player is currently facing.

    protected bool canMove = true; // Entities can move by default

    public virtual void Initialize() {
        rb = GetComponent<Rigidbody2D>();
        currentHorizontalMaxSpeed = horizontalMaxSpeed;
        currentHorizontalForce = horizontalForce;
        directionVector = Vector2.right;
    }

    public void SetAnimator(Animator animator) {
        this.animator = animator;
    }

    /// <summary>
    /// Stops this character movement
    /// </summary>
    public virtual void Freeze() {
        rb.velocity = Vector2.zero;
        canMove = false;
    }

    /// <summary>
    /// Resumes this character movement
    /// </summary>
    public virtual void Unfreeze() {
        canMove = true;
    }
    
    public void Flip() {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        directionVector *= -1;

        Vector3 newWScale = transform.localScale;
        newWScale.x *= -1;
        transform.localScale = newWScale;
    }
    
    public void MoveHorizontally(float xMove) {
        // Dampeing factor multiplies the applied force so that when close to max speed, 
        //float dampeningFactor = horizontalMaxSpeed - Mathf.Abs(rb.velocity.x);
        // Velocity damping
        rb.AddForce(currentHorizontalForce * xMove * Vector2.right);
        // If current velocity exceeds the maximum speed, a force in the opposite direction is added
        if (rb.velocity.x > currentHorizontalMaxSpeed) {
            rb.AddForce(currentHorizontalForce * Vector2.left);
        } else if (rb.velocity.x < -currentHorizontalMaxSpeed) {
            rb.AddForce(currentHorizontalForce * Vector2.right);
        }
    }

    /// <summary>
    /// Slows down the entity by the given factor, reducing both the force that it applies when it moves and its acceleration
    /// </summary>
    /// <param name="reductionFactor">Percentage of horizontal speed reduction</param>
    /// <returns></returns>
    public void Slowdown(float reductionFactor) {
        currentHorizontalMaxSpeed = Mathf.Lerp(horizontalMaxSpeed, 0f, reductionFactor/100f);
        currentHorizontalForce = Mathf.Lerp(horizontalForce, 0f, reductionFactor/100f);
    }

    /// <summary>
    /// Returns the controller max current speeds to their default value
    /// </summary>
    public void ResetToFullSpeed() {
        currentHorizontalMaxSpeed = horizontalMaxSpeed;
        currentHorizontalForce = horizontalForce;
    }

    /// <summary>
    /// Applies forces opposite to max velocity if this entity exceeds them
    /// </summary>
    public void DampVelocity() {
        float excessVelocity = currentHorizontalMaxSpeed - Mathf.Abs(rb.velocity.x);
        // If current velocity exceeds the maximum speed, a force in the opposite direction is added
        if (rb.velocity.x > currentHorizontalMaxSpeed) {
            // Damping is applied
            rb.AddForce(horizontalDamp * excessVelocity * Vector2.right);
        } else if (rb.velocity.x < -currentHorizontalMaxSpeed) {
            // Damping is applied
            rb.AddForce(horizontalDamp * excessVelocity * Vector2.left);
        }
    }
    
    /// <summary>
    /// Avoids the AI controlled movement for s seconds
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns> 
    public IEnumerator LockMovement(float s) {
        canMove = false;
        yield return new WaitForSeconds(s);
        canMove = true;
    }
    
    /// <summary>
    /// Applies a linear descending impulse on the movable's rigidbody. 
    /// </summary>
    /// <param name="impulse"></param>
    /// <param name="duration"> How much he force is reduced per second</param>
    /// <returns></returns>
    public IEnumerator ApplyImpulse(Vector2 impulse, float duration, float reductionFactor) {
        // How much the impulse is reduced each second
        float currentTime = 0;
        reductionFactor = impulse.magnitude / duration;
        Vector2 currentForce = impulse, forceDirection = Vector3.Normalize(impulse);
        while (currentTime < duration) {
            rb.AddForce(currentForce);
            currentForce -= reductionFactor * Time.fixedDeltaTime * forceDirection;
            currentTime += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
    
    public void SetGrounded(bool grounded) {
        animator.SetBool("Ground", grounded);
    }
    
    public void SetAnimatorXSpeed(float newVelocity) {
        animator.SetFloat("Speed", newVelocity);
    }

    public void SetAnimatorYSpeed(float newVelocity) {
        animator.SetFloat("vSpeed", newVelocity);
    }
    
    public void SetSliding(bool sliding) {
        animator.SetBool("Sliding", sliding);
    }
    
    public void SetClinging(bool clinging) {
        animator.SetBool("Clinging",clinging);
    }
}