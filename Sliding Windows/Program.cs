using static Solution;


Console.WriteLine("MaxProfit");
Console.WriteLine(MaxProfit([7, 1, 5, 3, 6, 4])); // 5
Console.WriteLine(MaxProfit([7, 6, 4, 3, 1])); // 0
Console.WriteLine(MaxProfit([3, 2, 6, 5, 0, 3])); // 4
Console.WriteLine(MaxProfit([2, 1, 4])); // 3
Console.WriteLine(MaxProfit([2, 1, 2, 1, 0, 1, 2])); // 2

Console.WriteLine("LengthOfLongestSubstring"); 
Console.WriteLine(LengthOfLongestSubstring("abcabcbb")); //3
Console.WriteLine(LengthOfLongestSubstring("bbbbb")); //1
Console.WriteLine(LengthOfLongestSubstring("pwwkew")); //3
Console.WriteLine(LengthOfLongestSubstring("dvdf")); //3
Console.WriteLine(LengthOfLongestSubstring(" ")); //1
Console.WriteLine(LengthOfLongestSubstring("ckilbkd")); //5
Console.WriteLine(LengthOfLongestSubstring("tmmzuxt")); //5

Console.WriteLine("CharacterReplacement");
Console.WriteLine(CharacterReplacement("ABAB", 2));     //4
Console.WriteLine(CharacterReplacement("ABAB", 0));     //1
Console.WriteLine(CharacterReplacement("ABABBA", 2));     //5
Console.WriteLine(CharacterReplacement("AABABBA", 1));  //4
Console.WriteLine(CharacterReplacement("AAAB", 0));  //3
Console.WriteLine(CharacterReplacement("ABAA", 0));  //2

Console.WriteLine("CheckInclusion");
Console.WriteLine(CheckInclusion("abc", "lecabee"));  //true
Console.WriteLine(CheckInclusion("abc", "lecaabee"));  //false
Console.WriteLine(CheckInclusion("ab", "eidbaooo"));  //true
Console.WriteLine(CheckInclusion("ab", "eidboaoo"));  //false
Console.WriteLine(CheckInclusion("adc", "dcda"));  //true

public static class Solution
{
    public static int MaxProfit(int[] prices)
    {
        int minPrice = prices[0], profit = 0;
        foreach (int price in prices.Skip(1))
        {
            profit = Math.Max(profit, price - minPrice);
            minPrice = Math.Min(minPrice, price);
        }
        return profit;
    }

    public static int LengthOfLongestSubstring(string s)
    {
        Dictionary<char, int> letters = []; //maps char to last occurance (index)
        int max = 0, left = 0, count = 0;
        for (int right = 0; right < s.Length; right++)
        {
            // If a char appeared previously, AND it's within the window
            if (letters.TryGetValue(s[right], out int lastOccurance)
                && left <= lastOccurance)
            {
                left = lastOccurance + 1; // Reduce the window from the left to skip the char
                count = right - lastOccurance; // Update count
            }
            else // unique char OR duplicate is outside the window
            {
                count++;
            }
            max = Math.Max(max, count);
            letters[s[right]] = right; // override if a new occurance appeared
        }
        return max;
    }

    public static int CharacterReplacement(string s, int k)
    {
        int[] counts = new int[26];
        int maxFreq = 0, left = 0, maxLength = 0;

        for (int right = 0; right < s.Length; right++)
        {
            int idx = s[right] - 'A';
            counts[idx]++;
            maxFreq = Math.Max(maxFreq, counts[idx]);

            while (right - left + 1 - maxFreq > k)
            {
                counts[s[left] - 'A']--;
                left++;
            }

            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }

    /// <summary>
    /// Return <see langword="true"/> if <paramref name="longStr"/> 
    /// contains a permutation of <paramref name="shortStr"/>, or <see langword="false"/> otherwise.
    /// </summary>
    /// <param name="shortStr"></param>
    /// <param name="longStr"></param>
    /// <returns></returns>
    public static bool CheckInclusion(string shortStr, string longStr)
    {
        int[] longStrHist = new int[26];
        int[] shortStrHist = new int[26];
        foreach (char letter in shortStr)
        {
            shortStrHist[letter - 'a']++;
        }
        for (int right = 0; right < longStr.Length; right++)
        {
            char currentChar = longStr[right];
            int idx = currentChar - 'a';
            longStrHist[idx]++;
            if (right > shortStr.Length - 1) // update hist to fit window
            {
                longStrHist[longStr[right - shortStr.Length] - 'a']--;
            }
            if (shortStrHist.SequenceEqual(longStrHist))
            {
                return true;
            }
        }
        return false;
    }
}
