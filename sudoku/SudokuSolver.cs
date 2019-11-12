using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sudoku
{
    public class SudokuSolver
    {
        public static bool isSafe(int[,] board,
                            int row, int col,
                            int num)
        {
            // row has the unique (row-clash) 
            for (int d = 0; d < board.GetLength(0); d++)
            {
                // if the number we are trying to  
                // place is already present in  
                // that row, return false; 
                if (board[row, d] == num)
                {
                    return false;
                }
            }

            // column has the unique numbers (column-clash) 
            for (int r = 0; r < board.GetLength(0); r++)
            {
                // if the number we are trying to 
                // place is already present in 
                // that column, return false; 
                if (board[r, col] == num)
                {
                    return false;
                }
            }

            // corresponding square has 
            // unique number (box-clash) 
            int sqrt = (int)Math.Sqrt(board.GetLength(0));
            int boxRowStart = row - row % sqrt;
            int boxColStart = col - col % sqrt;

            for (int r = boxRowStart;
                    r < boxRowStart + sqrt; r++)
            {
                for (int d = boxColStart;
                        d < boxColStart + sqrt; d++)
                {
                    if (board[r, d] == num)
                    {
                        return false;
                    }
                }
            }

            // if there is no clash, it's safe 
            return true;
        }

        public static bool solveSudoku(int[,] board, int n)
        {
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (board[i, j] == 0)
                    {
                        row = i;
                        col = j;

                        // we still have some remaining 
                        // missing values in Sudoku 
                        isEmpty = false;
                        break;
                    }
                }
                if (!isEmpty)
                {
                    break;
                }
            }

            // no empty space left 
            if (isEmpty)
            {
                return true;
            }

            // else for each-row backtrack 
            for (int num = 1; num <= n; num++)
            {
                if (isSafe(board, row, col, num))
                {
                    board[row, col] = num;
                    if (solveSudoku(board, n))
                    {
                        // print(board, n); 
                        return true;
                    }
                    else
                    {
                        board[row, col] = 0; // replace it 
                    }
                }
            }
            return false;
        }

        public static int[,] ConvertBoard(string board)
        {
            var col = 0;
            var row = 0;
            char[,] tempboard = new char[9, 9];
            int[,] newboard = new int[9, 9];
            foreach (char value in board)
            {
                if (col != 9 && row != 9)
                {
                    tempboard[row, col] = value;
                    col += 1;
                }
                else if (col == 9 && row == 8)
                {
                    break;
                }
                else
                {
                    row += 1;
                    col = 0;
                    tempboard[row, col] = value;
                    col += 1;
                }
            }
            for (int i = 0; i < newboard.GetLength(0); i++)
            {
                for (int j = 0; j < newboard.GetLength(1); j++)
                {
                    newboard[i, j] = Convert.ToInt32(tempboard[i, j].ToString());
                }
            }
            return newboard;
        }

        public static string DisplayBoard(int[,] board,int N)
        {
            var boardString = "";
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    boardString += board[i, j].ToString();
                    
                }
            }
            return boardString;
        }
    }
}
