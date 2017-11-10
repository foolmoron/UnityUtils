using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class InspectorButtonAttribute : PropertyAttribute {
    public readonly string MethodName;

    public InspectorButtonAttribute(string methodName) {
        MethodName = methodName;
    }
}