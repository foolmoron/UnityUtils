using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class AlphaPulser : MonoBehaviour {

    [Range(0, 10f)]
    public float PulseTime = 0.5f;
    [Range(0, 2f)]
    public float PulseOffset = 0f;
    [Range(0, 1)]
    public float MinAlpha = 0.4f;
    [Range(0, 1)]
    public float MaxAlpha = 1f;

    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        var offsetPulseTime = (Time.time + PulseOffset) % PulseTime;
        var interp = (offsetPulseTime / PulseTime) * 2 - 1;
        spriteRenderer.color = spriteRenderer.color.withAlpha(Mathf.Lerp(MinAlpha, MaxAlpha, Mathf.Abs(interp)));
    }
}
