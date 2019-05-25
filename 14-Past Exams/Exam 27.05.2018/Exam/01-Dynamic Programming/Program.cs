using System;
using System.Numerics;

public class Program
{
    private static bool[,] matrix;
    private static BigInteger[] cache;
    public static void Main()
    {
        int c = int.Parse(Console.ReadLine());
        matrix = new bool[c, c];

        for (int row = 0; row < c; row++)
        {
            string data = Console.ReadLine();
            for (int col = 0; col < c; col++)
            {
                matrix[row, col] = data[col] == 'R';
            }
        }

        cache = new BigInteger[c];

        BigInteger sum = 0;
        for (int row = 0; row < c; row++)
        {
            sum += GetMoney(row);
        }

        Console.WriteLine($"${sum:f2}");
    }

    private static BigInteger GetMoney(int row)
    {

        if (cache[row] > 0)
        {
            return cache[row];
        }

        BigInteger money = 0;
        int referrals = 0;

        for (int i = 0; i < matrix.GetLength(1); i++)
        {
            if (matrix[row, i])
            {
                referrals++;
                money += GetMoney(i);
            }
        }

        money *= referrals;

        if (money == 0)
        {
            money = 1;
        }

        cache[row] = money;
        return money;
    }
}
