using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

public class Program
{
    private static Dictionary<int, HashSet<int>> graph;
    private static Dictionary<int, HashSet<int>> splittedGraph;

    public static void Main()
    {
        int[] line = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int areas = line[0];
        int connections = line[1];
        int start = line[2];

        graph = new Dictionary<int, HashSet<int>>();
        splittedGraph = new Dictionary<int, HashSet<int>>();

        for (int i = 1; i <= areas; i++)
        {
            graph[i] = new HashSet<int>();
            splittedGraph[i] = new HashSet<int>();
        }

        for (int i = 0; i < connections; i++)
        {
            int[] connection = Console.ReadLine().Split().Select(int.Parse).ToArray();

            graph[connection[0]].Add(connection[1]);
            graph[connection[1]].Add(connection[0]);

            splittedGraph[connection[0]].Add(connection[1]);
            splittedGraph[connection[1]].Add(connection[0]);
        }

        var visited = new HashSet<int>();

        DFS(start, start, visited);
        Console.WriteLine(GetMax());
    }

    private static int DFS(int node, int root, HashSet<int> visited)
    {
        visited.Add(node);
        int childCount = 0;
        foreach (var child in graph[node])
        {
            if (!visited.Contains(child) && child != root)
            {

                int subtreeLength = DFS(child, node, visited);

                if (subtreeLength % 2 == 0)
                {
                    splittedGraph[node].Remove(child);
                    splittedGraph[child].Remove(node);
                }

                childCount += subtreeLength;
            }
        }

        return childCount + 1;
    }

    private static int GetMax()
    {
        int max = 0;
        var visited = new HashSet<int>();
        foreach (var area in splittedGraph.Keys)
        {
            if (!visited.Contains(area))
            {
                int value = GetValue(area, visited);

                if (value > max)
                {
                    max = value;
                }
            }
        }

        return max;
    }

    private static int GetValue(int root, HashSet<int> visited)
    {
        visited.Add(root);
        int value = 0;
        foreach (var child in splittedGraph[root])
        {
            if (!visited.Contains(child))
            {
                value += GetValue(child, visited);
            }
        }

        return value + root;
    }
}
