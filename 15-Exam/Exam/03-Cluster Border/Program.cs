using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    private static int[] dp;
    private static Stack<string> res;
    public static void Main()
    {
        int[] singleTimes = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int[] pairTimes = Console.ReadLine().Split().Select(int.Parse).ToArray();
        dp = new int[singleTimes.Length + 1];
        res = new Stack<string>();

        dp[1] = singleTimes[0];
        for (int i = 2; i < dp.Length; i++)
        {
            int whenSingle = dp[i - 1] + singleTimes[i - 1];
            int whenInPair = dp[i - 2] + pairTimes[i - 2];
            dp[i] = Math.Min(whenSingle, whenInPair);
        }

        int ships = singleTimes.Length;
        int optimalTime = 0;

        while (ships > 0)
        {
            if (dp[ships - 1] + singleTimes[ships - 1] == dp[ships])
            {
                optimalTime += singleTimes[ships - 1];
                res.Push($"Single {ships}");
                ships--;
            }
            else
            {
                optimalTime += pairTimes[ships - 2];
                res.Push($"Pair of {ships - 1} and {ships}");
                ships -= 2;
            }
        }

        Console.WriteLine($"Optimal Time: {optimalTime}");
        Console.WriteLine(string.Join(Environment.NewLine, res));
    }
}
