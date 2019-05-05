using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static int n;
    private static char[,,] cube;
    public static void Main()
    {
        n = int.Parse(Console.ReadLine());
        cube = new char[n,n,n];

        ReadCube();

        Dictionary<char, int> stars = new Dictionary<char, int>();

        for (int dimension = 1; dimension < n - 1; dimension++)
        {
            for (int row = 1; row < n - 1; row++)
            {
                for (int col = 1; col < n - 1; col++)
                {
                    char letter = cube[dimension, row, col];

                    bool hasStar = cube[dimension + 1, row, col] == letter
                                   && cube[dimension - 1, row, col] == letter
                                   && cube[dimension, row - 1, col] == letter
                                   && cube[dimension, row + 1, col] == letter
                                   && cube[dimension, row, col - 1] == letter
                                   && cube[dimension, row, col + 1] == letter;

                    if (hasStar)
                    {
                        if (!stars.ContainsKey(letter))
                        {
                            stars[letter] = 0;
                        }

                        stars[letter]++;
                    }

                }
            }
        }

        Console.WriteLine(stars.Values.Sum());
        foreach (var kvp in stars.OrderBy(s => s.Key))
        {
            Console.WriteLine($"{kvp.Key} -> {kvp.Value}");
        }
    }

    private static void ReadCube()
    {
        for (int row = 0; row < n; row++)
        {
            string[] dimensions = Console.ReadLine().Split('|').ToArray();
            for (int dimension = 0; dimension < n; dimension++)
            {
                string[] dimensionRowData =
                    dimensions[dimension].Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries).ToArray();

                for (int col = 0; col < n; col++)
                {
                    cube[dimension, row, col] = dimensionRowData[col][0];
                }
            }
        }
    }
}
