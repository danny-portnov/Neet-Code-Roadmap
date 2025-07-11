using System.Linq;
using static Solution;
using static Extensions.Extension;

Console.WriteLine("HasDuplicate");
Console.WriteLine(HasDuplicate([1, 2, 3, 3])); //true
Console.WriteLine(HasDuplicate([1, 2, 3, 4])); //false
Console.WriteLine(HasDuplicate([])); //false

Console.WriteLine("IsAnagram");
Console.WriteLine(IsAnagram("racecar", "carrace")); //true
Console.WriteLine(IsAnagram("jar", "jam")); //false
Console.WriteLine(IsAnagram("", "")); //false

Console.WriteLine("TwoSum");
Print(TwoSum([3, 2, 4], 6));
Print(TwoSum([3, 3], 6));

Console.WriteLine("GroupAnagrams");
Print(GroupAnagrams(["eat", "tea", "tan", "ate", "nat", "bat"]));
Print(GroupAnagrams(["eat", "tea", "aet", "aet", "ate", "ate", "nat", "bat"]));
Print(GroupAnagrams(["", ""]));
Print(GroupAnagrams([""]));

Console.WriteLine("TopKFrequent");
Print(TopKFrequent([3, 1, 1, 1, 2, 2, 4, 4, 4], 2)); // [1, 4]
Print(TopKFrequent([-1, -1], 1)); // [-1]
Print(TopKFrequent([1], 1)); // [1]
Print(TopKFrequent([4, 1, -1, 2, -1, 2, 3], 2)); // [-1, 2]

Console.WriteLine("LongestConsecutive");
Console.WriteLine(LongestConsecutive([100, 4, 200, 1, 3, 2])); // 4 (1,2,3,4)
Console.WriteLine(LongestConsecutive([0, 3, 7, 2, 5, 8, 4, 6, 0, 1])); //9
Console.WriteLine(LongestConsecutive([1, 2, 0, 1])); // 3
Console.WriteLine(LongestConsecutive([0])); //1
Console.WriteLine(LongestConsecutive([0, 0])); // 1
Console.WriteLine(LongestConsecutive([])); //0
Console.WriteLine(LongestConsecutive([9, 1, 4, 7, 3, -1, 0, 5, 8, -1, 6])); //7

Console.WriteLine("MajorityElement");
Console.WriteLine(MajorityElement([3, 2, 3])); //3
Console.WriteLine(MajorityElement([2, 2, 1, 1, 1, 2, 2])); //2
Console.WriteLine(MajorityElement([1, 1, 1, 2, 2, 2, 2])); //2
Console.WriteLine(MajorityElement([3, 3, 4])); //30
Console.WriteLine(MajorityElement([6, 5, 5])); //5

Console.WriteLine("RemoveElement");
RemoveElement([3, 2, 2, 3], 3); //2
RemoveElement([0, 1, 2, 2, 3, 0, 4, 2], 2); //5
RemoveElement([], 2); //0
RemoveElement([1], 1); //0
RemoveElement([1], 2); //1

Console.WriteLine("MajorityElement");
Console.WriteLine(MajorityElement([3, 2, 3])); //3
Console.WriteLine(MajorityElement([2, 2, 1, 1, 1, 2, 2])); //2
Console.WriteLine(MajorityElement([1, 1, 1, 2, 2, 2, 2])); //2
Console.WriteLine(MajorityElement([3, 3, 4])); //30
Console.WriteLine(MajorityElement([6, 5, 5])); //5

Console.WriteLine("MyHashSet");
MyHashSet myHashSet = new();
myHashSet.Add(1);      // set = [1]
myHashSet.Add(2);      // set = [1, 2]
Console.WriteLine(myHashSet.Contains(1)); // return True
Console.WriteLine(myHashSet.Contains(3)); // return False, (not found)
myHashSet.Add(2);      // set = [1, 2]
Console.WriteLine(myHashSet.Contains(2)); // return True
myHashSet.Remove(2);   // set = [1]
Console.WriteLine(myHashSet.Contains(2)); // return False, (already removed)

Console.WriteLine("MyHashMap");
MyHashMap myHashMap = new MyHashMap();
myHashMap.Put(1, 1); // The map is now [[1,1]]
myHashMap.Put(2, 2); // The map is now [[1,1], [2,2]]
Console.WriteLine(myHashMap.Get(1));    // return 1, The map is now [[1,1], [2,2]]
Console.WriteLine(myHashMap.Get(3));    // return -1 (i.e., not found), The map is now [[1,1], [2,2]]
myHashMap.Put(2, 1); // The map is now [[1,1], [2,1]] (i.e., update the existing value)
Console.WriteLine(myHashMap.Get(2));    // return 1, The map is now [[1,1], [2,1]]
myHashMap.Remove(2); // remove the mapping for 2, The map is now [[1,1]]
Console.WriteLine(myHashMap.Get(2));    // return -1 (i.e., not found), The map is now [[1,1]]

