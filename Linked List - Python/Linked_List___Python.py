
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

    def mergeTwoLists(
        self, list1: Optional[ListNode], list2: Optional[ListNode]
    ) -> Optional[ListNode]:
        dummy = head = ListNode()
        while list1 or list2:
            if list1 and (not list2 or list1.val < list2.val):
                head.next = list1
                list1 = list1.next
            else:
                head.next = list2
                list2 = list2.next
            head = head.next
        return dummy.next


class SolutionFromList:
    def __init__(self):
        self.sol = Solution()

    def reverseList(self, head: Optional[list[int]]) -> Optional[list[int]]:
        return self.sol.reverseList(ListNode.from_list(head))

    def mergeTwoLists(
        self, list1: Optional[ListNode], list2: Optional[ListNode]
    ) -> Optional[ListNode]:
        return self.sol.mergeTwoLists(
            ListNode.from_list(list1), ListNode.from_list(list2)
        )


solution = SolutionFromList()
print("reverseList")
print(solution.reverseList([0, 1, 2, 3]))  # [3,2,1,0]
print("mergeTwoLists")
print(solution.mergeTwoLists(list1=[1, 2, 4], list2=[1, 3, 5]))  #  [1,1,2,3,4,5]
print(solution.mergeTwoLists(list1=[1, 3, 5], list2=[1, 2, 4]))  #  [1,1,2,3,4,5]
print(solution.mergeTwoLists(list1=[], list2=[1, 2]))  #  [1,2]
print(solution.mergeTwoLists(list1=[1, 2], list2=[]))  #  [1,2]
print(solution.mergeTwoLists(list1=[], list2=[]))  #  []