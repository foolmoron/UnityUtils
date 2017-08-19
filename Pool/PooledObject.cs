using UnityEngine;
using System.Collections;

public class PooledObject : MonoBehaviour {
    public ObjectPool Source;

    public void Release() {
        Source.Release(gameObject);
    }
}
