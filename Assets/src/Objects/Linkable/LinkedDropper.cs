using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedDropper : Linkable {

    public GameObject droppedObjectPrefab;

    public List<GameObject> instantiatedDroppedObjects;

    public int maximumDroppedObjects = 3;

    private void Start() {
        instantiatedDroppedObjects = new List<GameObject>();
    }

    public override void Activate() {
        instantiatedDroppedObjects.RemoveAll((o => o == null));
        if (instantiatedDroppedObjects.Count < maximumDroppedObjects) {
            GameObject newDroppedObject = Instantiate(droppedObjectPrefab,transform.position,new Quaternion());
            instantiatedDroppedObjects.Add(newDroppedObject);
        }
    }

    public override void Deactivate() {
    }
}
