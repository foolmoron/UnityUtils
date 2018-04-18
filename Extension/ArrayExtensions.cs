using System;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ArrayExtensions {

    public static T First<T>(this IList<T> array) {
        return array[0];
    }
    public static T FirstOrDefault<T>(this IList<T> array, T defaultValue = default(T)) {
        return array.Count > 0 ? array[0] : defaultValue;
    }

    public static T Last<T>(this IList<T> array) {
        return array[array.Count - 1];
    }
    public static T LastOrDefault<T>(this IList<T> array, T defaultValue = default(T)) {
        return array.Count > 0 ? array[array.Count - 1] : defaultValue;
    }

    public static T Random<T>(this IList<T> array) {
        return array[Mathf.FloorToInt(UnityEngine.Random.value * array.Count)];
    }

    public static T RandomExceptRange<T>(this IList<T> array, int indexStart, int indexRange = 1) {
        var indexExceptRange = (indexStart + indexRange + Mathf.FloorToInt(UnityEngine.Random.value * (array.Count - indexRange))) % array.Count;
        return array[indexExceptRange];
    }
    
    public static TArray RandomWhere<TArray, TItem>(this IList<TArray> array, TItem item, Func<TArray, TItem, bool> filterPredicate) {
        var items = Count(array, item, filterPredicate);
        var matchingItemToPick = Mathf.FloorToInt(UnityEngine.Random.value * items);
        var matchingItems = 0;
        for (int i = 0; i < array.Count; i++) {
            if (filterPredicate(array[i], item)) {
                if (matchingItems == matchingItemToPick) {
                    return array[i];
                }
                matchingItems++;
            }
        }
        return default(TArray);
    }

    public static T RandomWhere<T>(this IList<T> array, Func<T, bool> filterPredicate) {
        var items = Count(array, filterPredicate);
        var matchingItemToPick = Mathf.FloorToInt(UnityEngine.Random.value * items);
        var matchingItems = 0;
        for (int i = 0; i < array.Count; i++) {
            if (filterPredicate(array[i])) {
                if (matchingItems == matchingItemToPick) {
                    return array[i];
                }
                matchingItems++;
            }
        }
        return default(T);
    }

    public static T Next<T>(this IList<T> array, T current) {
        var currentIndex = array.IndexOf(current);
        var nextIndex = (currentIndex + 1) % array.Count;
        return array[nextIndex];
    }

    public static int Count<T>(this IList<T> array, Func<T, bool> countPredicate) {
        var count = 0;
        foreach (var it in array) {
            if (countPredicate(it))
                count++;
        }
        return count;
    }

    public static int Count<TArray, TItem>(this IList<TArray> array, TItem item, Func<TArray, TItem, bool> countPredicate) {
        var count = 0;
        foreach (var it in array) {
            if (countPredicate(it, item))
                count++;
        }
        return count;
    }

    public static bool All<T>(this IList<T> array, Func<T, bool> condition) {
        foreach (var it in array) {
            if (!condition(it)) {
                return false;
            }
        }
        return true;
    }

    public static bool All<TArray, TItem>(this IList<TArray> array, TItem item, Func<TArray, TItem, bool> comparator) {
        foreach (var it in array) {
            if (!comparator(it, item)) {
                return false;
            }
        }
        return true;
    }

    public static bool Any<T>(this IList<T> array, Func<T, bool> condition) {
        foreach (var it in array) {
            if (condition(it)) {
                return true;
            }
        }
        return false;
    }
    
    public static bool Any<TArray, TItem>(this IList<TArray> array, TItem item, Func<TArray, TItem, bool> comparator) {
        for (int i = 0; i < array.Count; i++) {
            if (comparator(array[i], item)) {
                return true;
            }
        }
        return false;
    }

    public static int Sum<T>(this IList<T> array, Func<T, int> sumFunction) {
        var sum = 0;
        for (int i = 0; i < array.Count; i++) {
            sum += sumFunction(array[i]);
        }
        return sum;
    }

    public static float Sum<T>(this IList<T> array, Func<T, float> sumFunction) {
        var sum = 0f;
        for (int i = 0; i < array.Count; i++) {
            sum += sumFunction(array[i]);
        }
        return sum;
    }

    public static void ForEach<T>(this IList<T> array, Action<T> action) {
        for (int i = 0; i < array.Count; i++) {
            action(array[i]);
        }
    }

    public static TReturn[] Map<TArray, TReturn>(this IList<TArray> array, Func<TArray, TReturn> map) {
        var newArray = new TReturn[array.Count];
        for (int i = 0; i < array.Count; i++) {
            newArray[i] = map(array[i]);
        }
        return newArray;
    }

    public static TReturn[] Map<TArray, TReturn>(this IList<TArray> array, Func<TArray, TReturn> map, TReturn[] preallocatedArray) {
        for (int i = 0; i < preallocatedArray.Length; i++) {
            preallocatedArray[i] = i < array.Count ? map(array[i]) : default(TReturn);
        }
        return preallocatedArray;
    }

    public static int IndexOf<T>(this IList<T> array, T item) {
        for (int i = 0; i < array.Count; i++) {
            if ((array[i] == null && item == null) || (array[i] != null && array[i].Equals(item)))
                return i;
        }
        return -1;
    }

    public static int IndexOf<TArray, TItem>(this IList<TArray> array, TItem item, Func<TArray, TItem, bool> comparator) {
        for (int i = 0; i < array.Count; i++) {
            if (comparator(array[i], item))
                return i;
        }
        return -1;
    }

    public static T Find<T>(this IList<T> array, Func<T, bool> searchPredicate) {
        for (int i = 0; i < array.Count; i++) {
            if (searchPredicate(array[i]))
                return array[i];
        }
        return default(T);
    }

    public static TArray Find<TArray, TItem>(this IList<TArray> array, TItem item, Func<TArray, TItem, bool> comparator) {
        for (int i = 0; i < array.Count; i++) {
            if (comparator(array[i], item))
                return array[i];
        }
        return default(TArray);
    }

    public static bool Contains<T>(this IList<T> array, T item) {
        for (int i = 0; i < array.Count; i++) {
            if ((array[i] == null && item == null) || (array[i] != null && array[i].Equals(item)))
                return true;
        }
        return false;
    }

    public static bool Contains<TArray, TItem>(this IList<TArray> array, TItem item, Func<TArray, TItem, bool> comparator) {
        for (int i = 0; i < array.Count; i++) {
            if (comparator(array[i], item))
                return true;
        }
        return false;
    }

    public static bool ContainsIgnoreCase(this IList<string> array, string item) {
        for (int i = 0; i < array.Count; i++) {
            if (string.Compare(array[i], item, StringComparison.OrdinalIgnoreCase) == 0)
                return true;
        }
        return false;
    }

    public static IList<T> Shuffle<T>(this IList<T> array) {
        // fisher-yates
        for (int i = 0; i < array.Count - 1; i++) {
            // random index [i, n)
            var index = Mathf.FloorToInt(UnityEngine.Random.value * (array.Count - i)) + i;
            // swap
            var swap = array[i];
            array[i] = array[index];
            array[index] = swap;
        }
        return array;
    }

    public static void AddOnce<T>(this List<T> list, T item) {
        if (!list.Contains(item)) {
            list.Add(item);
        }
    }

    public static int RemoveAll<T>(this List<T> list, T item) {
        var count = 0;
        while (list.Contains(item)) {
            list.Remove(item);
            count++;
        }
        return count;
    }
    
    public static int RemoveInactive(this List<GameObject> list) {
        var count = 0;
        for (int i = 0; i < list.Count; i++) {
            if (!list[i] || !list[i].activeSelf) {
                list.RemoveAt(i);
                i--;
                count++;
            }
        }
        return count;
    }

    public static int RemoveInactive<T>(this List<T> list) where T : MonoBehaviour {
        var count = 0;
        for (int i = 0; i < list.Count; i++) {
            if (!list[i].isActiveAndEnabled) {
                list.RemoveAt(i);
                i--;
                count++;
            }
        }
        return count;
    }
}