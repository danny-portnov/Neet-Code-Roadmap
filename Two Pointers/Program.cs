using static Solution;

Console.WriteLine("IsPalindrome");
Console.WriteLine(IsPalindrome("A man, a plan, a canal: Panama"));
Console.WriteLine(IsPalindrome(" "));
Console.WriteLine(IsPalindrome("race a car"));

public static class Solution
{

    /// <summary>
    /// A phrase is a <b>palindrome</b> if, after converting all uppercase letters into 
    /// lowercase letters and removing all non-alphanumeric characters, it reads the same forward and backward. Alphanumeric characters include letters and numbers.
    /// Given a string <paramref name="s"/>, return <see langword="true"/> if it is a palindrome,
    /// or <see langword="false"/> otherwise.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static bool IsPalindrome(string s)
    {
        int l = 0, r = s.Length - 1;
        while (l <= r)
        {
            while (l < r && !char.IsAsciiLetterOrDigit(s[l])) l++;
            while (l < r && !char.IsAsciiLetterOrDigit(s[r])) r--;
            if (char.ToLower(s[r]) != char.ToLower(s[l])) return false;
            l++;
            r--;
        }
        return true;
    }
}