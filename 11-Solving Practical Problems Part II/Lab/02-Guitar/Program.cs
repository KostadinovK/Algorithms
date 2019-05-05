using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var intervals = Console.ReadLine()
            .Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        var initialVolume = int.Parse(Console.ReadLine());
        var maxVolume = int.Parse(Console.ReadLine());

        bool[,] volumes = new bool[intervals.Length + 1, maxVolume + 1];
        volumes[0, initialVolume] = true;

        for (int row = 1; row < volumes.GetLength(0); row++)
        {
            for (int col = 0; col < volumes.GetLength(1); col++)
            {
                if (volumes[row - 1, col])
                {
                    if (col - intervals[row - 1] >= 0)
                    {
                        volumes[row, col - intervals[row - 1]] = true;
                    }

                    if (col + intervals[row - 1] <= maxVolume)
                    {
                        volumes[row, col + intervals[row - 1]] = true;
                    }
                }
            }
        }

        int result = -1;
        for (int col = volumes.GetLength(1) - 1; col >= 0; col--)
        {
            if (volumes[intervals.Length, col])
            {
                result = col;
                break;
            }
        }

        Console.WriteLine(result);
    }
}
