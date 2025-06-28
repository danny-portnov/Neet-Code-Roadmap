using static Solution;
using static Extension;

Console.WriteLine("ReverseList");
PrintListNode(ReverseList([1, 2, 3, 4, 5]));
PrintListNode(ReverseList([1, 2, 3]));
PrintListNode(ReverseList([1]));
PrintListNode(ReverseList([]));

Console.WriteLine();
Console.WriteLine("MergeTwoLists");
PrintListNode(MergeTwoLists([1, 2, 4], [1, 3,5]));
PrintListNode(MergeTwoLists([1, 2, 4], [1]));
PrintListNode(MergeTwoLists([1], [1]));
PrintListNode(MergeTwoLists([], [1]));
PrintListNode(MergeTwoLists([], []));

Console.WriteLine();
Console.WriteLine("HasCycle");
Console.WriteLine(HasCycle([], 0)); // false
Console.WriteLine(HasCycle([], -1)); // false
Console.WriteLine(HasCycle([1], 0)); // true
Console.WriteLine(HasCycle([1], -1)); // false
Console.WriteLine(HasCycle([1,2], 0)); // true
Console.WriteLine(HasCycle([1,2], -1)); // false
Console.WriteLine(HasCycle([1, 2, 3], 1)); // true
Console.WriteLine(HasCycle([1, 2, 3], -1)); // false
Console.WriteLine(HasCycle([1, 2, 3, 4], 1)); // true
Console.WriteLine(HasCycle([1, 2, 3, 4], -1)); // false

Console.WriteLine();
Console.WriteLine("ReorderList");
PrintListNode(ReorderList([1])); // 1
PrintListNode(ReorderList([1, 2])); // 1, 2
PrintListNode(ReorderList([1, 2, 3])); // 1, 3, 2 
PrintListNode(ReorderList([1, 2, 3, 4])); // 1, 4, 2, 3
PrintListNode(ReorderList([1, 2, 3, 4, 5])); // 1,5,2,4,3
PrintListNode(ReorderList([1, 2, 3, 4, 5, 6, 7])); // 1,7,2,6,3,5,4

Console.WriteLine();
Console.WriteLine("RemoveNthFromEnd");
PrintListNode(RemoveNthFromEnd([1, 2, 3, 4, 5], 2)); // [1, 2, 3, 5]
PrintListNode(RemoveNthFromEnd([1, 2, 3, 4, 5], 5)); // [2, 3, 4, 5]
PrintListNode(RemoveNthFromEnd([1, 2, 3, 4, 5], 1)); // [1, 2, 3, 4]
PrintListNode(RemoveNthFromEnd([1], 1)); // []
PrintListNode(RemoveNthFromEnd([1,2], 1)); // [1]
PrintListNode(RemoveNthFromEnd([1,2], 2)); // [2]

Console.WriteLine();
Console.WriteLine("CopyRandomList");
PrintListNode(CopyRandomList([[7, null], [13, 0], [11, 4], [10, 2], [1, 0]])); 
PrintListNode(CopyRandomList([]));

Console.WriteLine();
Console.WriteLine("AddTwoNumbers");
PrintListNode(AddTwoNumbers([2, 4, 3], [5, 6, 4])); // 7,0,8
PrintListNode(AddTwoNumbers([0], [0])); //0
PrintListNode(AddTwoNumbers([9, 9, 9, 9, 9, 9, 9], [9, 9, 9, 9])); // 8,9,9,9,0,0,0,1 

Console.WriteLine();
Console.WriteLine("FindDuplicate");
Console.WriteLine(FindDuplicate([1, 2, 3, 4, 4])); // 4
Console.WriteLine(FindDuplicate([1, 3, 4, 2, 2])); // 2
Console.WriteLine(FindDuplicate([1, 2, 3, 2, 2])); //2
Console.WriteLine(FindDuplicate([3, 1, 3, 4, 2])); //3
Console.WriteLine(FindDuplicate([3, 3, 3, 3, 3])); // 3

