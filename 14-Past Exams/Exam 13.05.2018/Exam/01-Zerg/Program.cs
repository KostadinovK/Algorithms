using System;
using System.Linq;
using System.Numerics;

public class Program
{
    private static BigInteger[,] matrix;
    public static void Main()
    {
        int[] matrixSize = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int[] mainBaseLocation = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int rows = matrixSize[0];
        int cols = matrixSize[1];
        int mainBaseRow = mainBaseLocation[0];
        int mainBaseCol = mainBaseLocation[1];

        matrix = new BigInteger[rows, cols];

        matrix[0, 0] = 1;

        int enemies = int.Parse(Console.ReadLine());
        for (int i = 0; i < enemies; i++)
        {
            int[] coords = Console.ReadLine().Split().Select(int.Parse).ToArray();
            matrix[coords[0], coords[1]] = -1;
        }

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                if (matrix[row, col] == -1)
                {
                    continue;
                }

                if (IsValidCell(row, col - 1) && matrix[row, col - 1] > 0)
                {
                    matrix[row, col] += matrix[row, col - 1];
                }

                if (IsValidCell(row - 1, col) && matrix[row - 1, col] > 0)
                {
                    matrix[row, col] += matrix[row - 1, col];
                }
            }
        }

        Console.WriteLine(matrix[mainBaseRow, mainBaseCol]);
    }

    private static bool IsValidCell(int row, int col)
    {
        return row >= 0 && row < matrix.GetLength(0) && col >= 0 && col < matrix.GetLength(1);
    }
}
