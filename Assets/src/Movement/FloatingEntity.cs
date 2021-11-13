using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEntity : MonoBehaviour {

    public float height;

    public float speed;
    
    private float currentTime;

    private Vector3 initialPosition;

    private void Start() {
        initialPosition = transform.position;
    }

    private void Update() {
        if (currentTime < 180) {
            currentTime += Time.deltaTime * speed;
        } else {
            currentTime -= Time.deltaTime * speed;
        }
    }

    private void FixedUpdate() {
        transform.position = initialPosition + (height*Mathf.Sin(currentTime)*Vector3.up);
    }
}
