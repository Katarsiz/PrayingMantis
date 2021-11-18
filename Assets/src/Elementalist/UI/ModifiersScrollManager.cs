using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifiersScrollManager : ScrollManager
{
    protected override void Start() {
        base.Start();
        // Sets the original scroll manager variable of modifier wraps in content panel
        for (int i = 0; i < container.childCount; i++) {
            if (container.GetChild(i).TryGetComponent(out ModifierWrap modifierWrap)) {
                modifierWrap.originalContainer = this;
            }
        }
    }
}
