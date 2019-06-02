using System;
using System.Numerics;

public class Program
{
    private static BigInteger[] mem;
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        int k = int.Parse(Console.ReadLine());
        mem = new BigInteger[n + 1];

        BigInteger nFact = CalcFactorial(n);
        BigInteger kFact = CalcFactorial(k);

        BigInteger result = nFact / (kFact * CalcFactorial(n - k));
        Console.WriteLine(result);
    }

    private static BigInteger CalcFactorial(int n)
    {
        if (mem[n] != 0)
        {
            return mem[n];
        }

        if (n == 1 || n == 0)
        {
            return 1;
        }

        BigInteger number = n * CalcFactorial(n - 1);

        mem[n] = number;

        return number;
    }
}
