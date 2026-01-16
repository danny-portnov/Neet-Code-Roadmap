using static Solution;
using static Extensions.Extension;

Console.WriteLine("ReverseString");
Print(ReverseString(['n', 'e', 'e', 't'])); // ["t","e","e","n"]
Print(ReverseString(['r', 'a', 'c', 'e', 'c', 'a', 'r'])); //["r", "a", "c", "e", "c", "a", "r"]

Console.WriteLine("IsPalindrome");
Console.WriteLine(IsPalindrome("A man, a plan, a canal: Panama"));
Console.WriteLine(IsPalindrome(" "));
Console.WriteLine(IsPalindrome("race a car"));

//Console.WriteLine("ValidPalindrome");
//Console.WriteLine(ValidPalindrome("aba")); // true
//Console.WriteLine(ValidPalindrome(" ")); // true
//Console.WriteLine(ValidPalindrome("abc")); // false
//Console.WriteLine(ValidPalindrome("abca")); // true

Console.WriteLine("MergeAlternately");
Console.WriteLine(MergeAlternately("abc", "pqr")); // apbqcr
Console.WriteLine(MergeAlternately("ab", "pqrs")); // apbqrs
Console.WriteLine(MergeAlternately("abcd", "pq")); // apbqcd

Console.WriteLine("TwoSum");
Print(TwoSum([2, 7, 11, 15], 9)); // [1,2]
Print(TwoSum([2, 7, 11, 15], 14)); // []
Print(TwoSum([12, 13, 23, 28, 43, 44, 59, 60, 61, 68, 70, 86, 88, 92, 124, 125,
    136, 168, 173, 173, 180, 199, 212, 221, 227, 230, 277, 282, 306, 314,
    316, 321, 325, 328, 336, 337, 363, 365, 368, 370, 370, 371, 375, 384,
    387, 394, 400, 404, 414, 422, 422, 427, 430, 435, 457, 493, 506, 527,
    531, 538, 541, 546, 568, 583, 585, 587, 650, 652, 677, 691, 730, 737, 740,
    751, 755, 764, 778, 783, 785, 789, 794, 803, 809, 815, 847, 858, 863, 863,
    874, 887, 896, 916, 920, 926, 927, 930, 933, 957, 981, 997], 542));

Console.WriteLine("ThreeSum");
Print(ThreeSum([-1, 0, 1, 2, -1, -4])); // [[-1, -1, 2], [0, -1, 1]]

Console.WriteLine("MaxArea");
Console.WriteLine(MaxArea([1, 8, 6, 2, 5, 4, 8, 3, 7])); // 49
public static class Solution
{
    public static char[] ReverseString(char[] s)
    {
        int l = 0, r = s.Length - 1;
        while (l < r)
        {
            (s[l], s[r]) = (s[r], s[l]);
            l++;
            r--;
        }
        return s;
    }

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

    public static bool ValidPalindrome(string s)
    {
        int l = 0, r = s.Length - 1;
        bool skipped = false;
        while (l < r)
        {
            if (s[r] != s[l])
            {
                if (skipped) return false;
                //if (s[r-1] != s[l]) 
            }
            l++;
            r--;
        }
        return true;
    }

    public static string MergeAlternately(string word1, string word2)
    {
        var word = new char[word1.Length + word2.Length];
        int w1 = 0, w2 = 0;
        for (int i = 0; i < word.Length; i++)
        {
            if (i >= word1.Length && i < word2.Length)
            {
                word[i] = word2[w2];
                w2++;
            }
            else
            {
                if (int.IsEvenInteger(i))
                {
                    word[i] = word1[w1];
                    w1++;
                }
                else
                {
                    word[i] = word2[w2];
                    w2++;
                }
            }

        }
        return new string(word);
    }
    public static int[] TwoSum(int[] numbers, int target)
    {
        int l = 0, r = numbers.Length - 1;
        while (l < r)
        {
            while (l - 1 < r && numbers[l] + numbers[r] > target) r--;
            while (l < r - 1 && numbers[l] + numbers[r] < target) l++;
            if (numbers[l] + numbers[r] == target) return [l + 1, r + 1];
            if (numbers[l] + numbers[r] > target) r--; else l++;
        }
        return [];
    }

    /// <summary>
    /// Given an integer array nums, return all the triplets [nums[i], nums[j], nums[k]] 
    /// such that i != j, i != k, and j != k, and nums[i] + nums[j] + nums[k]
    /// = 0.Notice that the solution set must not contain duplicate triplets.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static IList<IList<int>> ThreeSum(int[] nums)
    {
        Array.Sort(nums);
        IList<IList<int>> triplets = [];
        for (int i = 0; i < nums.Length - 2; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }
            int l = i + 1, r = nums.Length - 1;
            int target = -nums[i];
            var indices = TwoSum(nums, target);
            if (indices.Length != 0)
            {
                triplets.Add([nums[i], nums[indices[0] - 1], nums[indices[1] - 1]]);
            }
        }
        return triplets;
    }

    /// <summary>
    /// You are given an integer array height of length n. 
    /// There are n vertical lines drawn such that the two endpoints of the ith line are
    /// (i, 0) and (i, height[i]). Find two lines that together with the x-axis form a container,
    /// such that the container contains the most water. Return the maximum amount of water a container can store.
    /// Notice that you may not slant the container.
    /// </summary>
    /// <param name="height"></param>
    /// <returns></returns>
    public static int MaxArea(int[] height)
    {
        int l = 0, r = height.Length - 1;
        int area = Math.Min(height[l], height[r]) * (r - l);
        while (l < r)
        {
            if (height[r] > height[l])
                l++;
            else
                r--;
            area = Math.Max(area, Math.Min(height[l], height[r]) * (r - l));
        }
        return area;
    }
}