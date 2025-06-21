using System.Diagnostics;
using static Solution;

//Console.WriteLine(MaxProfit([7, 1, 5, 3, 6, 4])); // 5
//Console.WriteLine(MaxProfit([7, 6, 4, 3, 1])); // 0
//Console.WriteLine(MaxProfit([3, 2, 6, 5, 0, 3])); // 4
//Console.WriteLine(MaxProfit([2, 1, 4])); // 3
//Console.WriteLine(MaxProfit([2, 1, 2, 1, 0, 1, 2])); // 2

Console.WriteLine(LengthOfLongestSubstring("abcabcbb")); //3
Console.WriteLine(LengthOfLongestSubstring("bbbbb")); //1
Console.WriteLine(LengthOfLongestSubstring("pwwkew")); //3
Console.WriteLine(LengthOfLongestSubstring("dvdf")); //3
Console.WriteLine(LengthOfLongestSubstring(" ")); //1
Console.WriteLine(LengthOfLongestSubstring("ckilbkd")); //5
Console.WriteLine(LengthOfLongestSubstring("tmmzuxt")); //5
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
        Dictionary<char, int> letters = [];
        int max = 0, left = 0;
        for (int right = 0; right < s.Length; right++)
        {
            if (letters.TryGetValue(s[right], out int value))
            {
                right = value + 1;
            }
            else
            {
                count++;
            }
            max = Math.Max(max, count);
            letters.Add(s[right], right);
        }
        return max;
    }
}
