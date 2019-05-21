using System;
using System.Collections.Generic;
using System.Numerics;

public class Program
{
    private static string marines;
    private static BigInteger[] mem;
    public static void Main()
    {
        marines = Console.ReadLine();
        mem = new BigInteger[marines.Length + 1];

        BigInteger nFact = CalcFactorial(marines.Length);

        Dictionary<char, int> colorCounts = new Dictionary<char, int>();

        for (int i = 0;i < marines.Length; i++)
        {
            if (!colorCounts.ContainsKey(marines[i]))
            {
                colorCounts[marines[i]] = 0;
            }

            colorCounts[marines[i]]++;
        }

        BigInteger multiplyResult = 1;
        foreach (var kvp in colorCounts)
        {
            BigInteger fact = CalcFactorial(kvp.Value);
            multiplyResult *= fact;
        }

        Console.WriteLine(nFact / multiplyResult);
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
