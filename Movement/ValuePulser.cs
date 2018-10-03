using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class ValuePulser : MonoBehaviour {

    [Range(0, 10f)]
    public float PulseTime = 0.5f;
    [Range(0, 2f)]
    public float PulseOffset = 0f;
    [Range(0, 1)]
    public float MinValue = 0.4f;
    [Range(0, 1)]
    public float MaxValue = 1f;

    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        var offsetPulseTime = (Time.time + PulseOffset) % PulseTime;
        var interp = (offsetPulseTime / PulseTime) * 2 - 1;
        spriteRenderer.color = spriteRenderer.color.withValue(Mathf.Lerp(MinValue, MaxValue, Mathf.Abs(interp)));
    }
}
