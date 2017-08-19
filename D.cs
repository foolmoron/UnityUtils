using UnityEngine;
using System.Collections;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public static class D {

    [Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition, string message) {
        if (!condition) {
            throw new System.Exception(message);
        }
    }

    [Conditional("UNITY_EDITOR")]
    public static void Warn(bool condition, string message) {
        if (!condition) {
            Debug.LogWarning(message);
        }
    }
}
