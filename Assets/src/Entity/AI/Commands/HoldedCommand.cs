using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoldedCommand : Command {
    
    /// <summary>
    /// Time this command will last
    /// </summary>
    public float duration;
    
    /// <summary>
    /// Event called when the command starts
    /// </summary>
    public UnityEvent holdedEvent;
    
    public override IEnumerator Execute() {
        yield return new WaitForSeconds(duration);
        float currentTime = 0;
        startEvent?.Invoke();
        while (currentTime<duration) {
            // Holded commands execute every frame
            holdedEvent?.Invoke();
            currentTime += Time.deltaTime;
            yield return null;
        }
        endEvent?.Invoke();
    }

    public override void OnStart() {
        throw new System.NotImplementedException();
    }

    public override void OnEnd() {
        throw new System.NotImplementedException();
    }
}
