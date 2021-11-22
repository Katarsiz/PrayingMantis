using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticRailedObject : RailedObject, Magnetic {

    public void OnAttractionDetected(float magneticForce) {
        speed = magneticForce;
        StartCoroutine(GoToTarget());
    }
}
