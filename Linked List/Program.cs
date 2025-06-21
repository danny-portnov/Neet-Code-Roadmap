using System;
using System.Data;
using System.Linq;
using static Solution;
using static Extension;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;

//PrintListNode(ReverseList(new([1, 2, 3, 4, 5])));
//PrintListNode(ReverseList(new([1, 2, 3])));
//PrintListNode(ReverseList(new([1])));
//PrintListNode(MergeTwoLists(new([1, 2, 4]), new([1, 3, 4])));
//PrintListNode(MergeTwoLists(new([1]), new([1])));
//PrintListNode(MergeTwoLists(null, new([1])));
//PrintListNode(MergeTwoLists(null, null));
PrintListNode(ReorderList(new([1, 2, 3, 4])));
PrintListNode(ReorderList(new([1, 2, 3, 4, 5])));
PrintListNode(ReorderList(new([1])));
PrintListNode(ReorderList(new([1, 2, 3, 4, 5, 6, 7])));

public static class Solution
{
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

        public override string ToString() => val.ToString();
    }

    public static ListNode ReverseList(ListNode head)
    {
        ListNode prev = null;
        while (head != null)
        {
            ListNode next = head.next;
            head.next = prev;
            prev = head;
            head = next;
        }
        return prev;
    }

    public static bool HasCycle(ListNode head)
    {
        ListNode tort = head;
        ListNode hare = head;
        while (hare != null && hare.next != null)
        {
            tort = tort.next;
            hare = hare.next.next;
            if (tort == hare) return true;
        }
        return false;
    }

    public static ListNode ReorderListOnSpace(ListNode head)
    {
        if (head.next == null) return head;
        List<ListNode> nodes = [];
        for (int i = 0; head != null; i++)
        {
            nodes.Add(head);
            head = head.next;
        }
        head = nodes[0];
        int end = nodes.Count - 1;
        int start = 0;
        int pos = end;
        bool chooseStart = true;
        while (end >= start)
        {
            head.next = nodes[pos];
            head = head.next;

            if (chooseStart)
            {
                start++;
                pos = start;
            }
            else
            {
                end--;
                pos = end;
            }
            chooseStart = !chooseStart;
        }
        head.next = null;
        return nodes[0];
    }

    public static ListNode ReorderList(ListNode head)
    {
        ListNode tort = head, hare = head, result = head;
        while (hare != null && hare.next != null)
        {
            tort = tort.next;
            hare = hare.next.next;
        }
        ListNode middle = tort;
        ListNode reversed = ReverseList(middle);
        for (int i = 0; result != null; i++)
        {
            if (i % 2 == 0)
            {
                result.next = reversed;
                reversed = reversed?.next;
            }
            else
            {
                result.next = middle;
                middle = middle?.next;
            }
            result = result.next;
        }
        return head;
    }
    public static ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        ListNode result = new();
        ListNode head = result;
        while (list1 != null && list2 != null)
        {
            if (list1.val > list2.val)
            {
                result.next = list2;
                list2 = list2.next;
            }
            else
            {
                result.next = list1;
                list1 = list1.next;
            }
            result = result.next;
        }
        result.next = list1 ?? list2;
        return head.next;
    }
}


public static class Extension
{
    public static void Print<T>(IEnumerable<T> values)
        => Console.WriteLine($"[{string.Join(",", values)}]");

    public static void PrintListNode(ListNode l1)
    {
        Console.Write($"[{l1?.val}");
        while (l1?.next != null)
        {
            l1 = l1.next;
            Console.Write($",{l1.val}");
        }
        Console.Write("]");
        Console.WriteLine();
    }
}