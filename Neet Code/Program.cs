using static Solution;

Console.WriteLine(Search([-1, 0, 2, 4, 6, 8], 4)); //3
Console.WriteLine(Search([-1, 0, 2, 4, 6, 8], 3)); //-1
Console.WriteLine(SearchMatrix([[1, 3, 5, 7], [10, 11, 16, 20], [23, 30, 34, 60]], 3)); //true
Console.WriteLine(SearchMatrix([[1, 3, 5, 7], [10, 11, 16, 20], [23, 30, 34, 60]], 13)); //false
Console.WriteLine(SearchMatrix([[1]], 13)); //false
Console.WriteLine(MinEatingSpeed([1, 4, 3, 2], 9)); //2

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
        int low = 1, high = piles.Max();
        int hours = h;
        while (low <= high)
        {
            int k = (low + high) / 2;
            for (int i = 0; i < piles.Length; i++)
            {
                hours -= piles[i] / k;
            }
            if (hours == 0)
            {
                return k;
            }
            if (hours > 0)
            {
                high = k - 1;
            }
            else
            {
                low = k + 1;
            }
            hours = h;
        }
        return -1;
    }
}
