using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Events;

[Serializable]
public class UnityEventInt : UnityEvent<int> { }
[Serializable]
public class UnityEventFloat : UnityEvent<float> { }
[Serializable]
public class UnityEventBool : UnityEvent<bool> { }
[Serializable]
public class UnityEventString : UnityEvent<string> { }
[Serializable]
public class UnityEventVector2 : UnityEvent<Vector2> { }
[Serializable]
public class UnityEventVector3 : UnityEvent<Vector3> { }
[Serializable]
public class UnityEventGameObject : UnityEvent<GameObject> { }
[Serializable]
public class UnityEventRaycastHit2D : UnityEvent<RaycastHit2D> { }