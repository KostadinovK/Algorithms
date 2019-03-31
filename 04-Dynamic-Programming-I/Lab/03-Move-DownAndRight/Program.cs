using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int rows = int.Parse(Console.ReadLine());
        int cols = int.Parse(Console.ReadLine());

        int[,] matrix = new int[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            int[] rowData = Console.ReadLine().Split().Select(int.Parse).ToArray();
            for (int col = 0; col < cols; col++)
            {
                matrix[row, col] = rowData[col];
            }
        }

        for (int row = 1; row < rows; row++)
        {
            matrix[row, 0] += matrix[row - 1, 0];
        }

        for (int col = 1; col < cols; col++)
        {
            matrix[0, col] += matrix[0, col - 1];
        }

        

        for (int row = 1; row < rows; row++)
        {
            for (int col = 1; col < cols; col++)
            {
                int upValue = matrix[row - 1, col];
                int leftValue = matrix[row, col - 1];

                matrix[row, col] += Math.Max(upValue, leftValue);
            }

        }
        List<string> path = new List<string>();

        int endRow = rows - 1;
        int endCol = cols - 1;
        while (endRow != 0 || endCol != 0)
        {
            path.Add($"[{endRow}, {endCol}]");

            if (endRow > 0 && endCol > 0)
            {
                if (matrix[endRow - 1, endCol] > matrix[endRow, endCol - 1])
                {
                    endRow--;
                }
                else
                {
                    endCol--;
                }
            }
            else
            {
                if (endRow == 0)
                {
                    endCol--;
                }else if (endCol == 0)
                {
                    endRow--;
                }
            }

        }
        path.Add("[0, 0]");
        path.Reverse();
        Console.WriteLine(string.Join(" ", path));
    }
}
