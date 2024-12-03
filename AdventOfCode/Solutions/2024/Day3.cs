using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions._2024;
internal class Day3 : IDaySolution
{

    private const string nums = "1234567890";
    public string Solve(string input)
    {
        var stack = new Stack<char>();
        bool onSecondNum = false;
        int result = 0;

        foreach (char c in input)
        {
            // Push valid function prefix characters
            if (stack.Count < 4 && MatchesFunctionPrefix(stack.Count, c))
            {
                stack.Push(c);
                continue;
            }

            // Reset the stack if prefix is incomplete
            if (stack.Count < 4)
            {
                stack.Clear();
                continue;
            }

            // Push the first number
            if (!onSecondNum && IsNumber(c))
            {
                stack.Push(c);
                continue;
            }

            // Transition to the second number after a comma
            if (stack.Count > 4 && c == ',')
            {
                stack.Push(c);
                onSecondNum = true;
                continue;
            }

            // Push the second number or evaluate the expression
            if (onSecondNum)
            {
                if (IsNumber(c))
                {
                    stack.Push(c);
                    continue;
                }

                if (c == ')')
                {
                    stack.Push(c);
                    result += Evaluate(new string(stack.Reverse().ToArray()));
                }
            }

            // Malformed expression
            stack.Clear();
        }

        return result.ToString();
    }

    public string SolvePartTwo(string input)
    {
        var stack = new Stack<char>();
        bool onSecondNum = false;
        int result = 0;
        bool canEval = true; // Multiplications are enabled by default.

        foreach (char c in input)
        {
            // Push valid function prefix characters for "mul()"
            if (stack.Count < 4 && MatchesFunctionPrefix(stack.Count, c))
            {
                stack.Push(c);
                continue;
            }

            // Handle "do()" and "don't()" instructions
            if (MatchesToggleFunction(stack.Count, c))
            {
                stack.Push(c);

                // Evaluate toggle function when complete
                if (stack.Count >= 4 && TryEvaluateToggle(new string(stack.Reverse().ToArray()), out var toggle))
                {
                    canEval = toggle;
                    stack.Clear();    // Clear stack after handling toggle instruction.
                }

                continue;
            }

            // Reset the stack if prefix for "mul()" is incomplete
            if (stack.Count < 4)
            {
                stack.Clear();
                continue;
            }

            // Push the first number for "mul(x, y)"
            if (!onSecondNum && IsNumber(c))
            {
                stack.Push(c);
                continue;
            }

            // Transition to the second number after a comma
            if (stack.Count > 4 && c == ',')
            {
                stack.Push(c);
                onSecondNum = true;
                continue;
            }

            // Push the second number or evaluate the "mul()" instruction
            if (onSecondNum)
            {
                if (IsNumber(c))
                {
                    stack.Push(c);
                    continue;
                }

                if (c == ')')
                {
                    stack.Push(c);

                    // Evaluate "mul()" only if enabled
                    if (canEval)
                        result += Evaluate(new string(stack.Reverse().ToArray()));

                    stack.Clear(); // Clear stack after handling "mul()".
                }
            }

            // Clear stack for malformed expressions
            stack.Clear();
        }

        return result.ToString();
    }

    private bool TryEvaluateToggle(string expression, out bool toggled)
    {
        if (expression == "do()")
        {
            toggled = true;
            return true;
        }
        else if (expression == "don't()")
        {
            toggled = false;
            return true;
        }
        toggled = false;
        return false;

    }
    private static bool MatchesFunctionPrefix(int position, char c) =>
        position switch
        {
            0 => c == 'm',
            1 => c == 'u',
            2 => c == 'l',
            3 => c == '(',
            _ => false
        };

    private static bool MatchesToggleFunction(int position, char c) =>
    position switch
    {
        0 => c == 'd',
        1 => c == 'o',
        2 => c == 'n' || c == '(',
        3 => c == '\'' || c == ')',
        4 => c == 't',
        5 => c == '(',
        6 => c == ')',
        _ => false
    };
    private static bool IsNumber(char c) => char.IsDigit(c);
    private int Evaluate(string expression)
    {
        var input = expression.Substring("mul(".Length, expression.Length - "mul(".Length - 1);
        var nums = input.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        return nums[0] * nums[1];
    }

}
