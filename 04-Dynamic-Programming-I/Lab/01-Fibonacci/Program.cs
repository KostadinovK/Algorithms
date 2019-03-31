using System;

public class Program
{
    private static long[] nums;
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        nums = new long[n + 1];
        long result = Fibonacci(n);

        Console.WriteLine(result);
    }

    private static long Fibonacci(int n)
    {
        if (nums[n] != 0)
        {
            return nums[n];
        }

        if (n == 0)
        {
            return 0;
        }

        if (n == 1)
        {
            return 1;
        }

        var res = Fibonacci(n - 1) + Fibonacci(n - 2);
        nums[n] = res;
        return res;
    }
}

