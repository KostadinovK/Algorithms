using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    private static Dictionary<int, List<int>> graph;
    private static int[] distTo;
    private static bool[] visited;
    private static int start;
    public static void Main()
    {
        graph = new Dictionary<int, List<int>>();

        int junctions = int.Parse(Console.ReadLine());

        for (int i = 0; i < junctions; i++)
        {
            graph[i] = new List<int>();
        }

        int streets = int.Parse(Console.ReadLine());
        start = int.Parse(Console.ReadLine());

        for (int i = 0; i < streets; i++)
        {
            int[] edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
            graph[edge[0]].Add(edge[1]);
        }

        visited = new bool[junctions];
        distTo = new int[junctions];

        BFS(start);

        int min = int.MaxValue;

        for (int i = 0; i < graph.Count; i++)
        {
            if (visited[i] && graph[i].Contains(start))
            {
                if (distTo[i] + 1 < min)
                {
                    min = distTo[i] + 1;
                }
            }
        }

        if (min != int.MaxValue)
        {
            Console.WriteLine(min);
        }
        else
        {
            Console.WriteLine(visited.Count(x => x));
        }
    }

    private static void BFS(int node)
    {
        Queue<int> q = new Queue<int>();

        q.Enqueue(node);
        distTo[node] = 0;
        visited[node] = true;

        while (q.Count > 0)
        {
            int current = q.Dequeue();
            foreach (var child in graph[current])
            {
                if (!visited[child])
                {
                    q.Enqueue(child);
                    visited[child] = true;
                    distTo[child] = distTo[current] + 1;
                }
            }
        }
    }
}
