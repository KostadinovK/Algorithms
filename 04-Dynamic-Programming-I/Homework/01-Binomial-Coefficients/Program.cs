using System;

public class Program
{
    public static long[][] coefficients;

    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());

        coefficients = new long[n + 1][];
        coefficients[0] = new long[] {1};

        long res = GetCoefficient(n, k);
        Console.WriteLine(res);
    }

    private static long GetCoefficient(int n, int k)
    {
        if (coefficients[n] == null)
        {
            coefficients[n] = new long[n + 1];
        }

        if (k < 0 || k >= coefficients[n].Length)
        {
            return 0;
        }

        if (coefficients[n][k] != 0)
        {
            return coefficients[n][k];
        }

        long res = GetCoefficient(n - 1, k - 1) + GetCoefficient(n - 1, k);
        coefficients[n][k] = res;
        return res;
    }
}
