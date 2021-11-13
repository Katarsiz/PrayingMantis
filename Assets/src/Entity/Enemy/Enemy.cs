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

    protected override void Awake() {
        base.Awake();
        _canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Damageable d = other.gameObject.GetComponent<Damageable>();
            if (_canAttack && d) {
                Attack(d);
            }
        } 
    }
    
    public IEnumerator Charge(int awaitTime, Vector2 chargeDirection, float force){
        yield return new WaitForSeconds(awaitTime);
    }

    public void Attack(Damageable objective) {
        objective.TakeDamage(meleeDamage);
        StartCoroutine(LockMeleeAttack(meleeCooldown));
    }

    /// <summary>
    /// Locks the attacking flag of the entity for the given time
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    public IEnumerator LockMeleeAttack(float seconds) {
        _canAttack = false;
        yield return new WaitForSeconds(seconds);
        _canAttack = true;
    }

    public override void OnHeal() {
        throw new NotImplementedException();
    }
}
