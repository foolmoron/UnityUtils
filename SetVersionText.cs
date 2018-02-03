using UnityEngine;
using System.Collections;

public class SetVersionText : MonoBehaviour {

    public string Prefix;

    void Awake() {
        GetComponent<TextMesh>().text = Prefix + Application.version;
    }
}
