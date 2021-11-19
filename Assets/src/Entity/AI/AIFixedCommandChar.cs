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

    public MainCharacterModifiable modifiable;

    /// <summary>
    /// Manager containing all the bugs
    /// </summary>
    public BugManager bugManager;

    public PlatformerCharacter2D charController;

    void Start() {
        base.Start();
        Command [] commandsArray = GetComponentsInChildren<Command>();
        if (commandsArray != null) {
            commands = commandsArray.ToList();
        }
        StartCoroutine(ExecuteAllCommands());
        charController = GetComponent<PlatformerCharacter2D>();
        modifiable = GetComponent<MainCharacterModifiable>();
        // Platformer controller construction
        _platformerController = GetComponent<PlatformerCharacter2D>();
        _platformerController.jumpBug = modifiable.GetJumpBug();
    }
    
    void Update() {
        // Picks any interactable object if it is in range
        if (interactor.lastInteractableObject) {
            interactor.TryInteract();
        }
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
        bugManager.Correct();
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
}
