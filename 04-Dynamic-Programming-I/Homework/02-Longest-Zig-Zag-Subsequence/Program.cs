using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int[,] lengths = new int[2, numbers.Length];
        int[,] prev = new int[2, numbers.Length];

        lengths[0, 0] = 1;
        lengths[1, 0] = 1;
        prev[0, 0] = -1;
        prev[1, 0] = -1;

        int maxLength = 0;
        int maxLengthIndexRow = 0;
        int maxLengthIndexCol = 0;

        for (int currentIndex = 1; currentIndex < numbers.Length; currentIndex++)
        {
            int currentNumber = numbers[currentIndex];
            for (int prevIndex = 0; prevIndex < currentIndex; prevIndex++)
            {
                int prevNumber = numbers[prevIndex];

                if (currentNumber > prevNumber && lengths[0, currentIndex] < lengths[1, prevIndex] + 1)
                {
                    lengths[0, currentIndex] = lengths[1, prevIndex] + 1;
                    prev[0, currentIndex] = prevIndex;
                }

                if (currentNumber < prevNumber && lengths[1, currentIndex] < lengths[0, prevIndex] + 1)
                {
                    lengths[1, currentIndex] = lengths[0, prevIndex] + 1;
                    prev[1, currentIndex] = prevIndex;
                }
            }

            if (maxLength < lengths[0, currentIndex])
            {
                maxLength = lengths[0, currentIndex];
                maxLengthIndexRow = 0;
                maxLengthIndexCol = currentIndex;
            }

            if (maxLength < lengths[1, currentIndex])
            {
                maxLength = lengths[1, currentIndex];
                maxLengthIndexRow = 1;
                maxLengthIndexCol = currentIndex;
            }
        }

        List<int> res = new List<int>();
       
        while (maxLengthIndexCol >= 0)
        {
            res.Add(numbers[maxLengthIndexCol]);
            maxLengthIndexCol = prev[maxLengthIndexRow, maxLengthIndexCol];
            if (maxLengthIndexRow == 0)
            {
                maxLengthIndexRow = 1;
            }
            else
            {
                maxLengthIndexRow = 0;
            }
        }

        res.Reverse();
        Console.WriteLine(string.Join(" ", res));
    }
}
