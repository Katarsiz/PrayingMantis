using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterModifiable : Modifiable {

    public const int JUMP_BUG_INDEX = 0;

    public Bug GetJumpBug() {
        return bugs[JUMP_BUG_INDEX];
    }
}
