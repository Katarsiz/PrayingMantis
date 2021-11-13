using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Command : MonoBehaviour {

    /// <summary>
    /// Time it takes to execute this command once it has been called
    /// </summary>
    public float timeToExecute;
    
    /// <summary>
    /// Event called when the command starts
    /// </summary>
    public UnityEvent startEvent;
    
    /// <summary>
    /// Event called when the command finishes
    /// </summary>
    public UnityEvent endEvent;

    protected IEnumerator commandCoroutine;

    public abstract IEnumerator Execute();

    public abstract void OnStart();
    
    public abstract void OnEnd();
}
