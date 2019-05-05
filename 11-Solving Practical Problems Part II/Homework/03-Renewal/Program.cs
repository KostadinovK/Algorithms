using System;
using System.Linq;

public class Program
{
    private static int n;
    private static int[,] paths;
    private static int[,] buildPrices;
    private static int[,] destroyPrices;

    public static void Main()
    {
        n = int.Parse(Console.ReadLine());
        paths = new int[n,n];
        buildPrices = new int[n,n];
        destroyPrices = new int[n,n];

        ReadInput();


    }

    private static void ReadInput()
    {
        for (int row = 0; row < n; row++)
        {
            string rowData = Console.ReadLine();
            for (int col = 0; col < n; col++)
            {
                paths[row, col] = rowData[col] - '0';
            }
        }

        for (int row = 0; row < n; row++)
        {
            string rowData = Console.ReadLine();
            for (int col = 0; col < n; col++)
            {
                if (char.IsLower(rowData[col]))
                {
                    buildPrices[row, col] = rowData[col] - 'G';
                }
                else
                {
                    buildPrices[row, col] = rowData[col] - 'A';
                }
            }
        }

        for (int row = 0; row < n; row++)
        {
            string rowData = Console.ReadLine();
            for (int col = 0; col < n; col++)
            {
                if (char.IsLower(rowData[col]))
                {
                    destroyPrices[row, col] = rowData[col] - 'G';
                }
                else
                {
                    destroyPrices[row, col] = rowData[col] - 'A';
                }
            }
        }
    }
}
