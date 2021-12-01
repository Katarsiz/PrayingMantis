using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : Element {
    public FireElement(Effect[] entityEffects, Effect[] environmentalEffects) : base(entityEffects, environmentalEffects) {
        type = FIRE;
    }
}
