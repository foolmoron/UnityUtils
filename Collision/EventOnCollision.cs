using System;

using UnityEngine;
using System.Collections;

public class EventOnCollision : MonoBehaviour
{
    public event Action<Collider2D> OnTriggerEnter = delegate { };
    public event Action<Collider2D> OnTriggerExit = delegate { };
    public event Action<Collider2D> OnTriggerStay = delegate { };
    public event Action<Collision2D> OnCollisionEnter = delegate { };
    public event Action<Collision2D> OnCollisionExit = delegate { };
    public event Action<Collision2D> OnCollisionStay = delegate { };

    void OnTriggerEnter2D(Collider2D other) {
        OnTriggerEnter(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        OnTriggerExit(other);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerStay(other);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        OnCollisionEnter(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        OnCollisionExit(collision);

    }

    void OnCollisionStay2D(Collision2D collision)
    {
        OnCollisionStay(collision);
    }
}