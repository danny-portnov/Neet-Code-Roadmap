using static Solution;
using static Extension;

Console.WriteLine("ReverseList");
PrintListNode(ReverseList([1, 2, 3, 4, 5]));
PrintListNode(ReverseList([1, 2, 3]));
PrintListNode(ReverseList([1]));

Console.WriteLine("MergeTwoLists");
PrintListNode(MergeTwoLists([1, 2, 4], [1, 3,5]));
PrintListNode(MergeTwoLists([1, 2, 4], [1]));
PrintListNode(MergeTwoLists([1], [1]));
PrintListNode(MergeTwoLists([], [1]));
PrintListNode(MergeTwoLists([], []));

Console.WriteLine("HasCycle");
Console.WriteLine(HasCycle([1, 2, 3, 4], 1)); // true
Console.WriteLine(HasCycle([1, 2, 3, 4], -1)); // false


Console.WriteLine("ReorderList");
//PrintListNode(ReorderList(new([1, 2, 3, 4])));
//PrintListNode(ReorderList(new([1, 2, 3, 4, 5])));
//PrintListNode(ReorderList(new([1])));
//PrintListNode(ReorderList(new([1, 2, 3, 4, 5, 6, 7])));

public static class Solution
{
    public class ListNode
    {
        public int val;
        public ListNode? next;

        private ListNode(int val = 0, ListNode? next = null)
        {
            this.val = val;
            this.next = next;
        }

        //public ListNode(int[] nums = default, int index = -1, string s)
        //{
        //    _count++;
        //    if (_count == index + 1)
        //    {
        //        _cycleNode = this;
        //    }
        //    Value = nums?.ElementAt(0) ?? 0;
        //    Next = (nums?.Length ?? 0) > 1
        //        ? new ListNode(nums[1..], index)
        //        : _cycleNode;
        //    _count = 0;
        //}

        public ListNode(int[]? values = default, int index = -1)
        {
            values ??= [0];
            val = values[0];

            ListNode current = this;
            ListNode? cycleEntry = index == 0 ? this : null;

            for (int i = 1; i < values.Length; i++)
            {
                current = current.next = new ListNode(values[i]);

                if (i == index)
                {
                    cycleEntry = current;
                }
            }

            if (index != -1)
            {
                current.next = cycleEntry;
            }
        }
        public override string ToString() => val.ToString();
    }

    public static ListNode ReverseList(ListNode head)
    {
        ListNode? prev = null;
        while (head != null)
        {
            ListNode? next = head?.next;
            head.next = prev;
            prev = head;
            head = next;
        }
        return prev;
    }

    public static ListNode ReverseList(int[] nums)
        => ReverseList(new ListNode(nums));

    public static bool HasCycle(ListNode? head)
    {
        ListNode? slow = head;
        ListNode? fast = head?.next;
        while (fast != null && slow != null)
        {
            if (fast == slow)
            {
                return true;
            }
            slow = slow?.next;
            fast = fast?.next?.next;
        }
        return false;
    }

    public static bool HasCycle(int[] nums, int index)
        => HasCycle(new ListNode(nums, index));

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

    public static ListNode? MergeTwoLists(int[] list1, int[] list2)
        => MergeTwoLists(list1.Length > 0 ? new ListNode(list1) : null,
            list2?.Length > 0 ? new ListNode(list2) : null);
    public static ListNode? MergeTwoLists(ListNode? list1, ListNode? list2)
    {
        ListNode result = new();
        ListNode head = result;
        while (list1 != null && list2 != null)
        {
            if (list1.val < list2.val)
            {
                result.next = list1;
                list1 = list1.next;
            }
            else
            {
                result.next = list2;
                list2 = list2.next;
            }
            result = result.next;
        }
        result.next = list2 ?? list1;
        return head.next;
    }
}


public static class Extension
{
    public static void Print<T>(IEnumerable<T> values)
        => Console.WriteLine($"[{string.Join(",", values)}]");

    public static void PrintListNode(ListNode? l1)
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