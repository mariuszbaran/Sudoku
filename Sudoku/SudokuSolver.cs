﻿using System;
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
                Console.WriteLine(SolutionList.Count());
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


        //[Obsolete("Solve_9x9 is deprecated, please use FindAllSolutions_9x9 instead.")]
        //Solve sudoku 9 x 9 and find only one solution, even if there is more
        /// <summary>
        /// Returns solution of given board
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public bool Solve_9x9(int[,] board)
        {
            int row = -1;
            int col = -1;
            bool isSolved = true;

            //Check values in all boxes, whether empty box exists.
            //If there is any empty box then set row and column values and break both loops.
            for(int r = 0; r < 9; r++)
            {
                for(int c = 0; c < 9; c++)
                {
                    if (IsEmpty(board[r,c]))
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
            
            //If is solved (no empty boxes) return first possible solution
            if (isSolved)
            {
                return true;
            }
            else
            {
                //There are still empty boxes, find possible numbers to fill in 
                for (int num = 1; num <= 9; num++)
                {
                    if (IsPossible(board, row, col, num))
                    {

                        board[row, col] = num;
                        if (Solve_9x9(board))
                        {
                            return true;
                        }
                        else
                        {
                            board[row, col] = 0;
                        }
                    }
                }
            }
            return false;
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
            for(int c = 0; c < board.GetLength(1); c++)
            {
                if (number == board[row, c]) return true;
            }
            return false;
        }

        //Verify if the given number is present in column
        private bool IsInCol(int[,] board, int col, int number)
        {
            for(int r = 0; r < board.GetLength(0); r++)
            {
                if (number == board[r, col]) return true;
            }
            return false;
        }


        //Check if given number is present in square. Length of the board must have integer square root.
        //Boards 4x4, 9x9, 16x16 etc... are allowed. 
        private bool IsInSquare(int[,] board, int row, int col, int number)
        { 
            int sqrt = (int) Math.Sqrt(board.GetLength(0));
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
            int[,] newBoard = new int[rows,columns];
            
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    newBoard[i, j] = board[i, j];
                }
            }
            return newBoard;
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
