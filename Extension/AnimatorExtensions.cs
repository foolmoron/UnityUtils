using System;

using UnityEngine;
using System.Collections;

public static class AnimatorExtensions {

    public static void PlayFromBeginning(this Animator animator, string state, int layer = 0) {
        animator.Play(state, layer, 0);
        animator.Update(0);
    }

    public static void PlayAtEnd(this Animator animator, string state, int layer = 0) {
        animator.Play(state, layer, 1);
        animator.Update(0);
    }

    public static void setNormalizedTime(this Animator animator, float normalizedTime, int layer = 0) {
        if (animator.isActiveAndEnabled) {
            animator.Play(animator.GetCurrentAnimatorStateInfo(0).fullPathHash, layer, normalizedTime);
        }
    }
}
