using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    private static int[] result;
    private static StringBuilder sb;
    public static void Main()
    {
        int sum = int.Parse(Console.ReadLine());
        result = new int[sum];
        sb = new StringBuilder();

        GenSums(0, 0, sum);

        Console.WriteLine(sb.ToString().Trim());
    }

    private static void GenSums(int index, int currSum, int totalSum)
    {
        if (currSum <= totalSum && currSum != 0)
        {
            sb.AppendLine(string.Join(" ", result.Where(x => x != 0)));
        }

        if (currSum > totalSum || index == result.Length)
        {
            return;
        }

        for (int i = 1; i <= totalSum; i++)
        {
            result[index] = i;
            GenSums(index + 1, currSum + i, totalSum);
            result[index] = 0;
        }
    }
}
