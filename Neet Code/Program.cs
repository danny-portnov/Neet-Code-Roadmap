using static Solution;

Console.WriteLine(Search([-1, 0, 2, 4, 6, 8], 4)); //3
Console.WriteLine(Search([-1, 0, 2, 4, 6, 8], 3)); //-1
Console.WriteLine(SearchMatrix([[1, 3, 5, 7], [10, 11, 16, 20], [23, 30, 34, 60]], 3)); //true
Console.WriteLine(SearchMatrix([[1, 3, 5, 7], [10, 11, 16, 20], [23, 30, 34, 60]], 13)); //false
Console.WriteLine(SearchMatrix([[1]], 13)); //false
Console.WriteLine(MinEatingSpeed([1, 4, 3, 2], 9)); //2
Console.WriteLine(MinEatingSpeed([1, 1, 1, 999999999], 10)); //142857143
Console.WriteLine(MinEatingSpeed([3,6,7,11], 8)); //4
Console.WriteLine(MinEatingSpeed([30,11,23,4,20], 5)); //30
Console.WriteLine(MinEatingSpeed([30,11,23,4,20], 6)); //23
Console.WriteLine("FindMin");
Console.WriteLine(FindMin([3, 4, 5, 1, 2])); //1
Console.WriteLine(FindMin([4, 5, 6, 7, 0, 1, 2])); //0
Console.WriteLine(FindMin([17, 11, 13, 15])); //11
Console.WriteLine(FindMin([11, 13, 15, 17])); //11
Console.WriteLine(FindMin([4, 5, 1, 2, 3])); //1
Console.WriteLine(FindMin([5, 1, 2, 3, 4])); //1
Console.WriteLine("SearchInRotated");
Console.WriteLine(SearchInRotated([3, 4, 5, 6, 1, 2], 1)); // 4
Console.WriteLine(SearchInRotated([3, 5, 6, 0, 1, 2], 4)); // -1
Console.WriteLine(SearchInRotated([3, 4, 5, 1, 2], 2)); // 4
Console.WriteLine(SearchInRotated([4, 5, 6, 7, 0, 1, 2], 4)); //0
Console.WriteLine(SearchInRotated([17, 11, 13, 15], 11)); //1
Console.WriteLine(SearchInRotated([11, 13, 15, 17], 11)); //0
Console.WriteLine(SearchInRotated([4, 5, 1, 2, 3], 1)); //2
Console.WriteLine(SearchInRotated([5, 1, 2, 3, 4], 3)); //3
Console.WriteLine(SearchInRotated([3, 1], 1)); //1
Console.WriteLine(SearchInRotated([3, 1], 3)); //0

TimeMap timeMap = new();
//timeMap.Set("alice", "happy", 1); // store the key "alice" and value "happy" along with timestamp = 1.
//Console.WriteLine(timeMap.Get("alice", 1));                // return "happy"
//Console.WriteLine(timeMap.Get("alice", 2));                // return "happy", there is no value stored for timestamp 2, thus we return the value at timestamp 1.
//timeMap.Set("alice", "sad", 3);   // store the key "alice" and value "sad" along with timestamp = 3.
//Console.WriteLine(timeMap.Get("alice", 3));                // return "sad"

//// Overwriting with new timestamp
//Console.WriteLine(timeMap.Get("alice", 3)); // "sad"
//Console.WriteLine(timeMap.Get("alice", 2)); // "happy"
//Console.WriteLine(timeMap.Get("alice", 4)); // "sad"

//// Different key
//timeMap.Set("bob", "excited", 2);
//Console.WriteLine(timeMap.Get("bob", 1)); // "" (nothing <= 1)
//Console.WriteLine(timeMap.Get("bob", 2)); // "excited"
//Console.WriteLine(timeMap.Get("bob", 3)); // "excited"

//// Multiple sets for same key
//timeMap.Set("alice", "angry", 5);
//timeMap.Set("alice", "tired", 10);
//Console.WriteLine(timeMap.Get("alice", 5));  // "angry"
//Console.WriteLine(timeMap.Get("alice", 6));  // "angry"
//Console.WriteLine(timeMap.Get("alice", 10)); // "tired"
//Console.WriteLine(timeMap.Get("alice", 100)); // "tired"

//// No such key
//Console.WriteLine(timeMap.Get("charlie", 1)); // ""

//// Get before any set
//Console.WriteLine(timeMap.Get("alice", 0)); // ""

timeMap.Set("love", "high", 10);
timeMap.Set("love", "low", 20);

