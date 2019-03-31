using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int[] len = new int[nums.Length];
        int[] index = new int[nums.Length];

        int maxLength = Int32.MinValue;
        int indexOfMaxLength = -1;

        for (int i = 0; i < nums.Length; i++)
        {
            int currentNum = nums[i];
            int bestLength = 1;
            int prevIndex = -1;

            for (int j = 0; j < i; j++)
            {
                int prevNum = nums[j];
                int prevNumLength = len[j];

                if (prevNum < currentNum && bestLength <= prevNumLength)
                {
                    bestLength = prevNumLength + 1;
                    prevIndex = j;
                }
            }

            len[i] = bestLength;
            index[i] = prevIndex;

            if (bestLength > maxLength)
            {
                maxLength = bestLength;
                indexOfMaxLength = i;
            }
        }

        List<int> sequence = new List<int>();

        while (indexOfMaxLength != -1)
        {
            sequence.Add(nums[indexOfMaxLength]);
            indexOfMaxLength = index[indexOfMaxLength];
        }

        sequence.Reverse();

        Console.WriteLine(string.Join(" ", sequence));
    }
}
