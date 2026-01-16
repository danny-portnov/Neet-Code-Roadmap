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


solution = Solution()
print("isPalindrome")
print(solution.isPalindrome("Was it a car or a cat I saw?"))  # True
print(solution.isPalindrome("tab a cat"))  # False