using Shared;

namespace Part2;

internal static class Program
{
    public static void Main()
    {
        var allRows = File.ReadAllLines("data/input.txt");

        HashSet<HashSet<Direction>[][]> allEnergizedTiles =
            [..SendBeamsDown(allRows), ..SendBeamsUp(allRows), ..SendBeamsRight(allRows), ..SendBeamsLeft(allRows)];
        
        var max = allEnergizedTiles
            .Select(energizedTiles => energizedTiles
                .Sum(tile => tile
                    .Count(t => t.Count > 0)))
            .Max();
        
        Console.WriteLine(max);
    }
    
    private static HashSet<HashSet<Direction>[][]> SendBeamsDown(string[] allRows)
    {
        return allRows.First().Select((mirror, i) =>
        {
            HashSet<Beam> beams = mirror switch
            {
                '.' or '|' => [new Beam(new Coords(i, 0), Direction.Down)],
                '-' => [new Beam(new Coords(i, 0), Direction.Left), new Beam(new Coords(i, 0), Direction.Right)],
                '\\' => [new Beam(new Coords(i, 0), Direction.Right)],
                '/' => [new Beam(new Coords(i, 0), Direction.Left)],
                _ => throw new Exception("Invalid start")
            };
            return Contraption.SendBeams(allRows, beams);
        })
        .ToHashSet();
    }

    private static HashSet<HashSet<Direction>[][]> SendBeamsUp(string[] allRows)
    {
        return allRows.Last().Select((mirror, i) =>
        {
            HashSet<Beam> beams = mirror switch
            {
                '.' or '|' => [new Beam(new Coords(i, allRows.Length - 1), Direction.Up)],
                '-' =>
                [
                    new Beam(new Coords(i, allRows.Length - 1), Direction.Left),
                    new Beam(new Coords(i, allRows.Length - 1), Direction.Right)
                ],
                '\\' => [new Beam(new Coords(i, allRows.Length - 1), Direction.Left)],
                '/' => [new Beam(new Coords(i, allRows.Length - 1), Direction.Right)],
                _ => throw new Exception("Invalid start")
            };
            return Contraption.SendBeams(allRows, beams);
        })
        .ToHashSet();
    }

    private static HashSet<HashSet<Direction>[][]> SendBeamsRight(string[] allRows)
    {
        return allRows.Select((row, i) =>
            {
                HashSet<Beam> beams = row.First() switch
                {
                    '.' or '-' => [new Beam(new Coords(0, i), Direction.Right)],
                    '|' => [new Beam(new Coords(0, i), Direction.Up), new Beam(new Coords(0, i), Direction.Down)],
                    '\\' => [new Beam(new Coords(0, i), Direction.Down)],
                    '/' => [new Beam(new Coords(0, i), Direction.Up)],
                    _ => throw new Exception("Invalid start")
                };
                return Contraption.SendBeams(allRows, beams);
            })
            .ToHashSet();
    }
    
    private static HashSet<HashSet<Direction>[][]> SendBeamsLeft(string[] allRows)
    {
        return allRows.Select((row, i) =>
        {
            HashSet<Beam> beams = row.Last() switch
            {
                '.' or '-' => [new Beam(new Coords(row.Length - 1, i), Direction.Left)],
                '|' =>
                [
                    new Beam(new Coords(row.Length - 1, i), Direction.Up),
                    new Beam(new Coords(0, i), Direction.Down)
                ],
                '\\' => [new Beam(new Coords(row.Length - 1, i), Direction.Up)],
                '/' => [new Beam(new Coords(row.Length - 1, i), Direction.Down)],
                _ => throw new Exception("Invalid start")
            };
            return Contraption.SendBeams(allRows, beams);
        })
        .ToHashSet();
    }
}
