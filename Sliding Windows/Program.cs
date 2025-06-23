using System.Text.RegularExpressions;
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
Console.WriteLine(CheckInclusion("abc", "bbbca"));  //true

Console.WriteLine("MinWindow");
Console.WriteLine(MinWindow("ADOBECODEBANC", "ABC"));  //BANC
Console.WriteLine(MinWindow("a", "a"));  //a
Console.WriteLine($"\"{MinWindow("a", "aa")}\"");  //""
Console.WriteLine(MinWindow("OUZODYXAZV", "XYZ"));  //YXAZ
public static class Solution
{
    private const int HistogramLength = 26;

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
        int[] counts = new int[HistogramLength];
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
        int[] longStrHist = new int[HistogramLength], shortStrHist = new int[longStrHist.Length]; 
        foreach (char letter in shortStr)
        {
            shortStrHist[letter - 'a']++;
        }
        int matches = longStrHist.Zip(shortStrHist).Where(x => x.First == x.Second).Count();
        for (int right = 0; right < longStr.Length; right++)
        {
            char currentChar = longStr[right];
            int idx = currentChar - 'a';
            longStrHist[idx]++;
            if (shortStrHist[idx] == longStrHist[idx])
            {
                matches++;
            }
            else if (shortStrHist[idx] + 1 == longStrHist[idx])
            {
                matches--;
            }
            if (right > shortStr.Length - 1) // update hist to fit window
            {
                idx = longStr[right - shortStr.Length] - 'a';
                longStrHist[idx]--;
                if (shortStrHist[idx] == longStrHist[idx])
                {
                    matches++;
                }
                else if (shortStrHist[idx] - 1 == longStrHist[idx])
                {
                    matches--;
                }
            }
            if (matches == longStrHist.Length)
            {
                return true;
            }
        }
        return false;
    }

    public static string MinWindow(string longStr, string shortStr)
    {
        Dictionary<char, int> shortStrHist = shortStr.GroupBy(c => c)
            .ToDictionary(g => g.Key, g => g.Count());
        Dictionary<char, int> longStrHist = [];
        int left = 0, count = 0, sum = shortStrHist.Count;
        int? resultStart = default, resultLength = default;
        for (int right = 0; right < longStr.Length; right++)
        {
            char currentChar = longStr[right];
            longStrHist[currentChar] = longStrHist.GetValueOrDefault(currentChar) + 1;
            if (shortStrHist.TryGetValue(currentChar, out int value) 
                && longStrHist[currentChar] == value)
            {
                count++;
            }
            while (count == sum)
            {
                if (!resultStart.HasValue || right - left + 1 < resultLength)
                {
                    resultStart = left;
                    resultLength = right - left + 1;
                }
                char leftChar = longStr[left];
                longStrHist[leftChar]--;
                if (shortStrHist.TryGetValue(leftChar, out value)
                        && longStrHist[leftChar] < value)
                {
                    count--;
                }
                left++;
            }
        }
        return resultStart.HasValue ? longStr.Substring(resultStart.Value, resultLength.Value) : "";
    }
}
