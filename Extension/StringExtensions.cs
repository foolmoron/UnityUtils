using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Text;

public static class StringExtensions {
    
    static readonly StringBuilder sb = new StringBuilder(40);
    public static string LineWrap(this string text, int maxLineLength, char wordDelimiter = ' ', string lineDelimiter = "\n") {
        sb.Length = 0;
        var words = text.Split(wordDelimiter);
        var lineLength = 0;
        var currentWord = 0;
        while (currentWord < words.Length) {
            var word = words[currentWord];
            if (lineLength == 0 || (lineLength + word.Length + 1) < maxLineLength) {
                sb.Append(wordDelimiter);
                sb.Append(word);
                lineLength += 1 + word.Length;
                currentWord++;
            } else {
                sb.AppendLine();
                lineLength = 0;
            }
        }
        return sb.ToString();
    }
}
