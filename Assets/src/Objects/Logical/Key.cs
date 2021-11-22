﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.TryGetComponent(out AIFixedCommandChar character)) {
            character.interactor.canOpenLockedDoors = true;
            Destroy(gameObject);
        }
    }
}
