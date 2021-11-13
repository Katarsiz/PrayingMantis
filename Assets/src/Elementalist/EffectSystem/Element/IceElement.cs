using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceElement : Element {

    public IceElement(Effect[] entityEffects, Effect[] environmentalEffects) : base(entityEffects, environmentalEffects) {
        type = ICE;
    }
}
