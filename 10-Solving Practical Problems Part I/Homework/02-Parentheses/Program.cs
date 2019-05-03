using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

public class Program
{
    private static int n;
    private static char[] brackets;
    private static StringBuilder result = new StringBuilder();

    public static void Main()
    {
        n = int.Parse(Console.ReadLine());
        brackets = new char[n * 2];

        int openBracketsCount = 1;
        int closedBracketsCount = 0;
        brackets[0] = '(';

        Generate(1, openBracketsCount, closedBracketsCount);
        Console.WriteLine(result.ToString().Trim());
    }

    private static void Generate(int index, int openBracketsCount, int closedBracketsCount)
    {
        if (index == brackets.Length)
        {
            result.AppendLine(new string(brackets));
            return;
        }

        if (openBracketsCount != n)
        {
            brackets[index] = '(';
            Generate(index + 1, openBracketsCount + 1, closedBracketsCount);
        }

        if (closedBracketsCount < openBracketsCount)
        {
            brackets[index] = ')';
            Generate(index + 1, openBracketsCount, closedBracketsCount + 1);
        }
    }
}
