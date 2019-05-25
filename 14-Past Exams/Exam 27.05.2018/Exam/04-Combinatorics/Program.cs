using System;

class Solution
{
    static readonly uint numberOfNodes = uint.Parse(Console.ReadLine());
    static readonly uint partsToGet = uint.Parse(Console.ReadLine());

    static readonly uint[,] graphMatrix = new uint[numberOfNodes, numberOfNodes];

    static uint[] prenumerator = new uint[numberOfNodes];
    static uint[] lowest = new uint[numberOfNodes];
    static uint countDFS = 0;

    static void FillGraphMatrix()
    {
        for (int i = 0; i < numberOfNodes; i++)
        {
            string[] connectionsOfNode = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int j = 0; j < connectionsOfNode.Length; j++)
            {
                graphMatrix[i, int.Parse(connectionsOfNode[j]) - 1] = 1;
            }
        }
    }

    static uint Min(uint a, uint b)
    {
        return (a < b) ? a : b;
    }

    static void DFS(uint node)
    {
        prenumerator[node] = ++countDFS;

        for (uint edge = 0; edge < numberOfNodes; edge++)
        {
            if (graphMatrix[node, edge] != 0 && prenumerator[edge] == 0)
            {
                graphMatrix[node, edge] = 2;
                DFS(edge);
            }
        }
    }

    static void PostOrderTreeTraverse(uint node)
    {
        for (uint edge = 0; edge < numberOfNodes; edge++)
        {
            if (graphMatrix[node, edge] == 2)
            {
                PostOrderTreeTraverse(edge);
            }
        }

        lowest[node] = prenumerator[node];

        for (uint edge = 0; edge < numberOfNodes; edge++)
        {
            if (graphMatrix[node, edge] == 1)
            {
                lowest[node] = Min(lowest[node], prenumerator[edge]);
            }
        }

        for (uint edge = 0; edge < numberOfNodes; edge++)
        {
            if (graphMatrix[node, edge] == 2)
            {
                lowest[node] = Min(lowest[node], lowest[edge]);
            }
        }
    }

    static void FindArticulationPoints()
    {
        uint[] articulationPoints = new uint[numberOfNodes];
        uint counter = 0;

        DFS(0);

        for (uint i = 0; i < numberOfNodes; i++)
        {
            if (prenumerator[i] == 0)
            {
                Console.WriteLine(-2);
                return;
            }
        }

        PostOrderTreeTraverse(0);

        for (uint i = 0; i < numberOfNodes; i++)
        {
            if (graphMatrix[0, i] == 2)
            {
                counter++;
            }
        }

        if (counter > 1)
        {
            articulationPoints[0] = 1;
        }

        for (int i = 1; i < numberOfNodes; i++)
        {
            int j;

            for (j = 0; j < numberOfNodes; j++)
            {
                if (graphMatrix[i, j] == 2 && lowest[j] >= prenumerator[i])
                {
                    break;
                }
            }

            if (j < numberOfNodes)
            {
                articulationPoints[i] = 1;
            }
        }

        bool noArtPoints = true;
        bool artPointsButDoNotDivideCorrectly = true;

        for (int i = 0; i < numberOfNodes; i++)
        {
            for (int j = 0; j < numberOfNodes; j++)
            {
                if (graphMatrix[i, j] == 2)
                {
                    graphMatrix[i, j] = 1; //restarting matrix so that it can be traversed again
                }
            }
        }

        uint[,] backUpGraph = new uint[numberOfNodes, numberOfNodes];

        Array.Copy(graphMatrix, backUpGraph, graphMatrix.Length);

        for (int i = 0; i < numberOfNodes; i++)
        {
            if (articulationPoints[i] == 1)
            {
                noArtPoints = false;
                Array.Copy(backUpGraph, graphMatrix, graphMatrix.Length);

                for (int j = 0; j < numberOfNodes; j++) //removing this art point
                {
                    graphMatrix[i, j] = 0;
                    graphMatrix[j, i] = 0;
                }

                prenumerator = new uint[numberOfNodes];
                int partsCounter = 0;

                for (uint j = 0; j < numberOfNodes; j++)
                {
                    if (prenumerator[j] == 0 && j != i)
                    {
                        DFS(j);
                        partsCounter++;
                    }
                }

                if (partsCounter == partsToGet)
                {
                    artPointsButDoNotDivideCorrectly = false;
                    Console.WriteLine(i + 1);
                }
            }
        }

        if (noArtPoints)
        {
            Console.WriteLine(-1);
        }
        else if (artPointsButDoNotDivideCorrectly)
        {
            Console.WriteLine(0);
        }
    }

    static void Main()
    {
        FillGraphMatrix();
        FindArticulationPoints();
    }
}

