using System;
using System.Collections.Generic;
using System.Linq;


public class Point
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Log
{
    public int Id { get; private set; }
    public int Area { get; set; }
    public Point TopLeft { get; private set; }
    public Point BottomRight { get; private set; }

    public Log(int id, Point topLeft, Point bottomRight)
    {
        Id = id;
        Area = 0;
        TopLeft = topLeft;
        BottomRight = bottomRight;
    }



    public bool Intersects(Log other)
    {
        if ((other.BottomRight.X >= TopLeft.X && other.TopLeft.X <= BottomRight.X)
            && (other.BottomRight.Y <= TopLeft.Y && other.TopLeft.Y >= BottomRight.Y))
        {
            return true;
        }

        return false;
    }
}

public class Program
{
    private static bool[] visited;
    private static Dictionary<Log, List<Log>> lake;
    public static void Main()
    {
        int[] firstLine = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int logsCount = firstLine[0];
        int queriesCount = firstLine[1];

        List<Log> logs = new List<Log>();
        lake = new Dictionary<Log, List<Log>>();

        for (int i = 1; i <= logsCount; i++)
        {
            int[] rectangleInfo = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Point topLeft = new Point(rectangleInfo[0], rectangleInfo[1]);
            Point bottomRight = new Point(rectangleInfo[2], rectangleInfo[3]);
            Log log = new Log(i, topLeft, bottomRight);

            lake.Add(log, new List<Log>());

            for (int j = 0; j < logs.Count; j++)
            {
                if (log.Intersects(logs[j]))
                {
                    lake[log].Add(logs[j]);
                    lake[logs[j]].Add(log);
                }
            }

            logs.Add(log);
        }

        visited = new bool[logsCount + 1];
        int area = 0;
        foreach (var log in lake)
        {
            if (!visited[log.Key.Id])
            {
                DFS(log.Key, area);
                area++;
            }
        }

        for (int i = 0; i < queriesCount; i++)
        {
            int[] queryInfo = Console.ReadLine().Split().Select(int.Parse).ToArray();
            Log firstLog = logs[queryInfo[0] - 1];
            Log secondLog = logs[queryInfo[1] - 1];

            if (firstLog.Area == secondLog.Area)
            {
                Console.WriteLine("YES");
            }
            else
            {
                Console.WriteLine("NO");
            }
        }
    }

    private static void DFS(Log log, int area)
    {
        if (visited[log.Id])
        {
           return;
        }

        visited[log.Id] = true;
        log.Area = area;
        foreach (var child in lake[log])
        {
            DFS(child, area);
        }
    }
}
