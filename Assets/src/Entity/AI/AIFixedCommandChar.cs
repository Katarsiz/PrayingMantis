using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Class to control a character through a list of commands
/// </summary>
public class AIFixedCommandChar : MainCharacter {

    public List<Command> commands;

    private PlatformerCharacter2D _platformerController;

    public float xMove;
    
    public float yMove;

    public bool crouching;
    
    public bool jumping;

    public PlatformerCharacter2D charController;

    /// <summary>
    /// Collider used when to detect if the player is hovering its mouse over the entity
    /// </summary>
    public Collider2D hoverCollider;

    /// <summary>
    /// Collider used to detect enemies
    /// </summary>
    public Collider2D enemyDetectionCollider;

    /// <summary>
    /// Bug executed when attacking
    /// </summary>
    public Bug attackBug;

    /// <summary>
    /// Damage this entity deals
    /// </summary>
    public int damage;

    /// <summary>
    /// Time ita takes for this entity to attack again
    /// </summary>
    public float attackCooldown;

    /// <summary>
    /// Flag that allows the entity to attack (or not)
    /// </summary>
    private bool _canAttack;

    void Start() {
        base.Start();
        _canAttack = true;
        Command [] commandsArray = GetComponentsInChildren<Command>();
        if (commandsArray != null) {
            commands = commandsArray.ToList();
        }
        StartCoroutine(ExecuteAllCommands());
        charController = GetComponent<PlatformerCharacter2D>();
        // Platformer controller construction
        _platformerController = GetComponent<PlatformerCharacter2D>();
    }
    
    void Update() {
        // Picks any interactable object if it is in range
        if (interactor.lastInteractableObject) {
            interactor.TryInteract();
        }
        TryAttack();
    }

    public void TryAttack() {
        // Deal damage to the first detected enemy
        List<Collider2D> collidersInRange = new List<Collider2D>();
        for (int i = 0; i < enemyDetectionCollider.GetContacts(collidersInRange); i++) {
            if (_canAttack && collidersInRange[i].TryGetComponent(out Enemy enemy)) {
                Attack(enemy);
                break;
            }
        }
    }

    public void Attack(Affectable a) {
        StartCoroutine(AttackCoroutine(a));
        StartCoroutine(AttackCooldown());
    }
    
    public IEnumerator AttackCoroutine(Affectable a) {
        animator.SetBool("Attacking",true);
        attackBug.ApplyAllModifiers();
        a.TakeDamage(damage);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("Attacking",false);
    }

    public IEnumerator AttackCooldown() {
        _canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        _canAttack = true;
    }

    private void FixedUpdate() {
        if (ObstacleDetected() && charController.GetGrounded()) {
            xMove *= -1;
            controller.Flip();
        } else if (FallDetected()) {
            StartCoroutine(OneShotJump());
        }
        _platformerController.Move(xMove,yMove,crouching,jumping);
    }
    
    public override void OnDamageTaken() {
        levelManager.OnHealthChange();
    }

    public bool FallDetected() {
        return _platformerController.FallDetected();
    }
    
    public bool ObstacleDetected() {
        return _platformerController.ObstacleDetected();
    }

    public void JumpOnce() {
        StartCoroutine(OneShotJump());
    }

    public IEnumerator OneShotJump() {
        jumping = true;
        yield return new WaitForFixedUpdate();
        jumping = false;
    }

    public IEnumerator ExecuteAllCommands() {
        Command currentCommand;
        while (commands.Count>0) {
            currentCommand = commands[0];
            commands.RemoveAt(0);
            // Then,  we delete the command from the stack and wait for it to finish
            yield return ExecuteCommand(currentCommand);
        }
    }

    public void SetXMove(float x) {
        xMove = x;
    }
    
    public void SetYMove(float y) {
        yMove = y;
    }
    
    public void SetCrouch(bool crouch) {
        crouching = crouch;
    }
    
    public void SetJump(bool jump) {
        jumping = jump;
    }

    public IEnumerator ExecuteCommand(Command c) {
        yield return c.Execute();
    }

    public void OnSimulationStart() {
        hoverCollider.enabled = false;
        UnFreezeMovement();
    }
    
    public void OnSimulationEnd() {
        hoverCollider.enabled = true;
        FreezeMovement();
    }
}
