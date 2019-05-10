using System;
using System.Collections.Generic;
using System.Linq;

public class Rectangle : IComparable<Rectangle>
{
    public string Name { get; set; }
    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get; set; }
    public int Y2 { get; set; }
    public int MaxDepth { get; set; }
    public Rectangle Next { get; set; }

    public Rectangle(string name, int x1, int y1, int x2, int y2)
    {
        Name = name;
        X1 = x1;
        Y1 = y1;
        X2 = x2;
        Y2 = y2;
        MaxDepth = 0;
    }

    public bool IsInsideOf(Rectangle other)
    {
        bool isInside = X1 >= other.X1 && X2 <= other.X2 && Y1 <= other.Y1 && Y2 >= other.Y2; 

        return isInside;
    }

    public int CompareTo(Rectangle other)
    {
        int result = this.MaxDepth.CompareTo(other.MaxDepth);

        if (result == 0)
        {
            result = string.Compare(other.Name, this.Name, StringComparison.Ordinal);
        }

        return result;
    }

    public override string ToString()
    {
        return Name;
    }
}

public class Program
{
    public static void Main()
    {
        List<Rectangle> rectangles = new List<Rectangle>();

        string line = Console.ReadLine();

        while (line != "End")
        {
            string[] rectangleInfo = line.Split(':').ToArray();
            string name = rectangleInfo[0];
            int[] coordinates = rectangleInfo[1]
                .Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();


            Rectangle rectangle = new Rectangle(name, coordinates[0], coordinates[1], coordinates[2], coordinates[3]);
            rectangles.Add(rectangle);



            line = Console.ReadLine();
        }

        foreach (var rectangle in rectangles)
        {
            if (rectangle.MaxDepth == 0)
            {
                GetMaxDepth(rectangle, rectangles);
            }
        }

        Rectangle bestRectangle = rectangles.Max();
        List<Rectangle> result = new List<Rectangle>();

        while (bestRectangle != null)
        {
            result.Add(bestRectangle);
            bestRectangle = bestRectangle.Next;
        }

        Console.WriteLine(string.Join(" < ", result));

    }

    private static void GetMaxDepth(Rectangle rectangle, List<Rectangle> rectangles)
    {
        List<Rectangle> innerRectangles = new List<Rectangle>();
        foreach (var current in rectangles)
        {
            if (current != rectangle && current.IsInsideOf(rectangle))
            {
                if (current.MaxDepth == 0)
                {
                    GetMaxDepth(current, rectangles);
                }

                innerRectangles.Add(current);
            }
        }

        if (innerRectangles.Count == 0)
        {
            rectangle.MaxDepth = 1;
        }
        else
        {
            Rectangle best = innerRectangles.Max();
            rectangle.MaxDepth = best.MaxDepth + 1;
            rectangle.Next = best;
        }
    }
}
