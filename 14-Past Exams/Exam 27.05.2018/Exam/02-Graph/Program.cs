using System;


public class Program
{
    private static long[] cache;
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        cache = new long[n + 1];

        Console.WriteLine(GetPerfectShakesAmount(n));
    }

    private static long GetPerfectShakesAmount(int n)
    {
        if (n < 0 || n % 2 != 0)
        {
            return 0;
        }

        if (n == 0)
        {
            return 1;
        }

        if (cache[n] > 0)
        {
            return cache[n];
        }

        long sum = 0;
        for (int i = 0; i < n; i++)
        {
            sum += GetPerfectShakesAmount(i) * GetPerfectShakesAmount(n - i - 2);
        }

        cache[n] = sum;
        return sum;
    }
}
