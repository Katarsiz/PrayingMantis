using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class Damageable : MonoBehaviour {

    public int health;

    public int maxHealth;

    public bool canTakeDamage;

    public void TakeDamage(int damage) {
        if (canTakeDamage) {
            health -= damage;
            OnDamageTaken();
            if (health <= 0) {
                health = 0;
                OnDeath();
            }
        }
    }

    public virtual void OnDamageTaken() {
    }
    
    public virtual void OnDeath() {
        Destroy(gameObject);
    }

    public void Heal(int healAmmount) {
        OnHeal();
        health += healAmmount;
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    /// <summary>
    /// Applies invulnerability for the specified amount of time
    /// </summary>
    /// <param name="time"></param>
    public void ApplyInvulnerability(float time) {
        canTakeDamage = false;
        if (time >= 0) {
            StartCoroutine(RemoveInvulnerabilityAfter(time));
        }
    }

    public IEnumerator RemoveInvulnerabilityAfter(float time) {
        yield return new WaitForSeconds(time);
        canTakeDamage = true;
    }

    public void SetInvulnerability(bool value) {
        canTakeDamage = value;
    }
    public abstract void OnHeal();
}
