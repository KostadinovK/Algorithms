using System;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int[] answers = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
        answers = answers.Where(a => a >= 0).ToArray();

        
    }
}

