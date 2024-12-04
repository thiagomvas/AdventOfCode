using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions._2024;
internal class Day4 : IDaySolution
{
    public string Solve(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(l => l.Replace("\r", "")).ToArray();
        char[][] matrix = new char[lines.Length][];
        int result = 0;

        for (int line = 0; line < lines.Length; line++)
        {
            matrix[line] = new char[lines[line].Length];
            for (int column = 0; column < lines[line].Length; column++)
            {
                matrix[line][column] = lines[line][column];
            }
        }

        for (int line = 0; line < lines.Length; line++)
        {
            for (int column = 0; column < lines[line].Length; column++)
            {
                if (matrix[line][column] == 'X')
                    result += CountXmasPartOne(matrix, line, column);
            }
        }

        return result.ToString();
    }

    public string SolvePartTwo(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries).Select(l => l.Replace("\r", "")).ToArray();
        char[][] matrix = new char[lines.Length][];
        int result = 0;

        for (int line = 0; line < lines.Length; line++)
        {
            matrix[line] = new char[lines[line].Length];
            for (int column = 0; column < lines[line].Length; column++)
            {
                matrix[line][column] = lines[line][column];
            }
        }

        for (int line = 0; line < lines.Length; line++)
        {
            for (int column = 0; column < lines[line].Length; column++)
            {
                if (matrix[line][column] == 'M' || matrix[line][column] == 'S')
                    result += CountXmasPartTwo(matrix, line, column);
            }
        }

        return result.ToString();
    }

    private static int CountXmasPartOne(char[][] m, int l, int c)
    {
        if (m[l][c] != 'X') return 0;

        int count = 0;
        // Right
        if (m[l].Length > c + 3 && m[l][c + 1] is 'M' && m[l][c + 2] is 'A' && m[l][c + 3] is 'S')
            count++;

        // Bottom
        if (m.Length > l + 3 && m[l + 1][c] is 'M' && m[l + 2][c] is 'A' && m[l + 3][c] is 'S')
            count++;

        // Top
        if (l - 3 >= 0 && m[l - 1][c] is 'M' && m[l - 2][c] is 'A' && m[l - 3][c] is 'S')
            count++;

        // Left
        if (c - 3 >= 0 && m[l][c - 1] is 'M' && m[l][c - 2] is 'A' && m[l][c - 3] is 'S')
            count++; 

        // Top Left
        if(c - 3 >= 0 && l - 3 >= 0 && m[l - 1][c - 1] is 'M' && m[l - 2][c - 2] is 'A' && m[l - 3][c - 3] is 'S')
            count++;

        // Top Right 
        if(l - 3 >= 0 &&
            m[l - 1].Length > c + 1 &&  m[l - 1][c + 1] is 'M'
            && m[l - 2].Length > c + 2 && m[l - 2][c + 2] is 'A'
            && m[l - 3].Length > c + 3 && m[l - 3][c + 3] is 'S')
            count++;

        // Bottom Left
        if (c - 3 >= 0 && m.Length > l + 3
            && m[l + 1][c - 1] is 'M'
            && m[l + 2][c - 2] is 'A'
            && m[l + 3][c - 3] is 'S')
            count++;

        // Bottom Right
        if (m.Length > l + 3
            && m[l + 1].Length > c + 1 && m[l + 1][c + 1] is 'M'
            && m[l + 2].Length > c + 2 && m[l + 2][c + 2] is 'A'
            && m[l + 3].Length > c + 3 && m[l + 3][c + 3] is 'S')
            count++;

        return count;

    }

    private static int CountXmasPartTwo(char[][] m, int l, int c)
    {
        if (m[l][c] != 'M' && m[l][c] != 'S') return 0;

        int count = 0;

        // Always check towards bottom-right to prevent counting twice

        // M.S
        // .A.
        // M.S
        if (m.Length > l + 2
            && m[l][c] is 'M'
            && m[l + 1].Length > c + 1 && m[l + 1][c + 1] is 'A'
            && m[l + 2].Length > c + 2 && m[l + 2][c + 2] is 'S'
            && m[l][c + 2] is 'S'
            && m[l + 2][c] is 'M')
            count++;

        // S.S
        // .A.
        // M.M
        if (m.Length > l + 2
            && m[l][c] is 'S'
            && m[l + 1].Length > c + 1 && m[l + 1][c + 1] is 'A'
            && m[l + 2].Length > c + 2 && m[l + 2][c + 2] is 'M'
            && m[l][c + 2] is 'S'
            && m[l + 2][c] is 'M')
            count++;

        // S.M
        // .A.
        // S.M
        if (m.Length > l + 2
            && m[l][c] is 'S'
            && m[l + 1].Length > c + 1 && m[l + 1][c + 1] is 'A'
            && m[l + 2].Length > c + 2 && m[l + 2][c + 2] is 'M'
            && m[l][c + 2] is 'M'
            && m[l + 2][c] is 'S')
            count++;

        // M.M
        // .A.
        // S.S

        if (m.Length > l + 2
            && m[l][c] is 'M'
            && m[l + 1].Length > c + 1 && m[l + 1][c + 1] is 'A'
            && m[l + 2].Length > c + 2 && m[l + 2][c + 2] is 'S'
            && m[l][c + 2] is 'M'
            && m[l + 2][c] is 'S')
            count++;


        return count;
    }
}
