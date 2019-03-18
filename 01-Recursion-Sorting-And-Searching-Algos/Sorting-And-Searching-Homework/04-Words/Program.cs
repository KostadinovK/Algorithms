using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        List<char> letters = Console.ReadLine().ToCharArray().ToList();

        int distinctCount = letters.Distinct().ToList().Count;


        Dictionary<char, int> occurrences = new Dictionary<char, int>();

        foreach (var letter in letters)
        {
            if (!occurrences.ContainsKey(letter))
            {
                occurrences[letter] = 0;
            }

            occurrences[letter]++;
        }

        int maxOccurrences = occurrences.Values.Max();

        if (maxOccurrences >= distinctCount)
        {
            Console.WriteLine(0);
        }
    }

}
