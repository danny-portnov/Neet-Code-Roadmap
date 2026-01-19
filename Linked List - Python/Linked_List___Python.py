
# Definition for singly-linked list.
from __future__ import annotations
from typing import Optional


class ListNode:
    def __init__(self, val=0, next: Optional[ListNode] = None):
        self.val = val
        self.next = next

    @classmethod
    def from_list(cls, vals: list[int], index: int = -1) -> Optional[ListNode]:
        """Creates a linked list from a list. If pos >= 0, creates a cycle to that index."""
        if not vals:
            return None

        # 1. Create all nodes and store them in a list for easy indexing
        nodes = [cls(v) for v in vals]

        # 2. Link the nodes together
        for i in range(len(nodes) - 1):
            nodes[i].next = nodes[i + 1]

        # 3. Create the cycle if pos is valid
        if index != -1 and 0 <= index < len(nodes):
            nodes[-1].next = nodes[index]

        return nodes[0]

    def __str__(self) -> str:
        """Prints the list, handling cycles safely."""
        res = []
        visited = set()
        curr = self

        while curr:
            if curr in visited:
                res.append(f"CYCLE_TO({curr.val})")
                break
            visited.add(curr)
            res.append(str(curr.val))
            curr = curr.next

        return "[" + " -> ".join(res) + "]"

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
        while list1 and list2:
            if list1.val < list2.val:
                head.next = list1
                list1 = list1.next
            else:
                head.next = list2
                list2 = list2.next
            head = head.next
        # Returns the first "truthy" value i.e. non-empty list
        head.next = list1 or list2
        return dummy.next

    def hasCycle(self, head: Optional[ListNode]) -> bool:
        slow = head
        fast = head and head.next and head.next.next
        while fast is not None and fast != slow:
            slow = slow.next
            fast = fast.next and fast.next.next
        return fast == slow and fast is not None


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

    def hasCycle(self, head: Optional[ListNode], index: int) -> bool:
        return self.sol.hasCycle(ListNode.from_list(head, index))


solution = SolutionFromList()
print("reverseList")
print(solution.reverseList([0, 1, 2, 3]))  # [3,2,1,0]
print("mergeTwoLists")
print(solution.mergeTwoLists(list1=[1, 2, 4], list2=[1, 3, 5]))  #  [1,1,2,3,4,5]
print(solution.mergeTwoLists(list1=[1, 3, 5], list2=[1, 2, 4]))  #  [1,1,2,3,4,5]
print(solution.mergeTwoLists(list1=[], list2=[1, 2]))  #  [1,2]
print(solution.mergeTwoLists(list1=[1, 2], list2=[]))  #  [1,2]
print(solution.mergeTwoLists(list1=[], list2=[]))  #  []
print("hasCycle")
print(solution.hasCycle(head=[1, 2, 3, 4], index=1))  # True
print(solution.hasCycle(head=[1, 2], index=-1))  # False
print(solution.hasCycle(head=[], index=-1))  # False