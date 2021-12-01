using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOSPlayer : MonoBehaviour {

    public AudioClip[] clips;

    public void PlayOneShotAC(int index) {
        AudioEventManager.PlayOneShotAudioClip(clips[index ]);
    }
}
