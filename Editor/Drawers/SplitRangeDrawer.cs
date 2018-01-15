using System;
using System.Globalization;

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SplitRangeAttribute))]
class SplitRangeDrawer : PropertyDrawer {
    const int TEXT_FIELD_WIDTH = 54;

    float sliderValue;
    float textValue;

    public override float GetPropertyHeight(SerializedProperty prop, GUIContent label) {
        return base.GetPropertyHeight(prop, label) * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        var splitRangeAttribute = (SplitRangeAttribute) attribute;
        EditorGUI.BeginProperty(position, label, property);
        if (property.propertyType == SerializedPropertyType.Float) {
            var val = property.floatValue;
            // get normalized 0-1 value from current real value
            {
                var wasInLastHalf = sliderValue >= 0.5f; // allows the algorithm to prefer the half that it previously was on, instead of blindly choosing left
                var inFirstHalf = (val >= splitRangeAttribute.Start && val <= splitRangeAttribute.Midpoint) || (val <= splitRangeAttribute.Start && val >= splitRangeAttribute.Midpoint);
                var inLastHalf = (val >= splitRangeAttribute.Midpoint && val <= splitRangeAttribute.End) || (val <= splitRangeAttribute.Midpoint && val >= splitRangeAttribute.End);
                var useFirstHalf = (!wasInLastHalf && inFirstHalf) || (wasInLastHalf && inFirstHalf && !inLastHalf);
                var useLastHalf = (wasInLastHalf && inLastHalf) || (!wasInLastHalf && !inFirstHalf && inLastHalf);

                if (useFirstHalf) {
                    sliderValue = Mathf.InverseLerp(splitRangeAttribute.Start, splitRangeAttribute.Midpoint, val) / 2;
                } else if (useLastHalf) {
                    sliderValue = Mathf.InverseLerp(splitRangeAttribute.Midpoint, splitRangeAttribute.End, val) / 2 + 0.5f;
                } else {
                    sliderValue = 0;
                }
            }
            // make slider with current normalized value and get new slider value
            {
                EditorGUI.LabelField(new Rect(position.x, position.y + position.height * 0.28f, position.width, position.height / 2), label.text, " ");
                sliderValue = EditorGUI.Slider(new Rect(position.x, position.y, position.width + TEXT_FIELD_WIDTH, position.height / 2), " ", sliderValue, 0, 1);
            }
            // convert new slider position to new real value
            float newRealValue;
            {
                if (sliderValue >= 0 && sliderValue <= 0.5) {
                    newRealValue = Mathf.Lerp(splitRangeAttribute.Start, splitRangeAttribute.Midpoint, sliderValue * 2);
                } else if (sliderValue >= 0.5 && sliderValue <= 1) {
                    newRealValue = Mathf.Lerp(splitRangeAttribute.Midpoint, splitRangeAttribute.End, (sliderValue - 0.5f) * 2);
                } else {
                    newRealValue = splitRangeAttribute.Start;
                }
            }
            // make text field with the new real value and get the new text value and parse it into a number
            {
                string newTextValue = EditorGUI.TextField(
                    new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), 
                    " ", 
                    newRealValue.ToString(CultureInfo.InvariantCulture)
                );
                if (float.TryParse(newTextValue, out newRealValue)) {
                } else {
                    newRealValue = splitRangeAttribute.Start;
                }
            }
            // set the new real value as the property value
            {
                //Debug.Log(newRealValue);
                property.floatValue = newRealValue;
            }
        } else {
            EditorGUI.LabelField(position, label.text, "Use SplitRange with float.");
        }
        EditorGUI.EndProperty();
    }
}