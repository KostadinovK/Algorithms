using System;
using System.Collections.Generic;
using System.Linq;

public class StronglyConnectedComponents
{
    private static List<List<int>> stronglyConnectedComponents;
    private static List<int>[] graph;
    private static List<int>[] reversedGraph;
    private static int size;
    private static bool[] visited;
    private static Stack<int> nodeStack;

    public static List<List<int>> FindStronglyConnectedComponents(List<int>[] targetGraph)
    {
        size = targetGraph.Length;
        graph = targetGraph;
        stronglyConnectedComponents = new List<List<int>>();
        nodeStack = new Stack<int>();
        visited = new bool[size];

        BuildReverseGraph();

        for (int i = 0; i < size; i++)
        {
            DFS(i);
        }
        visited = new bool[size];

        while (nodeStack.Count > 0)
        {
            var node = nodeStack.Pop();

            if (!visited[node])
            {
                stronglyConnectedComponents.Add(new List<int>());
                ReverseDFS(node);
            }
        }

        return stronglyConnectedComponents;
    }

    private static void ReverseDFS(int node)
    {
        if (visited[node])
        {
            return;
        }

        visited[node] = true;
        stronglyConnectedComponents.Last().Add(node);

        foreach (var child in reversedGraph[node])
        {
            ReverseDFS(child);
        }
    }

    private static void BuildReverseGraph()
    {
        reversedGraph = new List<int>[size];

        for (int node = 0; node < size; node++)
        {
            reversedGraph[node] = new List<int>();
        }

        for (int node = 0; node < size; node++)
        {
            foreach (var child in graph[node])
            {
                reversedGraph[child].Add(node);
            }
        }
    }

    private static void DFS(int node)
    {
        if (visited[node])
        {
            return;
        }

        visited[node] = true;
        foreach (var child in graph[node])
        {
            DFS(child);
        }

        nodeStack.Push(node);
    }
}
