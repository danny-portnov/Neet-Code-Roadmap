using static Solution;
//Console.WriteLine(string.Join(", ", TwoSum([3, 2, 4], 6)));
//Console.WriteLine(string.Join(", ", TwoSum([3, 3], 6)));
//Console.WriteLine($"[{string.Join("x", GroupAnagrams(["eat", "tea", "tan", "ate", "nat", "bat"]).Select(x => string.Join(", ", x)))}]");
//Console.WriteLine($"[{string.Join("x", GroupAnagramsWithGroupBy(["eat", "tea", "tan", "ate", "nat", "bat"]).Select(x => string.Join(", ", x)))}]");
//Console.WriteLine(string.Join(", ", GroupAnagrams(["", ""])));
//Console.WriteLine(string.Join(", ", TopKFrequent([3, 1, 1, 1, 2, 2, 4, 4, 4], 2)));
//Console.WriteLine(string.Join(", ", TopKFrequent([-1, -1], 1)));
//Console.WriteLine(string.Join(", ", TopKFrequent([1], 1)));
//Console.WriteLine(string.Join(", ", TopKFrequent([4, 1, -1, 2, -1, 2, 3], 2)));
//Console.WriteLine(LongestConsecutive([100, 4, 200, 1, 3, 2]));

//Console.WriteLine(LongestConsecutive([0, 3, 7, 2, 5, 8, 4, 6, 0, 1]));

//Console.WriteLine(LongestConsecutive([1, 2, 0, 1]));
//Console.WriteLine(LongestConsecutive([0]));
//Console.WriteLine(LongestConsecutive([0, 0]));
//Console.WriteLine(LongestConsecutive([]));
//Console.WriteLine(LongestConsecutive([9, 1, 4, 7, 3, -1, 0, 5, 8, -1, 6]));

//Console.WriteLine(IsPalindrome("A man, a plan, a canal: Panama"));
//Console.WriteLine(IsPalindrome(" "));
//Console.WriteLine(IsPalindrome("race a car"));

//Console.WriteLine(string.Join(", ", TwoSum([2, 7, 11, 15], 9)));
//Console.WriteLine(string.Join(", ", TwoSum([12, 13, 23, 28, 43, 44, 59, 60, 61, 68, 70, 86, 88, 92, 124, 125, 136, 168, 173, 173, 180, 199, 212, 221, 227, 230, 277, 282, 306, 314, 316, 321, 325, 328, 336, 337, 363, 365, 368, 370, 370, 371, 375, 384, 387, 394, 400, 404, 414, 422, 422, 427, 430, 435, 457, 493, 506, 527, 531, 538, 541, 546, 568, 583, 585, 587, 650, 652, 677, 691, 730, 737, 740, 751, 755, 764, 778, 783, 785, 789, 794, 803, 809, 815, 847, 858, 863, 863, 874, 887, 896, 916, 920, 926, 927, 930, 933, 957, 981, 997], 542)));

//Console.WriteLine(string.Join(", ", ThreeSum([-1, 0, 1, 2, -1, -4])));
Console.WriteLine(MaxArea([1, 8, 6, 2, 5, 4, 8, 3, 7]));

