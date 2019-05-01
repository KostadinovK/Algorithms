using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
public class Program
{
    public static void Main()
    {
        int minSeats = int.Parse(Console.ReadLine());
        int partiesCount = int.Parse(Console.ReadLine());

        List<int> parties = new List<int>();
        
        for (int i = 0; i < partiesCount; i++)
        {
            int party = int.Parse(Console.ReadLine());
            
            parties.Add(party);
        }

        BigInteger[] sums = new BigInteger[parties.Sum() + 1];
        sums[0] = 1;

        foreach (var party in parties)
        {
            for (int i = parties.Sum(); i >= 0; i--)
            {
                if (sums[i] != 0)
                {
                    sums[party + i] += sums[i];
                }
            }
        }

        BigInteger result = 0;
        for (int i = minSeats; i < sums.Length; i++)
        {
            result += sums[i];
        }

        Console.WriteLine(result);
    }
}
