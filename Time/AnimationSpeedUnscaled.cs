using System;

using UnityEngine;
using System.Collections;

public class AnimationSpeedUnscaled : MonoBehaviour {

    public float ActualSpeed = 1;
    Animator animator;
    
    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        animator.speed =  Time.timeScale.inverse() * ActualSpeed;
    }
}