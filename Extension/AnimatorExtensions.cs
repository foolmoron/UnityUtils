using System;

using UnityEngine;
using System.Collections;

public static class AnimatorExtensions {

    public static void PlayFromBeginning(this Animator animator, string state) {
        animator.Play(state, 0, 0);
        animator.Update(0);
    }

    public static void PlayAtEnd(this Animator animator, string state) {
        animator.Play(state, 0, 1);
        animator.Update(0);
    }

    public static void setNormalizedTime(this Animator animator, float normalizedTime) {
        if (animator.isActiveAndEnabled) {
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, 0, normalizedTime);
        }
    }
}
