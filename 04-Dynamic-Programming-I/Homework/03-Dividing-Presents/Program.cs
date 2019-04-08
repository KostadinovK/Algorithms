using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int[] presents = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int sum = presents.Sum();
        int[] sums = new int[sum + 1];

        sums[0] = 0;

        for (int i = 1; i < sums.Length; i++)
        {
            sums[i] = -1;
        }

        for (int presentIndex = 0; presentIndex < presents.Length; presentIndex++)
        {
            for (int j = sum - presents[presentIndex]; j >= 0; j--)
            {
                var presentValue = presents[presentIndex];

                if (sums[j] != -1 && sums[j + presentValue] == -1)
                {
                    sums[j + presentValue] = presentIndex;
                }
            }
        }

        var halfSum = sum / 2;

        for (int i = halfSum; i >= 0; i--)
        {
            if (sums[i] == -1)
            {
                continue;
            }

            Console.WriteLine($"Difference: {Math.Abs(sum - i - i)}");
            Console.WriteLine($"Alan:{i} Bob:{sum - i}");
            Console.Write("Alan takes: ");
            while (i > 0)
            {
                Console.Write(presents[sums[i]] + " ");
                i = i - presents[sums[i]];
            }
            Console.WriteLine();
            Console.WriteLine("Bob takes the rest.");
        }
    }
}
