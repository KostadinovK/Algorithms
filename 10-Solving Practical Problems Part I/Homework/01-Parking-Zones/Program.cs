using System;
using System.Collections.Generic;
using System.Linq;

public class Point
{
    public int X { get; }
    public int Y { get; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class Zone
{
    public string Name { get; private set; }
    public Point TopLeft { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public decimal PricePerMinute { get; private set; }

    public Zone(string name, int x, int y, int width, int height, decimal pricePerMinute)
    {
        Name = name;
        TopLeft = new Point(x, y);
        Width = width;
        Height = height;
        PricePerMinute = pricePerMinute;
    }

}

public class Place
{
    public Point Coords { get; private set; }

    public Zone Zone { get; set; }

    public Place(int x, int y)
    {
        Coords = new Point(x, y);
    }

    public bool BelongsToZone(Zone zone)
    {
        return (Coords.X >= zone.TopLeft.X && Coords.X <= zone.TopLeft.X + zone.Width) &&
               (Coords.Y >= zone.TopLeft.Y && Coords.Y <= zone.TopLeft.Y + zone.Height);
    }
}


public class Program
{
    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        List<Place> places = new List<Place>();
        List<Zone> zones = new List<Zone>();

        for (int i = 0; i < n; i++)
        {
            string[] zoneInfo = Console.ReadLine().Split(':');
            string zoneName = zoneInfo[0];
            zoneInfo[1] = zoneInfo[1].TrimStart();
            string[] zoneArgs = zoneInfo[1]
                .Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();

            Zone zone = new Zone(zoneName, int.Parse(zoneArgs[0]), int.Parse(zoneArgs[1]),
                    int.Parse(zoneArgs[2]), int.Parse(zoneArgs[3]), decimal.Parse(zoneArgs[4]));

            zones.Add(zone);
        }

        string[] placesInfo = Console.ReadLine().Split(';').ToArray();

        foreach (var placeInfo in placesInfo)
        {
            int[] placeCoords = placeInfo
                .Split(new [] {',', ' '}, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            Place place = new Place(placeCoords[0], placeCoords[1]);
            places.Add(place);
            foreach (var zone in zones)
            {
                if (place.BelongsToZone(zone))
                {
                    place.Zone = zone;
                    break;
                }
            }
        }

        int[] shopCoords = Console.ReadLine()
            .Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        Place store = new Place(shopCoords[0], shopCoords[1]);

        int k = int.Parse(Console.ReadLine());

        decimal minPrice = int.MaxValue;
        int minSeconds = int.MaxValue;
        Place closestPlace = null;

        foreach (var zone in zones)
        {
            decimal price = 0;
            var closestPlaceToStore = places
                .Where(p => p.Zone.Name == zone.Name)
                .OrderBy(p => Math.Abs(p.Coords.X - store.Coords.X) + Math.Abs(p.Coords.Y - store.Coords.Y) - 1)
                .FirstOrDefault();

            int totalBlocks = (Math.Abs(closestPlaceToStore.Coords.X - store.Coords.X) + Math.Abs(closestPlaceToStore.Coords.Y - store.Coords.Y) - 1) * 2;

            int totalSeconds = k * totalBlocks;
            int totalMinutes = totalSeconds / 60;

            if (totalSeconds % 60 != 0)
            {
                totalMinutes++;
            }

            price = totalMinutes * zone.PricePerMinute;

            if (price < minPrice)
            {
                minPrice = price;
                minSeconds = totalSeconds;
                closestPlace = closestPlaceToStore;
            }
            else if (price == minPrice && totalSeconds < minSeconds)
            {
                minPrice = price;
                minSeconds = totalSeconds;
                closestPlace = closestPlaceToStore;
            }
        }

        Console.WriteLine($"Zone Type: {closestPlace.Zone.Name}; X: {closestPlace.Coords.X}; Y: {closestPlace.Coords.Y}; Price: {minPrice:f2}");
    }
}
