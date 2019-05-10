using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    public static void Main()
    {
        string[] nums = Console.ReadLine()
            .Split()
            .ToArray();

        int lastBridgeIndex = 0;
        List<int> bridgeIndexes = new List<int>();

        for (int i = 0; i < nums.Length; i++)
        {
            for (int j = lastBridgeIndex; j < i; j++)
            {
                string start = nums[j];
                string end = nums[i];

                if (start == end)
                {
                    lastBridgeIndex = i;
                    bridgeIndexes.Add(i);
                    bridgeIndexes.Add(j);
                }
            }    
        }

        string[] result = Enumerable.Repeat("X", nums.Length).ToArray();
        for (int i = 0; i < result.Length; i++)
        {
            if (bridgeIndexes.Contains(i))
            {
                result[i] = nums[i];
            }
        }

        if (bridgeIndexes.Count == 0)
        {
            Console.WriteLine("No bridges found");
        }else if (bridgeIndexes.Count == 2)
        {
            Console.WriteLine("1 bridge found");
        }
        else
        {
            Console.WriteLine($"{bridgeIndexes.Count / 2} bridges found");
        }

        Console.WriteLine(string.Join(" ", result));
    }
}
