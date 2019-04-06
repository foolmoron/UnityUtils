using System;
using UnityEngine;
using System.Collections;

public class MatchTransform : MonoBehaviour {

    [Flags]
    public enum Options {
        All = 7,
        NoPosition = 3,
        NoRotation = 5,
        NoScale = 6,
        OnlyPosition = 1 << 0,
        OnlyRotation = 1 << 1,
        OnlyScale = 1 << 2,
    }
    
    public Transform Target;
    public Options MatchOptions = Options.All;

    public Vector3 LocalPosition;
    public Vector3 LocalRotation;
    public Vector3 LocalScale = Vector3.one;


    void LateUpdate() {
        if ((MatchOptions & Options.OnlyPosition) > 0) {
            transform.position = Target.position + transform.TransformVector(LocalPosition);
        }
        if ((MatchOptions & Options.OnlyRotation) > 0) {
            transform.rotation = Target.rotation * Quaternion.Euler(LocalRotation);
        }
        if ((MatchOptions & Options.OnlyScale) > 0) {
            transform.localScale = Vector3.Scale(Target.lossyScale, LocalScale);
        }
    }
}
