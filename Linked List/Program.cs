using System;
using System.Data;
using System.Linq;
using static Solution;
using static Extension;

Console.WriteLine("ReverseList");
PrintListNode(ReverseList([1, 2, 3, 4, 5]));
PrintListNode(ReverseList([1, 2, 3]));
PrintListNode(ReverseList([1]));

Console.WriteLine("MergeTwoLists");
PrintListNode(MergeTwoLists([1, 2, 4], [1, 3,5]));
PrintListNode(MergeTwoLists([1], [1]));
PrintListNode(MergeTwoLists([], [1]));
PrintListNode(MergeTwoLists([], []));
//PrintListNode(ReorderList(new([1, 2, 3, 4])));
//PrintListNode(ReorderList(new([1, 2, 3, 4, 5])));
//PrintListNode(ReorderList(new([1])));
//PrintListNode(ReorderList(new([1, 2, 3, 4, 5, 6, 7])));

public static class Solution
{
    public class ListNode
    {
        public int Value;
        public ListNode? Next;

        public ListNode(int[] nums = default)
        {
            Value = nums?.ElementAt(0) ?? 0;
            Next = (nums?.Length ?? 0) > 1
                ? new ListNode(nums[1..])
                : null;
        }

        public override string ToString() => Value.ToString();
    }

    public static ListNode ReverseList(ListNode head)
    {
        ListNode? prev = null;
        while (head != null)
        {
            ListNode? next = head?.Next;
            head.Next = prev;
            prev = head;
            head = next;
        }
        return prev;
    }

    public static ListNode ReverseList(int[] nums)
        => ReverseList(new ListNode(nums));

    public static bool HasCycle(ListNode head)
    {
        ListNode tort = head;
        ListNode hare = head;
        while (hare != null && hare.Next != null)
        {
            tort = tort.Next;
            hare = hare.Next.Next;
            if (tort == hare) return true;
        }
        return false;
    }

    public static ListNode ReorderListOnSpace(ListNode head)
    {
        if (head.Next == null) return head;
        List<ListNode> nodes = [];
        for (int i = 0; head != null; i++)
        {
            nodes.Add(head);
            head = head.Next;
        }
        head = nodes[0];
        int end = nodes.Count - 1;
        int start = 0;
        int pos = end;
        bool chooseStart = true;
        while (end >= start)
        {
            head.Next = nodes[pos];
            head = head.Next;

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
        head.Next = null;
        return nodes[0];
    }

    public static ListNode ReorderList(ListNode head)
    {
        ListNode tort = head, hare = head, result = head;
        while (hare != null && hare.Next != null)
        {
            tort = tort.Next;
            hare = hare.Next.Next;
        }
        ListNode middle = tort;
        ListNode reversed = ReverseList(middle);
        for (int i = 0; result != null; i++)
        {
            if (i % 2 == 0)
            {
                result.Next = reversed;
                reversed = reversed?.Next;
            }
            else
            {
                result.Next = middle;
                middle = middle?.Next;
            }
            result = result.Next;
        }
        return head;
    }

    public static ListNode? MergeTwoLists(int[] list1, int[] list2)
        => MergeTwoLists(list1.Length > 0 ? new ListNode(list1) : null,
            list2?.Length > 0 ? new ListNode(list2) : null);
    public static ListNode? MergeTwoLists(ListNode? list1, ListNode? list2)
    {
        ListNode result = new();
        ListNode head = result;
        while (list1 != null && list2 != null)
        {
            if (list1.Value < list2.Value)
            {
                result.Next = list1;
                list1 = list1.Next;
            }
            else
            {
                result.Next = list2;
                list2 = list2.Next;
            }
            result = result.Next;
        }
        result.Next = list2 ?? list1;
        return head.Next;
    }
}


public static class Extension
{
    public static void Print<T>(IEnumerable<T> values)
        => Console.WriteLine($"[{string.Join(",", values)}]");

    public static void PrintListNode(ListNode? l1)
    {
        Console.Write($"[{l1?.Value}");
        while (l1?.Next != null)
        {
            l1 = l1.Next;
            Console.Write($",{l1.Value}");
        }
        Console.Write("]");
        Console.WriteLine();
    }
}