using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {

    public AudioClip openAudio;

    public virtual void Open() {
        AudioEventManager.PlayOneShotAudioClip(openAudio);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Animator>().SetTrigger("Open");
    }
    public override void OnInteractionRange() {
    }

    public override void OnOutOfInteractionRange() {
    }

    public override IEnumerator OnInteraction(Interactor i) {
        Open();
        yield return null;
    }

    public override void OnInteractionStop(Interactor i) {
        throw new System.NotImplementedException();
    }
}
