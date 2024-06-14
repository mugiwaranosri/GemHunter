using System;
using System.Linq;

namespace GemHunters
{
    // Position Class
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

    // Player Class
    class Player
    {
        public string Name { get; }
        public Position Position { get; set; }
        public int GemCount { get; set; }

        public Player(string name, Position position)
        {
            Name = name;
            Position = position;
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

    // Cell Class
    class Cell
    {
        public string Occupant { get; set; }

        public Cell()
        {
            Occupant = "-";
        }
    }

    // Board Class
    class Board
    {
        public Cell[,] Grid { get; set; }
        private Random randomizer = new Random();

        public Board()
        {
            Grid = new Cell[6, 6];
            InitializeGrid();
            PlaceObstacles(5);
            PlaceGems(5);
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

        public void Display(Player player1, Player player2, int totalMoves)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                           Welcome to Gem Hunters!                            ║");
            Console.WriteLine("╚══════════════════════════════════════════════════════════════════════════════╝");
            Console.WriteLine("Players compete to collect the most gems.");
            Console.WriteLine("Move using U (up), D (down), L (left), R (right).");
            Console.WriteLine("Avoid obstacles (O) and collect gems (G).");
            Console.WriteLine("Good luck!\n");

            Console.WriteLine($"Total Moves: {totalMoves}");
            Console.WriteLine($"Player1 (P1) gems: {player1.GemCount}");
            Console.WriteLine($"Player2 (P2) gems: {player2.GemCount}\n");

            Console.WriteLine("╔════════════════════════════════╗");

            for (int i = 0; i < 6; i++)
            {
                Console.Write("║ ");
                for (int j = 0; j < 6; j++)
                {
                    if (player1.Position.X == i && player1.Position.Y == j)
                    {
                        Console.Write("P1 ");
                    }
                    else if (player2.Position.X == i && player2.Position.Y == j)
                    {
                        Console.Write("P2 ");
                    }
                    else
                    {
                        Console.Write(Grid[i, j].Occupant + " ");
                    }
                }
                Console.WriteLine("║");
            }

            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        public bool IsValidMove(Player player, char direction)
        {
            Position newPosition = new Position(player.Position.X, player.Position.Y);

            switch (direction)
            {
                case 'U':
                    newPosition.X -= 1;
                    break;
                case 'D':
                    newPosition.X += 1;
                    break;
                case 'L':
                    newPosition.Y -= 1;
                    break;
                case 'R':
                    newPosition.Y += 1;
                    break;
            }

            if (newPosition.X < 0 || newPosition.X >= 6 || newPosition.Y < 0 || newPosition.Y >= 6)
            {
                return false;
            }

            if (Grid[newPosition.X, newPosition.Y].Occupant == "O")
            {
                return false;
            }

            return true;
        }

        public void CollectGem(Player player)
        {
            if (Grid[player.Position.X, player.Position.Y].Occupant == "G")
            {
                player.GemCount += 1;
                Grid[player.Position.X, player.Position.Y].Occupant = "-";

                // Check if all gems are collected
                if (!Grid.Cast<Cell>().Any(cell => cell.Occupant == "G"))
                {
                    Console.WriteLine("All gems collected! Game over!");
                
                    Environment.Exit(0);
                    
                }

            }
        }
    }

    // Game Class
    class Game
    {
        public Board Board { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentTurn { get; set; }
        public int TotalMoves { get; set; }

        public Game()
        {
            Board = new Board();
            Player1 = new Player("P1", new Position(0, 0));
            Player2 = new Player("P2", new Position(5, 5));
            CurrentTurn = Player1;
            TotalMoves = 0;
        }

        public void Start()
        {
            while (!IsGameOver())
            {
                Board.Display(Player1, Player2, TotalMoves);
                Console.WriteLine($"{CurrentTurn.Name}'s turn (U, D, L, R): ");
                char move = Console.ReadKey().KeyChar;
                Console.WriteLine();

                if (Board.IsValidMove(CurrentTurn, move))
                {
                    CurrentTurn.Move(move);
                    Board.CollectGem(CurrentTurn);
                    TotalMoves++;
                    SwitchTurn();
                }
                else
                {
                    Console.WriteLine("Invalid move! Try again.");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
             
            }

            AnnounceWinner();
            ReplayOption();
        }

        public void SwitchTurn()
        {
            CurrentTurn = (CurrentTurn == Player1) ? Player2 : Player1;
        }

        public bool IsGameOver()
        {
            // Game ends after 30 moves or when all gems are collected
            return TotalMoves >= 30 || !Board.Grid.Cast<Cell>().Any(cell => cell.Occupant == "G");
        }

        public void AnnounceWinner()
        {
            Board.Display(Player1, Player2, TotalMoves);
            Console.WriteLine("Game Over!");

            if (Player1.GemCount > Player2.GemCount)
            {
                Console.WriteLine("Player1 (P1) wins!");
            }
            else if (Player2.GemCount > Player1.GemCount)
            {
                Console.WriteLine("Player2 (P2) wins!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }

        public void ReplayOption()
        {
            Console.WriteLine("\nDo you want to play again? (Y/N): ");
            char replay = Console.ReadKey().KeyChar;
            if (replay == 'Y' || replay == 'y')
            {
                ResetGame();
                Start();
            }
            else
            {
                Console.WriteLine("\nThank you for playing Gem Hunters!");
            }
        }

        public void ResetGame()
        {
            Board = new Board();
            Player1 = new Player("P1", new Position(0, 0));
            Player2 = new Player("P2", new Position(5, 5));
            CurrentTurn = Player1;
            TotalMoves = 0;
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();
        }
    }
}
