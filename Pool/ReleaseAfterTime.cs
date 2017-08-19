using UnityEngine;
using System.Collections;

public class ReleaseAfterTime : MonoBehaviour {
    [Range(0, 120)]
    public float TimeToRelease = 10;
    float releaseTimer;
    bool obtained;

    void Update() {
        if (obtained) {
            releaseTimer -= Time.deltaTime;
            if (releaseTimer <= 0) {
                gameObject.Release();
            }
        }
    }

    void OnObtain() {
        obtained = true;
        releaseTimer = TimeToRelease;
    }
    
    void OnRelease() {
        obtained = false;
        releaseTimer = TimeToRelease;
    }
}
