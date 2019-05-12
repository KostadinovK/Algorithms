using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public class Program
{
    private static List<Edge>[] graph;
    private static PriorityQueue<Edge> queue;
    private static Dictionary<int, List<int>> msf;
    private static bool[] marked;
    private static int[] damage;
    public static void Main()
    {
        int vertices = int.Parse(Console.ReadLine());
        graph = new List<Edge>[vertices];

        for (int i = 0; i < graph.Length; i++)
        {
            graph[i] = new List<Edge>();
        }

        int edges = int.Parse(Console.ReadLine());
        int lightningsCount = int.Parse(Console.ReadLine());

        for (int i = 0; i < edges; i++)
        {
            int[] edgeInfo = Console.ReadLine().Split().Select(int.Parse).ToArray();

            Edge edge = new Edge(edgeInfo[0], edgeInfo[1], edgeInfo[2]);
            graph[edgeInfo[0]].Add(edge);
            graph[edgeInfo[1]].Add(edge);
        }

        msf = new Dictionary<int, List<int>>();
        for (int i = 0; i < graph.Length; i++)
        {
            msf[i] = new List<int>();
        }
        marked = new bool[graph.Length];
        for (int i = 0; i < graph.Length; i++)
        {
            PrimMSF(i);
        }

        damage = new int[graph.Length];

        for (int i = 0; i < lightningsCount; i++)
        {
            int[] lightningInfo = Console.ReadLine().Split().Select(int.Parse).ToArray();
            DFS(lightningInfo[0], lightningInfo[0], lightningInfo[1]);
        }

        Console.WriteLine(damage.Max());
    }

    private static void DFS(int vertex, int parent, int power)
    {
        damage[vertex] += power;
        foreach (var child in msf[vertex])
        {
            if (child != parent)
            {
                DFS(child, vertex, power / 2);
            }
        }
    }

    private static void PrimMSF(int source)
    {
        queue = new PriorityQueue<Edge>();

        Visit(source);

        while (queue.Count > 0)
        {
            Edge edge = queue.DequeueMin();
            int start = edge.Start;
            int end = edge.End;

            if (marked[start] && marked[end]) continue;

            msf[edge.Start].Add(edge.End);
            msf[edge.End].Add(edge.Start);

            if (!marked[start]) Visit(start);
            if (!marked[end]) Visit(end);
        }
    }

    

    private static void Visit(int source)
    {
        marked[source] = true;
        foreach (var edge in graph[source])
        {
            int otherNode = edge.Start;
            if (edge.Start == source)
            {
                otherNode = edge.End;
            }

            if (!marked[otherNode])
            {
                queue.Enqueue(edge);
            }
        }
    }
}

public class Edge : IComparable<Edge>
{
    public int Start { get; set; }
    public int End { get; set; }
    public int Distance { get; set; }

    public Edge(int start, int end, int distance)
    {
        Start = start;
        End = end;
        Distance = distance;
    }

    public int CompareTo(Edge other)
    {
        return this.Distance.CompareTo(other.Distance);
    }
}

public class PriorityQueue<T> where T : IComparable<T>
{
    private Dictionary<T, int> searchCollection;
    private List<T> heap;

    public PriorityQueue()
    {
        this.heap = new List<T>();
        this.searchCollection = new Dictionary<T, int>();
    }

    public int Count {
        get {
            return this.heap.Count;
        }
    }

    public T DequeueMin()
    {
        var min = this.heap[0];
        var last = this.heap[this.heap.Count - 1];
        this.searchCollection[last] = 0;
        this.heap[0] = last;
        this.heap.RemoveAt(this.heap.Count - 1);
        if (this.heap.Count > 0)
        {
            this.Sink(0);
        }

        this.searchCollection.Remove(min);

        return min;
    }

    public T PeekMin()
    {
        return this.heap[0];
    }

    public void Enqueue(T element)
    {
        this.searchCollection.Add(element, this.heap.Count);
        this.heap.Add(element);
        this.Swim(this.heap.Count - 1);
    }

    private void Sink(int i)
    {
        var left = (2 * i) + 1;
        var right = (2 * i) + 2;
        var smallest = i;

        if (left < this.heap.Count && this.heap[left].CompareTo(this.heap[smallest]) < 0)
        {
            smallest = left;
        }

        if (right < this.heap.Count && this.heap[right].CompareTo(this.heap[smallest]) < 0)
        {
            smallest = right;
        }

        if (smallest != i)
        {
            T old = this.heap[i];
            this.searchCollection[old] = smallest;
            this.searchCollection[this.heap[smallest]] = i;
            this.heap[i] = this.heap[smallest];
            this.heap[smallest] = old;
            this.Sink(smallest);
        }
    }

    private void Swim(int i)
    {
        var parent = (i - 1) / 2;
        while (i > 0 && this.heap[i].CompareTo(this.heap[parent]) < 0)
        {
            T old = this.heap[i];
            this.searchCollection[old] = parent;
            this.searchCollection[this.heap[parent]] = i;
            this.heap[i] = this.heap[parent];
            this.heap[parent] = old;

            i = parent;
            parent = (i - 1) / 2;
        }
    }

    public void DecreaseKey(T element)
    {
        int index = this.searchCollection[element];
        this.Swim(index);
    }
}
