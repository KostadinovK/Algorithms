using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int[] nums = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int a = nums[0];
        int b = nums[1];
        int c = nums[2];

        int A = a * -1;
        int B = b * -1;
        int C = c * -1;

        if (a + b + c == 13 || A + b + c == 13 || a + B + c == 13 || a + b + C == 13 || A + B + c == 13 ||
            a + B + C == 13 || A + b + C == 13 || A + B + C == 13)
        {
            Console.WriteLine("Yes");
        }
        else
        {
            Console.WriteLine("No");
        }
    }
}
