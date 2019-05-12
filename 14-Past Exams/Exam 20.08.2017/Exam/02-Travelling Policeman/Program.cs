using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        int fuel = int.Parse(Console.ReadLine());
        Dictionary<int, Street> streets = new Dictionary<int, Street>();
       

        string line = Console.ReadLine();
        int streetIndex = 1;
        while (line != "End")
        {
            string[] streetInfo = line.Split(',').Select(x => x.Trim()).ToArray();
            Street street = new Street(streetInfo[0], int.Parse(streetInfo[1]), int.Parse(streetInfo[2]), int.Parse(streetInfo[3]));
            if (street.Value > 0)
            {
                streets[streetIndex] = street;
                streetIndex++;
            }
            line = Console.ReadLine();
        }

        int[,] matrix = new int[streets.Count + 1, fuel + 1];
        bool[,] isItemIncluded = new bool[streets.Count + 1, fuel + 1];

        for (int itemIndex = 1; itemIndex < matrix.GetLength(0); itemIndex++)
        {
            for (int currentCapacity = 1; currentCapacity < matrix.GetLength(1); currentCapacity++)
            {
                if (currentCapacity < streets[itemIndex].Length)
                {
                    matrix[itemIndex, currentCapacity] = matrix[itemIndex - 1, currentCapacity];
                }
                else
                {
                    int valueWithOutItem = matrix[itemIndex - 1, currentCapacity];
                    int valueWithItem = matrix[itemIndex - 1, currentCapacity - streets[itemIndex].Length] +
                                        streets[itemIndex].Value;

                    if (valueWithItem > valueWithOutItem)
                    {
                        isItemIncluded[itemIndex, currentCapacity] = true;
                    }

                    int maxValue = Math.Max(valueWithOutItem, valueWithItem);

                    matrix[itemIndex, currentCapacity] = maxValue;
                }
            }
        }

        List<Street> path = new List<Street>();


        int tempCap = fuel;

        for (int i = streets.Count; i > 0; i--)
        {
            if (!isItemIncluded[i, tempCap])
            {
                continue;
            }

            Street item = streets[i];
            path.Add(item);
            tempCap -= item.Length;
        }

        path.Reverse();

        Console.WriteLine(string.Join(" -> ", path.Select(s => s.Name)));
        int totalPokemons = path.Select(s => s.Pokemons).Sum();
        int totalDamage = path.Select(s => s.Damage).Sum();
        int fuelLeft = fuel - path.Select(s => s.Length).Sum();

        Console.WriteLine($"Total pokemons caught -> {totalPokemons}");
        Console.WriteLine($"Total car damage -> {totalDamage}");
        Console.WriteLine($"Fuel Left -> {fuelLeft}");
    }
}

public class Street
{
    public string Name { get; set; }
    public int Length { get; set; }
    public int Pokemons { get; set; }
    public int Damage { get; set; }
    public int Value => Pokemons * 10 - Damage;

    public Street(string name, int damage, int pokemons, int length)
    {
        Name = name;
        Damage = damage;
        Pokemons = pokemons;
        Length = length;
    }
}
