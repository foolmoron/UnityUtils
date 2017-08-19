using UnityEngine;
using System.Collections;

public static class AudioClipExtensions {

    public static void Play(this AudioClip audioClip, Vector3? pos = null, float volume = 1.0f, float pitch = 1.0f, float pan = 0.0f) {
        if (!audioClip) return;

        var position = pos ?? Vector3.zero;
        float originalTimeScale = Time.timeScale;
        Time.timeScale = 1.0f;	// ensure that all audio plays

        GameObject go = new GameObject("One-shot audio");
        AudioSource goSource = go.AddComponent<AudioSource>();
        goSource.clip = audioClip;
        go.transform.position = position;
        goSource.volume = volume;
        goSource.pitch = pitch;
        goSource.panStereo = pan;

        goSource.Play();
        Object.Destroy(go, audioClip.length);

        Time.timeScale = originalTimeScale;
    }

    public static void Play(this AudioClip[] clips, Vector3? pos = null, float volume = 1.0f, float pitch = 1.0f, float pan = 0.0f) {
        if (clips == null || clips.Length == 0) return;

        clips.Random().Play(pos, volume, pitch, pan);
    }
}