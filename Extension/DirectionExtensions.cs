using UnityEngine;
using System.Collections;

public static class DirectionExtensions {

    public static float toAngle(this Direction direction) {
        switch (direction) {
            case Direction.Up:
                return 90;
            case Direction.Down:
                return 270;
            case Direction.Left:
                return 180;
            case Direction.Right:
                return 0;
            default:
                return 0;
        }
    }

    public static float toRad(this Direction direction) {
        switch (direction) {
            case Direction.Up:
                return Mathf.PI / 2;
            case Direction.Down:
                return 3 * Mathf.PI / 2;
            case Direction.Left:
                return Mathf.PI;
            case Direction.Right:
                return 0;
            default:
                return 0;
        }
    }

    public static Vector2 toVector(this Direction direction) {
        switch (direction) {
            case Direction.Up:
                return Vector2.up;
            case Direction.Down:
                return -Vector2.up;
            case Direction.Left:
                return -Vector2.right;
            case Direction.Right:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }
}
