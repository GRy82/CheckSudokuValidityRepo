using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckSudokuValidity
{
    class Program
    {
        public static Dictionary<int[], bool> threeGridsValidated;
        public const int singleDimensionLength = 9;

        static void Main(string[] args)
        {
            char[,] board = new char[,] {
                { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                  { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                  { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                  { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                  { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                  { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                  { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                  { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                  { '.', '.', '.', '.', '8', '.', '.', '7', '9'}};
            char[,] board2 = new char[,] {
                { '5', '3', '.', '5', '7', '.', '.', '.', '.' },
                  { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                  { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                  { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                  { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                  { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                  { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                  { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                  { '.', '.', '.', '.', '8', '.', '.', '7', '9'}};
            char[,] board3 = new char[,] {
                { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                  { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                  { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                  { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                  { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                  { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                  { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                  { '.', '.', '.', '4', '1', '9', '.', '9', '5' },
                  { '.', '.', '.', '.', '8', '.', '.', '7', '9'}};
            char[,] board4 = new char[,] {
                { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                  { '5', '.', '.', '1', '9', '5', '.', '.', '.' },
                  { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                  { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                  { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                  { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                  { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                  { '.', '.', '.', '4', '1', '9', '.', '9', '5' },
                  { '.', '.', '.', '.', '8', '.', '.', '7', '9'}};
            char[,] board5 = new char[,] {
                  { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                  { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                  { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                  { '4', '.', '.', '8', '.', '3', '.', '.', '1' },
                  { '7', '.', '.', '.', '2', '.', '.', '.', '6' },
                  { '.', '6', '.', '.', '.', '.', '2', '8', '.' },
                  { '.', '.', '.', '4', '1', '9', '.', '.', '5' },
                  { '.', '.', '.', '.', '8', '.', '.', '7', '9'}};
            char[,] board6 = new char[,] {
                  { '6', '.', '.', '1', '9', '5', '.', '.'},
                  { '.', '9', '8', '.', '.', '.', '.', '6'},
                  { '8', '.', '.', '.', '6', '.', '.', '.'},
                  { '4', '.', '.', '8', '.', '3', '.', '.'},
                  { '7', '.', '.', '.', '2', '.', '.', '.'},
                  { '.', '6', '.', '.', '.', '.', '2', '8'},
                  { '.', '.', '.', '4', '1', '9', '.', '.'},
                  { '.', '.', '.', '.', '8', '.', '.', '7'}};
            char[,] board7 = new char[,] {
                  { '6', '.', '.' },
                  { '.', '9', '8'},
                  { '8', '.', '.'} };

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("1. " + IsValidSudoku(board));
            Console.WriteLine("2. " + IsValidSudoku(board2));
            Console.WriteLine("3. " + IsValidSudoku(board3));
            Console.WriteLine("4. " + IsValidSudoku(board4));
            Console.WriteLine("5. " + IsValidSudoku(board5));
            Console.WriteLine("6. " + IsValidSudoku(board6));
            Console.WriteLine("7. " + IsValidSudoku(board7));
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds + "ms elapsed.");
            Console.ReadLine();
        }

        public static bool IsValidSudoku(char[,] board)
        {
            var dimensionLength = Math.Sqrt(board.Length);
            if (dimensionLength != singleDimensionLength) return false;
            threeGridsValidated = new Dictionary<int[], bool> { };

            return IsValidColumn(board, 0, singleDimensionLength) && IsValidRow(board, 0, singleDimensionLength)
                && IsValidThreeGrid(board, 0, 0, singleDimensionLength);
        }

        public static bool IsValidColumn(char[,] board, int column, int dimensionLength)
        {
            if (column >= dimensionLength) return true;
            List<char> numbersSeen = new List<char> { };
            for (int i = 0; i < dimensionLength; i++)
            {
                if (numbersSeen.Contains(board[i, column]))
                    return false;
                if (board[i, column] != '.')
                    numbersSeen.Add(board[i, column]);
            }
            return IsValidColumn(board, column + 1, dimensionLength);
        }
        public static bool IsValidRow(char[,] board, int row, int dimensionLength)
        {
            if (row >= dimensionLength) return true;
            List<char> numbersSeen = new List<char> { };
            for (int i = 0; i < dimensionLength; i++)
            {
                if (numbersSeen.Contains(board[row, i]))
                    return false;
                if (board[row, i] != '.')
                    numbersSeen.Add(board[row, i]);
            }
            return IsValidRow(board, row + 1, dimensionLength);
        }

        public static bool IsValidThreeGrid(char[,] board, int row, int column, int dimensionLength)
        {
            if (row >= dimensionLength || column >= dimensionLength) return true;
            int[] currentThreeGrid = new int[] { row, column };
            if (threeGridsValidated.TryGetValue(currentThreeGrid, out bool value)) return true;
            List<char> numbersSeen = new List<char> { };
            int lastRow = row + 3;
            int lastColumn = column + 3;
            for (int i = row; i < lastRow; i++)
            {
                for (int j = column; j < lastColumn; j++)
                {
                    if (numbersSeen.Contains(board[i, j]))
                        return false;
                    if (board[i, j] != '.')
                    {
                        numbersSeen.Add(board[i, j]);
                    }
                }
            }
            threeGridsValidated.Add(currentThreeGrid, true);

            return IsValidThreeGrid(board, lastRow, column, dimensionLength) && IsValidThreeGrid(board, row, lastColumn, dimensionLength);
        }

        //public static bool IsValidSudoku(char[,] board)
        //{
        //    List<char> numbersSeenRows = new List<char> { };
        //    List<char> numbersSeenColumns = new List<char> { };
        //    for (int j = 0; j < 9; j++)
        //    {
        //        for (int i = 0; i < 9; i++)
        //        {
        //            if (numbersSeenColumns.Contains(board[j,i]))
        //                return false;
        //            if(board[j, i] != '.')
        //                numbersSeenColumns.Add(board[j,i]);
        //            if (numbersSeenRows.Contains(board[i,j]))
        //                return false;
        //            if (board[i, j] != '.')
        //                numbersSeenRows.Add(board[i,j]);
        //        }
        //        numbersSeenColumns = new List<char> { };
        //        numbersSeenRows = new List<char> { };
        //    }

        //    List<char> numbersSeenSectional = new List<char> { };
        //    for (int i = 0; i < 9; i += 3)
        //    {
        //        int lastIndexRow = i + 2;
        //        for (int j = 0; j < 9; j += 3)
        //        {
        //            int lastIndexColumn = j + 2;
        //            for (int k = i; k <= lastIndexRow; k++)
        //            {
        //                for (int l = j; l <= lastIndexColumn; l++)
        //                {
        //                    if (numbersSeenSectional.Contains(board[k,l]))
        //                        return false;
        //                    if (board[k, l] != '.')
        //                        numbersSeenSectional.Add(board[k,l]);
        //                }
        //            }
        //            numbersSeenSectional = new List<char> { };
        //        }
        //    }
        //    return true;
        //}
    }
}
