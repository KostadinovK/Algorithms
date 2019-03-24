using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{

    public static void Main()
    {
        double[] fractionInfo = Console.ReadLine().Split('/').Select(double.Parse).ToArray();

        double targetNom = fractionInfo[0];
        double targetDenom = fractionInfo[1];

        if (targetNom >= targetDenom)
        {
            Console.WriteLine("Error (fraction is equal to or greater than 1)");
            return;
        }

        Console.Write($"{targetNom}/{targetDenom} = ");

        double nominator = 0;
        double denominator = 2;
        while (nominator != targetNom)
        {
            if (nominator + targetDenom / denominator > targetNom)
            {
                denominator++;
                continue;
            }

            if (nominator == 0)
            {
                Console.Write($"1/{denominator}");
            }
            else
            {
                Console.Write($" + 1/{denominator}");
            }

            nominator += targetDenom / denominator;

            denominator++;
        }

    }
}
