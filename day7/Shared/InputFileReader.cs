using Shared.Records;

namespace Shared;

public static class InputFileReader
{
    public static IEnumerable<Hand> ParseFile(string filePath, IReadOnlyDictionary<char, int> labelStrengths)
    {
        var lines = File.ReadAllLines(filePath);
        return lines.Select(line => ParseToHand(line, labelStrengths));
    }

    private static Hand ParseToHand(string line, IReadOnlyDictionary<char, int> labelStrengths)
    {
        var splitLine = line.Split(' ');
        var values = splitLine[0]
            .Select(label => labelStrengths[label])
            .ToArray();

        return new Hand(values[0], values[1],
            values[2], values[3],
            values[4], int.Parse(splitLine[1]));
    }
}
