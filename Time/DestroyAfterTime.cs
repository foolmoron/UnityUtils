using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    [Range(0, 120)]
    public float TimeToDestroy = 10;
    float destroyTimer;

    void Update() {
        destroyTimer += Time.deltaTime;
        if (destroyTimer >= TimeToDestroy) {
            Destroy(gameObject);
        }
    }
}
