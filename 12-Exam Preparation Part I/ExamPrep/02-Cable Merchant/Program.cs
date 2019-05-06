using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int[] cables = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        int connectorPrice = int.Parse(Console.ReadLine());
        int connectPrice = connectorPrice * 2;

        for (int i = 1; i < cables.Length; i++)
        {
            int splits = (i + 1) / 2;

            int start = 0;
            int end = i - 1;
            while (splits > 0)
            {
                int otherPrice = cables[start++] + cables[end--] - connectPrice;
                cables[i] = Math.Max(cables[i], otherPrice);
                splits--;
            }
        }

        Console.WriteLine(string.Join(" ", cables));
    }
}
