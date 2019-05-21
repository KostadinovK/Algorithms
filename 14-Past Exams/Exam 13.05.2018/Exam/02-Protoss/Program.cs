using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int zealotsAmount = int.Parse(Console.ReadLine());
        string[] zealots = new string[zealotsAmount];

        for (int i = 0; i < zealotsAmount; i++)
        {
            string zealotConnections = Console.ReadLine();
            zealots[i] = zealotConnections;
        }

        int maxConnections = 0;

        foreach (var zealot in zealots)
        {
            HashSet<int> connections = new HashSet<int>();

            for (int i = 0; i < zealot.Length; i++)
            {
                if (zealot[i] == 'Y')
                {
                    connections.Add(i);
                }
            }

            foreach (var conn in connections.ToArray())
            {
                for (int i = 0; i < zealots[conn].Length; i++)
                {
                    if (zealots[conn][i] == 'Y')
                    {
                        connections.Add(i);
                    }
                }
            }

            maxConnections = Math.Max(maxConnections, connections.Count - 1);
        }


        Console.WriteLine(maxConnections);
    }
}
