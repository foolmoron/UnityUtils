using System;

using UnityEngine;
using System.Collections;

public static class Tween
{
	public static void MoveTo(GameObject obj, Vector3 destination, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete = null, bool world = false)
	{
		Tweening tweening = obj.GetComponent<Tweening>();
		if (!tweening)
			tweening = obj.AddComponent<Tweening>();

		tweening.MoveTo(destination, duration, easingFunction, onComplete, world);
	}

    public static void MoveBy(GameObject obj, Vector3 displacement, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete = null, bool world = false)
	{
		Vector3 destination = obj.transform.localPosition + displacement;
		MoveTo(obj, destination, duration, easingFunction, onComplete, world);
	}

    public static void RotateTo(GameObject obj, Vector3 destination, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete = null, bool world = false)
	{
		Tweening tweening = obj.GetComponent<Tweening>();
		if (!tweening)
			tweening = obj.AddComponent<Tweening>();
		
		tweening.RotateTo(destination, duration, easingFunction, onComplete, world);
	}

    public static void RotateBy(GameObject obj, Vector3 displacement, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete = null, bool world = false)
	{
		Vector3 destination = obj.transform.localRotation.eulerAngles + displacement;
		RotateTo(obj, destination, duration, easingFunction, onComplete, world);
	}

    public static void ScaleTo(GameObject obj, Vector3 destination, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete = null)
	{
		Tweening tweening = obj.GetComponent<Tweening>();
		if (!tweening)
			tweening = obj.AddComponent<Tweening>();

        tweening.ScaleTo(destination, duration, easingFunction, onComplete);
	}

    public static void ScaleBy(GameObject obj, Vector3 displacement, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete = null)
	{
		Vector3 destination = obj.transform.localScale + displacement;
		ScaleTo(obj, destination, duration, easingFunction, onComplete);
	}


    public static void ColorTo(GameObject obj, Color destination, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete = null)
	{
		Tweening tweening = obj.GetComponent<Tweening>();
		if (!tweening)
			tweening = obj.AddComponent<Tweening>();
		
		tweening.ColorTo(destination, duration, easingFunction, onComplete);
	}
}