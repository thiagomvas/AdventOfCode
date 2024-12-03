using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions._2024;
internal class Day2 : IDaySolution
{
    private const long minDelta = 1;
    private const long maxDelta = 3;
    public string Solve(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        var reports = lines.Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select(r => r.Select(l => long.Parse(l)).ToArray());

        long safe = 0;

        foreach (var report in reports)
        {
            if (IsReportSafe(report))
                safe++;
        }

        return safe.ToString();
    }
    public string SolvePartTwo(string input)
    {
        var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

        var reports = lines.Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select(r => r.Select(l => long.Parse(l)).ToArray());

        long safe = 0;

        foreach (var report in reports)
        {
            if (IsReportSafe(report))
            {
                safe++;
                continue;
            }

            // Try removing one level and check if it becomes safe
            bool isSafeWithRemoval = false;
            for (int i = 0; i < report.Length; i++)
            {
                var reducedReport = report.Where((_, index) => index != i).ToArray();
                if (IsReportSafe(reducedReport))
                {
                    isSafeWithRemoval = true;
                    break;
                }
            }

            if (isSafeWithRemoval)
                safe++;
        }

        return safe.ToString();
    }

    private bool IsReportSafe(long[] report)
    {
        if (report.Length < 2) return true; // A report with fewer than 2 levels is always safe

        bool increasing = report[1] > report[0];
        for (int i = 1; i < report.Length; i++)
        {
            var delta = report[i] - report[i - 1];
            if ((increasing && delta < 0)
                || (!increasing && delta > 0)
                || (Math.Abs(delta) < 1 || Math.Abs(delta) > 3))
            {
                return false;
            }
        }
        return true;
    }

}