public static class Solution
{
    public static bool ContainsDuplicate(int[] nums)
    {
        Dictionary<int, int> numbers = [];
        foreach (int num in nums)
        {
            if (!numbers.TryAdd(num, num))
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsAnagram(string s, string t)
    {
        int[] sHist = new int[26];
        int[] tHist = new int[26];
        for (int i = 0; i < s.Length || i < t.Length; i++)
        {
            if (i < s.Length)
            {
                sHist[s[i] - 'a']++;
            }
            if (i < t.Length)
            {
                tHist[t[i] - 'a']++;
            }
        }
        for (int i = 0; i < sHist.Length; i++)
        {
            if (sHist[i] != tHist[i])
            {
                return false;
            }
        }
        return true;
    }

    //public static int[] TwoSum(int[] nums, int target)
    //{
    //    // Hash table to store number->index mapping
    //    Dictionary<int, int> numbers = [];

    //    // Iterate through the array
    //    for (int i = 0; i < nums.Length; i++)
    //    {
    //        int firstNumber = nums[i];
    //        int secondNumber = target - firstNumber;
    //        // Check if complement (target - current number) exists in hash table
    //        if (numbers.TryGetValue(secondNumber, out int index))
    //        {
    //            // If found, return indices of both numbers
    //            return [index, i];
    //        }

    //        // Store current number and its index in hash table
    //        numbers[firstNumber] = i;
    //    }

    //    // Return empty array if no solution found
    //    return [];
    //}

    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, IList<string>> anagrams = [];
        foreach (string str in strs)
        {
            string sortedStr = Sort(str);
            if (!anagrams.TryAdd(sortedStr, [str]))
            {
                anagrams[sortedStr].Add(str);
            }
        }
        return [.. anagrams.Values];
    }

    /// <summary>
    /// Given an integer array <paramref name="nums"/> and an integer <paramref name="k"/>,
    /// return the <paramref name="k"/> most frequent elements. You may return the answer in any order.
    /// Your algorithm's time complexity must be better than O(n log n), where n is the array's size.
    /// </summary>
    /// <param name="nums"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    public static int[] TopKFrequent(int[] nums, int k)
    {
        Dictionary<int, int> numToFreq = [];
        var freqToNum = new PriorityQueue<int, int>(
            Comparer<int>.Create((x, y) => y.CompareTo(x)));
        foreach (var num in nums)
        {
            numToFreq[num] = numToFreq.GetValueOrDefault(num, 0) + 1;
        }
        foreach (var numToFreqPair in numToFreq)
        {
            freqToNum.Enqueue(numToFreqPair.Key, numToFreqPair.Value);
        }
        var result = new int[k];
        for (int i = 0; i < k; i++)
        {
            result[i] = freqToNum.Dequeue();
        }
        return result;
    }

    /// <summary>
    /// Given an unsorted array of integers <paramref name="nums"/>, return the length of the longest consecutive elements sequence.
    /// You must write an algorithm that runs in <c>O(n)</c> time.
    /// </summary>
    /// <param name="nums"></param>
    /// <returns></returns>
    public static int LongestConsecutive(int[] nums)
    {
        HashSet<int> numsUnique = [.. nums];
        IEnumerable<int> sequenceStarters
            = numsUnique.Where(x => !numsUnique.Contains(x - 1));
        int maxCount = 0;
        foreach (int num in sequenceStarters)
        {
            int count = 1;
            int next = num + 1;
            while (numsUnique.Contains(next++))
            {
                count++;
            }
            maxCount = Math.Max(maxCount, count);
        }
        return maxCount;
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

    public static int[] TwoSum(int[] numbers, int target)
    {
        int l = 0, r = numbers.Length - 1;
        while (l <= r)
        {
            while (l < r && numbers[l] + numbers[r] > target) r--;
            while (l < r && numbers[l] + numbers[r] < target) l++;
            if (numbers[l] + numbers[r] == target) return [l + 1, r + 1];
            if (numbers[l] + numbers[r] > target) r--; else l++;
        }
        return null;
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
        // Doesn't work, fix while to check sum
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
            while (l <= r)
            {
                while (l < r && nums[l] + nums[r] > target && nums[r] == nums[r + 1]) r--;
                while (l < r && nums[l] + nums[r] < target && nums[l] == nums[l - 1]) l++;
                if (l == r) break;
                if (nums[l] + nums[r] == target)
                {
                    triplets.Add([nums[i], nums[l], nums[r]]);
                    break;
                }
                if (nums[l] + nums[r] > target) r--; else l++;
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

    public static IList<IList<string>> GroupAnagramsWithGroupBy(string[] strs)
        => strs.GroupBy(Sort).Select(x => (IList<string>)x).ToList();

    static string Sort(string str) => string.Concat(str.Order());
}