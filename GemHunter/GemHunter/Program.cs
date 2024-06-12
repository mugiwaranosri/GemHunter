﻿using System;

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

        public Cell(string occupant = "-")
        {
            Occupant = occupant;
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
                Position.Y -= 1;
                break;
            case 'D':
                Position.Y += 1;
                break;
            case 'L':
                Position.X -= 1;
                break;
            case 'R':
                Position.X += 1;
                break;
        }
    }
}

