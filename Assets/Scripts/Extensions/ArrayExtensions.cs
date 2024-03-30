using System;

public static class ArrayExtensions
{
    private static Random rng = new Random();

    public static void Shuffle<T>(this T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}