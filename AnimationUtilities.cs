using UnityEngine;
using System.Collections;

public class AnimationUtilities : MonoBehaviour {

    public AudioClip[] Sounds;

    public float[] Delays;
    float delayRemaining;
    float preDelaySpeed;

    public float[] Speeds;

    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        // handle delays
        {
            if (delayRemaining > 0) {
                animator.speed = 0;
                delayRemaining -= Time.deltaTime;
                if (delayRemaining < 0) {
                    delayRemaining = 0;
                    animator.speed = preDelaySpeed;
                }
            }
        }
    }

    public void DelayByIndexOncePerPlay(int delayIndex) {
        if (delayIndex >= 0 && delayIndex < Delays.Length) {
            DelayByValueOncePerPlay(Delays[delayIndex]);
        } else {
            Debug.LogError("No delay with index " + delayIndex + " found on this AnimationUtilities component!");
        }
    }

    public void DelayByIndexEveryLoop(int delayIndex) {
        if (delayIndex >= 0 && delayIndex < Delays.Length) {
            DelayByValueEveryLoop(Delays[delayIndex]);
        } else {
            Debug.LogError("No delay with index " + delayIndex + " found on this AnimationUtilities component!");
        }
    }

    public void DelayByValueOncePerPlay(float delay) {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) // this means it's after the first loop
            return;
        DelayByValueEveryLoop(delay);
    }

    public void DelayByValueEveryLoop(float delay) {
        if (animator.speed != 0) {
            preDelaySpeed = animator.speed;
        }
        delayRemaining = delay;
    }

    public void SetSpeedByIndex(int speedIndex) {
        if (speedIndex >= 0 && speedIndex < Speeds.Length) {
            SetSpeedByValue(Speeds[speedIndex]);
        } else {
            Debug.LogError("No speed with index " + speedIndex + " found on this AnimationUtilities component!");
        }
    }

    public void SetSpeedByValue(float speed) {
        animator.speed = speed;
    }

    public void PlaySoundByIndex(int soundIndex) {
        if (soundIndex >= 0 && soundIndex < Sounds.Length) {
            Sounds[soundIndex].Play();
        } else {
            Debug.LogError("No sound with index " + soundIndex + " found on this AnimationUtilities component!");
        }
    }

    public void PlaySoundByName(string soundName) {
        for (int i = 0; i < Sounds.Length; i++) {
            if (Sounds[i].name == soundName) {
                PlaySoundByIndex(i);
            }
        }
    }

    public void SpawnPrefab(GameObject prefab) {
        Instantiate(prefab);
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }

    public void DestroyParent() {
        if (gameObject.transform.parent != null)
            Destroy(gameObject.transform.parent.gameObject);
    }

    public void ReleaseSelf() {
        gameObject.Release();
    }

    public void ReleaseParent() {
        if (gameObject.transform.parent != null)
            gameObject.transform.parent.gameObject.Release();
    }

    public void PlayAnimationState(string state) {
        animator.Play(state);
    }
}
