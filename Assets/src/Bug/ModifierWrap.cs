using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ModifierWrap : Image{

    /// <summary>
    /// Modifier applied by the bug
    /// </summary>
    public Modifier applyModifier;
    
    /// <summary>
    /// ;Modifier applied when the bug is corrected
    /// </summary>
    public Modifier resetModifier;

    /// <summary>
    /// The bug this modifier has been associated to
    /// </summary>
    private Bug bug;

    public ScrollManager originalContainer;

    public void SetBug(Bug b) {
        bug = b;
    }

    /// <summary>
    /// Determines if this modifier wrap has been associated with a bug
    /// </summary>
    /// <returns></returns>
    public Bug GetBug() {
        return bug;
    }
}