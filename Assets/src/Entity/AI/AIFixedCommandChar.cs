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

    void Start() {
        base.Start();
        Command [] commandsArray = GetComponentsInChildren<Command>();
        if (commandsArray != null) {
            commands = commandsArray.ToList();
        }
        _platformerController = GetComponent<PlatformerCharacter2D>();
        StartCoroutine(ExecuteAllCommands());
    }
    
    void Update() {
        if (ObstacleDetected()) {
            xMove *= -1;
            controller.Flip();
        } else if (FallDetected()) {
            StartCoroutine(OneShotJump());
        }
        // Picks any interactable object if it is in range
        if (interactor.lastInteractableObject) {
            interactor.TryInteract();
        }
    }
    
    /// <summary>
    /// Functions called when the mouse is hovered over the entity
    /// </summary>
    public override void OnMouseHovered() {
        base.OnMouseHovered();
        // Deactivates the interactor collider, so it doesn't hinder the main colliders
        interactor.GetComponent<Collider2D>().enabled = false;
    }
    
    /// <summary>
    /// Functions called when the mouse has stopped being hovered over the entity
    /// </summary>
    public override void OnMouseExited() {
        base.OnMouseExited();
        // Activates the interactor collider
        interactor.GetComponent<Collider2D>().enabled = true;
    }

    private void FixedUpdate() {
        _platformerController.Move(xMove,yMove,crouching,jumping);
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
