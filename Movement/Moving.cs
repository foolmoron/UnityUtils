using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

    public Vector3 MovementPerSecond;

    void Update() {
        transform.position += MovementPerSecond * Time.deltaTime;
    }
}
