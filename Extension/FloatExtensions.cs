using UnityEngine;
using System.Collections;

public static class FloatExtensions {

    public static Vector2 toVectorRad(this float angle) {
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public static Vector2 toVectorDeg(this float angle) {
        angle = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }

    public static float inverse(this float val, float minForZero = 0f) {
        return val <= minForZero ? 0 : 1 / val;
    }

    public static float to01(this float val) {
        // maps [-1, 1] to [0, 1], useful for trig functions, input axes, etc
        return (Mathf.Clamp(val, -1, 1) + 1) / 2;
    }

    public static Direction toAnimationDirection(this float angle) {
        var angleOfInputRotated = (angle - 45 + 360) % 360; // rotate so 0 is top-right corner
        if (angleOfInputRotated >= 0 && angleOfInputRotated <= 90) { // down/up use inclusive comparators to prefer them for diagonal movement
            return Direction.Up;
        } else if (angleOfInputRotated > 90 && angleOfInputRotated < 180) {
            return Direction.Left;
        } else if (angleOfInputRotated >= 180 && angleOfInputRotated <= 270) {
            return Direction.Down;
        } else if (angleOfInputRotated > 270 && angleOfInputRotated < 360) {
            return Direction.Right;
        }
        return Direction.None;
    }
}
