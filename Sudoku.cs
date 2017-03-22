using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class Sudoku
    {
        private int[,] _sudoku = new int[9, 9];

        public Sudoku(int[,] sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    _sudoku[i,j] = sudoku[i,j];
                }
            }
        }

        public Sudoku Solve()
        {
            Sudoku clone = new Sudoku(_sudoku);
            Sudoku solution = clone.SolveInternal();
            if (solution == null) throw new Exception("No Solution!");
            return solution;
        }

        private Sudoku SolveInternal()
        {
            //Console.WriteLine(partialSolution);
            if (IsSolved()) return this;

            int row, col;
            if (!FindNextBlank(out row, out col))
            {
                return null;
            }

            int[] candidates = GetCandidates(row, col);
            //Console.WriteLine(candidates.ToFormattedString());
            if (candidates.Length == 0) return null;

            // try each candidate
            foreach (int candidate in candidates)
            {
                Sudoku clone = new Sudoku(_sudoku);
                clone._sudoku[row,col] = candidate;
                Sudoku attemptedSolve = clone.SolveInternal();
                if (attemptedSolve != null) return attemptedSolve;
            }

            return null;
        }

        public bool IsSolved()
        {
            for (int candidate = 1; candidate <= 9; candidate++)
            {
                for (int i = 0; i < 9; i++)
                {
                    int boxRow = i / 3;
                    int boxCol = i % 3;
                    if (!RowContains(i, candidate) || !ColumnContains(i, candidate) || !BoxContains(boxRow, boxCol, candidate))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public int[] GetCandidates(int row, int col)
        {
            List<int> candidates = new List<int>(9);

            int boxRow = row / 3;
            int boxCol = col / 3;

            for (int candidate = 1; candidate <= 9; candidate++)
            {
                if (!RowContains(row, candidate) && !ColumnContains(col, candidate) && !BoxContains(boxRow, boxCol, candidate))
                {
                    candidates.Add(candidate);
                }
            }

            return candidates.ToArray();
        }

        private bool RowContains(int row, int candidate)
        {
            for (int col = 0; col < 9; col++)
            {
                if (_sudoku[row, col] == candidate) return true;
            }
            return false;
        }

        private bool ColumnContains(int col, int candidate)
        {
            for (int row = 0; row < 9; row++)
            {
                if (_sudoku[row, col] == candidate) return true;
            }
            return false;
        }

        private bool BoxContains(int boxRow, int boxCol, int candidate)
        {
            int baseRow = boxRow * 3;
            int baseCol = boxCol * 3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_sudoku[baseRow + i,baseCol + j] == candidate) return true;
                }
            }

            return false;
        }

        private bool FindNextBlank(out int row, out int col)
        {
            row = 0;
            col = 0;
            for (row = 0; row < 9; row++)
            {
                for (col = 0; col < 9; col++)
                {
                    if (_sudoku[row,col] == 0) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" ----------------------- ");
            for (int i = 0; i < 9; i++)
            {
                if (i > 0 && i % 3 == 0) sb.AppendLine("|-------+-------+-------|");
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0) sb.Append("| ");
                    sb.Append(_sudoku[i,j]);
                    sb.Append(" ");
                }
                sb.AppendLine("|");
            }
            sb.AppendLine(" ----------------------- ");

            return sb.ToString();
        }
    }
}
