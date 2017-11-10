using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class LogRangeAttribute : PropertyAttribute {
    public float Min { get; private set; }
    public float Max { get; private set; }

    public LogRangeAttribute(float min, float max) {
        Min = min;
        Max = max;
    }
}