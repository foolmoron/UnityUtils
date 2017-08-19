using System;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ArrayExtensions {

    public static T Random<T>(this IList<T> array) {
        return array[Mathf.FloorToInt(UnityEngine.Random.value * array.Count)];
    }

    public static int Count<T>(this IList<T> array, Func<T, bool> countPredicate) {
        var count = 0;
        for (int i = 0; i < array.Count; i++) {
            if (countPredicate(array[i]))
                count++;
        }
        return count;
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
}