namespace Part2.Extensions;

public static class EnumerableExtensions
{
    public static string ConvertToString(this IEnumerable<char> enumerable) =>
        enumerable.Aggregate("", (acc, curr) => acc + curr);
}
