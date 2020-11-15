using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Sudoku
{
    public class SudokuSolver
    {
        public List<int[,]> SolutionList { get => solutionList; }
        private List<int[,]> solutionList = new List<int[,]>();

        public SudokuSolver()
        {

        }

        //Find all solutions for different size boards and store them in solutionList
        /// <summary>
        /// Finds all solutions and store them in SolutionList
        /// </summary>
        /// <param name="board"></param>
        public void FindAllSolutions(int[,] board)
        {
            int boardLength = board.GetLength(0);
            int row = -1;
            int col = -1;
            bool isSolved = true;

            //Check values in all boxes, whether empty box exists.
            //If there is any empty box then set row and column values and break both loops.
            for (int r = 0; r < boardLength; r++)
            {
                for (int c = 0; c < boardLength; c++)
                {
                    if (IsEmpty(board[r, c]))
                    {
                        isSolved = false;
                        row = r;
                        col = c;
                        break;
                    }
                }
                if (!isSolved)
                {
                    break;
                }
            }

            //If is solved (no empty boxes) add solution to the list
            if (isSolved)
            {
                SolutionList.Add(CloneBoard(board));
                //Console.WriteLine(SolutionList.Count());
            }
            else
            {
                //There are still empty boxes, find possible numbers to fill in 
                for (int num = 1; num <= boardLength; num++)
                {
                    if (IsPossible(board, row, col, num))
                    {
                        board[row, col] = num;
                        FindAllSolutions(board);
                        //reset box for next try
                        board[row, col] = 0;
                    }
                }

            }
        }

        /// <summary>
        /// Finds specified number of solutions and stores in Solution List
        /// </summary>
        /// <param name="board"></param>
        /// <param name="numberOfSolutions"></param>
        public void FindSolutions(int[,] board, List<int[,]> list, int numberOfSolutions)
        {
            int boardLength = board.GetLength(0);
            int row = -1;
            int col = -1;
            bool isSolved = true;

            //Check values in all boxes, whether empty box exists.
            //If there is any empty box then set row and column values and break both loops.
            for (int r = 0; r < boardLength; r++)
            {
                for (int c = 0; c < boardLength; c++)
                {
                    if (IsEmpty(board[r, c]))
                    {
                        isSolved = false;
                        row = r;
                        col = c;
                        break;
                    }
                }
                if (!isSolved)
                {
                    break;
                }
            }

            //If is solved (no empty boxes) add solution to the list
            if (isSolved)
            {
                list.Add(CloneBoard(board));
            }
            else
            {
                if (list.Count() < numberOfSolutions)
                {
                    //There are still empty boxes, find possible numbers to fill in 
                    for (int num = 1; num <= boardLength; num++)
                    {
                        if (IsPossible(board, row, col, num))
                        {
                            board[row, col] = num;
                            FindSolutions(board, list, numberOfSolutions);
                            //reset box for next try
                            board[row, col] = 0;
                        }
                    }
                }

            }
        }

        /// <summary>
        /// Returns random, correct board of given length filled with numbers and a given number of empty cells.
        /// Returns null if there is more solutions than only one for given number of empty cells.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="emptyCells"></param>
        /// <returns></returns>
        public int[,] Generate(int length, int emptyCells)
        {
            int[,] newBoard;
            //Try to find correct board with given number of empty cells, if not possible, decrease number of empty cells and repeat.
            for(int ec = emptyCells; ec >= 0; ec--)
            {
                Console.WriteLine("Generating Board {0}x{0} with {1} empty cells.",length,ec);
                //Try to find correct board in a number of attempts.
                int attempts = 100;
                for (int i = 0; i < attempts; i++)
                {
                    newBoard = RemoveNumbers(GenerateFull(length), ec);
                    if (IsCorrect(newBoard)) return newBoard;
                }
                Console.WriteLine("Generating Board {0}x{0} with {1} empty cells - FAIL.", length, ec);
            }
            throw new Exception("Not Possible to create correct board");
        }

        /// <summary>
        /// Returns random, correct board of given length with numbers.
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public int[,] GenerateFull(int length)
        {
            if (!IsProperSize(length))
            {
                throw new System.ArgumentException();
            }
            List<int[,]> solutions = new List<int[,]>();
            //Create board and fillup with empty cells (zeros).
            int[,] board = new int[length, length];
            for (int r = 0; r < length; r++)
            {
                for (int c = 0; c < length; c++)
                {
                    board[r, c] = 0;
                }
            }
            //create array of numbers.
            int[] numberArray = new int[length];
            //fill array with cumbers.
            for (int i = 0; i < length; i++)
            {
                numberArray[i] = i + 1;
            }
            //fill up three (top left, center, bottom right) squares with randomized numbers.
            int sqrt = (int)Math.Sqrt(length);
            for (int i = 0; i < length; i += sqrt)
            {
                int index = 0;
                numberArray = Shuffle(numberArray);
                for (int r = i; r < i + sqrt; r++)
                {
                    for (int c = i; c < i + sqrt; c++)
                    {
                        board[r, c] = numberArray[index++];
                    }
                }
            }
            FindSolutions(board, solutions, 100);
            Random rand = new Random();
            return solutions[rand.Next(solutions.Count())];
        }
        //=====================================================change to  remove one number and check if correct 
        //Remove random numbers.
        public int[,] RemoveNumbers(int[,] board, int emptyCells)
        {
            if (emptyCells > (board.GetLength(0) * board.GetLength(0))) throw new ArgumentOutOfRangeException();
            
            int[,] newBoard = CloneBoard(board);
            int counter = 0;
            int row = 0;
            Random rand = new Random();
            while(counter < emptyCells)
            {
                if (row == newBoard.GetLength(0)) row = 0;
                int col = rand.Next(newBoard.GetLength(0));
                while(IsEmpty(newBoard[row, col]))
                {
                    col++;
                    if (col == newBoard.GetLength(0)) col = 0;
                }
                newBoard[row, col] = 0;
                counter++;
                row++;
            }
            return newBoard;
        }

        //Varify whether given board is a correct sudoku board - has only one solution.
        public bool IsCorrect(int[,] board)
        {
            List<int[,]> solutions = new List<int[,]>();
            FindSolutions(board, solutions, 2);
            if (solutions.Count() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Verify if a square of given length is a perfect square.
        private bool IsProperSize(int length)
        {
            double sqrt = Math.Sqrt((double)length);
            return ((sqrt - Math.Floor(sqrt)) == 0);
        }

        //Chceck whether given cell is empty or not (if equals 0)
        private bool IsEmpty(int number)
        {
            return (number == 0) ? true : false;
        }

        //Check if the given number can be placed in cell,
        //if the same number is present in coresponding row, column or square then it returns false.
        private bool IsPossible(int[,] board, int row, int col, int number)
        {
            if (IsInRow(board, row, number)) return false;
            if (IsInCol(board, col, number)) return false;
            if (IsInSquare(board, row, col, number)) return false;
            return true;
        }

        //Verify if the given number is present in row
        private bool IsInRow(int[,] board, int row, int number)
        {
            for (int c = 0; c < board.GetLength(1); c++)
            {
                if (number == board[row, c]) return true;
            }
            return false;
        }

        //Verify if the given number is present in column
        private bool IsInCol(int[,] board, int col, int number)
        {
            for (int r = 0; r < board.GetLength(0); r++)
            {
                if (number == board[r, col]) return true;
            }
            return false;
        }


        //Check if given number is present in square. Length of the board must have integer square root.
        //Boards 4x4, 9x9, 16x16 etc... are allowed. 
        private bool IsInSquare(int[,] board, int row, int col, int number)
        {
            int sqrt = (int)Math.Sqrt(board.GetLength(0));
            int tempRow = row / sqrt * sqrt;
            int tempCol = col / sqrt * sqrt;

            for (int r = tempRow; r < tempRow + sqrt; r++)
            {
                for (int c = tempCol; c < tempCol + sqrt; c++)
                {
                    if (number == board[r, c]) return true;
                }
            }
            return false;
        }

        //Clone 2d array of integers
        public int[,] CloneBoard(int[,] board)
        {
            int rows = board.GetLength(0);
            int columns = board.GetLength(1);
            int[,] newBoard = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    newBoard[i, j] = board[i, j];
                }
            }
            return newBoard;
        }

        private int[] Shuffle(int[] array)
        {
            int tempIndex = -1;
            int tempValue = -1;
            Random rand = new Random();

            for(int index = 0; index < array.Length; index++)
            {
                tempIndex = rand.Next(array.Length);
                tempValue = array[index];
                array[index] = array[tempIndex];
                array[tempIndex] = tempValue;
            }
            return array;
        }

        //Print board to console
        /// <summary>
        /// Prints board given as parameter
        /// </summary>
        /// <param name="board"></param>
        public void PrintBoard(int[,] board)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        //Print all solutions to console
        /// <summary>
        /// Prints list of solutions to console
        /// </summary>
        public void PrintSolutionList()
        {
            for(int i =0; i< SolutionList.Count; i++)
            {
                Console.WriteLine("Solution " + (i + 1));
                PrintBoard(SolutionList[i]);
            }
        }
    }
}
