using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ReleaseWhenDead : MonoBehaviour {
    ParticleSystem particles;

    void Awake() {
        particles = GetComponent<ParticleSystem>();
    }

    void Update() {
        if (!particles.IsAlive()) {
            gameObject.Release();
        }
    }
}
