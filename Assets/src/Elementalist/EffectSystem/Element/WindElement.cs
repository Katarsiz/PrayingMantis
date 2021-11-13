using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindElement : Element {
    
    public WindElement(Effect[] entityEffects, Effect[] environmentalEffects) : base(entityEffects, environmentalEffects) {
        type = WIND;
    }
}
