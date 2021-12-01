using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : Affectable {

    public int meleeDamage;

    /// <summary>
    /// Distance at which an enemy decides to attack its current target
    /// </summary>
    public float meleeRange;

    /// <summary>
    /// Time it takes for an enemy to attack again
    /// </summary>
    public float meleeCooldown;

    /// <summary>
    /// Flag that lets an enemy attack or not
    /// </summary>
    protected bool _canAttack;

    /// <summary>
    /// Current target an enemy has
    /// </summary>
    protected bool target;

    public float horizontalImpulse;
    
    public float verticalImpulse;
    
    public float impulseDuration;

    /// <summary>
    /// Intensity of the slowdown applied when an entity is pushed
    /// </summary>
    public float slowdownIntensity;
    
    /// <summary>
    /// Duration of the slowdown
    /// </summary>
    public float slowdownDuration;

    private SlowdownEffect _slowdownEffect;
    
    public Collider2D hoverCollider;

    protected override void Awake() {
        base.Awake();
        _slowdownEffect = new SlowdownEffect(slowdownIntensity,slowdownDuration);
        _canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Affectable a = other.gameObject.GetComponent<Affectable>();
            if (_canAttack && a) {
                Attack(a);
            }
        } 
    }

    public void ApplyImpulse(Affectable a) {
        _slowdownEffect.Apply(a);
        // The force of the flurry is multiplied if the affected entity has a negative crowd control resistance
        float resistance = a.GetResistanceTo(Resistance.CROWD_CONTROL);
        float forceMultiplier = (resistance<0) ? Mathf.Lerp(1f, 2f,-resistance/100f) : 1f;
        a.StartCoroutine(
            a.controller.ApplyImpulse(Vector3.Scale(controller.directionVector , 
                    new Vector2(forceMultiplier * horizontalImpulse, forceMultiplier * verticalImpulse)), 
                impulseDuration, 0f));
    }
    
    public IEnumerator Charge(int awaitTime, Vector2 chargeDirection, float force){
        yield return new WaitForSeconds(awaitTime);
    }

    public void Attack(Affectable objective) {
        StartCoroutine(AttackCoroutine(objective));
        StartCoroutine(AttackCooldown(meleeCooldown));
    }

    public IEnumerator AttackCoroutine(Affectable target) {
        animator.SetBool("Attack",true);
        target.TakeDamage(meleeDamage);
        ApplyImpulse(target);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("Attack",false);
    }

    /// <summary>
    /// Locks the attacking flag of the entity for the given time
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public IEnumerator AttackCooldown(float seconds) {
        _canAttack = false;
        yield return new WaitForSeconds(seconds);
        _canAttack = true;
    }

    public override void OnDeath() {
        PlayDeathAudio();
        controller.Freeze();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        animator.SetTrigger("Die");
    }

    public void SetMaxHealth(int newHealth) {
        // If enemy is yet to be damaged, health is updated
        if (health == maxHealth) {
            health = newHealth;
            maxHealth = newHealth;
        }
    }
    
    public void OnSimulationStart() {
        hoverCollider.enabled = false;
        UnFreezeMovement();
    }
    
    public void OnSimulationEnd() {
        hoverCollider.enabled = true;
        FreezeMovement();
    }

    public override void OnHeal() {
        throw new NotImplementedException();
    }
}
