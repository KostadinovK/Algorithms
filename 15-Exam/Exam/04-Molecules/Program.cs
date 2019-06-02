using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static List<Edge>[] graph;
    private static int[] costTo;
    private static bool[] visited;
    public static void Main()
    {
        int molecules = int.Parse(Console.ReadLine());
        int connections = int.Parse(Console.ReadLine());

        graph = new List<Edge>[molecules + 1];

        for (int i = 1; i <= molecules; i++)
        {
            graph[i] = new List<Edge>();
        }

        for (int i = 1; i <= connections; i++)
        {
            int[] connectionArgs = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int from = connectionArgs[0];
            int to = connectionArgs[1];
            int cost = connectionArgs[2];

            graph[from].Add(new Edge(from, to, cost));
        }

        int[] destinations = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int start = destinations[0];
        int end = destinations[1];

        costTo = new int[molecules + 1];
        visited = new bool[molecules + 1];

        for (int i = 0; i < costTo.Length; i++)
        {
            costTo[i] = int.MaxValue;
        }

        Dijkstra(start, end);
        Console.WriteLine(costTo[end]);
        List<int> unvisitedMolecules = new List<int>();
        for (int i = 1; i < costTo.Length; i++)
        {
            if (costTo[i] == int.MaxValue)
            {
                unvisitedMolecules.Add(i);
            }
        }
        Console.WriteLine(string.Join(" ", unvisitedMolecules));
    }

    private static void Dijkstra(int start, int end)
    {
        costTo[start] = 0;

        while (true)
        {
            int vertex = GetVertex();

            if (vertex == -1 || vertex == end)
            {
                break;
            }

            Visit(vertex);
        }
    }



    private static int GetVertex()
    {
        int minCostIndex = -1;
        int minCost = int.MaxValue;

        for (int i = 0; i < costTo.Length; i++)
        {
            if (costTo[i] < minCost && !visited[i])
            {
                minCost = costTo[i];
                minCostIndex = i;
            }
        }

        return minCostIndex;
    }

    private static void Visit(int vertex)
    {
        visited[vertex] = true;
        foreach (var edge in graph[vertex])
        {
            int cost = costTo[vertex] + edge.Cost;

            if (costTo[edge.To] > cost)
            {
                costTo[edge.To] = cost;
            }
        }
    }
}

public class Edge
{
    public int From { get; set; }
    public int To { get; set; }
    public int Cost { get; set; }

    public Edge(int from, int to, int cost)
    {
        From = from;
        To = to;
        Cost = cost;
    }
}
