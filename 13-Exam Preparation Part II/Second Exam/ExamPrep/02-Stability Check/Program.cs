using System;
using System.Linq;

public class Program
{
    private static int buildingSize;
    private static int n;
    private static long[,] matrix;
    public static void Main()
    {
        buildingSize = int.Parse(Console.ReadLine());
        n = int.Parse(Console.ReadLine());
        matrix = new long[n, n];

        for (int row = 0; row < n; row++)
        {
            long[] rowData = Console.ReadLine().Split().Select(long.Parse).ToArray();
            for (int col = 0; col < n; col++)
            {
                matrix[row, col] = rowData[col];
            }
        }

        long result = GetMaxBuildingSum();

        Console.WriteLine(result);
    }

    public static long GetMaxBuildingSum()
    {
        long maxSum = long.MinValue;
        long[,] stripSum = new long[n, n]; 
  
        for (int col = 0; col < n; col++) 
        { 
            long sum = 0; 
            for (int row = 0; row < buildingSize; row++)
            {
                sum += matrix[row, col];
            }
                
            stripSum[0, col] = sum; 
  
            for (int row = 1; row < n - buildingSize + 1; row++) 
            { 
                sum += matrix[row + buildingSize - 1, col] - matrix[row - 1, col]; 
                stripSum[row, col] = sum; 
            } 
        } 
  
        for (int i = 0; i < n - buildingSize + 1; i++) 
        { 
            long sum = 0; 
            for (int j = 0; j < buildingSize; j++)
            {
                sum += stripSum[i, j];
            }
  
            if (sum > maxSum) 
            { 
                maxSum = sum; 
            } 
  
            for (int j = 1; j < n - buildingSize + 1; j++) 
            { 
                sum += stripSum[i, j + buildingSize - 1] - stripSum[i, j - 1]; 
  
                if (sum > maxSum) 
                { 
                    maxSum = sum; 
                } 
            } 
        }

        return maxSum;
    } 
}
