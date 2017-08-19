using System;
using System.Collections.Generic;
using UnityEngine;

public class NonGameObjectPool<T> where T : class, new() {
    
    // using a Stack because it has convenient push/pop semantics, but internally it's still an array with nice performance
    // downside is that you can't access objects via index, but if that's really necessary then just change it to a List and add an extra count tracking variable
    public Stack<T> Objects { get; private set; }
    public Action<T> OnObtain;
    public Action<T> OnRelease;

    public NonGameObjectPool(int initialCount = 1) {
        Objects = new Stack<T>(initialCount);
        for (int i = 0; i < initialCount; i++) {
            Release(new T());
        }
    }

    /// <summary>Obtains an object from the pool.</summary>
    public T Obtain() {
        if (Objects.Count < 1) {
            Debug.LogWarning("Had to instantiate pooled object at runtime! Raise the initial count of [" + GetType().Name + "].");
            Release(new T());
        }
        var obj = Objects.Pop();
        if (OnObtain != null) { 
            OnObtain(obj);
        }
        return obj;
    }

    /// <summary>Releases an object into the pool.</summary>
    public void Release(T obj) {
        if (OnRelease != null) {
            OnRelease(obj);
        }
        Objects.Push(obj);
    }
}
