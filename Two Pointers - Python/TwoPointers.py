class Solution:
    def isPalindrome(self, s: str) -> bool:
        (r, l) = 0, len(s) - 1
        while r < l:
            while not s[r].isalnum() and r < l:
                r += 1
            while not s[l].isalnum() and r < l:
                l -= 1
            if s[r].lower() != s[l].lower():
                return False
            r += 1
            l -= 1
        return True

    # Given an array of integers numbers that is sorted in non-decreasing order.
    # Return the indices (1-indexed) of two numbers, [index1, index2],
    # such that they add up to a given target number target and index1 < index2.
    # Note that index1 and index2 cannot be equal, therefore you may not use the same element twice.
    # There will always be exactly one valid solution.
    # Your solution must use  O(1) additional space.

    def twoSum(self, numbers: list[int], target: int) -> list[int]:
        (r, l) = 0, len(numbers) - 1
        while r < l:
            targetSum = numbers[r] + numbers[l]
            if targetSum < target:
                r += 1
            elif targetSum > target:
                l -= 1
            else:  # Target found
                return [r + 1, l + 1]

    def maxArea(self, heights: list[int]) -> int:

        (r, l) = 0, len(heights) - 1
        maxArea = 0
        while r < l:
            maxArea = max(min(heights[r], heights[l]) * (l - r), maxArea)
            if heights[l] < heights[r]:
                l -= 1
            else:
                r += 1
        return maxArea


solution = Solution()
print("isPalindrome")
print(solution.isPalindrome("Was it a car or a cat I saw?"))  # True
print(solution.isPalindrome("tab a cat"))  # False
print("twoSum")
print(solution.twoSum(numbers=[1, 2, 3, 4], target=3))  # [1,2]
print(solution.twoSum([2, 7, 11, 15], target=9))  # [1,2]
print(solution.twoSum([2, 3, 4], target=6))  # [1,3]
print(solution.twoSum([-1, 0], target=-1))  # [1,2]
print(solution.twoSum([1, 2, 3, 4], target=3))  # [1,2]
print("maxArea")
print(solution.maxArea([1, 7, 2, 5, 4, 7, 3, 6]))  # 36
print(solution.maxArea([2, 2, 2]))  # 4
print(solution.maxArea([1, 7, 2, 5, 12, 3, 500, 500, 7, 8, 4, 7, 3, 6]))  # 500