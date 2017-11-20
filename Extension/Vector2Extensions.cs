using UnityEngine;
using System.Collections;

public static class Vector2Extensions {

    public static Vector3 to3(this Vector2 vector) {
        return vector;
    }

    public static Vector3 to3(this Vector2 vector, float z) {
        return new Vector3(vector.x, vector.y, z);
    }

    public static Vector2 withX(this Vector2 vector, float x) {
        return new Vector2(x, vector.y);
    }

    public static Vector2 withY(this Vector2 vector, float y) {
        return new Vector2(vector.x, y);
    }

    public static Vector2 plusX(this Vector2 vector, float plusX) {
        return new Vector2(vector.x + plusX, vector.y);
    }

    public static Vector2 plusY(this Vector2 vector, float plusY) {
        return new Vector2(vector.x, vector.y + plusY);
    }

    public static Vector2 timesX(this Vector2 vector, float timesX) {
        return new Vector2(vector.x * timesX, vector.y);
    }

    public static Vector2 timesY(this Vector2 vector, float timesY) {
        return new Vector2(vector.x, vector.y * timesY);
    }

    public static Vector2 scaledWith(this Vector2 vector, Vector2 other) {
        vector.Scale(other);
        return vector;
    }

    public static Vector2 orthogonal(this Vector2 vector) {
        return new Vector2(-vector.y, vector.x);
    }

    public static float angle(this Vector2 vector) {
        return Mathf.Atan2(vector.y, vector.x);
    }

    public static float angleDeg(this Vector2 vector) {
        var deg = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        deg = (deg + 360f) % 360f;
        return deg;
    }

    public static float aspect(this Vector2 vector) {
        return vector.x / vector.y;
    }

	public static Vector2 Rotate(this Vector2 vector, float degrees) {
		float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
		float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);
		
		float tx = vector.x;
		float ty = vector.y;
		vector.x = (cos * tx) - (sin * ty);
		vector.y = (sin * tx) + (cos * ty);
		return vector;
	}
}
