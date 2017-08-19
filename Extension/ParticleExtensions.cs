using UnityEngine;
using System.Collections;

public static class ParticleExtensions {
    
    public static void enableEmission(this ParticleSystem particles, bool enabled) {
        var em = particles.emission;
        em.enabled = enabled;
    }

    public static void setColor(this ParticleSystem particles, Color color) {
        var m = particles.main;
        m.startColor = color;
    }

    public static void setSpeed(this ParticleSystem particles, float speed) {
        var m = particles.main;
        m.startSpeed = speed;
    }
}
