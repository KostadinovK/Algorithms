using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Program
{
    private static int seeds;
    private static string field;
    private static HashSet<int> plantedSeeds;
    private static StringBuilder sb;

    public static void Main()
    {
        seeds = int.Parse(Console.ReadLine());
        field = Console.ReadLine().Replace(" ", "");

        plantedSeeds = new HashSet<int>();
        sb = new StringBuilder();

        int startIndex = field.IndexOf("1");

        PlantSeeds(startIndex, 0);

        Console.WriteLine(sb.ToString().Trim());
    }

    private static void PlantSeeds(int index, int planted)
    {
        if (planted == seeds)
        {
            AddFieldToResult();
            return;
        }


        for (int i = index; i < field.Length; i++)
        {
            if (field[i] == '1')
            {
                plantedSeeds.Add(i);
                PlantSeeds(i + 2, planted + 1);
                plantedSeeds.Remove(i);
            }
        }
    }

    private static void AddFieldToResult()
    {
        for (int i = 0; i < field.Length; i++)
        {
            if (plantedSeeds.Contains(i))
            {
                sb.Append(".");
            }
            else
            {
                sb.Append(field[i]);
            }
        }

        sb.AppendLine();
    }
}
