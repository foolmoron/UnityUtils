using UnityEngine;
using System.Collections;

public class MatchTransform : MonoBehaviour {
    
    public Transform Target;

    public Vector3 LocalPosition;
    public Vector3 LocalRotation;
    public Vector3 LocalScale = Vector3.one;

    void LateUpdate() {
        transform.position = Target.position + transform.TransformVector(LocalPosition);
        transform.rotation = Target.rotation * Quaternion.Euler(LocalRotation);
        transform.localScale = Vector3.Scale(Target.lossyScale, LocalScale);
    }
}
