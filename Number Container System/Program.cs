using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using System.Collections.Generic;

NumberContainers nc = new();
nc.Find(10); // There is no index that is filled with number 10. Therefore, we return -1.
nc.Change(2, 10); // Your container at index 2 will be filled with number 10.
nc.Change(1, 10); // Your container at index 1 will be filled with number 10.
nc.Change(3, 10); // Your container at index 3 will be filled with number 10.
nc.Change(5, 10); // Your container at index 5 will be filled with number 10.
nc.Find(10); // Number 10 is at the indices 1, 2, 3, and 5. Since the smallest index that is filled with 10 is 1, we return 1.
nc.Change(1, 20); // Your container at index 1 will be filled with number 20. Note that index 1 was filled with 10 and then replaced with 20. 
nc.Find(10); // Number 10 is at the indices 2, 3, and 5. The smallest index that is filled with 10 is 2. Therefore, we return 2.


/// <summary>
/// Design a number container system that can do the following: 
/// Insert or Replace a number at the given index in the system. 
/// Return the smallest index for the given number in the system. 
/// Implement the NumberContainers class: NumberContainers() Initializes the number container system.
/// </summary>
public class NumberContainers
{
    readonly SortedDictionary<int, SortedSet<int>> numberToIndex = [];
    readonly Dictionary<int, int> indexToNumber = [];

    /// <summary>
    /// Fills the container at index with the number.If there is already a number at that index, replace it.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="number"></param>
    public void Change(int index, int number)
    {
        if (indexToNumber.TryGetValue(index, out int oldNumber))
        {
            // Remove old number's index mapping
            numberToIndex[oldNumber].Remove(index);
            if (numberToIndex[oldNumber].Count == 0)
            {
                numberToIndex.Remove(oldNumber);
            }
        }

        // Add new number's index mapping
        if (!numberToIndex.TryGetValue(number, out SortedSet<int>? value))
        {
            value = [];
            numberToIndex[number] = value;
        }

        value.Add(index);

        // Update index-to-number mapping
        indexToNumber[index] = number;
    }

    /// <summary>
    /// Returns the smallest index for the given number, or -1 if there is no index that is filled by number in the system.
    /// </summary>
    /// <param name="number"></param>
    /// <returns></returns>
    public int Find(int number)
        => numberToIndex.TryGetValue(number, out SortedSet<int>? value)
            ? value.First()
            : -1;
}

/**
 * Your NumberContainers object will be instantiated and called as such:
 * NumberContainers obj = new NumberContainers();
 * obj.Change(index,number);
 * int param_2 = obj.Find(number);
 */

