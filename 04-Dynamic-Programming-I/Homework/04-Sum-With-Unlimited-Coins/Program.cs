using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int[] coins = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int sum = int.Parse(Console.ReadLine());

        int[,] sums = new int[coins.Length,sum + 1];

        for (int i = 0; i < coins.Length; i++)
        {
            sums[i, 0] = 1;
        }

        for (int currentCoinIndex = 1; currentCoinIndex < coins.Length; currentCoinIndex++)
        {
            int currentCoin = coins[currentCoinIndex - 1];
            for (int currentSum = 1; currentSum < sums.Length; currentSum++)
            {
                if (currentCoin <= currentSum)
                {
                    sums[currentCoinIndex, currentSum] = sums[currentCoinIndex - 1, currentSum] + sums[currentCoinIndex, currentSum - coins[currentCoinIndex - 1]];
                }
                else
                {
                    sums[currentCoinIndex, currentSum] = sums[currentCoinIndex - 1, currentSum];
                }
            }
        }

        Console.WriteLine(sums[coins.Length, sum]);
    }
}
