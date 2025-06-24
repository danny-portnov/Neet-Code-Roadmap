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
Console.WriteLine(HasCycle([], -1)); // false
Console.WriteLine(HasCycle([1], 0)); // true
Console.WriteLine(HasCycle([1], -1)); // false
Console.WriteLine(HasCycle([1,2], 0)); // true
Console.WriteLine(HasCycle([1,2], -1)); // false
Console.WriteLine(HasCycle([1, 2, 3], 1)); // true
Console.WriteLine(HasCycle([1, 2, 3], -1)); // false
Console.WriteLine(HasCycle([1, 2, 3, 4], 1)); // true
Console.WriteLine(HasCycle([1, 2, 3, 4], -1)); // false


Console.WriteLine("ReorderList");
PrintListNode(ReorderList([1])); // 1
PrintListNode(ReorderList([1, 2])); // 1, 2
PrintListNode(ReorderList([1, 2, 3])); // 1, 3, 2 
PrintListNode(ReorderList([1, 2, 3, 4])); // 1, 4, 2, 3
PrintListNode(ReorderList([1, 2, 3, 4, 5])); // 1,5,2,4,3
PrintListNode(ReorderList([1, 2, 3, 4, 5, 6, 7])); // 1,7,2,6,3,5,4

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

        public ListNode(int[]? values, int index = -1)
        {
            val = (values ??= [0]).ElementAtOrDefault(0);

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
        ListNode? slow = head, fast = head?.next;
        while (fast != null && slow != null && fast != slow)
        {
            slow = slow?.next;
            fast = fast?.next?.next;
        }
        return fast == slow && fast != null;
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

    public static ListNode ReorderList(int[] ints)
        => ReorderList(new ListNode(ints));
    public static ListNode ReorderList(ListNode head)
    {
        ListNode start = head, middle = head;
        ListNode? fast = head.next;
        while (fast != null && fast != middle)
        {
            middle = middle.next!;
            fast = fast?.next?.next;
        }
        ListNode rev = ReverseList(middle);
        middle = head!.next!;
        for (int i = 0; head != null; i++)
        {
            if (int.IsEvenInteger(i))
            {
                head.next = rev;
                rev = rev?.next!;
            }
            else
            {
                head.next = middle;
                middle = middle?.next!;
            }
            head = head.next;
        }
        return start;
    }
    public static ListNode? MergeTwoLists(int[] list1, int[] list2)
        => MergeTwoLists(new ListNode(list1), new ListNode(list2));
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