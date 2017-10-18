using System;

using UnityEngine;
using System.Collections;

public class PassCollisionEvents : MonoBehaviour {

    public GameObject EventReceiver;

    public void Start() {
        D.Assert(EventReceiver, "Event receiver must not be null!");
    }

    public void OnTriggerEnter2D(Collider2D other) {
        EventReceiver.SendMessage("OnTriggerEnter2D", other, SendMessageOptions.DontRequireReceiver);
    }

    public void OnTriggerExit2D(Collider2D other) {
        EventReceiver.SendMessage("OnTriggerExit2D", other, SendMessageOptions.DontRequireReceiver);
    }

    public void OnTriggerStay2D(Collider2D other) {
        EventReceiver.SendMessage("OnTriggerStay2D", other, SendMessageOptions.DontRequireReceiver);
    }

    public void OnCollisionEnter2D(Collision2D collision) {
        EventReceiver.SendMessage("OnCollisionEnter2D", collision, SendMessageOptions.DontRequireReceiver);

    }

    public void OnCollisionExit2D(Collision2D collision) {
        EventReceiver.SendMessage("OnCollisionExit2D", collision, SendMessageOptions.DontRequireReceiver);

    }

    public void OnCollisionStay2D(Collision2D collision) {
        EventReceiver.SendMessage("OnCollisionStay2D", collision, SendMessageOptions.DontRequireReceiver);
    }
}