Console.WriteLine();
Console.WriteLine("LRUCache");
LRUCache lru = new(2); // "LRUCache" with capacity 2
Console.WriteLine(lru.Get(2)); // get(2) => -1
lru.Put(2, 6);           // put(2, 6)
Console.WriteLine(lru.Get(1)); // get(1) => -1
lru.Put(1, 5);           // put(1, 5)
lru.Put(1, 2);           // put(1, 2)
Console.WriteLine(lru.Get(1)); // get(1) => 5
Console.WriteLine(lru.Get(2)); // get(1) => 6
//lru.Put(4, 1);           // put(4, 1) -> evicts 
//lru.Put(4, 4);           // put(4, 4) => evicts key 1
//Console.WriteLine(lru.Get(3)); // get(3) => 3
//Console.WriteLine(lru.Get(4)); // get(4) => 4


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

        public ListNode(int[] values, int index = -1)
        {
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

    public class Node(int val = 0, ListNode? next = null, int? randomIndex = null) 
        : ListNode(val, next)
    {
        public new Node? next { get => (Node?)base.next; set => base.next = value; }

        public Node? random;
        public int? RandomIndex = randomIndex;
        public int HashValue => GetHashCode();
        public override string ToString() => $"[{val},{(object?)RandomIndex ?? "null"}]";

        public static Node? BuildList(int?[][] data)
        {
            if (data.Length == 0) return null;

            // Step 1: Create all nodes
            Node[] nodes = new Node[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                nodes[i] = new Node(data[i][0]!.Value, randomIndex: data[i][1]);
            }

            // Step 2: Link `next` and `random`
            for (int i = 0; i < data.Length; i++)
            {
                if (i < data.Length - 1)
                    nodes[i].next = nodes[i + 1];

                int? randomIndex = data[i][1];
                if (randomIndex is not null)
                    nodes[i].random = nodes[randomIndex.Value];
            }

            return nodes[0];
        }
    }

    public class LRUCache(int capacity)
    {
        class CacheEntry(int key, int value) { public int Key = key; public int Value = value; }
        private readonly int _capacity = capacity;
        private readonly LinkedList<CacheEntry> _list = [];
        private readonly Dictionary<int, LinkedListNode<CacheEntry>> _dict 
            = new(capacity);

        public int Get(int key)
            => TryMoveNodeToEnd(key)?.Value ?? -1;

        private CacheEntry? TryMoveNodeToEnd(int key)
        {
            if (_dict.TryGetValue(key, out LinkedListNode<CacheEntry>? value))
            {
                _list.Remove(value);
                _list.AddLast(value);
                return value.Value;
            }
            return default;
        }

        public void Put(int key, int value)
        {
            CacheEntry? entry = TryMoveNodeToEnd(key);
            if (entry != null)
            {
                entry.Value = value;
            }
            else
            {
                if (_list.Count >= _capacity)
                {
                    _dict.Remove(_list.First!.Value.Key);
                    _list.RemoveFirst();
                }
                _dict[key] = _list.AddLast(new CacheEntry(key, value));
            }
        }
    }

    public static ListNode? ReverseList(ListNode? head)
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

    public static ListNode? ReverseList(int[] nums)
        => ReverseList(NodeListOrNull(nums));

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
        => HasCycle(NodeListOrNull(nums, index));

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
        ListNode start = head, slow = head;
        ListNode? fast = head.next;
        while (fast != null && fast.next != null)
        {
            slow = slow.next!;
            fast = fast?.next?.next;
        }
        ListNode middle = slow.next;
        slow.next = null;  // first half ends here
        ListNode? rev = ReverseList(middle);
        while (rev != null)
        {
            ListNode tmp1 = head.next, tmp2 = rev.next;
            head.next = rev;
            rev.next = tmp1;
            head = tmp1;
            rev = tmp2;
        }
        return start;
    }

    public static ListNode? RemoveNthFromEnd(int[] head, int n)
        => RemoveNthFromEnd(new ListNode(head), n);

    public static ListNode? RemoveNthFromEnd(ListNode head, int n)
    {
        ListNode right = head, dummy = new(next: head), left = dummy;
        while (n != 0)
        {
            right = right.next;
            n--;
        }
        while (right != null)
        {
            left = left.next!; // Advance the dummy, not the head!
            right = right.next;
        }
        left.next = left.next!.next;
        return dummy.next;
    }

    public static ListNode? MergeTwoLists(int[] list1, int[] list2)
        => MergeTwoLists(NodeListOrNull(list1), NodeListOrNull(list2));

    private static ListNode? NodeListOrNull(int[] list, int index = -1)
        => list.Length == 0 ? null : new ListNode(list, index);

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
    public static Node? CopyRandomList(int?[][] nums)
        => CopyRandomList(Node.BuildList(nums));

    public static Node? CopyRandomList(Node? head)
    {
        Node? start = head, startCopy = null;
        while (head != null)
        {
            Node? temp = head.next;
            head.next = new Node(head.val)
            {
                next = temp,
                RandomIndex = head.RandomIndex
            };
            head = head.next.next;
        }
        head = start;
        startCopy = head?.next;
        while (head != null)
        {
            head.next!.random = head.random?.next;
            head = head.next.next;
        }
        head = start;
        while (head != null)
        {
            Node? temp = head.next;
            head.next = head.next?.next;
            head = temp;
        }
        return startCopy;
    }

    public static ListNode AddTwoNumbers(ListNode l1, ListNode l2, int carry = 0)
    {
        int sum = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
        return (l1, l2, carry) == (null, null, 0)
            ? null!
            : new ListNode(sum % 10, AddTwoNumbers(l1?.next, l2?.next, sum / 10));
    }
    public static ListNode AddTwoNumbers(int[] l1, int[] l2)
        => AddTwoNumbers(new ListNode(l1), new ListNode(l2));

    public static int FindDuplicate(int[] nums)
    {
        int slow = nums[0], fast = nums[nums[0]];
        while (slow != fast)
        {
            slow = nums[slow];
            fast = nums[nums[fast]];
        }
        slow = 0;
        while (slow != fast)
        {
            slow = nums[slow];
            fast = nums[fast];
        }
        return slow;
    }
}
//foreach (var item in originalToCopy)
//{
//    item.Value.next = originalToCopy[item.Key.next];
//    if (item.Value.random != null)
//    {
//        originalToCopy[item.Value].random = head.random;
//    }
//}

public static class Extension
{
    public static void Print<T>(IEnumerable<T> values)
        => Console.WriteLine($"[{string.Join(",", values)}]");

    public static void PrintListNode(ListNode? l1)
    {
        Console.Write($"[{(l1 is Node n1 ? n1 : l1?.val)}");
        while (l1?.next != null)
        {
            l1 = l1.next;
            Console.Write($",{(l1 is Node n2 ? n2 : l1.val)}");
        }
        Console.Write("]");
        Console.WriteLine();
    }

    

}