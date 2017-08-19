using System;
using System.Runtime.InteropServices;

using UnityEngine;
using System.Collections;

public class Tweening : MonoBehaviour
{
    bool positionWorld;
    Vector3 positionInitial;
    Vector3 positionDisplacement;
	Interpolate.Function positionFunc;
    Action<GameObject> positionOnComplete;
	float positionDuration;
	float positionDurationTotal;

    bool rotationWorld;
	Vector3 rotationDisplacement;
	Vector3 rotationInitial;
    Interpolate.Function rotationFunc;
    Action<GameObject> rotationOnComplete;
	float rotationDuration;
	float rotationDurationTotal;
	
	Vector3 scaleDisplacement;
	Vector3 scaleInitial;
    Interpolate.Function scaleFunc;
    Action<GameObject> scaleOnComplete;
	float scaleDuration;
	float scaleDurationTotal;
	
	Color colorDisplacement;
	Color colorInitial;
    Interpolate.Function colorFunc;
    Action<GameObject> colorOnComplete;
	float colorDuration;
	float colorDurationTotal;

    new Renderer renderer;

    void Awake() {
        renderer = GetComponent<Renderer>();
    }

    public void MoveTo(Vector3 destination, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete, bool world)
	{
        positionWorld = world;
        positionInitial = positionWorld ? transform.position : transform.localPosition;
		positionDisplacement = destination - positionInitial;
		positionFunc = Interpolate.Ease(easingFunction);
	    positionOnComplete = onComplete;
		positionDurationTotal = duration;
		positionDuration = 0;
	}

    public void RotateTo(Vector3 destination, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete, bool world)
	{
        rotationWorld = world;
        rotationInitial = rotationWorld ? transform.rotation.eulerAngles : transform.localRotation.eulerAngles;

		rotationDisplacement = destination - rotationInitial;		
		var rotX = rotationDisplacement.x; // check the other way around the circle
		var rotY = rotationDisplacement.y; // for each component
		var rotZ = rotationDisplacement.z;
		var rotX2 = (rotationDisplacement.x + 360) % 360;
		var rotY2 = (rotationDisplacement.y + 360) % 360;
		var rotZ2 = (rotationDisplacement.z + 360) % 360;
		if (Mathf.Abs(rotX2) < Mathf.Abs(rotX)) rotX = rotX2;
		if (Mathf.Abs(rotY2) < Mathf.Abs(rotY)) rotY = rotY2;
		if (Mathf.Abs(rotZ2) < Mathf.Abs(rotZ)) rotZ = rotZ2;
		rotationDisplacement = new Vector3(rotX, rotY, rotZ);

		rotationFunc = Interpolate.Ease(easingFunction);
        rotationOnComplete = onComplete;
		rotationDurationTotal = duration;
		rotationDuration = 0;
	}
	
	public void ScaleTo(Vector3 destination, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete)
	{
		scaleInitial = transform.localScale;
		scaleDisplacement = destination - scaleInitial;
		scaleFunc = Interpolate.Ease(easingFunction);
	    scaleOnComplete = onComplete;
		scaleDurationTotal = duration;
		scaleDuration = 0;
	}
	
	public void ColorTo(Color destination, float duration, Interpolate.EaseType easingFunction, Action<GameObject> onComplete)
	{
		colorInitial = renderer.material.color;
		colorDisplacement = destination - colorInitial;
		colorFunc = Interpolate.Ease(easingFunction);
	    colorOnComplete = onComplete;
		colorDurationTotal = duration;
		colorDuration = 0;
	}

	public void FixedUpdate()
	{
        if (positionDuration < positionDurationTotal) {
            positionDuration += Time.deltaTime;
            if (positionDuration >= positionDurationTotal) {
                positionDuration = positionDurationTotal;
            }
			Vector3 pos = positionInitial;
			pos.x = positionFunc(pos.x, positionDisplacement.x, positionDuration, positionDurationTotal);
			pos.y = positionFunc(pos.y, positionDisplacement.y, positionDuration, positionDurationTotal);
			pos.z = positionFunc(pos.z, positionDisplacement.z, positionDuration, positionDurationTotal);
            if (positionWorld) transform.position = pos; else transform.localPosition = pos;
            if (positionDuration >= positionDurationTotal) { // call event at the end after all properties have been set
                if (positionOnComplete != null) positionOnComplete(gameObject);
            }
		}
		
		rotationDuration += Time.deltaTime;
        if (rotationDuration < rotationDurationTotal) {
            if (rotationDuration >= rotationDurationTotal) {
                rotationDuration = rotationDurationTotal;
            }
			Vector3 rot = rotationInitial;
			rot.x = rotationFunc(rot.x, rotationDisplacement.x, rotationDuration, rotationDurationTotal);
			rot.y = rotationFunc(rot.y, rotationDisplacement.y, rotationDuration, rotationDurationTotal);
            rot.z = rotationFunc(rot.z, rotationDisplacement.z, rotationDuration, rotationDurationTotal);
            if (rotationWorld) transform.rotation = Quaternion.Euler(rot); else transform.localRotation = Quaternion.Euler(rot);
            if (rotationDuration >= rotationDurationTotal) { // call event at the end after all properties have been set
                if (rotationOnComplete != null) rotationOnComplete(gameObject);
            }
		}
		
		scaleDuration += Time.deltaTime;
        if (scaleDuration < scaleDurationTotal) {
            if (scaleDuration >= scaleDurationTotal) {
                scaleDuration = scaleDurationTotal;
            }
			Vector3 scale = scaleInitial;
			scale.x = scaleFunc(scale.x, scaleDisplacement.x, scaleDuration, scaleDurationTotal);
			scale.y = scaleFunc(scale.y, scaleDisplacement.y, scaleDuration, scaleDurationTotal);
			scale.z = scaleFunc(scale.z, scaleDisplacement.z, scaleDuration, scaleDurationTotal);
            transform.localScale = scale;
            if (scaleDuration >= scaleDurationTotal) { // call event at the end after all properties have been set
                if (scaleOnComplete != null) scaleOnComplete(gameObject);
            }
		}
		
		colorDuration += Time.deltaTime;
        if (colorDuration < colorDurationTotal) {
            if (colorDuration >= colorDurationTotal) {
                colorDuration = colorDurationTotal;
            }
			Color color = colorInitial;
			color.r = colorFunc(color.r, colorDisplacement.r, colorDuration, colorDurationTotal);
			color.g = colorFunc(color.g, colorDisplacement.g, colorDuration, colorDurationTotal);
			color.b = colorFunc(color.b, colorDisplacement.b, colorDuration, colorDurationTotal);
			color.a = colorFunc(color.a, colorDisplacement.a, colorDuration, colorDurationTotal);
            renderer.material.color = color;
            if (colorDuration >= colorDurationTotal) { // call event at the end after all properties have been set
                if (colorOnComplete != null) colorOnComplete(gameObject);
            }
		}
	}
}