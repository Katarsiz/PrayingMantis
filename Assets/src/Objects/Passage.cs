using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour {

    public GameObject passageTilemap;

    private void OnTriggerEnter2D(Collider2D other) {
        passageTilemap.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other) {
        passageTilemap.SetActive(true);
    }
}
