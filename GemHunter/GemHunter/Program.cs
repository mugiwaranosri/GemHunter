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
    public string Name { get; set; }
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
        public void Display(Player player1, Player player2, int totalMoves)
        {
            Console.Clear();
            DisplayHeader();
            DisplayInstructions();
            DisplayStats(player1, player2, totalMoves);
            Console.WriteLine();
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                  switch (Grid[i, j].Occupant)
                  {
                    case "P1":
                        Console.Write(" P1 ");
                        break;
                    case "P2":
                        Console.Write(" P2 ");
                        break;
                    case "G":
                        Console.Write(" 🌟 ");
                        break;
                    case "O":
                        Console.Write(" O ");
                        break;
                    default:
                        Console.Write(" - ");
                        break;
                  }
            }
                Console.WriteLine();
            }

        DisplayFooter();
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
    public void MovePlayer(Player player, char direction)
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
        }

        if (Grid[newX, newY].Occupant == "G")
        {
            player.GemCount++;
            Grid[newX, newY].Occupant = "-";
        }

        Grid[player.Position.X, player.Position.Y].Occupant = "-";
        player.Position.X = newX;
        player.Position.Y = newY;
        Grid[player.Position.X, player.Position.Y].Occupant = player.Name == "P1" ? "P1" : "P2";
    }
    public void Display(Player player1, Player player2, int totalMoves)
    {
        Console.Clear();
        DisplayHeader();
        DisplayInstructions();
        DisplayStats(player1, player2, totalMoves);
        Console.WriteLine();

        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                Console.Write(Grid[i, j].Occupant + " ");
            }
            Console.WriteLine();
        }
        DisplayFooter();

        
    }
    public void DisplayHeader()
    {
        Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                           Welcome to Gem Hunters!                            ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");

    }

    public void DisplayFooter()
    {
        Console.WriteLine("*******************************************************************************");
    }

    public void DisplayInstructions()
    {
        Console.WriteLine("Players compete to collect the most gems.");
        Console.WriteLine("Move using U (up), D (down), L (left), R (right).");
        Console.WriteLine("Avoid obstacles (O) and collect gems (G).");
        Console.WriteLine("Good luck!");
    }

    public void DisplayStats(Player player1, Player player2, int totalMoves)
    {
        Console.WriteLine($"Total Moves: {totalMoves}");
        Console.WriteLine($"{player1.Name} gems: {player1.GemCount}");
        Console.WriteLine($"{player2.Name} gems: {player2.GemCount}");
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
    public void Start()
    {
        Console.Write("Enter name for Player 1: ");
        Player1.Name = Console.ReadLine();
        Console.Write("Enter name for Player 2: ");
        Player2.Name = Console.ReadLine();

        do
        {
            TotalTurns = 0;
            Player1.GemCount = 0;
            Player2.GemCount = 0;
            Board = new Board();
            Board.PlacePlayers(Player1, Player2);
            CurrentTurn = Player1;

            while (TotalTurns < 30)
            {
                Board.Display(Player1, Player2, TotalTurns);
                Console.WriteLine($"{CurrentTurn.Name}'s turn (U, D, L, R): ");
                char move = Console.ReadKey().KeyChar;
                if (move == 'U' || move == 'D' || move == 'L' || move == 'R')
                {

                    if (Board.IsValidMove(CurrentTurn, move))
                    {
                        Board.MovePlayer(CurrentTurn, move);
                        TotalTurns++;
                        SwitchTurn();
                    }
                    else
                    {
                        Console.WriteLine("Invalid move. Try again.");
                        Console.ReadKey(true);
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Use U, D, L, R to move.");
                    Console.ReadKey(true);
                }
            }

            Board.Display(Player1, Player2, TotalTurns);
            AnnounceWinner();
            Console.WriteLine("Do you want to play again? (Y/N): ");
        } while (Console.ReadKey().KeyChar == 'Y');
    }

    private void SwitchTurn()
    {
        CurrentTurn = (CurrentTurn == Player1) ? Player2 : Player1;
    }

    private void AnnounceWinner()
    {
        if (Player1.GemCount > Player2.GemCount)
        {
            Console.WriteLine($"{Player1.Name} wins!");
        }
        else if (Player2.GemCount > Player1.GemCount)
        {
            Console.WriteLine($"{Player2.Name} wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }

}
    


