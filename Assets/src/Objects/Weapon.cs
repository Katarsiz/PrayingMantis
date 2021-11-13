using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Pickable {
    
    public bool activated;

    public float attackRate;

    public bool canAttack;

    public virtual IEnumerator OnWeaponHold() {
        while (holded) {
            if (Input.GetMouseButton(0)) {
                activated = true;                
            } else {
                activated = false;
            }

            if (activated) {
                if (canAttack) {
                    Attack();
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public override void OnPick(Interactor i) {
        base.OnPick(i);
        StartCoroutine(OnWeaponHold());
    }

    public override void OnDrop() {
        base.OnDrop();
        StopAllCoroutines();
    }

    /// <summary>
    /// Enables the attack in the given time
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public IEnumerator EnableAttack(float t) {
        yield return new WaitForSeconds(t);
        canAttack = true;
    }

    public virtual void Attack() {
        StartWeaponCooldown();
    }

    public void StartWeaponCooldown() {
        canAttack = false;
        StartCoroutine(EnableAttack(attackRate));
    }
}
