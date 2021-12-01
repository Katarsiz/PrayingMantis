using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Modifiers encapsulate an action that is called after some time has passed
/// </summary>
[System.Serializable]
public class Modifier {

    /// <summary>
    /// Time it takes for this action to take place once it is requested to be applied
    /// </summary>
    public float applyTime;

    public UnityEvent action;

    public IEnumerator Apply() {
        yield return new WaitForSeconds(applyTime);
        action.Invoke();
    }
}
