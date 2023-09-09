namespace Stabilt.OpenApi.FluentAssertions;

internal static class Extensions
{
    public static IEnumerable<T> Order<T>(this IEnumerable<T> source) => source.OrderBy(x => x);
}
