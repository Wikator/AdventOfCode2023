using Part2.Extensions;

namespace Part2;

internal static class Program
{
    public static void Main()
    {
        var lines = File.ReadLines("data/input.txt");
        var sections = lines.Aggregate(new List<List<string>> { new() }, (acc, curr) =>
        {
            if (curr == string.Empty)
                return [..acc, []];

            acc[^1].Add(curr);
            return acc;
        });
        
        var result = sections.Aggregate(0, (acc, section) =>
        {
            var originalReflection = GetMirrorHorizontalReflection(section)
                                     ?? GetMirrorVerticalReflection(section)!;
            
            for (var j = 0; j < section.Count; j++)
            {
                for (var z = 0; z < section[j].Length; z++)
                {
                    var current = section[j][z];

                    section[j] = current switch
                    {
                        '#' => $"{section[j].Take(z).ConvertToString()}.{section[j].Skip(z + 1).ConvertToString()}",
                        '.' => $"{section[j].Take(z).ConvertToString()}#{section[j].Skip(z + 1).ConvertToString()}",
                        _ => throw new Exception("Invalid symbols in input")
                    };
                    
                    var addToAcc = NewMirrorHorizontal(section, originalReflection) +
                           NewMirrorVertical(section, originalReflection);
                    
                    if (addToAcc > 0)
                        return acc + addToAcc;
                    
                    section[j] = $"{section[j].Take(z).ConvertToString()}{current}{section[j].Skip(z + 1).ConvertToString()}";
                }
            }

            throw new Exception("No new reflection found in section");
        });
        
        Console.WriteLine(result);
    }

    private static int NewMirrorHorizontal(IReadOnlyCollection<string> section,
        IReadOnlyCollection<string> originalReflection)
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

            if (firstPart.SequenceEqual(secondPart.Reverse()) &&
                !originalReflection.SequenceEqual([..firstPart, ..secondPart]))
            {
                return i * 100;
            }
        }

        return 0;
    }

    private static int NewMirrorVertical(IReadOnlyList<string> section,
        IReadOnlyCollection<string> originalReflection)
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
                
            if (firstPart[0].Length > secondPart[0].Length)
            {
                firstPart = firstPart
                    .Select(s => s
                        .Skip(firstPart[1].Length - secondPart[0].Length)
                        .ToArray())
                    .ToArray();
            }

            if (firstPart[0].Length < secondPart[0].Length)
            {
                secondPart = secondPart
                    .Select(s => s
                        .Take(firstPart[0].Length)
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

            if (!isMirrored)
                continue;
            
            secondPart = secondPart
                .Select(s => s
                    .Reverse()
                    .ToArray())
                .ToArray();

            var reflection = firstPart
                .Select((t, j) => $"{t.ConvertToString()}{secondPart[j].ConvertToString()}").ToArray();

            if (!originalReflection.SequenceEqual(reflection))
                return i;
        }

        return 0;
    }
    
    private static List<string>? GetMirrorHorizontalReflection(IReadOnlyCollection<string> section)
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
    
    private static List<string>? GetMirrorVerticalReflection(IReadOnlyList<string> section)
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
                
            if (firstPart[0].Length > secondPart[0].Length)
            {
                firstPart = firstPart
                    .Select(s => s
                        .Skip(firstPart[0].Length - secondPart[0].Length)
                        .ToArray())
                    .ToArray();
            }

            if (firstPart[0].Length < secondPart[0].Length)
            {
                secondPart = secondPart
                    .Select(s => s
                        .Take(firstPart[0].Length)
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

            if (!isMirrored)
                continue;

            secondPart = secondPart
                .Select(s => s
                    .Reverse()
                    .ToArray())
                .ToArray();

            return firstPart
                .Select((t, j) => $"{t.ConvertToString()}{secondPart[j].ConvertToString()}").ToList();
        }

        return null;
    }
}
