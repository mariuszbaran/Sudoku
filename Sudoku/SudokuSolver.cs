using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class SudokuSolver
    {

        public SudokuSolver()
        {

        }

        public int[,] solve(int[,] oBoard)
        {
            int[,] board = oBoard;

            for(int r = 0; r < 9; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    if(board[r,c] == 0) // the cell is empty 
                    {
                        for(int number = 1; number < 10; number++)
                        {
                            if (IsPossible(board, r, c, number))
                            {
                                board[r, c] = number;
                                board = solve(board);
                            }
                        }
                    }
                }
            }
            return board;
        }

        //Checks if the given number can be placed in cell,
        //if the same number is present in coresponding row, column or square then it returns false.
        private bool IsPossible(int[,] board, int row, int col, int number)
        {
            if (IsInRow(board, row, number)) return false;
            if (IsInCol(board, col, number)) return false;
            if (IsInSquare(board, row, col, number)) return false;
            return true;
        }

        private bool IsInRow(int[,] board, int row, int number)
        {
            for(int c = 0; c < 9; c++)
            {
                if (number == board[row, c]) return true;
            }
            return false;
        }

        private bool IsInCol(int[,] board, int col, int number)
        {
            for(int r = 0; r < 9; r++)
            {
                if (number == board[r, col]) return true;
            }
            return false;
        }

        private bool IsInSquare(int[,] board, int row, int col, int number)
        {
            int tempRow = row / 3 * 3;
            int tempCol = col / 3 * 3;

            for (int r = tempRow; r < 3; r++)
            {
                for(int c = tempCol; c < 3; c++)
                {
                    if (number == board[r, c]) return true;
                }
            }
            return false;
        }
    }
}
