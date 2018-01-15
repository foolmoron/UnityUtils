using System;
using System.Globalization;

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LogRangeAttribute))]
class LogRangeDrawer : PropertyDrawer {
    const int TEXT_FIELD_WIDTH = 50;

    float sliderValue;
    float textValue;
    float logValue;
    
    public override float GetPropertyHeight (SerializedProperty prop, GUIContent label) {
        return base.GetPropertyHeight(prop, label) * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        var logRangeAttribute = (LogRangeAttribute) attribute;
        EditorGUI.BeginProperty(position, label, property);
        if (property.propertyType == SerializedPropertyType.Float) {
            // TODO: handle a bunch of edge cases
            sliderValue = EditorGUI.Slider(new Rect(position.x, position.y, position.width, position.height / 2), label, sliderValue, logRangeAttribute.Min, logRangeAttribute.Max);
            textValue = LogInterp(logRangeAttribute.Min, logRangeAttribute.Max, sliderValue / (logRangeAttribute.Max - logRangeAttribute.Min));
            EditorGUI.LabelField(new Rect(position.x + position.width - TEXT_FIELD_WIDTH - 20, position.y + position.height / 2, TEXT_FIELD_WIDTH, position.height / 2), "eˣ");
            if (float.TryParse(EditorGUI.TextField(
                new Rect(position.x + position.width - TEXT_FIELD_WIDTH, position.y + position.height/2, TEXT_FIELD_WIDTH, position.height/2),
                textValue.ToString(CultureInfo.InvariantCulture)
                ), out textValue)) {
            } else {
                textValue = logRangeAttribute.Min;
            }
            sliderValue = LogInterpInverse(logRangeAttribute.Min, logRangeAttribute.Max, textValue) * (logRangeAttribute.Max - logRangeAttribute.Min);
            if (logValue != LogInterp(logRangeAttribute.Min, logRangeAttribute.Max, sliderValue) / (logRangeAttribute.Max - logRangeAttribute.Min)) {
                logValue = LogInterp(logRangeAttribute.Min, logRangeAttribute.Max, sliderValue) / (logRangeAttribute.Max - logRangeAttribute.Min);
                //property.floatValue = logValue;
                Debug.Log(logValue);
            }
        } else {
            EditorGUI.LabelField(position, label.text, "Use LogRange with float.");
        }
        EditorGUI.EndProperty();
    }

    // interps logarithmically between min and max using the following function: y = (M-N)^x - (1-x) + N
    // t should be between 0 and 1
    public float LogInterp(float min, float max, float t) {
        return Mathf.Pow(max - min, t) - (1 - t) + min;
    }

    // solve the LogInterp function for x: y = (M-N)^x - (1-x) + N
    // and you get: x = (y-N+1) - W(ln(M-N)*(M-N)^(y-N+1))/ln(M-N)
    // so this return the x (between 0 and 1) given a y, M, and N
    public float LogInterpInverse(double min, double max, double value) {
        // the guess is derived from the simplified LogInterp function: y = (M-N)^x + N
        // solved for x: x = ln(y-N)/ln(M-N)
        // so to find a good initial guess for the W function, set this simplified function and the real inverse function as equal:
        // ln(y-N)/ln(M-N) ~= (y-N+1) - W(ln(M-N)*(M-N)^(y-N+1))/ln(M-N)
        // then solve for the W part of the right side and you get:
        // ln(M-N)*(y-N+1) - ln(y-N)
        var guess = Math.Log(max - min) * (value - min + 1) - Math.Log(value - min);
        // with the guess and the W function we can just evaluate the inverse LogInterp function here: (y-N+1) - W(ln(M-N)*(M-N)^(y-N+1))/ln(M-N)
        var realValue = (value - min + 1) - LambertWNewtownApproximation(Math.Log(max - min) * Math.Pow(max - min, value - min + 1), guess) / Math.Log(max - min);
        return (float) realValue;
    }

    public double LambertWNewtownApproximation(double value, double guess, double precision = 0.001, int maxIterations = 10) {
        const float E = (float) Math.E;
        double w = guess;
        for (int i = 0; i < maxIterations; i++) {
            double oldW = w;
            w = w - ((w - (value / Math.Pow(E, w))) / (1 + w)); // Newton approximation http://en.wikipedia.org/wiki/Lambert_W_function#Numerical_evaluation
            if (Math.Abs(w - oldW) <= precision) // go to a certain precision
                break;
        }
        return w;
    }
}