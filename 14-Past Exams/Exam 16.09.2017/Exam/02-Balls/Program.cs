using System;
using System.Text;

public class Balls
{
    private static int pockets;
    private static int balls;
    private static int pocketSize;
    private static int[] res;
    private static StringBuilder sb;
    public static void Main()
    {
        pockets = int.Parse(Console.ReadLine());
        balls = int.Parse(Console.ReadLine());
        pocketSize = int.Parse(Console.ReadLine());

        res = new int[pockets];
        sb = new StringBuilder();

        Gen(0, balls);

        Console.WriteLine(sb.ToString().Trim());
    }

    private static void Gen(int index, int remainingBalls)
    {
        if (index == res.Length - 1)
        {
            if (remainingBalls <= pocketSize)
            {
                res[index] = remainingBalls;
                sb.AppendLine(string.Join(", ", res));
            }
        }
        else
        {
            int balls = Math.Min(pocketSize, remainingBalls - (res.Length - index - 1));

            for (int i = balls; i > 0; i--)
            {
                res[index] = i;
                Gen(index + 1, remainingBalls - i);
            }
        }
    }
}