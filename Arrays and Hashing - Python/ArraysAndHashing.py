
from collections import defaultdict
from typing import List, Union


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

    def createHist(self, s: Union[str, int]) -> dict[Union[str, int], int]:
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
        )  # Create a bucket for each possible frequency, 1 -> nums' length
        for num in nums:
            hist[num] += 1
        for num in hist:
            freqHist[hist[num]].append(num)
        for freq in freqHist:
            result[freq - 1] = freqHist[freq]  # Assign to each frequency its number*S*
        return [num for sublist in result if len(sublist) != 0 for num in sublist][
            -k:
        ]  # Return last greater than zero k elements

    # Design an algorithm to encode a list of strings to a string.
    # The encoded string is then sent over the network
    # and is decoded back to the original list of strings.

    def encode(self, strs: List[str]) -> str:
        string: str = ""
        for s in strs:
            string += f"{len(s)}:{s}"
        return string

    def decode(self, s: str) -> List[str]:
        result: List[str] = []
        i = 0  # Index in 's'
        while i < len(s):
            end = s.find(":", i)
            curr = int(s[i:end])
            # Length of expected next string, could be double digit
            result.append(s[end + 1 : end + 1 + curr])
            # String starts after the length specified till last char
            i = end + 1 + curr
            # Move 'i' to the NEXT length expected (the first char AFTER the last string)
        return result

    def longestConsecutive(self, nums: List[int]) -> int:
        """
        Given an array of integers nums, return the length of the longest consecutive sequence of elements that can be formed.
        A consecutive sequence is a sequence of elements in which each element is exactly 1 greater than the previous element
        The elements do not have to be consecutive in the original array.
        """
        hist: dict[int, int] = self.createHist(nums)
        sequenceStarters: set[int] = set()
        maxCount = 0
        for num in nums:
            if num - 1 not in hist:
                sequenceStarters.add(num)
        for x in sequenceStarters:
            count = 0
            while x in hist:
                count += 1
                x += 1
            maxCount = max(maxCount, count)
        return maxCount


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
strs = [["Hello", "World"], ["we", "say", ":", "yes", "!@#$%^&*()"]]
for x in strs:
    encoder = solution.encode(x)
    print(f"{x} -> {encoder} -> {solution.decode(encoder)}")
print(solution.longestConsecutive([2, 20, 4, 10, 3, 4, 5]))  # 4
print(solution.longestConsecutive([0, 3, 2, 5, 4, 6, 1, 1]))  # 7

# ["Hello","World"] -> 5Hello5World -> ["Hello","World"]
# ["we","say",":","yes","!@#$%^&*()"] -> 2we3say1:3yes10!@#$%^&*() ->["we","say",":","yes","!@#$%^&*()"]
