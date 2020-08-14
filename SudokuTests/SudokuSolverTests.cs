﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sudoku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Tests
{
    [TestClass()]
    public class SudokuSolverTests
    {
        bool compare(int[,] b0, int[,] b1)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (b0[i, j] != b1[i, j]) return false;
                }
            }
            return true;
        }



        [TestMethod()]
        public void solveTestEasy()
        {
            //Arrange
            int[,] originalBoard =
            {
                {2,0,5,0,0,9,0,0,4},
                {0,0,0,0,0,0,3,0,7},
                {7,0,0,8,5,6,0,1,0},
                {4,5,0,7,0,0,0,0,0},
                {0,0,9,0,0,0,1,0,0},
                {0,0,0,0,0,2,0,8,5},
                {0,2,0,4,1,8,0,0,6},
                {6,0,8,0,0,0,0,0,0},
                {1,0,0,2,0,0,7,0,8}
            };

            int[,] expectedBoard =
            {
                {2,1,5,3,7,9,8,6,4},
                {9,8,6,1,2,4,3,5,7},
                {7,3,4,8,5,6,2,1,9},
                {4,5,2,7,8,1,6,9,3},
                {8,6,9,5,4,3,1,7,2},
                {3,7,1,6,9,2,4,8,5},
                {5,2,7,4,1,8,9,3,6},
                {6,4,8,9,3,7,5,2,1},
                {1,9,3,2,6,5,7,4,8}
            };

            int[,] resultBoard;

            SudokuSolver ss = new SudokuSolver();

            //Act
            ss.Solve_9x9(originalBoard);
            resultBoard = originalBoard;


            //Assert
            Assert.IsTrue(compare(resultBoard, expectedBoard));
        }

        [TestMethod()]
        public void solveTestMedium()
        {
            //Arrange
            int[,] originalBoard =
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

            int[,] expectedBoard =
            {
                {8,7,6,4,9,3,2,5,1},
                {3,4,5,7,1,2,9,6,8},
                {2,9,1,5,6,8,4,7,3},
                {9,8,2,1,3,5,7,4,6},
                {7,5,4,8,2,6,3,1,9},
                {1,6,3,9,4,7,8,2,5},
                {4,1,7,3,5,9,6,8,2},
                {6,3,8,2,7,1,5,9,4},
                {5,2,9,6,8,4,1,3,7}
            };

            int[,] resultBoard;

            SudokuSolver ss = new SudokuSolver();

            //Act
            ss.Solve_9x9(originalBoard);
            resultBoard = originalBoard;


            //Assert
            Assert.IsTrue(compare(resultBoard, expectedBoard));
        }


        [TestMethod()]
        public void solveTestHard()
        {
            //Arrange
            int[,] originalBoard =
            {
                {0,0,0,8,0,0,0,0,0},
                {7,8,9,0,1,0,0,0,6},
                {0,0,0,0,0,6,1,0,0},
                {0,0,7,0,0,0,0,5,0},
                {5,0,8,7,0,9,3,0,4},
                {0,4,0,0,0,0,2,0,0},
                {0,0,3,2,0,0,0,0,0},
                {8,0,0,0,7,0,4,3,9},
                {0,0,0,0,0,1,0,0,0}
            };

            int[,] expectedBoard =
            {
                {1,6,5,8,4,7,9,2,3},
                {7,8,9,3,1,2,5,4,6},
                {4,3,2,5,9,6,1,7,8},
                {2,9,7,4,6,3,8,5,1},
                {5,1,8,7,2,9,3,6,4},
                {3,4,6,1,5,8,2,9,7},
                {9,7,3,2,8,4,6,1,5},
                {8,2,1,6,7,5,4,3,9},
                {6,5,4,9,3,1,7,8,2}
            };

            int[,] resultBoard;

            SudokuSolver ss = new SudokuSolver();

            //Act
            ss.Solve_9x9(originalBoard);
            resultBoard = originalBoard;


            //Assert
            Assert.IsTrue(compare(resultBoard, expectedBoard));
        }
    }
}