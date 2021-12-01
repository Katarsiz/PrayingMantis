using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

    public AudioClip[] availableTracks;
    
    private AudioSource audioPlayer;

    public const int INITIAL_TRACK = 0, CHASE_TRACK = 1;

    private void Start() {
        AudioEventManager.musicManager = this;
        audioPlayer = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays the clip at the selected index
    /// </summary>
    /// <param name="index"></param>
    public void PlayClipAt(int index) {
        if (availableTracks.Length > index) {
            audioPlayer.clip = availableTracks[index];
            PlayCurrentClip();
        }
    }

    public void PlayCurrentClip() {
        audioPlayer.Play();
    }

    public void Stop() {
        audioPlayer.Stop();
    }

    public void Resume() {
        audioPlayer.Play();
    }
}
