using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeySentientUIFadable : UIDFadable {

    public KeyCode activationKey;

    protected abstract void OnActivationKeyPressed();

    protected void Update() {
        if (Input.GetKeyDown(activationKey)) {
            OnActivationKeyPressed();
        }
    }
}
