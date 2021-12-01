using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon {

    public Transform muzzle;

    public float projectileSpeed;

    public Bullet projectilePrefab;

    private List<Bullet> shotBullets;

    public Coroutine movingCoroutine;

    public override IEnumerator OnWeaponHold() {
        while (holded) {
            RotateWithMouse();
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

    public void RotateWithMouse() {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 lookDirection = mousePosition - transform.position;
        lookDirection.z = 0;
        transform.right = lookDirection;
    }

    private void Start() {
        shotBullets = new List<Bullet>();
    }

    public override void Attack() {
        Shoot();
        base.Attack();
    }

    public void Shoot() {
        Bullet newBullet = Instantiate(projectilePrefab, muzzle.position, new Quaternion());
        newBullet.transform.right = muzzle.transform.right;
        newBullet.Initialize();
        shotBullets.Add(newBullet);
        if (movingCoroutine == null) {
            movingCoroutine = StartCoroutine(MoveProjectiles());
        }
    }

    /// <summary>
    /// The gun is in charge of moving each projectile
    /// </summary>
    public IEnumerator MoveProjectiles() {
        while (shotBullets.Count != 0) {
            for (int i = 0; i < shotBullets.Count; i++) {
                Bullet currentBullet = shotBullets[i];
                if (currentBullet.mustBeDestroyed) {
                    shotBullets.Remove(currentBullet);
                    Destroy(currentBullet.gameObject);
                } else {
                    currentBullet.transform.position+=(currentBullet.transform.right * Time.fixedDeltaTime * projectileSpeed);
                }
            }
            yield return new WaitForFixedUpdate();
        }

        movingCoroutine = null;
    }
}