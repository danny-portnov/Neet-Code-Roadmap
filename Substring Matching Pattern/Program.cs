// See https://aka.ms/new-console-template for more information
Console.WriteLine(Solution.HasMatch("leetcode", "ee*e"));
Console.WriteLine(Solution.HasMatch("letcode", "*l"));
Console.WriteLine(Solution.HasMatch("car", "c*v"));
Console.WriteLine(Solution.HasMatch("luck", "u*"));
Console.WriteLine(Solution.HasMatch("luck", "*"));
Console.WriteLine(Solution.HasMatch("jjj", "*j"));

public static class Solution
{
    /// <summary>
    /// You are given a string s and a pattern string p, where p contains exactly one '*' character.
    ///     The '*' in p can be replaced with any sequence of zero or more characters.
    ///     Return true if p can be made a substring of s, and false otherwise.
    /// </summary>
    /// <param name="s">string</param>
    /// <param name="p">pattern string, contains exactly one '*' character</param>
    /// <returns></returns>
    public static bool HasMatch(string s, string p)
    {
        var splitedP = p.Split('*');
        string beforeStar = splitedP[0], afterStar = splitedP[1];
        int leftSubstringIndex = s.IndexOf(beforeStar);
        int rightSubstringIndex = s.IndexOf(afterStar, leftSubstringIndex + beforeStar.Length);
        return leftSubstringIndex != -1 && rightSubstringIndex != -1 && leftSubstringIndex <= rightSubstringIndex;
    }
}