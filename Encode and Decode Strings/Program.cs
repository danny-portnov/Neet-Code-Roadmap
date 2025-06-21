// See https://aka.ms/new-console-template for more information
using System.Linq;
using System.Text;
using static Solution;
Console.WriteLine(string.Join(",", Decode(Encode(["neet", "code", "love", "you"]))));
Console.WriteLine(string.Join(",", Decode(Encode(["we", "say", ":", "yes"]))));

/// <summary>
/// Design an algorithm to encode a list of strings to a single string. <para></para>
/// The encoded string is then decoded back to the original list of strings. <para></para>
/// Please implement <see cref="Encode(IList{string})"/> and <see cref="Decode(string)"/>.
/// </summary>
public static class Solution
{
    const char Separator = ':';
    public static string Encode(IList<string> strs)
    {
        StringBuilder result = new();
        foreach (string str in strs)
        {
            result.Append($"{str.Length}{Separator}{str}");
        }
        return result.ToString();
    }

    public static List<string> Decode(string s)
    {
        List<string> result = [];
        int beginning = 0;
        for (int i = s.IndexOf(Separator); i != -1; i = s.IndexOf(Separator, beginning))
        {
            var num = int.Parse(s.Substring(beginning, i - beginning));
            //var num = int.Parse(s[beginning..i]);
            result.Add(s.Substring(i + 1, num));
            //result.Add(s[(i + 1)..(i + 1 + num)]);
            beginning = i + 1 + num;
        }
        return result;
    }
}

