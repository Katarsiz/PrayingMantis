using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockElement : Element {

    public RockElement(Effect[] entityEffects, Effect[] environmentalEffects) : base(entityEffects, environmentalEffects) {
        type = ROCK;
    }
}
