using System;
using System.Linq;

using UnityEngine;
using System.Collections;
using System.Diagnostics;

public static class StopwatchExtensions {

    public static void Restart(this Stopwatch stopwatch) {
        stopwatch.Reset();
        stopwatch.Start();
    }
}
