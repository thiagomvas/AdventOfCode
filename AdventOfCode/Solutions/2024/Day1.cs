namespace AdventOfCode.Solutions._2024;
internal class Day1 : IDaySolution
{
    public string Solve(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int[] left = new int[lines.Length];
        int[] right = new int[lines.Length];

        for(int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split("   ");
            left[i] = int.Parse(parts[0]);
            right[i] = int.Parse(parts[1]);
        }

        left = [.. left.Order()];
        right = [.. right.Order()];

        int res = 0;
        for (int i = 0; i < left.Length; i++)
        {
            res += Math.Abs(left[i] - right[i]);
        }

        return res.ToString();
    }

    public string SolvePartTwo(string input)
    {
        var lines = input.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        int[] left = new int[lines.Length];
        int[] right = new int[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split("   ");
            left[i] = int.Parse(parts[0]);
            right[i] = int.Parse(parts[1]);
        }

        var groupedRightNums = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

        int similarity = 0;
        for(int i = 0; i < left.Length; i++)
        {
            similarity += groupedRightNums.TryGetValue(left[i], out var count) ? count * left[i] : 0;
        }

        return similarity.ToString();
    }
}
