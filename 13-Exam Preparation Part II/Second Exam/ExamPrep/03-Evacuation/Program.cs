using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    private static List<Edge>[] graph;
    private static long[] distTo;
    private static bool[] visited;
    public static void Main()
    {
        int rooms = int.Parse(Console.ReadLine());
        graph = new List<Edge>[rooms];
        for (int i = 0; i < rooms; i++)
        {
            graph[i] = new List<Edge>();
        }

        List<int> exits = Console.ReadLine().Split().Select(int.Parse).ToList();
        int edges = int.Parse(Console.ReadLine());

        for (int i = 0; i < edges; i++)
        {
            string[] edgeInfo = Console.ReadLine().Split();
            int[] timeInfo = edgeInfo[2].Split(':').Select(int.Parse).ToArray();
            int seconds = timeInfo[0] * 60 + timeInfo[1];

            Edge edge = new Edge(int.Parse(edgeInfo[0]), int.Parse(edgeInfo[1]), seconds);
            graph[int.Parse(edgeInfo[0])].Add(edge);
            graph[int.Parse(edgeInfo[1])].Add(edge);
        }

        int[] time = Console.ReadLine().Split(':').Select(int.Parse).ToArray();

        Dictionary<int, long> times = new Dictionary<int, long>();

        for (int room = 0; room < graph.Length; room++)
        {
            long shortestTime = long.MaxValue;

            for (int exitIndex = 0; exitIndex < exits.Count; exitIndex++)
            {
                int exit = exits[exitIndex];
                if (!exits.Contains(room))
                {
                    visited = new bool[rooms];
                    distTo = new long[rooms];

                    for (int i = 0; i < distTo.Length; i++)
                    {
                        distTo[i] = long.MaxValue;
                    }

                    long timeToExit = Dijkstra(room, exit);

                    if (timeToExit < shortestTime)
                    {
                        shortestTime = timeToExit;
                    }
                }
            }

            if (!exits.Contains(room))
            {
                times[room] = shortestTime;
            }
        }

        long maxSeconds = time[0] * 60 + time[1];

        if (times.Values.Count(t => t > maxSeconds) == 0)
        {
            Console.WriteLine("Safe");
            var longestTimeKeyValuePair = times.OrderByDescending(x => x.Value).ThenBy(x => x.Key).First();
            Console.WriteLine($"{longestTimeKeyValuePair.Key} ({GetTime(longestTimeKeyValuePair.Value)})");
        }
        else
        {
            Console.WriteLine("Unsafe");
            List<string> result = new List<string>();
            foreach (var kvp in times.Where(x => x.Value > maxSeconds).OrderBy(x => x.Key))
            {
                if (kvp.Value == long.MaxValue)
                {
                    result.Add($"{kvp.Key} (unreachable)");
                }
                else
                {
                    result.Add($"{kvp.Key} ({GetTime(kvp.Value)})");
                }
            }


            Console.WriteLine(string.Join(", ", result));
        }

        
    }

    private static string GetTime(long seconds)
    {
        int minutes = (int) seconds / 60;
        int newSeconds = (int) seconds % 60;
        int hours = minutes / 60;
        if (hours > 0)
        {
            minutes = minutes % 60;
        }
        StringBuilder res = new StringBuilder();

        res.Append(hours < 10 ? $"0{hours}:" : $"{hours}:");
        res.Append(minutes < 10 ? $"0{minutes}:" : $"{minutes}:");
        res.Append(newSeconds < 10 ? $"0{newSeconds}" : $"{newSeconds}");

        return res.ToString();
    }

    private static long Dijkstra(int room, int exit)
    {
        distTo[room] = 0;

        while (true)
        {
            int vertex = GetVertex();

            if (vertex == -1 || vertex == exit)
            {
                break;
            }

            Visit(vertex);
        }

        return distTo[exit];
    }

    private static int GetVertex()
    {
        int minDistIndex = -1;
        long minDist = long.MaxValue;

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

    private static void Visit(int vertex)
    {
        visited[vertex] = true;
        foreach (var edge in graph[vertex])
        {
            long time = distTo[vertex] + edge.Seconds;
            int otherVertex = edge.To;

            if (otherVertex == vertex)
            {
                otherVertex = edge.From;
            }

            if (distTo[otherVertex] > time)
            {
                distTo[otherVertex] = time;
            }
        }
    }
}

public class Edge
{
    public int From { get; set; }
    public int To { get; set; }
    public long Seconds { get; set; }

    public Edge(int from, int to, long seconds)
    {
        From = from;
        To = to;
        Seconds = seconds;
    }

}
