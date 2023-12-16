namespace Shared;

public static class Contraption
{
    public static HashSet<Direction>[][] SendBeams(string[] allTiles, HashSet<Beam> beams)
    {
        var energizedTiles = allTiles
            .Select(tile => tile
                .Select(_ => new HashSet<Direction>())
                .ToArray())
            .ToArray();
        
        while (beams.Count > 0)
        {
            beams = beams.Aggregate(new HashSet<Beam>(), (newBeams, beam) =>
            {
                if (energizedTiles[beam.Coords.Y][beam.Coords.X].Contains(beam.Direction))
                    return newBeams;

                energizedTiles[beam.Coords.Y][beam.Coords.X].Add(beam.Direction);
                
                var newCoords = beam.Direction switch
                {
                    Direction.Up => beam.Coords with { Y = beam.Coords.Y - 1 },
                    Direction.Right => beam.Coords with { X = beam.Coords.X + 1 },
                    Direction.Down => beam.Coords with { Y = beam.Coords.Y + 1 },
                    Direction.Left => beam.Coords with { X = beam.Coords.X - 1 },
                    _ => throw new Exception()
                };

                if (newCoords.Y < 0 || newCoords.Y >= allTiles.Length || newCoords.X < 0 ||
                    newCoords.X >= allTiles.First().Length)
                {
                    return newBeams;
                }

                return allTiles[newCoords.Y][newCoords.X] switch
                {
                    '.' => [..newBeams, beam with { Coords = newCoords }],
                    '|' => beam.Direction switch
                    {
                        Direction.Left or Direction.Right =>
                        [
                            ..newBeams, new Beam(newCoords, Direction.Up), new Beam(newCoords, Direction.Down)
                        ],
                        Direction.Down or Direction.Up => [..newBeams, beam with { Coords = newCoords }],
                        _ => throw new Exception()
                    },
                    '-' => beam.Direction switch
                    {
                        Direction.Down or Direction.Up =>
                        [
                            ..newBeams, new Beam(newCoords, Direction.Right), new Beam(newCoords, Direction.Left)
                        ],
                        Direction.Left or Direction.Right => [..newBeams, beam with { Coords = newCoords }],
                        _ => throw new Exception()
                    },
                    '\\' => beam.Direction switch
                    {
                        Direction.Down => [..newBeams, new Beam(newCoords, Direction.Right)],
                        Direction.Up => [..newBeams, new Beam(newCoords, Direction.Left)],
                        Direction.Right => [..newBeams, new Beam(newCoords, Direction.Down)],
                        Direction.Left => [..newBeams, new Beam(newCoords, Direction.Up)],
                        _ => throw new Exception()
                    },
                    '/' => beam.Direction switch
                    {
                        Direction.Up => [..newBeams, new Beam(newCoords, Direction.Right)],
                        Direction.Right => [..newBeams, new Beam(newCoords, Direction.Up)],
                        Direction.Down => [..newBeams, new Beam(newCoords, Direction.Left)],
                        Direction.Left => [..newBeams, new Beam(newCoords, Direction.Down)],
                        _ => throw new Exception()
                    },
                    _ => throw new Exception()
                };
            });
        }

        return energizedTiles;
    }
}
