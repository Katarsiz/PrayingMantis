using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class MainCharacter : Affectable {
    
    // Object the player uses to interact with environment objects
    public Interactor interactor;

    protected LevelManager levelManager;

    private Animator _mAnim;

    protected void Start() {
        _mAnim = GetComponentInChildren<Animator>();
    }

    public void Initialize(LevelManager newLevelManager) {
        levelManager = newLevelManager;
    }

    /// <summary>
    /// Method to call when picking up an object
    /// </summary>
    public void OnHoldObject() {
        _mAnim.SetBool("Holding", true);
    }
    
    /// <summary>
    /// Method to call when dropping an equiped object
    /// </summary>
    public void OnDropObject() {
        _mAnim.SetBool("Holding", false);
    }

    /// <summary>
    /// Freezes the player and plays the death screens
    /// </summary>
    public override void OnDeath() {
        levelManager.OnLevelCompleted();
    }

    public override void OnHeal() {
        throw new NotImplementedException();
    }
}