using System;
using System.Collections;
using System.Collections.Generic;

public class Program
{
    private static char[,] matrix;
    private static bool[,] visited;
    public static void Main()
    {
        int rows = int.Parse(Console.ReadLine());
        Point start = null;
        Point end = null;

        char[] firstRowData = Console.ReadLine().ToCharArray();
        int cols = firstRowData.Length;
        matrix = new char[rows, cols];
        visited = new bool[rows, cols];

        for (int i = 0; i < firstRowData.Length; i++)
        {
            if (firstRowData[i] == '3')
            {
               start = new Point(0, i);
            }

            if (firstRowData[i] == '5')
            {
                end = new Point(0, i);
            }

            matrix[0, i] = firstRowData[i];
        }

        for (int row = 1; row < rows; row++)
        {
            char[] rowData = Console.ReadLine().ToCharArray();
            for (int col = 0; col < rowData.Length; col++)
            {
                if (rowData[col] == '3')
                {
                    start = new Point(row, col);
                }

                if (rowData[col] == '5')
                {
                    end = new Point(row, col);
                }
                matrix[row, col] = rowData[col];
            }
        }

        Console.WriteLine(BFS(start, end));
    }

    public static int BFS(Point start, Point end)
    {
        int result = 0;
        visited[start.Row, start.Col] = true;
  
        var queue = new Queue<Point>();
        queue.Enqueue(start);
       
        while (queue.Count != 0) 
        { 
            var point = queue.Dequeue();

            if (point.Row == end.Row && point.Col == end.Col)
            {
                return result;
            }

            var left = new Point(point.Row, point.Col - 1);
            var right = new Point(point.Row, point.Col + 1);
            var up = new Point(point.Row - 1, point.Col);
            var down = new Point(point.Row + 1, point.Col);

            var neighbors = new List<Point>{left, right, up, down};
            int dirs = 0;
            for (int i = 0; i< neighbors.Count; i++) 
            {
                if (IsValid(neighbors[i]) && !visited[neighbors[i].Row, neighbors[i].Col]) 
                { 
                    visited[neighbors[i].Row, neighbors[i].Col] = true;
                    queue.Enqueue(neighbors[i]);
                    dirs++;
                } 
            }

            if (dirs > 1)
            {
                result++;
            }
        } 
        return result; 
    }

    private static bool IsValid(Point point)
    {
        return point.Row >= 0 &&
               point.Row < matrix.GetLength(0) &&
               point.Col >= 0 && 
               point.Col < matrix.GetLength(1) && 
               matrix[point.Row, point.Col] != '1';
    }
}

public class Point
{
    public int Row { get; }
    public int Col { get; }
    public Point(int row, int col)
    {
        Row = row;
        Col = col;
    }
}
