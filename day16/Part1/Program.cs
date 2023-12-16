using Shared;

namespace Part1;

internal static class Program
{
    public static void Main()
    {
        var allTiles = File.ReadAllLines("data/input.txt");
        
        var initialDirection = allTiles[0][0] switch
        {
            '-' or '.' => Direction.Right,
            '\\' or '|' => Direction.Down,
            _ => throw new Exception("Invalid start")
        };

        var beam = new Beam(new Coords(0, 0), initialDirection);

        var energizedTiles = Contraption.SendBeam(allTiles, beam);

        var result = energizedTiles
            .Sum(tile => tile
                .Count(t => t.Count > 0));
        
        Console.WriteLine(result);
    }
}
