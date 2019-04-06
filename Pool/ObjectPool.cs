using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ObjectPoolExtensions {

    /// <summary>Releases the <paramref name="obj"/> to its ObjectPool, creating a new pool if necessary.</summary>
    public static void Release(this GameObject obj) {
        var pooledObj = obj.GetComponent<PooledObject>();
        if (!pooledObj) {
            pooledObj = obj.AddComponent<PooledObject>();
            pooledObj.Source = obj.GetObjectPool();
        }
        pooledObj.Release();
    }

    /// <summary>Returns an ObjectPool in the scene that provides <paramref name="obj"/>, or creates a new one for <paramref name="obj"/> with the given <paramref name="initialCount"/>.</summary>
    public static ObjectPool GetObjectPool(this GameObject obj, int initialCount = 5, bool keepObjectsParented = false) {
        var allPools = Object.FindObjectsOfType<ObjectPool>();
        for (int i = 0; i < allPools.Length; i++) {
            var pool = allPools[i];
            if (pool.Object == obj) {
                return pool;
            }
        }
        var newPoolObj = new GameObject("Pool - " + obj.name);
        var newPool = newPoolObj.AddComponent<ObjectPool>();
        newPool.Object = obj;
        newPool.InitialCount = initialCount;
        newPool.KeepObjectsParented = keepObjectsParented;
        return newPool;
    }
}

public class ObjectPool : MonoBehaviour {

    public event Action<GameObject> OnObtain = delegate { };
    public event Action<GameObject> OnRelease = delegate { };

    public GameObject Object;
    [Range(0, 100)]
    public int InitialCount;
    public bool KeepObjectsParented;

    // using a Stack because it has convenient push/pop semantics, but internally it's still an array with nice performance
    // downside is that you can't access objects via index, but if that's really necessary then just change it to a List and add an extra count tracking variable
    Stack<GameObject> objects;

    // all objects that came through this ObjectPool
    public List<GameObject> AllObjects { get; private set; }

    void Awake() {
        if (objects == null) {
            Init();
        }
    }

    void Init() {
        AllObjects = new List<GameObject>(InitialCount);
        objects = new Stack<GameObject>(InitialCount);
        for (int i = 0; i < InitialCount; i++) {
            Release(Instantiate(Object));
        }
    }

    /// <summary>Obtains an object from the pool and sends the OnObtain message.</summary>
    public GameObject ObtainSimple(Vector3? position = null) {
        if (objects == null) {
            Init();
        }
        if (objects.Count < 1) {
            Debug.LogWarning("Had to instantiate pooled object at runtime! Raise the initial count of [" + Object.name + "].", gameObject);
            Release(Instantiate(Object));
        }

        var obj = objects.Pop();
        obj.transform.parent = KeepObjectsParented ? transform : null;
        if (position.HasValue) { // allow setting a position BEFORE activating object, so unintended collisions don't occur
            obj.transform.position = position.Value;
        }
        obj.SetActive(true);
        obj.SendMessage("OnObtain", SendMessageOptions.DontRequireReceiver);
        return obj;
    }

    /// <summary>Obtains an object from the pool (simple), and returns the component of the given type.</summary>
    public T ObtainSimple<T>(Vector3? position = null) where T : Component {
        return ObtainSimple(position).GetComponent<T>();
    }

    /// <summary>Obtains an object from the pool, sends the OnObtain message, and resets common components.</summary>
    public GameObject Obtain(Vector3? position = null) {
        var obj = ObtainSimple(position);
        // reset Animator or FSM or something
        {
        }
        // reset another component, etc.
        {
        }
        return obj;
    }

    /// <summary>Obtains an object from the pool, and returns the component of the given type.</summary>
    public T Obtain<T>(Vector3? position = null) where T : Component {
        var component = Obtain(position).GetComponent<T>();
        D.Assert(component != null, "Component '" + typeof(T).Name + "' does not exist on pooled object!");
        if (component) {
            OnObtain(component.gameObject);
        }
        return component;
    }

    /// <summary>Releases an object into the pool and sends the OnRelease message.</summary>
    public void Release(GameObject obj) {
        if (objects == null) {
            Init();
        }
        if (objects.Contains(obj)) {
            return;
        }
        AllObjects.AddOnce(obj);

        var pooledObj = obj.GetComponent<PooledObject>() ?? obj.AddComponent<PooledObject>();
        pooledObj.Source = this;
        objects.Push(obj);
        obj.transform.parent = transform;
        obj.SendMessage("OnRelease", SendMessageOptions.DontRequireReceiver);
        obj.SetActive(false); // SendMessage only works on active objects, so deactive after
        OnRelease(obj);
    }
}
