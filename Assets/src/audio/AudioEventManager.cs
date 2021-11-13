using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioEventManager {

    public static AudioManager audioManager;
    
    public static MusicManager musicManager;

    public static void PlayOneShotAudioClip(AudioClip a) {
        audioManager.PlayOneShot(a);
    }
    
    public static void ChangeMusicTrack(int newTrackIndex) {
        musicManager.PlayClipAt(newTrackIndex);
    }

    public static void StopMusic() {
        musicManager.Stop();
    }
    
    public static void ResumeMusic() {
        musicManager.Resume();
    }
}
