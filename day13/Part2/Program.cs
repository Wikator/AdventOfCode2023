namespace Part2;

internal static class Program
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
        var acc = 0;
        for (var i = 0; i < sections.Count; i++)
        {
            var originalReflection = GetOriginalMirrorHorizontal(sections[i])
                                     ?? GetOriginalMirrorVertical(sections[i]);
            
            for (var j = 0; j < sections[i].Count; j++)
            {
                var con = true;
                for (var z = 0; z < sections[i][j].Length; z++)
                {
                    var current = sections[i][j][z];

                    if (current == '#')
                    {
                        sections[i][j] = sections[i][j].Take(z).Aggregate("", (acc, curr) => acc + curr) + '.' + sections[i][j].Skip(z + 1).Aggregate("", (acc, curr) => acc + curr);
                    }
                    else
                    {
                        sections[i][j] = sections[i][j].Take(z).Aggregate("", (acc, curr) => acc + curr) + '#' + sections[i][j].Skip(z + 1).Aggregate("", (acc, curr) => acc + curr);
                    }
                    acc += MirrorHorizontal(sections[i], originalReflection) +
                           MirrorVertical(sections[i], originalReflection);
                    
                    
                    if (MirrorHorizontal(sections[i], originalReflection) +
                        MirrorVertical(sections[i], originalReflection) > 0)
                    {
                        sections[i][j] = sections[i][j].Take(z).Aggregate("", (acc, curr) => acc + curr) + current + sections[i][j].Skip(z + 1).Aggregate("", (acc, curr) => acc + curr);
                        con = false;
                        break;
                    }
                    sections[i][j] = sections[i][j].Take(z).Aggregate("", (acc, curr) => acc + curr) + current + sections[i][j].Skip(z + 1).Aggregate("", (acc, curr) => acc + curr);
                }
                if (!con)
                    break;
            }
        }
        
        Console.WriteLine(acc);
    }

    private static int MirrorHorizontal(IReadOnlyCollection<string> section, List<string> originalReflection)
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

            if (firstPart.SequenceEqual(secondPart.Reverse()) &&
                !originalReflection.SequenceEqual([..firstPart, ..secondPart]))
                acc += i * 100;
        }

        return acc;
    }

    private static int MirrorVertical(IReadOnlyList<string> section, List<string> originalReflection)
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
            {
                secondPart = secondPart
                    .Select(s => s
                        .Reverse()
                        .ToArray())
                    .ToArray();

                var reflection = new List<string>();
                
                for (var j = 0; j < firstPart.Length; j++)
                    reflection.Add(firstPart[j].Aggregate("", (acc1, curr) => acc1 + curr) +
                                   secondPart[j].Aggregate("", (acc1, curr) => acc1 + curr));

                if (!originalReflection.SequenceEqual(reflection))
                {
                    foreach (var line in originalReflection)
                    {
                        Console.WriteLine(line);
                    }
                    
                    Console.WriteLine("");
                    foreach (var line in reflection)
                    {
                        Console.WriteLine(line);
                    }
                    Console.WriteLine("");
                    acc += i;
                }

            }
        }

        return acc;
    }
    
    private static List<string>? GetOriginalMirrorHorizontal(IReadOnlyCollection<string> section)
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
                return [..firstPart, ..secondPart];
        }

        return null;
    }
    
    private static List<string>? GetOriginalMirrorVertical(IReadOnlyList<string> section)
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
            {
                secondPart = secondPart
                    .Select(s => s
                        .Reverse()
                        .ToArray())
                    .ToArray();

                var reflection = new List<string>();

                for (var j = 0; j < firstPart.Length; j++)
                    reflection.Add(firstPart[j].Aggregate("", (acc, curr) => acc + curr) +
                                   secondPart[j].Aggregate("", (acc, curr) => acc + curr));

                Console.WriteLine("");
                return reflection;
            }
        }

        return null;
    }
}