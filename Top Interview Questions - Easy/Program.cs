using static Solution;
using static Extension;

Console.WriteLine("RemoveDuplicates");
Print(Rotate([1], 1)); 
Print(Rotate([1], 2)); 
Print(Rotate([1,2], 2)); 
Print(Rotate([1,2], 0)); 
Print(Rotate([1,2], 3)); 
Print(Rotate([1, 2, 3, 4, 5, 6, 7], 1)); 
Print(Rotate([1, 2, 3, 4, 5, 6, 7], 2)); 
Print(Rotate([1, 2, 3, 4, 5, 6, 7], 3)); 
Print(Rotate([-1, -100, 3, 99], 2)); 

public static class Solution
{
    public static int[] Rotate(int[] nums, int k)
    {
        k %= nums.Length;
        Array.Reverse(nums);
        Array.Reverse(nums, 0, k);
        Array.Reverse(nums, k, nums.Length - k);
        return nums;
    }
}

public static class Extension
{
    public static void Print<T>(IEnumerable<T> values)
        => Console.WriteLine($"[{string.Join(",", values)}]");
}