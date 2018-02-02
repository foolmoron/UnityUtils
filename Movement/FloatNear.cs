using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatNear : MonoBehaviour {

    public Vector3 BaseTarget;
    public Vector2 CurrentOffset;
    public Vector2 OffsetRange;
    [Range(0, 3f)]
    public float OffsetInterval;
    float offsetTime;
    [Range(0, 0.3f)]
    public float Speed = 0.1f;

    void Awake() {
        BaseTarget = transform.localPosition;
    }

    void FixedUpdate() {
        // timing
        if (offsetTime > 0) {
            offsetTime -= Time.deltaTime;
        } else {
            CurrentOffset = OffsetRange.scaledWith(new Vector2(Random.value - 0.5f, Random.value - 0.5f));
            offsetTime = OffsetInterval;
        }
        // move
        var target = BaseTarget + CurrentOffset.to3();
        transform.localPosition = Vector3.Lerp(transform.localPosition, target, Speed);
    }
}