using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

public class Program
{
    private static string medenka;
    private static List<int> indices;
    private static int nuts;
    private static StringBuilder result;
    public static void Main()
    {
        medenka = Console.ReadLine().Replace(" ", "");

        indices = new List<int>();

        int start = medenka.IndexOf('1');
        int end = medenka.IndexOf('1', start + 1);

        nuts = 0;
        for (int i = 0; i < medenka.Length; i++)
        {
            if (medenka[i] == '1')
            {
                nuts++;
            }
        }
        result = new StringBuilder();

        GenBreaks(start, end, 0);

        Console.WriteLine(result.ToString().Trim());
    }

    private static void GenBreaks(int start, int end, int cuts)
    {
        if (cuts == nuts - 1)
        {
            InsertResultInStringBuilder();
            return;
        }

        for (int i = start; i < end; i++)
        {
            indices.Add(i);
            GenBreaks(end, medenka.IndexOf('1', end + 1), cuts + 1);
            indices.Remove(i);
        }
    }

    private static void InsertResultInStringBuilder()
    {
        for (int i = 0; i < medenka.Length; i++)
        {
            result.Append(medenka[i]);
            if (indices.Contains(i))
            {
                result.Append('|');
            }
        }

        result.AppendLine();
    }
}
