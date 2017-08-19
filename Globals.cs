using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

    static Globals TheOnlyOne;

    void Awake() {
        if (TheOnlyOne == null) {
            TheOnlyOne = this;
            DontDestroyOnLoad(gameObject);
        } else {
            // have to use DestroyImmediate because if you use Destroy then
            // the object only gets destroyed at the end of the frame
            // which means that any Starts that run during the same frame as this Awake
            // can accidentally get this object when doing FindObjectOfType
            // and are left hanging onto a dead reference instead of the
            // correct Globals object
            DestroyImmediate(gameObject);
        }
    }
}
