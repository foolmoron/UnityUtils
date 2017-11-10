using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ForceCameraAspectRatio : MonoBehaviour {

	public Vector2 CameraSize = new Vector2(1920, 1080);

	new Camera camera;

	void Awake() {
		camera = GetComponent<Camera>();
	}

	void Start() {
		ScaleToAspectRatio();
	}
	
	public void ScaleToAspectRatio() {
		var rect = camera.rect;

		var targetAspect = CameraSize.aspect();
		var windowAspect = new Vector2(Screen.width, Screen.height).aspect();
		var scaleHeight = windowAspect / targetAspect;

		// if scaled height is less than current height, add letterbox
		if (scaleHeight < 1.0f) {
			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1.0f - scaleHeight) / 2.0f;
		} else {
			// add pillarbox
			var scaleWidth = 1.0f / scaleHeight;
			rect.width = scaleWidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scaleWidth) / 2.0f;
			rect.y = 0;
		}

		camera.rect = rect;
	}
}