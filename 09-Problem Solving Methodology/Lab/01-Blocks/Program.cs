using System;
using System.Collections.Generic;

public class Program
{
    private static char[] letters;
    private static char[] block = new char[4];

    private static HashSet<string> usedBlocks = new HashSet<string>();
    private static List<string> result = new List<string>();
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        letters = new char[n];
        for (int i = 0; i < n; i++)
        {
            letters[i] = (char)('A' + i);
        }

        bool[] used = new bool[letters.Length];

        GenerateVariations(0, used);

        Console.WriteLine("Number of blocks: " + result.Count);
        Console.WriteLine(string.Join(Environment.NewLine, result));
    }

    private static void GenerateVariations(int index, bool[] used)
    {
        if (index == block.Length)
        {
            if (!usedBlocks.Contains(new string(block)))
            {
                AddRotationsToSet(block);
                result.Add(new string(block));
            }
            return;
        }

        for (int i = 0; i < letters.Length; i++)
        {
            if (!used[i])
            {
                used[i] = true;
                block[index] = letters[i];
                GenerateVariations(index + 1, used);
                used[i] = false;
            }
        }
    }

    private static void AddRotationsToSet(char[] block)
    {
        usedBlocks.Add(new string(block));
        usedBlocks.Add(new string(new[] {block[2], block[0], block[3], block[1]}));
        usedBlocks.Add(new string(new[] {block[3], block[2], block[1], block[0]}));
        usedBlocks.Add(new string(new[] {block[1], block[3], block[0], block[2]}));
    }
}
