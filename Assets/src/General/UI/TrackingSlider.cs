using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class TrackingSlider : Slider {

    public Transform target;

    void FixedUpdate() {
        transform.position = Camera.main.WorldToScreenPoint (target.position);
    }
}
