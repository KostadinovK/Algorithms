using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static Dictionary<int, HashSet<int>> sticks;
    private static Dictionary<int, HashSet<int>> reversedSticks;
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());
        sticks = new Dictionary<int, HashSet<int>>();
        reversedSticks = new Dictionary<int, HashSet<int>>();
        for (int i = 0; i < n; i++)
        {
            sticks[i] = new HashSet<int>();
            reversedSticks[i] = new HashSet<int>();
        }

        int placings = int.Parse(Console.ReadLine());

        for (int i = 0; i < placings; i++)
        {
            int[] pair = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            sticks[pair[0]].Add(pair[1]);
            reversedSticks[pair[1]].Add(pair[0]);
        }

        List<int> result = new List<int>();
        
        while (true)
        {
            var stickToRemove = reversedSticks.Where(s => s.Value.Count == 0).OrderByDescending(s => s.Key).FirstOrDefault();

            if (stickToRemove.Value == null)
            {
                break;
            }

            result.Add(stickToRemove.Key);
            reversedSticks.Remove(stickToRemove.Key);
            foreach (var stick in sticks[stickToRemove.Key])
            {
                reversedSticks[stick].Remove(stickToRemove.Key);
            }
        }

        if (result.Count != n)
        {
            Console.WriteLine("Cannot lift all sticks");
        }

        Console.WriteLine(string.Join(" ", result));
        
    }
}
