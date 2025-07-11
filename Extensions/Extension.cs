namespace Extensions;

public static class Extension
{
    public static void Print<T>(IEnumerable<T> values)
        => Console.WriteLine($"[{string.Join(",", values)}]");

    public static void Print(IList<IList<string>> lists)
        => Console.WriteLine($"[{string.Join(", ", lists.Select(inner => $"[{string.Join(", ", inner.Select(AddQuotes))}]"))}]");

    public static void Print(string str)
        => Console.WriteLine(AddQuotes(str));

    private static string AddQuotes(string str) => $"\"{str}\"";
}
