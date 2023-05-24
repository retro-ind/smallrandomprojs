using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        public class Enemy
        {
            public int X { get; set; }
            public int Y { get; set; } 
        }

        List<Enemy> enemies = new List<Enemy>();

        void AddEnemies()
        {
            enemies.Add(new Enemy { X = 5, Y = 5 });
            enemies.Add(new Enemy { X = 10, Y = 10 });
        }

        void MoveEnemiesTowardsPlayer(int playerX, int playerY, List<Enemy> enemies)
        {
            foreach (Enemy enemy in enemies)
            {
                int dx = playerX - enemy.X;
                int dy = playerY - enemy.Y;

                // Move enemy towards player by one unit in the x direction
                if (dx < 0)
                {
                    enemy.X -= 1;
                }
                else if (dx > 0)
                {
                    enemy.X += 1;
                }

                // Move enemy towards player by one unit in the y direction
                if (dy < 0)
                {
                    enemy.Y -= 1;
                }
                else if (dy > 0)
                {
                    enemy.Y += 1;
                }
            }
        }
        static void Main(string[] args)
        {
            Random rand = new Random();
            int width = 40;
            int height = 20;
            char[,] maze = GenerateMaze(width, height);
            DisplayMaze(maze);

            // Set up player starting position
            int playerX = 1;
            int playerY = FindStartingY(maze);

            // Set up Enimies and placement

            // Find the end of the maze
            int endX = width - 2;
            int endY = rand.Next(1, height - 1);

            maze[endX, endY] = 'E';


            // Game loop
            while (true)
            {
            
                Console.Write("Enter move (left, right, up, down): ");
                string move = Console.ReadLine().ToLower();

                // Check if the move is valid
                bool moveValid = false;
                int nextX = playerX;
                int nextY = playerY;
                if (move == "left" && maze[playerX - 1, playerY] != '#')
                {
                    nextX = playerX - 1;
                    moveValid = true;
                }
                else if (move == "right" && maze[playerX + 1, playerY] != '#')
                {
                    nextX = playerX + 1;
                    moveValid = true;
                }
                else if (move == "up" && maze[playerX, playerY - 1] != '#')
                {
                    nextY = playerY - 1;
                    moveValid = true;
                }
                else if (move == "down" && maze[playerX, playerY + 1] != '#')
                {
                    nextY = playerY + 1;
                    moveValid = true;
                }

                // Check if the player reached the end
                if (playerX == endX && playerY == endY)
                {
                    Console.WriteLine("Congratulations, you made it to the end of the maze!");
                    Console.WriteLine("Press any key to exit.");
                    Console.ReadKey();
                    Environment.Exit(0);
                }

                if (moveValid)
                {
                    // Update player position
                    maze[playerX, playerY] = ' ';
                    playerX = nextX;
                    playerY = nextY;
                    maze[playerX, playerY] = 'P';

                    // Check if the player has reached the end
                    if (playerX == width - 2 && maze[playerX, playerY] == ' ')
                    {
                        Console.WriteLine("You win!");
                        break;
                    }

                    //MoveEnemiesTowardsPlayer(playerX, playerY, Enemy);

                    // Redraw the maze
                    Console.Clear();
                    DisplayMaze(maze);
                }
                else
                {
                    Console.WriteLine("Invalid move. Please try again.");
                }
            }
        }
        static char[,] GenerateMaze(int width, int height)
        {
            // Initialize the maze with walls
            char[,] maze = new char[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    maze[x, y] = '#';
                }
            }

            // Generate the maze
            Random rand = new Random();
            int startX = rand.Next(2, width - 3);
            int startY = rand.Next(2, height - 3);
            maze[startX, startY] = ' ';

            GenerateMaze(startX, startY, maze, rand);

            // Add entrance and exit
            maze[1, startY] = ' ';
            maze[width - 2, startY] = ' ';

            // Add player starting position (note: this is the current reason nothing is showing up in the console. The app is waiting for imput with the 'console.readLine' statement writen earlier (currently resolved)
            maze[1, FindStartingY(maze)] = 'P';
            return maze;
        }
        public class Direction
        {
            public int X { get; set; }
            public int Y { get; set; }

            public Direction(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        static void GenerateMaze(int x, int y, char[,] maze, Random rand)
        {
            List<Direction> directions = new List<Direction>()
    {
        new Direction(-2, 0), // Left
        new Direction(2, 0), // Right
        new Direction(0, -2), // Up
        new Direction(0, 2) // Down
    };

            Shuffle(directions, rand);

            foreach (Direction direction in directions)
            {
                int nextX = x + direction.X;
                int nextY = y + direction.Y;

                if (nextX >= 1 && nextX < maze.GetLength(0) - 1 && nextY >= 1 && nextY < maze.GetLength(1) - 1 && maze[nextX, nextY] == '#')
                {
                    maze[nextX, nextY] = ' ';
                    maze[x + (direction.X / 2), y + (direction.Y / 2)] = ' ';
                    GenerateMaze(nextX, nextY, maze, rand);
                }
            }
        }

        static void Shuffle(List<Direction> list, Random rand)
        {
            for (int i = 0; i < list.Count; i++)
            {
                int j = rand.Next(i, list.Count);
                Direction temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        static void DisplayMaze(char[,] maze)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                for (int x = 0; x < maze.GetLength(0); x++)
                {
                    Console.Write(maze[x, y]);
                }
                Console.WriteLine();
            }
        }

        static int FindStartingY(char[,] maze)
        {
            for (int y = 1; y < maze.GetLength(1) - 1; y++)
            {
                if (maze[1, y] == ' ')
                {
                    return y;
                }
            }
            return 1;
        }

    }
}

