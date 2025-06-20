
//Definition for singly - linked list.

public class ListNode
{
    public int val;
    public ListNode? next;
    public ListNode(int val = 0, ListNode? next = null)
    {
        this.val = val;
        this.next = next;
    }

    public ListNode(int[] nums)
    {
        val = nums[0];
        next = nums.Length == 1
            ? null
            : new ListNode(nums[1..]);
    }
}//

public static class Solution
{
    public static ListNode AddTwoNumbersIterative(ListNode? l1, ListNode? l2)
    {
        ListNode result = new();
        ListNode beginningOfResult = result;
        while (l1 != null || l2 != null)
        {
            int sum = (l1?.val ?? 0) + (l2?.val ?? 0);
            result.val += sum;
            if (l1?.next != null || l2?.next != null || result.val >= 10)
            {
                result.next = new();
            }
            if (result.val >= 10)
            {
                result.val -= 10;
                result.next.val = 1;
            }

            l1 = l1?.next;
            l2 = l2?.next;
            result = result.next;
        }
        return beginningOfResult;
    }

    public static ListNode AddTwoNumbers(ListNode l1, ListNode l2, int carry = 0)
    {
        // Stop condition: if reached the end of both lists, and no carry, return null
        if (l1 == null && l2 == null && carry == 0) return null;
        // Add the values (0 if null) with the carry
        int sum = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
        // Create a node; its val is sum mod 10 (sum -= 10), and next is created recursivly.
        //  The carry is passed on, is 1 if sum >= 10.
        return new ListNode(sum % 10, AddTwoNumbers(l1?.next, l2?.next, sum / 10));
    }

    // Shorter version
    public static ListNode? Add2Numbers(ListNode? l1, ListNode? l2, int carry = 0)
    {
        int sum = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
        return (l1, l2, carry) == (null, null, 0)
            ? null
            : new ListNode(sum % 10, Add2Numbers(l1?.next, l2?.next, sum / 10));
    }

}

public static class Program
{
    public static void Main()
    {
        ListNode l1 = new([2, 4, 3]);
        //ListNode l1 = new([9, 9, 9, 9, 9, 9, 9]);
        PrintListNode(l1);
        Console.Write('+');
        ListNode l2 = new([5, 6, 4]);
        //ListNode l2 = new([9, 9]);
        PrintListNode(l2);
        Console.Write('=');
        ListNode l = Solution.Add2Numbers(l1, l2);
        PrintListNode(l);
    }

    private static void PrintListNode(ListNode l1)
    {
        if (l1 == null)
        {
            return;
        }
        PrintListNode(l1.next);
        Console.Write(l1.val);
    }
}