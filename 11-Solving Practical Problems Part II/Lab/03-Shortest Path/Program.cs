using System;
using System.Collections.Generic;
using System.Text;

public class Program
{
    private static char[] path;
    private static char[] combination;
    private static int result;
    private static StringBuilder variations;
    public static void Main()
    {
        path = Console.ReadLine().ToCharArray();
        combination = new char[path.Length];

        result = 0;
        variations = new StringBuilder();

        GenVars(0);

        Console.WriteLine(result);
        Console.WriteLine(variations.ToString().Trim());
    }

    private static void GenVars(int index)
    {
        if (index == combination.Length)
        {
            result++;
            variations.AppendLine(new string(combination));
            return;
        }

        if (path[index] == '*')
        {
            combination[index] = 'L';
            GenVars(index + 1);
            combination[index] = 'R';
            GenVars(index + 1);
            combination[index] = 'S';
            GenVars(index + 1);
        }
        else
        {
            combination[index] = path[index];
            GenVars(index + 1);
        }
    }
}
