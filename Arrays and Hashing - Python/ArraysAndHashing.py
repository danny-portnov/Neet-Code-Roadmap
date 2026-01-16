
from collections import defaultdict
from typing import List


class Solution:
    # Given an integer array nums, return true if any value appears more than once in the array, otherwise return false.
    def hasDuplicate(self, nums: List[int]) -> bool:
        # uniqueNums = set()
        # for num in nums:
        #     if num not in uniqueNums:
        #         uniqueNums.add(num)
        #     else:
        #         return True
        # return False
        return len(set(nums)) < len(nums)

    # Given two strings s and t, return true if the two strings are anagrams of each other, otherwise return false.
    # An anagram is a string that contains the exact same characters as another string, but the order of the characters can be different.
    def isAnagram(self, s: str, t: str) -> bool:
        return self.createHist(t) == self.createHist(s)

    def createHist(self, s: str) -> dict[str, int]:
        hist = {}
        for c in s:
            hist[c] = hist.get(c, 0) + 1
        return hist

    def createHistArray(self, s: str) -> List[int]:
        hist = [0] * 26
        for c in s:
            hist[ord(c) - ord("a")] += 1
        return hist

    def createHistTuple(self, s: str) -> tuple[int, ...]:
        return tuple(self.createHistArray(s))

    # Given an array of integers nums and an integer target,
    # return the indices i and j such that nums[i] + nums[j] == target and i != j.
    # You may assume that every input has exactly one pair of
    # indices i and j that satisfy the condition.
    # Return the answer with the smaller index first.
    def twoSum(self, nums: List[int], target: int) -> List[int]:
        hist = {}
        for i, num in enumerate(nums):
            hist[num] = i
        for i, num in enumerate(nums):
            if target - num in hist and hist[target - num] != i:
                return [i, hist[target - num]]

    # Given an array of strings strs, group all anagrams
    # together into sublists. You may return the output in any order.
    # An anagram is a string that contains
    # the exact same characters as another string, but the order of the characters can be different.
    def groupAnagrams(self, strs: List[str]) -> List[List[str]]:
        """
        Given an array of strings strs, group all anagrams
        together into sublists. You may return the output in any order.
        An anagram is a string that contains
        the exact same characters as another string, but the order of the characters can be different.
        """
        result: dict[tuple[int, ...], List[str]] = defaultdict(
            list
        )  # Creates an empty list for each key inserted
        for s in strs:
            hist = self.createHistTuple(s)
            result[hist].append(s)
        return list(result.values())

    def topKFrequent(self, nums: List[int], k: int) -> List[int]:
        """
        Given an integer array nums and an integer k, return the k most frequent elements. You may return the answer in any order.
        """
        hist: dict[int, int] = defaultdict(int)
        freqHist: dict[int, List[int]] = defaultdict(list)
        result: List[List[int]] = [[]] * len(
            nums
        )  # Creat a bucket for each possible frequency, 1 -> nums' length
        for num in nums:
            hist[num] += 1
        for num in hist:
            freqHist[hist[num]].append(num)
        for freq in freqHist:
            result[freq - 1] = freqHist[freq]  # Assign to each frequency its number*S*
        return [num for sublist in result if len(sublist) != 0 for num in sublist][
            -k:
        ]  # Return last greater than zero k elements


solution = Solution()
print("hasDuplicate")
print(solution.hasDuplicate([1, 2, 3, 1]))  # True
print(solution.hasDuplicate([1, 2, 3, 4]))  # False
print(solution.isAnagram(s="racecar", t="carrace"))  # True
print(solution.isAnagram(s="jar", t="jam"))  # False
print(solution.isAnagram(s="xx", t="x"))  # False
print(solution.isAnagram(s="xx", t="x"))  # False
print(
    solution.groupAnagrams(strs=["act", "pots", "tops", "cat", "stop", "hat"])
)  # [["hat"],["act", "cat"],["stop", "pots", "tops"]]
print(solution.topKFrequent(nums=[1, 2, 2, 3, 3, 3], k=2))  # [2,3]
print(solution.topKFrequent(nums=[1, 2, 2, 3, 3, 3], k=1))  # [3]
print(solution.topKFrequent(nums=[1, 2, 2, 2, 3, 3, 3], k=2))  # [2,3]
print(solution.topKFrequent(nums=[1, 2, 1, 2, 1, 2, 3, 1, 3, 2], k=2))  # [1,2]
print(solution.topKFrequent(nums=[7, 7], k=1))  # [7]
