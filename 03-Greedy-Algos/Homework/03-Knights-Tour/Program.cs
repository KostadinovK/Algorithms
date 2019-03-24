using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public class Cell
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public bool IsVisited { get; set; }
        public int VisitedTurn { get; set; }

        public Cell(int row, int col)
        {
            Row = row;
            Col = col;
            IsVisited = false;
            VisitedTurn = 0;
        }
    }

    public static List<Cell> board = new List<Cell>();

    public static void Main()
    {
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                board.Add(new Cell(i, j));
            }
        }

        var currentCell = board[0];
        currentCell.IsVisited = true;
        currentCell.VisitedTurn = 1;

        int turn = 2;
        while (board.Any(c => !c.IsVisited))
        {
            currentCell = GetNextCell(currentCell);
            currentCell.IsVisited = true;
            currentCell.VisitedTurn = turn;
            turn++;
        }

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write(board[i * n + j].VisitedTurn.ToString().PadLeft(3) + " ");
            }
            Console.WriteLine();
        }
    }

    private static Cell GetNextCell(Cell current)
    {
        var topLeft = board.FirstOrDefault(c => c.Row == current.Row - 2 && c.Col == current.Col - 1);
        var leftTop = board.FirstOrDefault(c => c.Row == current.Row - 1 && c.Col == current.Col - 2);
        var rightTop = board.FirstOrDefault(c => c.Row == current.Row - 1 && c.Col == current.Col + 2);
        var topRight = board.FirstOrDefault(c => c.Row == current.Row - 2 && c.Col == current.Col + 1);

        var bottomLeft = board.FirstOrDefault(c => c.Row == current.Row + 2 && c.Col == current.Col - 1);
        var leftBottom = board.FirstOrDefault(c => c.Row == current.Row + 1 && c.Col == current.Col - 2);
        var rightBottom = board.FirstOrDefault(c => c.Row == current.Row + 1 && c.Col == current.Col + 2);
        var bottomRight = board.FirstOrDefault(c => c.Row == current.Row + 2 && c.Col == current.Col + 1);

        return new List<Cell>(){topLeft, leftTop, rightTop, topRight, bottomLeft, leftBottom, rightBottom, bottomRight}
            .Where(c => c != null && !c.IsVisited)
            .OrderBy(c => GetAdjacentCellsCount(c))
            .First();
    }

    private static int GetAdjacentCellsCount(Cell current)
    {
        var topLeft = board.FirstOrDefault(c => c.Row == current.Row - 2 && c.Col == current.Col - 1);
        var leftTop = board.FirstOrDefault(c => c.Row == current.Row - 1 && c.Col == current.Col - 2);
        var rightTop = board.FirstOrDefault(c => c.Row == current.Row - 1 && c.Col == current.Col + 2);
        var topRight = board.FirstOrDefault(c => c.Row == current.Row - 2 && c.Col == current.Col + 1);

        var bottomLeft = board.FirstOrDefault(c => c.Row == current.Row + 2 && c.Col == current.Col - 1);
        var leftBottom = board.FirstOrDefault(c => c.Row == current.Row + 1 && c.Col == current.Col - 2);
        var rightBottom = board.FirstOrDefault(c => c.Row == current.Row + 1 && c.Col == current.Col + 2);
        var bottomRight = board.FirstOrDefault(c => c.Row == current.Row + 2 && c.Col == current.Col + 1);

        return new List<Cell>(){topLeft, leftTop, rightTop, topRight, bottomLeft, leftBottom, rightBottom, bottomRight}
            .Count(c => c != null && !c.IsVisited);
    }
}
