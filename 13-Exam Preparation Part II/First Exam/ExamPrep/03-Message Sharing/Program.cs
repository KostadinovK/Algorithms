using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    private static Dictionary<string, List<string>> socialNetwork;
    private static Dictionary<string, int> received;
    public static void Main()
    {
        string[] people = Console.ReadLine()
            .Substring(8)
            .Split(new[] {',', ' '}, StringSplitOptions.RemoveEmptyEntries)
            .ToArray();

        string[] connections = Console.ReadLine().Substring(13).Split(',').ToArray();
        string[] starting = Console.ReadLine().Substring(7).Split(new []{',', ' '}, StringSplitOptions.RemoveEmptyEntries).ToArray();

        socialNetwork = new Dictionary<string, List<string>>();
       
        foreach (var person in people)
        {
            socialNetwork[person] = new List<string>();
        }

        foreach (var connection in connections)
        {
            string[] pair = connection.Split(new[] {' ', '-', ' '}, StringSplitOptions.RemoveEmptyEntries).ToArray();
            socialNetwork[pair[0]].Add(pair[1]);
            socialNetwork[pair[1]].Add(pair[0]);
        }

       int steps = BFS(starting);

       if (received.Count == socialNetwork.Count)
       {
           Console.WriteLine($"All people reached in {steps} steps");
           Console.WriteLine($"People at last step: {string.Join(", ", GetPeopleAtLastStep(steps))}");
       }
       else
       {
           Console.WriteLine($"Cannot reach: {string.Join(", ", GetUnreachedPeople())}");
       }
    }

    private static List<string> GetPeopleAtLastStep(int steps)
    {
        return received.Where(kvp => kvp.Value == steps).Select(kvp => kvp.Key).OrderBy(x => x).ToList();
    }

    private static List<string> GetUnreachedPeople()
    {
        return socialNetwork.Where(kvp => !received.ContainsKey(kvp.Key)).Select(kvp => kvp.Key).OrderBy(x => x).ToList();
    }

    private static int BFS(string[] starting)
    {
        received = new Dictionary<string, int>();
        Queue<string> queue = new Queue<string>();

        foreach (var person in starting)
        {
            received[person] = 0;
            queue.Enqueue(person);
        }

        int lastStep = 0;

        while (queue.Count > 0)
        {
            string person = queue.Dequeue();

            if (received[person] > lastStep)
            {
                lastStep = received[person];
            }

            foreach (var friend in socialNetwork[person])
            {
                if (!received.ContainsKey(friend))
                {
                    received[friend] = received[person] + 1;
                    queue.Enqueue(friend);
                }
            }
        }

        return lastStep;
    }
}
