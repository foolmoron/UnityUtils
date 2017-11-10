using UnityEngine;
using UnityEditor;
using System.Reflection;

// From: https://www.reddit.com/r/Unity3D/comments/1s6czv/inspectorbutton_add_a_custom_button_to_your/

[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonDrawer : PropertyDrawer {
    MethodInfo eventMethodInfo;

    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label) {
        var inspectorButtonAttribute = (InspectorButtonAttribute) attribute;
        var isPrefab = PrefabUtility.GetPrefabParent(prop.serializedObject.targetObject) == null && PrefabUtility.GetPrefabObject(prop.serializedObject.targetObject) != null;
        label.text = (isPrefab ? "PREFAB - " : "") + label.text;
        var width = Mathf.Min(position.width, GUI.skin.button.CalcSize(label).x + 12);
        var buttonRect = new Rect(
            position.x + (position.width - width) * 0.5f, 
            position.y,
            width,
            position.height
        );
        if (GUI.Button(buttonRect, label.text)) {
            var eventOwnerType = prop.serializedObject.targetObject.GetType();
            var eventName = inspectorButtonAttribute.MethodName;

            if (eventMethodInfo == null) {
                eventMethodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
            }

            if (eventMethodInfo != null) {
                eventMethodInfo.Invoke(prop.serializedObject.targetObject, null);
            } else {
                Debug.LogError(string.Format("InspectorButton: Unable to find method {0} in {1}", eventName, eventOwnerType));
            }
        }
    }
}