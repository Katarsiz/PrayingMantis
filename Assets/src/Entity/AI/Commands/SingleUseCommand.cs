using System.Collections;

/// <summary>
/// Single use commands last for one frame
/// </summary>
public class SingleUseCommand : Command {
    
    public override IEnumerator Execute() {
        startEvent?.Invoke();
        yield return null;
        endEvent?.Invoke();
    }

    public override void OnStart() {
        throw new System.NotImplementedException();
    }

    public override void OnEnd() {
        throw new System.NotImplementedException();
    }
}
