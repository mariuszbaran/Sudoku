using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] board =
            {
                {0,0,0,1,7,0,0,9,0},
                {7,2,0,0,0,4,5,1,0},
                {0,3,1,9,0,0,0,7,4},
                {6,5,0,0,4,9,2,0,0},
                {0,0,4,0,0,0,9,0,0},
                {0,0,2,7,3,0,0,4,5},
                {2,4,0,0,0,7,1,5,0},
                {0,1,5,4,0,0,0,2,6},
                {0,7,0,0,1,2,0,0,0}
            };

            print(board);

            SudokuSolver sb = new SudokuSolver();
            board = sb.solve(board);

            print(board);
            Console.ReadKey();

        }

        public static void print(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
