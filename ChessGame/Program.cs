using System;

namespace Chess
{

    public class Pawn
    {
        private int row;
        private int col;
        private int size;

        public Pawn(int row, int col, int size)
        {
            this.row = row;
            this.col = col;
            this.size = size;
        }

        public void PrintPossibleMoves()
        {
            // it can move forward one or two squares. If a pawn has already moved,
            // then it can move forward just one square at a time. It attacks (or captures)
            // each square diagonally to the left or right.
            bool[,] moves = GetPossibleMoves();

            Console.WriteLine("Possible moves:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (moves[i, j])
                    {
                        Console.Write($"({i + 1}, {j + 1}) ");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine();

            // Draw the chessboard with the pawn and possible moves
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == row - 1 && j == col - 1)
                    {
                        Console.Write("P ");
                    }
                    else if (moves[i, j])
                    {
                        Console.Write("x ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }
        }

        private bool[,] GetPossibleMoves()
        {
            bool[,] moves = new bool[size, size];

            // Check forward moves
            int forwardRow = row - 1;
            if (forwardRow >= 0)
            {
                moves[forwardRow, col - 1] = true;

                if (col > 1)
                {
                    moves[forwardRow, col - 2] = true;
                }

                if (col < size && forwardRow > 0)
                {
                    moves[forwardRow - 1, col] = true;
                }

                if (col > 0 && forwardRow > 0)
                {
                    moves[forwardRow - 1, col - 2] = true;
                }
            }

            return moves;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Queen's position (row, column):");
            Console.Write("Row: ");
            int row = int.Parse(Console.ReadLine());
            Console.Write("Column: ");
            int col = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter chessboard size (number of rows and columns):");
            Console.Write("Size: ");
            int size = int.Parse(Console.ReadLine());

            // Get the possible moves of the queen
            bool[,] moves = GetPossibleMoves(row - 1, col - 1, size);

            // Print the possible moves
            Console.WriteLine("Possible moves:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (moves[i, j])
                    {
                        Console.Write($"({i + 1}, {j + 1}) ");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine();

            // Draw the chessboard with the queen and possible moves
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == row - 1 && j == col - 1)
                    {
                        Console.Write("Q ");
                    }
                    else if (moves[i, j])
                    {
                        Console.Write("x ");
                    }
                    else
                    {
                        Console.Write(". ");
                    }
                }
                Console.WriteLine();
            }


            // using OOP to call Pawn:
            Console.WriteLine("Enter pawn's position (row, column):");
            Console.Write("Row: ");
            int pawnrow = int.Parse(Console.ReadLine());
            Console.Write("Column: ");
            int pawncol = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter chessboard size (number of rows and columns):");
            Console.Write("Size: ");
            //int size = int.Parse(Console.ReadLine()); // already declared

            Pawn pawn = new Pawn(row, col, size);
            pawn.PrintPossibleMoves();

        }

        static bool[,] GetPossibleMoves(int row, int col, int size)
        {
            bool[,] moves = new bool[size, size];

            // Check horizontally and vertically
            for (int i = 0; i < size; i++)
            {
                if (i != row)
                {
                    moves[i, col] = true;
                }
                if (i != col)
                {
                    moves[row, i] = true;
                }
            }

            // Check diagonally
            for (int i = row - 1, j = col - 1; i >= 0 && j >= 0; i--, j--)
            {
                moves[i, j] = true;
            }
            for (int i = row + 1, j = col + 1; i < size && j < size; i++, j++)
            {
                moves[i, j] = true;
            }
            for (int i = row - 1, j = col + 1; i >= 0 && j < size; i--, j++)
            {
                moves[i, j] = true;
            }
            for (int i = row + 1, j = col - 1; i < size && j >= 0; i++, j--)
            {
                moves[i, j] = true;
            }

            return moves;
        }
    }
}
