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

            int[,] boardEasy =
            {
                {0,0,5,0,0,9,0,0,4},//2 on 0
                {0,0,0,0,0,0,3,0,7},
                {7,0,0,8,5,6,0,1,0},//6 on 5
                {4,5,0,7,0,0,0,0,0},
                {0,0,9,0,0,0,1,0,0},
                {0,0,0,0,0,2,0,8,5},
                {0,2,0,4,1,8,0,0,6},
                {0,0,8,0,0,0,0,0,0}, //6 on 0
                {1,9,0,2,0,0,7,0,8}
            };

            int[,] boardMedium =
            {
                {0,0,6,0,9,0,2,0,0},
                {0,0,0,7,0,2,0,0,0},
                {0,9,0,5,0,8,0,7,0},
                {9,0,0,0,3,0,0,0,6},
                {7,5,0,0,0,0,0,1,9},
                {1,0,0,0,4,0,0,0,5},
                {0,1,0,3,0,9,0,8,0},
                {0,0,0,2,0,1,0,0,0},
                {0,0,9,0,8,0,1,0,0}
            };

            int[,] boardHard =
            {
                {0,0,0,8,0,0,0,0,0},
                {7,8,9,0,1,0,0,0,6},
                {0,0,0,0,0,6,1,0,0}, // 1 on 7
                {0,0,7,0,0,0,0,5,0},
                {5,0,8,7,0,9,3,0,4},
                {0,4,0,0,0,0,2,0,0},
                {0,0,3,2,0,0,0,0,0},
                {8,0,0,0,7,0,4,3,9},
                {0,0,0,0,0,1,0,0,0}
            };

            SudokuSolver ss = new SudokuSolver();
            board = boardHard;
            ss.PrintBoard(board);

            //ss.FindAllSolutions_9x9(board);
            ss.FindAllSolutions(board);

            int numberOfSolutions = ss.SolutionList.Count();
            if (numberOfSolutions == 1)
            {
                Console.WriteLine("One solution");
                ss.PrintSolutionList();
            }
            if (numberOfSolutions == 0) Console.WriteLine("No solutions");
            if (numberOfSolutions > 1)
            {
                Console.WriteLine("Too many solutions");
                ss.PrintSolutionList();
            }
         
            Console.ReadKey();


        }
    }
}
