using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugableDoor : Door {
    
    /// <summary>
    /// Bug containing the functions called when 
    /// </summary>
    public Bug openDoorBug;
    
    public override void Open() {
        openDoorBug.ApplyAllModifiers();
        base.Open();
    }
}
