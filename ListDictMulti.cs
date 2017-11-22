using System;

using System.Collections;
using System.Collections.Generic;

public class ListDict2<TKey, TValue1, TValue2> {
    public readonly List<TKey> Keys;
    public readonly List<TValue1> Values1;
    public readonly List<TValue2> Values2;
    public int Count { get { return Keys.Count; } }

    public ListDict2(int capacity = 0) {
        Keys = new List<TKey>(capacity);
        Values1 = new List<TValue1>(capacity);
        Values2 = new List<TValue2>(capacity);
    }

    public void Add(TKey key, TValue1 value1, TValue2 value2 = default(TValue2)) {
        var index = Keys.IndexOf(key);
        if (index < 0) {
            Keys.Add(key);
            Values1.Add(default(TValue1));
            Values2.Add(default(TValue2));
            index = Keys.Count - 1;
        }
        Values1[index] = value1;
        Values2[index] = value2;
    }

    public bool Remove(TKey key) {
        return RemoveAt(Keys.IndexOf(key));
    }

    public int RemoveAll(TKey key) {
        var removedCount = 0;
        var nextIndex = Keys.IndexOf(key);
        while (nextIndex >= 0) {
            RemoveAt(nextIndex);
            removedCount++;
            nextIndex = Keys.IndexOf(key);
        }
        return removedCount;
    }

    public bool RemoveAt(int index) {
        if (index < 0 || index >= Count) {
            return false;
        }
        Keys.RemoveAt(index);
        Values1.RemoveAt(index);
        Values2.RemoveAt(index);
        return true;
    }

    public void Clear() {
        Keys.Clear();
        Values1.Clear();
        Values2.Clear();
    }

    public bool ContainsKey(TKey key) {
        return Keys.IndexOf(key) >= 0;
    }
    
    public TValue1 this[TKey key] {
        get { return Values1[Keys.IndexOf(key)]; }
        set { Add(key, value, default(TValue2)); }
    }

    public TValue1 GetValue1(TKey key) {
        return Values1[Keys.IndexOf(key)];
    }
    public void SetValue1(TKey key, TValue1 value1) {
        Values1[Keys.IndexOf(key)] = value1;
    }
    public TValue2 GetValue2(TKey key) {
        return Values2[Keys.IndexOf(key)];
    }
    public void SetValue2(TKey key, TValue2 value2) {
        Values2[Keys.IndexOf(key)] = value2;
    }

    public override string ToString() {
        return string.Format("Count = {0}", Count);
    }
}

public class ListDict3<TKey, TValue1, TValue2, TValue3> {
    public readonly List<TKey> Keys;
    public readonly List<TValue1> Values1;
    public readonly List<TValue2> Values2;
    public readonly List<TValue3> Values3;
    public int Count { get { return Keys.Count; } }

    public ListDict3(int capacity = 0) {
        Keys = new List<TKey>(capacity);
        Values1 = new List<TValue1>(capacity);
        Values2 = new List<TValue2>(capacity);
        Values3 = new List<TValue3>(capacity);
    }

    public void Add(TKey key, TValue1 value1, TValue2 value2 = default(TValue2), TValue3 value3 = default(TValue3)) {
        var index = Keys.IndexOf(key);
        if (index < 0) {
            Keys.Add(key);
            Values1.Add(default(TValue1));
            Values2.Add(default(TValue2));
            Values3.Add(default(TValue3));
            index = Keys.Count - 1;
        }
        Values1[index] = value1;
        Values2[index] = value2;
        Values3[index] = value3;
    }

    public bool Remove(TKey key) {
        return RemoveAt(Keys.IndexOf(key));
    }

    public int RemoveAll(TKey key) {
        var removedCount = 0;
        var nextIndex = Keys.IndexOf(key);
        while (nextIndex >= 0) {
            RemoveAt(nextIndex);
            removedCount++;
            nextIndex = Keys.IndexOf(key);
        }
        return removedCount;
    }

    public bool RemoveAt(int index) {
        if (index < 0 || index >= Count) {
            return false;
        }
        Keys.RemoveAt(index);
        Values1.RemoveAt(index);
        Values2.RemoveAt(index);
        Values3.RemoveAt(index);
        return true;
    }

    public void Clear() {
        Keys.Clear();
        Values1.Clear();
        Values2.Clear();
        Values3.Clear();
    }

    public bool ContainsKey(TKey key) {
        return Keys.IndexOf(key) >= 0;
    }
    
    public TValue1 this[TKey key] {
        get { return Values1[Keys.IndexOf(key)]; }
        set { Add(key, value, default(TValue2), default(TValue3)); }
    }

    public TValue1 GetValue1(TKey key) {
        return Values1[Keys.IndexOf(key)];
    }
    public void SetValue1(TKey key, TValue1 value1) {
        Values1[Keys.IndexOf(key)] = value1;
    }
    public TValue2 GetValue2(TKey key) {
        return Values2[Keys.IndexOf(key)];
    }
    public void SetValue2(TKey key, TValue2 value2) {
        Values2[Keys.IndexOf(key)] = value2;
    }
    public TValue3 GetValue3(TKey key) {
        return Values3[Keys.IndexOf(key)];
    }
    public void SetValue3(TKey key, TValue3 value3) {
        Values3[Keys.IndexOf(key)] = value3;
    }

    public override string ToString() {
        return string.Format("Count = {0}", Count);
    }
}