
# Definition for singly-linked list.
from typing import Optional


class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next

    @classmethod
    def from_list(cls, vals: list[int]):
        """Factory method to create a linked list from a Python list."""
        if not vals:
            return None

        # Create the head node
        head = cls(vals[0])
        current = head

        # Build the rest of the chain
        for v in vals[1:]:
            current.next = cls(v)
            current = current.next

        return head

    def __str__(self):
        """Allows printing the linked list in a readable format."""
        nodes = []
        current = self
        while current:
            nodes.append(str(current.val))
            current = current.next
        return "[" + ", ".join(nodes) + "]"

    def __repr__(self):
        return self.__str__()


class Solution:

    def reverseList(self, head: Optional[ListNode]) -> Optional[ListNode]:
        curr = None
        while head:
            nextNode = head.next
            head.next = curr
            curr = head
            head = nextNode
        return curr


class SolutionFromList:
    def __init__(self):
        self.sol = Solution()

    def reverseList(self, head: Optional[list[int]]) -> Optional[list[int]]:
        return self.sol.reverseList(ListNode.from_list(head))


solution = SolutionFromList()
print("reverseList")
print(solution.reverseList([0, 1, 2, 3]))  # [3,2,1,0]