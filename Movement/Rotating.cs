using UnityEngine;
using System.Collections;

public class Rotating : MonoBehaviour {

    public Vector3 DegreesPerSecond;

    void Update() {
        transform.Rotate(DegreesPerSecond * Time.deltaTime);
    }
}
