using System;
using System.Collections;

using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : Manager<T> {
    static T inst;
    public static T Inst {
        get {
            if (!inst) {
                inst = FindObjectOfType<T>(); // allows Inst to be accessed during anyone's Awake
            }
            return inst;
        }
    }
}