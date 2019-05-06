using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public class Program
{
    private static bool[] colors;
    private static List<Edge>[] graph;
    private static int[] distTo;
    private static int[] stepTo;
    private static bool[] visited;

    public static void Main()
    {
        string[] nodes = Console.ReadLine().Split().ToArray();

        colors = new bool[nodes.Length];

        for (int i = 0; i < nodes.Length; i++)
        {
            if (nodes[i][nodes[i].Length - 1] == 'w')
            {
                colors[i] = true;
            }
        }

        int energy = int.Parse(Console.ReadLine());
        int waitingCost = int.Parse(Console.ReadLine());
        int start = int.Parse(Console.ReadLine());
        int end = int.Parse(Console.ReadLine());
        int edges = int.Parse(Console.ReadLine());

        graph = new List<Edge>[nodes.Length];

        for (int i = 0; i < nodes.Length; i++)
        {
            graph[i] = new List<Edge>();
        }

        for (int i = 0; i < edges; i++)
        {
            int[] line = Console.ReadLine().Split().Select(int.Parse).ToArray();
            int from = line[0];
            int to = line[1];
            int distance = line[2];

            graph[from].Add(new Edge(from, to, distance));
        }

        distTo = new int[nodes.Length];
        stepTo = new int[nodes.Length];
        visited = new bool[nodes.Length];

        for (int i = 0; i < distTo.Length; i++)
        {
            distTo[i] = int.MaxValue;
        }

        Dijkstra(start, end, waitingCost);

        int remainingEnergy = energy - distTo[end];

        if (remainingEnergy >= 0)
        {
            Console.WriteLine(remainingEnergy);
        }
        else
        {
            Console.WriteLine($"Busted - need {Math.Abs(remainingEnergy)} more energy");
        }
    }

    private static void Dijkstra(int start, int end, int waitingCost)
    {
        distTo[start] = 0;

        while (true)
        {
            int vertex = GetVertex();

            if (vertex == -1 || vertex == end)
            {
                break;
            }

            Visit(vertex, waitingCost);
        }
    }

    private static int GetVertex()
    {
        int minDistIndex = -1;
        int minDist = int.MaxValue;

        for (int i = 0; i < distTo.Length; i++)
        {
            if (distTo[i] < minDist && !visited[i])
            {
                minDist = distTo[i];
                minDistIndex = i;
            }
        }

        return minDistIndex;
    }

    private static void Visit(int vertex, int waitingCost)
    {
        visited[vertex] = true;
        foreach (var edge in graph[vertex])
        {
            int steps = stepTo[vertex];
            bool color = steps % 2 == 0 ? colors[edge.To] : !colors[edge.To];

            int distance = distTo[vertex] + edge.Distance;

            if (!color)
            {
                distance += waitingCost;
            }

            if (distTo[edge.To] > distance)
            {
                distTo[edge.To] = distance;
                stepTo[edge.To] = steps + 1;

                if (!color)
                {
                    stepTo[edge.To]++;
                }
            }
        }
    }
}

public class Edge
{
    public int From { get; set; }
    public int To { get; set; }
    public int Distance { get; set; }

    public Edge(int from, int to, int distance)
    {
        From = from;
        To = to;
        Distance = distance;
    }
}
