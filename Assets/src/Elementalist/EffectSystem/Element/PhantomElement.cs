using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomElement : Element {
    public PhantomElement(Effect[] entityEffects, Effect[] environmentalEffects) : base(entityEffects, environmentalEffects) {
        type = PHANTOM;
    }
}
