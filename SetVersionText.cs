using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetVersionText : MonoBehaviour {

    public string Prefix;

    void Awake() {
        if (GetComponent<TextMesh>()) {
            GetComponent<TextMesh>().text = Prefix + Application.version;
        } else if (GetComponent<Text>()) {
            GetComponent<Text>().text = Prefix + Application.version;
        }
    }
}
