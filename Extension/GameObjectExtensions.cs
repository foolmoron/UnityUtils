using System;

using UnityEngine;
using UnityEngine.Networking;

public static class GameObjectExtensions {

    public static T GetComponentInSelfOrChildren<T>(this GameObject gameObject) where T : Component {
        var t = gameObject.GetComponent<T>();
        if (!t) {
            t = gameObject.GetComponentInChildren<T>();
        }
        return t;
    }

    public static T GetComponentInSelfOrChildren<T>(this Component component) where T : Component {
        var t = component.GetComponent<T>();
        if (!t) {
            t = component.GetComponentInChildren<T>();
        }
        return t;
    }

    public static bool isNetworked(this GameObject go) {
        return go.GetComponent<NetworkIdentity>() != null;
    }
}
