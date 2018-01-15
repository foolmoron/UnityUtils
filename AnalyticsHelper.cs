using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.Analytics;

public class AnalyticsHelper {
    static readonly Dictionary<string, object> dict = new Dictionary<string, object>(10);

    public static void Event(string eventName,
        string param1Name = null, object param1 = null,
        string param2Name = null, object param2 = null,
        string param3Name = null, object param3 = null,
        string param4Name = null, object param4 = null,
        string param5Name = null, object param5 = null,
        string param6Name = null, object param6 = null,
        string param7Name = null, object param7 = null,
        string param8Name = null, object param8 = null,
        string param9Name = null, object param9 = null,
        string param10Name = null, object param10 = null
    ) {
        dict.Clear();
        if (param1Name != null) dict.Add(param1Name, param1);
        if (param2Name != null) dict.Add(param2Name, param2);
        if (param3Name != null) dict.Add(param3Name, param3);
        if (param4Name != null) dict.Add(param4Name, param4);
        if (param5Name != null) dict.Add(param5Name, param5);
        if (param6Name != null) dict.Add(param6Name, param6);
        if (param7Name != null) dict.Add(param7Name, param7);
        if (param8Name != null) dict.Add(param8Name, param8);
        if (param9Name != null) dict.Add(param9Name, param9);
        if (param10Name != null) dict.Add(param10Name, param10);
        Analytics.CustomEvent(eventName, dict);
    }

    public static void Event(string eventName, Vector3 position) {
        Analytics.CustomEvent(eventName, position);
    }
}