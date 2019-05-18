using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var firstStr = Console.ReadLine();
        var secondStr = Console.ReadLine();

        int[,] matrix = new int[firstStr.Length + 1, secondStr.Length + 1];

        int maxSubstring = 0;
        int maxSubstringRow = 0;
        int maxSubstringCol = 0;

        for (int row = 1; row < matrix.GetLength(0); row++)
        {
            for (int col = 1; col < matrix.GetLength(1); col++)
            {
                if (firstStr[row - 1] == secondStr[col - 1])
                {
                    matrix[row, col] = matrix[row - 1, col - 1] + 1;
                    if (matrix[row, col] > maxSubstring)
                    {
                        maxSubstring = matrix[row, col];
                        maxSubstringRow = row;
                        maxSubstringCol = col;
                    }
                }
                else
                {
                    matrix[row, col] = 0;
                }
            }
        }

        Stack<char> result = new Stack<char>();

        while (matrix[maxSubstringRow, maxSubstringCol] != 0)
        {
            result.Push(firstStr[maxSubstringRow - 1]);
            maxSubstringRow--;
            maxSubstringCol--;
        }

        Console.WriteLine(string.Join("", result));
    }
}
