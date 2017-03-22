using System;

public static class SudokuExtensions
{
    public static string ToFormattedString(this int[] arr)
    {
        return $"{{ {string.Join(", ", arr)} }}";
    }
}