Console.WriteLine(timeMap.Get("love", 5));   // Expect: ""
Console.WriteLine(timeMap.Get("love", 10));  // Expect: "high"
Console.WriteLine(timeMap.Get("love", 15));  // Expect: "high"
Console.WriteLine(timeMap.Get("love", 20));  // Expect: "low"
Console.WriteLine(timeMap.Get("love", 25));  // Expect: "low"


//// Multiple close timestamps
//timeMap.Set("dave", "v1", 5);
//timeMap.Set("dave", "v2", 6);
//timeMap.Set("dave", "v3", 7);
//Console.WriteLine(timeMap.Get("dave", 6)); // "v2"
//Console.WriteLine(timeMap.Get("dave", 8)); // "v3"
public static class Solution
{
    public static int Search(int[] nums, int target)
    {
        int low = 0, high = nums.Length - 1;
        
        while (low <= high)
        {
            int mid = (low + high) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }
            if (nums[mid] > target)
            {
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }
        return -1;
    }

    public static bool SearchMatrix(int[][] matrix, int target)
    {
        int rowlength = matrix.First().Length;
        int low = 0, high = rowlength * matrix.Length - 1;

        while (low <= high)
        {
            int mid = (low + high) / 2;
            int row = mid / rowlength;
            int column = mid % rowlength;
            if (matrix[row][column] == target)
            {
                return true;
            }
            if (matrix[row][column] > target)
            {
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }
        return false;
    }

    public static int MinEatingSpeed(int[] piles, int h)
    {
        int low = 1, high = piles.Max(), minK = 0;
        while (low <= high)
        {
            int k = (low + high) / 2;
            long hours = 0;
            foreach (int pile in piles)
            {
                hours += (int)Math.Ceiling((double)pile / k);
            }
            if (hours <= h) // Hours = h doesn't necessarily mean a minimum k was found!
            {
                high = k - 1;
                minK = k;
            }
            else
            {
                low = k + 1;
            }
        }
        return minK;
    }

    public static int FindMin(int[] nums)
    {
        int low = 0, high = nums.Length - 1;
        while (low < high) 
        {
            int mid = (low + high) / 2;
            if (nums[mid] < nums[high])
            {
                high = mid;
            }
            else
            {
                low = mid + 1;
            }
        }
        return nums[low];
    }

    public static int SearchInRotated(int[] nums, int target)
    {
        int low = 0, high = nums.Length - 1;
        while (low <= high)
        {
            int mid = (low + high) / 2;
            if (nums[mid] == target)
            {
                return mid;
            }
            if (nums[low] > nums[mid]) // Mid is on the right half (not equal because if they're equal, mid is on the left).
            {
                // We need to move to the left half, if....
                // Target on the left (less then mid) or target is on the left half (higher than high) 
                if (nums[mid] > target || nums[high] < target) 
                {
                    high = mid - 1;
                }
                else
                {
                    low = mid + 1;
                }
            }
            else // Mid is on the left half
            {
                // We need to move to the right half, if....
                // Target on the right (bigger than mid) or target is on the right half (lower than low) 
                if (nums[mid] < target || nums[low] > target) 
                {
                    low = mid + 1;
                }
                // Otherwise, keep moving left.
                else
                {
                    high = mid - 1;
                }
            }
        }
        return -1;
    }
}

public class TimeMap
{
    public class Value(string valueName, List<int> timestamps)
    {
        public string Name = valueName;
        public List<int> Timestamps = timestamps;
    }

    //Dictionary<string, Value> dict;
    Dictionary<string, List<int>> keyToTimestamps = [];
    Dictionary<int, string> timestampToValue = [];
    public void Set(string key, string value, int timestamp)
    {
        if (keyToTimestamps.TryGetValue(key, out List<int>? val))
        {
            val.Add(timestamp);
        }
        else
        {
            keyToTimestamps.Add(key, [timestamp]);
        }
        timestampToValue.Add(timestamp, value);
    }

    public string Get(string key, int timestamp)
    {
        if (keyToTimestamps.TryGetValue(key, out List<int>? val))
        {
            int low = 0, high = val.Count - 1, result = -1;
            while (low <= high)
            {
                int mid = (low + high) / 2;
                if (val[mid] == timestamp)
                {
                    result = mid;
                    break;
                }
                if (val[mid] > timestamp)
                {
                    high = mid - 1;
                }
                else
                {
                    result = mid;
                    low = mid + 1;
                }
            }
            if (result != -1)
            {
                return timestampToValue[val[result]];
            }
        }
        return string.Empty;
    }
}
