using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIImageTextContainer : UIDFadable {

    public override IEnumerator Fade() {
        animator.SetTrigger("Fade");
        yield return null;
    }

    public override IEnumerator Opaque() {
        animator.SetTrigger("Opaque");
        yield return null;
    }
}
