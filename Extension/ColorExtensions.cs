using UnityEngine;
using System.Collections;

public static class ColorExtensions {

    public static Color withAlpha(this Color color, float alpha) {
        color.a = alpha;
        return color;
    }

    public static Color withValue(this Color color, float value) {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);
        v = value;
        return Color.HSVToRGB(h, s, v).withAlpha(color.a);
    }
}
