using Shared;

namespace Part2;

internal static class Program
{
    public static void Main()
    {
        var allTiles = File.ReadAllLines("data/input.txt");

        IEnumerable<HashSet<Direction>[][]> allEnergizedTiles =
            [..SendBeamsDown(allTiles), ..SendBeamsUp(allTiles), ..SendBeamsRight(allTiles), ..SendBeamsLeft(allTiles)];
        
        var max = allEnergizedTiles
            .Select(energizedTiles => energizedTiles
                .Sum(tile => tile
                    .Count(t => t.Count > 0)))
            .Max();
        
        Console.WriteLine(max);
    }
    private static IEnumerable<HashSet<Direction>[][]> SendBeamsDown(string[] allTiles)
    {
        return allTiles.First().Select((mirror, i) => (HashSet<Beam>)(mirror switch
            {
                '.' or '|' => [new Beam(new Coords(i, 0), Direction.Down)],
                '-' => [new Beam(new Coords(i, 0), Direction.Left), new Beam(new Coords(i, 0), Direction.Right)],
                '\\' => [new Beam(new Coords(i, 0), Direction.Right)],
                '/' => [new Beam(new Coords(i, 0), Direction.Left)],
                _ => throw new Exception("Invalid start")
            }))
            .Select(beams => Contraption.SendBeams(allTiles, beams));
    }

    private static IEnumerable<HashSet<Direction>[][]> SendBeamsUp(string[] allTiles)
    {
        return allTiles.Last().Select((mirror, i) => (HashSet<Beam>)(mirror switch
            {
                '.' or '|' => [new Beam(new Coords(i, allTiles.Length - 1), Direction.Up)],
                '-' =>
                [
                    new Beam(new Coords(i, allTiles.Length - 1), Direction.Left),
                    new Beam(new Coords(i, allTiles.Length - 1), Direction.Right)
                ],
                '\\' => [new Beam(new Coords(i, allTiles.Length - 1), Direction.Left)],
                '/' => [new Beam(new Coords(i, allTiles.Length - 1), Direction.Right)],
                _ => throw new Exception("Invalid start")
            }))
            .Select(beams => Contraption.SendBeams(allTiles, beams));
    }

    private static IEnumerable<HashSet<Direction>[][]> SendBeamsRight(string[] allTiles)
    {
        return allTiles.Select((tile, i) => (HashSet<Beam>)(tile.First() switch
            {
                '.' or '-' => [new Beam(new Coords(0, i), Direction.Right)],
                '|' => [new Beam(new Coords(0, i), Direction.Up), new Beam(new Coords(0, i), Direction.Down)],
                '\\' => [new Beam(new Coords(0, i), Direction.Down)],
                '/' => [new Beam(new Coords(0, i), Direction.Up)],
                _ => throw new Exception("Invalid start")
            }))
            .Select(beams => Contraption.SendBeams(allTiles, beams));
    }
    
    private static IEnumerable<HashSet<Direction>[][]> SendBeamsLeft(string[] allTiles)
    {
        return allTiles.Select((tile, i) => (HashSet<Beam>)(tile.Last() switch
            {
                '.' or '-' => [new Beam(new Coords(tile.Length - 1, i), Direction.Left)],
                '|' =>
                [
                    new Beam(new Coords(tile.Length - 1, i), Direction.Up), new Beam(new Coords(0, i), Direction.Down)
                ],
                '\\' => [new Beam(new Coords(tile.Length - 1, i), Direction.Up)],
                '/' => [new Beam(new Coords(tile.Length - 1, i), Direction.Down)],
                _ => throw new Exception("Invalid start")
            }))
            .Select(beams => Contraption.SendBeams(allTiles, beams));
    }
}
