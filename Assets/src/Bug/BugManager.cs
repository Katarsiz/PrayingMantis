using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BugManager{

    public List<Bug> bugs;

    public bool correctionActivated;
    
    public void Correct() {
        if (correctionActivated) {
            for (int i = 0; i < bugs.Count; i++) {
                if (bugs[i].Correct()) {
                    break;
                }
            }
        }
    }
}
