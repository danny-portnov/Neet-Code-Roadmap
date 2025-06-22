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
Console.WriteLine(CharacterReplacement("AABABBA", 1));  //4
Console.WriteLine(CharacterReplacement("AAAB", 0));  //3
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

    public class CharData
    {
        public int Count;
        //public int 
    }
    public static int CharacterReplacement(string s, int k)
    {
        Dictionary<char, int> result = s.GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());
        int max = result.Values.Max();
        char mostFrequent = result.Where(x => x.Value == max).Select(x => x.Key).First();
        Dictionary<char, int> charCount = [];
        int maxCount = 0, left = 0, count = 0, originalK = k; // k is window length ?
        bool startCounting = false;
        for (int right = 0; right < s.Length; right++)
        {
            if (s[right] != mostFrequent && k == 0)
            {
                //left = charCount[s[right]] + 1; // Reduce the window from the left to skip the char
                count = charCount.TryGetValue(s[right], out int value) ? right - value : 0;
                startCounting = false;
                k = originalK;
            }
            if (s[right] == mostFrequent
                || startCounting) // begin/resume counting the most frequent 
            {
                count++;
                startCounting = true;
                if (s[right] != mostFrequent)
                {
                    k--; 
                }
            }
            maxCount = Math.Max(maxCount, count);
            charCount[s[right]] = right;
        }
        return maxCount;
    }
}
