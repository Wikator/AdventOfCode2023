namespace Part1;

internal static class Program
{
    public static void Main()
    {
        var lines = File.ReadLines("data/input.txt");
        var sections = lines.Aggregate(new List<List<string>> { new()}, (acc, curr) =>
        {
            if (curr == string.Empty)
                return [..acc, []];

            acc.Last().Add(curr);
            return acc;
        });

        var result = sections.Aggregate(0, (acc, section) =>
            acc + MirrorHorizontal(section) + MirrorVertical(section));
        
        Console.WriteLine(result);
    }

    private static int MirrorHorizontal(IReadOnlyCollection<string> section)
    {
        for (var i = 1; i < section.Count; i++)
        {
            var firstPart = section
                .Take(i)
                .ToArray();
                
            var secondPart = section
                .Skip(i)
                .ToArray();

            if (firstPart.Length > secondPart.Length)
            {
                firstPart = firstPart
                    .Skip(firstPart.Length - secondPart.Length)
                    .ToArray();
            }
                
            if (firstPart.Length < secondPart.Length)
            {
                secondPart = secondPart
                    .Take(firstPart.Length)
                    .ToArray();
            }
                
            if (firstPart.SequenceEqual(secondPart.Reverse()))
                return i * 100;
        }

        return 0;
    }

    private static int MirrorVertical(IReadOnlyList<string> section)
    {
        for (var i = 1; i < section[0].Length; i++)
        {
            var firstPart = section
                .Select(s => s
                    .Take(i)
                    .ToArray())
                .ToArray();
                
            var secondPart = section
                .Select(s => s
                    .Skip(i)
                    .ToArray())
                .ToArray();
                
            if (firstPart.First().Length > secondPart.First().Length)
            {
                firstPart = firstPart
                    .Select(s => s
                        .Skip(firstPart.First().Length - secondPart.First().Length)
                        .ToArray())
                    .ToArray();
            }

            if (firstPart.First().Length < secondPart.First().Length)
            {
                secondPart = secondPart
                    .Select(s => s
                        .Take(firstPart.First().Length)
                        .ToArray())
                    .ToArray();
            }
                
            secondPart = secondPart
                .Select(s => s
                    .Reverse()
                    .ToArray())
                .ToArray();

            var isMirrored = !firstPart
                .Where((t, j) => !t
                    .SequenceEqual(secondPart[j]))
                .Any();

            if (isMirrored)
                return i;
        }

        return 0;
    }
}
