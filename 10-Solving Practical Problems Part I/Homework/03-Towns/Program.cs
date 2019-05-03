using System;
using System.Linq;

class Program
{
    private static int[] towns;
    private static int[] lis;
    private static int[] lds;
    public static void Main()
    {
        int townsCount = int.Parse(Console.ReadLine());
        towns = new int[townsCount];
        lis = new int[townsCount];
        lis[0] = 1;
        lds = new int[towns.Length];
        lds[0] = 1;
        for (int i = 0; i < townsCount; i++)
        {
            int peoples = int.Parse(Console.ReadLine().Split(' ').ToArray()[0]);
            towns[i] = peoples;
        }

        PerformLongestIncreasingSubsequence(lis);
        Array.Reverse(towns);
        PerformLongestIncreasingSubsequence(lds);
        Array.Reverse(lds);

        for (int i = 0; i < townsCount; i++)
        {
            lis[i] += lds[i] - 1;
        }

        Console.WriteLine(lis.Max());
    }

    private static void PerformLongestIncreasingSubsequence(int[] arr)
    {
        for (int currentTownIndex = 1; currentTownIndex < towns.Length; currentTownIndex++)
        {
            int maxLength = 1;
            int currentTown = towns[currentTownIndex];
            for (int prevTownIndex = currentTownIndex - 1; prevTownIndex >= 0; prevTownIndex--)
            {
                if (currentTown > towns[prevTownIndex])
                {
                    if (arr[prevTownIndex] + 1 > maxLength)
                    {
                        maxLength = arr[prevTownIndex] + 1;
                    }
                }
            }

            arr[currentTownIndex] = maxLength;
        }
    }
}
