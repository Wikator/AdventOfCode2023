namespace Part1;

internal class Program
{
    public static void Main()
    {
        var lines = File.ReadLines("data/input.txt");
        var sections = lines.Aggregate(new List<List<string>> { new List<string>()}, (acc, curr) =>
        {
            if (curr == string.Empty)
                return [..acc, []];

            acc[^1].Add(curr);
            return acc;
        });

        var result = sections.Aggregate(0, (acc, section) =>
            acc + MirrorHorizontal(section) + MirrorVertical(section));
        
        Console.WriteLine(result);
    }

    private static int MirrorHorizontal(IReadOnlyCollection<string> section)
    {
        var acc = 0;
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
                acc += i * 100;
        }

        return acc;
    }

    private static int MirrorVertical(IReadOnlyList<string> section)
    {
        var acc = 0;
        
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
                
            if (firstPart[1].Length > secondPart[1].Length)
            {
                firstPart = firstPart
                    .Select(s => s
                        .Skip(firstPart[1].Length - secondPart[1].Length)
                        .ToArray())
                    .ToArray();
            }

            if (firstPart[1].Length < secondPart[1].Length)
            {
                secondPart = secondPart
                    .Select(s => s
                        .Take(firstPart[1].Length)
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
                acc += i;
        }

        return acc;
    }
}