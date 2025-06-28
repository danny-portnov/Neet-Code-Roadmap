using System.Linq;
using static Solution;

Console.WriteLine("HasDuplicate");
Console.WriteLine(HasDuplicate([1, 2, 3, 3])); //true
Console.WriteLine(HasDuplicate([1, 2, 3, 4])); //false
Console.WriteLine(HasDuplicate([])); //false


Console.WriteLine("IsAnagram");
Console.WriteLine(IsAnagram("racecar", "carrace")); //true
Console.WriteLine(IsAnagram("jar", "jam")); //false
Console.WriteLine(IsAnagram("","")); //false


public static class Solution
{
    public static bool HasDuplicate(int[] nums) 
        => nums.ToHashSet().Count < nums.Length;

    public static bool IsAnagram(string s, string t)
    {
        return GetHist(s).SequenceEqual(GetHist(t));

        static int[] GetHist(string str)
        {
            var sHist = new int[26];
            foreach (char letter in str)
            {
                sHist[letter - 'a']++;
            }
            return sHist;
        }
    }

    public static int[] TwoSum(int[] nums, int target)
    {
        Dictionary<int, int> numToIndex = [];
        for (int i = 0; i < nums.Length; i++)
        {
            if (numToIndex.TryGetValue(target - nums[i], out int value))
            {
                return [value, i];
            }
            numToIndex[nums[i]] = i; // Cannot be overriden 
        }
        return [];
    }
}