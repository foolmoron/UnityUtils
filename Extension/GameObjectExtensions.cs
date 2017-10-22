using System;

using UnityEngine;

public static class GameObjectExtensions {

    public static T GetComponentInSelfOrChildren<T>(this GameObject gameObject) where T : Component {
        var t = gameObject.GetComponent<T>();
        if (!t) {
            t = gameObject.GetComponentInChildren<T>();
        }
        return t;
    }
}
