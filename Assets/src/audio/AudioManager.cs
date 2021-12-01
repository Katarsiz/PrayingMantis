using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start() {
        AudioEventManager.audioManager = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot(AudioClip newClip) {
        audioSource.clip = newClip;
        audioSource.Play();
    }
}