Console.WriteLine("SortArray");
Print(SortArray([5, 2, 3, 1])); //1,2,3,4,5

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
    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        Dictionary<string, IList<string>> anagrams = [];
        foreach (string str in strs)
        {
            string sortedStr = Sort(str);
            if (!anagrams.TryAdd(sortedStr, [str]) /*&& str != sortedStr*/)
            {
                anagrams[sortedStr].Add(str);
            }
        }
        return [.. anagrams.Values];
    }
    static string Sort(string str) => string.Concat(str.Order());

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
    public static IList<IList<string>> GroupAnagramsWithGroupBy(string[] strs)
        => [.. strs.GroupBy(Sort).Select(x => (IList<string>)x)];

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

    public static int MajorityElement(int[] nums)
    {
        int count = 0, max = 0;
        foreach (var num in nums)
        {
            if (count == 0)
            {
                max = num;
            }
            count += (num == max) ? 1 : -1;
        }
        return max;
    }

    public static int[] GetConcatenation(int[] nums)
    {
        var result = new int[nums.Length * 2];
        for (int i = 0; i < nums.Length; i++)
        {
            result[i] = result[i + nums.Length] = nums[i];
        }
        return result;
    }

    public static string LongestCommonPrefix(string[] strs)
    {
        string min = strs.Min()!;
        for (int i = 0; i < min.Length; i++)
        {
            if (strs.Any(s => s[i] != min[i]))
            {
                return min[..i];
            }
        }
        return min;
    }

    public static int RemoveElement(int[] nums, int val)
    {
        int k = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] != val)
            {
                nums[k++] = nums[i];
            }
        }
        PrintSameLine(nums);
        Console.WriteLine($" val = {val}");
        return k;
    }
    public static int[] SortArray(int[] nums)
    {
        MergeSort(nums, 0, nums.Length - 1);
        return nums;

        static void MergeSort(int[] arr, int l, int r)
        {
            if (l == r) return;

            int m = (l + r) / 2;
            MergeSort(arr, l, m);
            MergeSort(arr, m + 1, r);
            Merge(arr, l, m, r);
        }

        static void Merge(int[] arr, int L, int M, int R)
        {
            int[] left = arr[L..(M + 1)], right = arr[(M + 1)..(R + 1)];
            int i = L, j = 0, k = 0;

            while (j < left.Length && k < right.Length)
            {
                if (left[j] <= right[k])
                {
                    arr[i++] = left[j++];
                }
                else
                {
                    arr[i++] = right[k++];
                }
            }

            while (j < left.Length)
            {
                arr[i++] = left[j++];
            }

            while (k < right.Length)
            {
                arr[i++] = right[k++];
            }
        }
    }
}

public class MyHashSet
{
    const int IndexRange = 1000;
    private readonly List<int>[] _arr = new List<int>[IndexRange];
    private int GetHashedIndex(int key) => key % IndexRange;

    public void Add(int key)
    {
        int hashedKey = GetHashedIndex(key);
        _arr[hashedKey] ??= []; // lazy init
        if (!_arr[hashedKey].Contains(key))
        {
            _arr[hashedKey].Add(key);
        }
    }

    public void Remove(int key) => _arr[GetHashedIndex(key)]?.Remove(key);

    public bool Contains(int key) => _arr[GetHashedIndex(key)]?.Contains(key) ?? false;
}

public class MyHashMap
{
    private record Pair(int key, int value)
    {
        public int Key { get; set; } = key;

        public int Value { get; set; } = value;
    }
    const int IndexRange = 10000;
    private readonly List<Pair>[] _arr = new List<Pair>[IndexRange];
    private int GetHashedIndex(int key) => key % IndexRange;

    public void Put(int key, int value)
    {
        int hashedKey = GetHashedIndex(key);
        int index = (_arr[hashedKey] ??= []).FindIndex(x => x.Key == key);
        if (index != -1)
        {
            _arr[hashedKey][index].Value = value;
        }
        else
        {
            _arr[hashedKey].Add(new Pair(key, value));
        }
    }

    public int Get(int key) => _arr[GetHashedIndex(key)]?.Find(p => p.Key == key)?.Value ?? -1;

    public void Remove(int key) => _arr[GetHashedIndex(key)]?.RemoveAll(p => p.Key == key);
}
