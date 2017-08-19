using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolOfList<TItem> : NonGameObjectPool<List<TItem>> {
    
    public PoolOfList(int initialCount = 1) : base(initialCount) {
        if (OnRelease == null) {
            OnRelease = obj => obj.Clear();
        }
    }
}
