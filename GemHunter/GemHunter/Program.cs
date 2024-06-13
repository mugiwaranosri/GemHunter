using System;

namespace GemHunters
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Gem Hunters!");
        }
    }
}

    class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }


    class Cell
    {
        public string Occupant { get; set; }

        public Cell()
        {
            Occupant = "_";
        }
    }
class Player
{
    public string Name { get; }
    public Position Position { get; set; }
    public int GemCount { get; set; }

    public Player(string name, Position startPosition)
    {
        Name = name;
        Position = startPosition;
        GemCount = 0;
    }

    public void Move(char direction)
    {
        switch (direction)
        {
            case 'U':
                Position.X -= 1;
                break;
            case 'D':
                Position.X += 1;
                break;
            case 'L':
                Position.Y -= 1;
                break;
            case 'R':
                Position.Y += 1;
                break;
        }
    }
}


    class Board
    {
        private Cell[,] Grid;

        public Board()
        {
            Grid = new Cell[6, 6];
            InitializeGrid();
            PlacePlayers();
        }

        private void InitializeGrid()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Grid[i, j] = new Cell();
                }
            }
        }
        public void PlacePlayers(Player p1, Player p2)
        {
            Grid[p1.Position.X, p1.Position.Y].Occupant = "P1";
            Grid[p2.Position.X, p2.Position.Y].Occupant = "P2";
        }
        public void Display()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Console.Write(Grid[i, j].Occupant + " ");
                }
                Console.WriteLine();
            }
        }
        private Random randomizer = new Random();

        private void PlaceObstacles(int count)
        {
               int placed = 0;
               while (placed < count)
               {
                   int x = randomizer.Next(6);
                   int y = randomizer.Next(6);

                   if (Grid[x, y].Occupant == "-")
                   {
                       Grid[x, y].Occupant = "O";
                       placed++;
                   }
               }
        }

        private void PlaceGems(int count)
        {
             int placed = 0;
             while (placed < count)
             {
                  int x = randomizer.Next(6);
                  int y = randomizer.Next(6);

                  if (Grid[x, y].Occupant == "-")
                  {
                      Grid[x, y].Occupant = "G";
                      placed++;
                  }
             }
        }
        public bool IsValidMove(Player player, char direction)
        {
                  int newX = player.Position.X;
                  int newY = player.Position.Y;

                  switch (direction)
                  {
            case 'U':
                newX -= 1;
                break;
            case 'D':
                newX += 1;
                break;
            case 'L':
                newY -= 1;
                break;
            case 'R':
                newY += 1;
                break;
            default:
                return false;
        }

        if (newX < 0 || newX >= 6 || newY < 0 || newY >= 6)
        {
            return false;
        }

        if (Grid[newX, newY].Occupant == "O")
        {
            return false;
        }

        return true;
    }
}

    
class Game
{
    private Board Board { get; set; }
    private Player Player1 { get; set; }
    private Player Player2 { get; set; }
    private Player CurrentTurn { get; set; }
    private int TotalTurns { get; set; }

    public Game()
    {
        Board = new Board();
        Player1 = new Player("P1", new Position(0, 0));
        Player2 = new Player("P2", new Position(5, 5));
        Board.PlacePlayers(Player1, Player2);
        CurrentTurn = Player1;
        TotalTurns = 0;
    }
}


