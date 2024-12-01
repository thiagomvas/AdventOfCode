using AdventOfCode;

Console.WriteLine("Enter formatted yyyy/dd solution number (e.g. 2024/01):");

var input = Console.ReadLine();
if (input is null)
{
    Console.WriteLine("Invalid input.");
    return;
}

var parts = input.Split('/');

if (parts.Length != 2)
{
    Console.WriteLine("Invalid input.");
    return;
}

var year = parts[0];
var day = parts[1].TrimStart('0');

// Use reflection to get the solution
var solutionType = Type.GetType($"AdventOfCode.Solutions._{year}.Day{day}");

if (solutionType is null)
{
    Console.WriteLine("Solution not found.");
    return;
}

var solution = Activator.CreateInstance(solutionType) as IDaySolution;

if (solution is null)
{
    Console.WriteLine("Solution not found.");
    return;
}

// Read the input file if it exists
var inputfile = File.ReadAllText($"Inputs/{year}/Day{day}.txt");

var result = solution.Solve(inputfile);

Console.WriteLine(result);