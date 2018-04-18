using System;
using UnityEngine;
using Random = UnityEngine.Random;

public struct WithRandomSeed : IDisposable {

    readonly Random.State prevState;

    public WithRandomSeed(int seed) {
        prevState = Random.state;
        Random.InitState(seed);
    }

    public void Dispose() {
        Random.state = prevState;
    }
}
