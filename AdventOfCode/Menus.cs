using System.Reflection;
using System.Text.RegularExpressions;

namespace AdventOfCode;
internal static class Menus
{
    public static Menu GetInputsForYearMenu(string year)
    {
        var inputFiles = Directory.GetFiles($"Inputs/{year}", "*.txt");

        var options = inputFiles.Select(f => new MenuOption(Path.GetFileNameWithoutExtension(f), () => Console.WriteLine(File.ReadAllText(f)))).ToList();

        return new Menu($"Advent Of Code > Input Files > {year}", options);
    }

    public static Menu GetInputYearsMenu()
    {
        var years = Directory.GetDirectories("Inputs")
            .Select(Path.GetFileNameWithoutExtension)
            .Select(d => new MenuOption(d, submenu: GetInputsForYearMenu(Path.GetFileName(d)))).ToList();
        return new Menu("Advent Of Code > Input Files", years);
    }

    public static Menu GetProblemYearsMenu()
    {
        var years = Directory.GetDirectories("Problems")
            .Select(Path.GetFileNameWithoutExtension)
            .Select(d => new MenuOption(d, submenu: GetProblemsForYearMenu(Path.GetFileName(d)))).ToList();
        return new Menu("Advent Of Code > Problems", years);
    }

    public static Menu GetProblemsForYearMenu(string year)
    {
        var problems = Directory.GetDirectories($"Problems/{year}")
            .Select(Path.GetFileNameWithoutExtension)
            .Select(d => new MenuOption(d, submenu: GetProblemMenu(year, Path.GetFileName(d)))).ToList();
        return new Menu($"Advent Of Code > Problems > {year}", problems);
    }

    public static Menu GetProblemMenu(string year, string problem)
    {
        var problemMenu = new Menu($"Advent Of Code > Problems > {year} > {problem}", new List<MenuOption>
        {
            new MenuOption("Part 1", () => Console.WriteLine(File.ReadAllText($"Problems/{year}/{problem}/1.txt"))),
            new MenuOption("Part 2", () => Console.WriteLine(File.ReadAllText($"Problems/{year}/{problem}/2.txt")))
        });
        return problemMenu;
    }

    public static Menu GetSolutionYearsMenu()
    {
        // Regex to extract the year from namespace format: AdventOfCode._YYYY
        string pattern = @"_([0-9]{4})";

        // Find all unique years from namespaces
        var yearOptions = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.FullName?.StartsWith("AdventOfCode.Solutions._") ?? false)
            .Select(t => Regex.Match(t.FullName!, pattern)) // Match the year pattern
            .Where(m => m.Success) // Only keep successful matches
            .Select(m => m.Groups[1].Value) // Extract the year (without `_`)
            .Distinct() // Ensure years are unique
            .OrderBy(y => y) // Sort years numerically
            .Select(year => new MenuOption(year, submenu: GetSolutionsForYearMenu(year))) // Create menu options
            .ToList();

        return new Menu("Advent Of Code > Solutions", yearOptions);
    }

    public static Menu GetSolutionsForYearMenu(string year)
    {
        var solutions = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t =>
                t.Namespace?.StartsWith($"AdventOfCode.Solutions._{year}") ?? false && 
                typeof(IDaySolution).IsAssignableFrom(t) && 
                !t.IsAbstract)
            .Where(t => t.Name.StartsWith("Day", StringComparison.InvariantCultureIgnoreCase))
            .Select(t => new MenuOption(
                t.Name, 
                () =>
                {
                    if (Activator.CreateInstance(t) is IDaySolution solution)
                    {
                        var input = File.ReadAllText($"Inputs/{year}/{t.Name}.txt");

                        Console.WriteLine($"Part 1: {solution.Solve(input)}");
                        Console.WriteLine($"Part 2: {solution.SolvePartTwo(input)}");
                    }
                }))
            .ToList();

        return new Menu($"Advent Of Code > Solutions > {year}", solutions);
    }

}

