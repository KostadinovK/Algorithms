using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int[] coins = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int sum = int.Parse(Console.ReadLine());

        int[,] sums = new int[coins.Length + 1, sum + 1];

        for (int i = 0; i <= coins.Length; i++)
        {
            sums[i, 0] = 1;
        }

        for (int nextCoinIndex = 1; nextCoinIndex <= coins.Length; nextCoinIndex++)
        {
            int currentCoinIndex = nextCoinIndex - 1;
            for (int currentSum = 1; currentSum <= sum; currentSum++)
            {
                if (coins[currentCoinIndex] <= currentSum)
                {
                    sums[nextCoinIndex, currentSum] = sums[currentCoinIndex, currentSum] +
                                                      sums[nextCoinIndex, currentSum - coins[currentCoinIndex]];
                }
                else
                {
                    sums[nextCoinIndex, currentSum] = sums[currentCoinIndex, currentSum];
                }
            }
        }

        Console.WriteLine(sums[coins.Length, sum]);
    }
}
