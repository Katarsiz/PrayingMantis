using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIDFadable : MonoBehaviour {

    protected Animator animator;

    protected LevelManager levelManager;

    public void Initialize(LevelManager newLevelManager) {
        levelManager = newLevelManager;
    }

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public abstract IEnumerator Fade();
    
    public abstract IEnumerator Opaque();
}
