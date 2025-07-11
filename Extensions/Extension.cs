namespace Extensions;

public static class Extension
{
    public static void Print<T>(IEnumerable<T> values)
        => Console.WriteLine($"[{string.Join(",", values)}]");

    public static void Print<T>(IList<IList<T>> lists)
        => Console.WriteLine($"[{string.Join(", ", lists.Select(inner => $"[{string.Join(", ", inner.Select(ConvertIfString))}]"))}]");

    public static void Print(string str)
        => Console.WriteLine(AddQuotes(str));

    private static string ConvertIfString<T>(T target) => target is string str ? AddQuotes(str) : target!.ToString()!;

    private static string AddQuotes(string str) => $"\"{str}\"";
}
