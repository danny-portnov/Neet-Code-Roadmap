using System;
using System.Data;
using System.Linq;
using static Solution;
using static Extension;

//Console.WriteLine(IsValid("()[]{}"));
//Console.WriteLine(IsValid("([])"));
//Console.WriteLine(IsValid(")[])"));

//Console.WriteLine(IsValid("()[]{}"));
//Console.WriteLine(IsValid("([])"));
//MinStack minStack = new();
//minStack.Push(-10);
//minStack.Push(14);
//minStack.Push(-20);
//Console.WriteLine(minStack.GetMin()); // return -20
//minStack.Pop();
//minStack.Push(10);
//minStack.Push(-7);
//Console.WriteLine(minStack.GetMin());    // return -10

//Console.WriteLine(EvalRPN(["2", "1", "+", "3", "*"])); //9
//Console.WriteLine(EvalRPN(["4", "13", "5", "/", "+"])); //6
//Console.WriteLine(EvalRPN(["10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+"])); //22

Print(DailyTemperatures([73, 74, 75, 71, 69, 72, 76, 73])); // [1,1,4,2,1,1,0,0]
Print(DailyTemperatures([30, 40, 50, 60])); // [1,1,1,0]
Print(DailyTemperatures([30, 60, 90])); // [1,1,0]

public static class Extension
{
    public static void Print<T>(IEnumerable<T> values)
        => Console.WriteLine($"[{string.Join(",", values)}]");
}
public static class Solution
{
    public static bool IsValid(string s)
    {
        Stack<char> stack = [];
        Dictionary<char, char> parenthesis = new()
        {
            {')',  '('},
            {'}',  '{'},
            {']',  '['},
        };
        foreach (char c in s)
        {
            if (parenthesis.ContainsValue(c))
            {
                stack.Push(c);
            }
            else if (!stack.TryPop(out char close) || close != parenthesis[c])
            {
                return false;
            }
        }
        return stack.Count == 0;
    }

    static DataTable dataTable = new();
    public static int EvalRPN(string[] tokens)
    {
        Stack<int> nums = [];
        foreach (string token in tokens)
        {
            if (int.TryParse(token, out int result))
            {
                nums.Push(result);
            }
            else
            {
                int first = nums.Pop(), second = nums.Pop();
                nums.Push((int)Convert.ToDouble(
                    dataTable.Compute($"{second}{token}{first}", null)));
            }
        }
        return nums.Pop();
    }

    //public IList<string> GenerateParenthesis(int n)
    //{
    //    List<string> parenthesis = [];


    //    // ["()"]
    //    // ["()()", "(())"]
    //    // ["((()))","(()())","(())()","()(())","()()()"]
    //}

    public static int[] DailyTemperatures(int[] temperatures)
    {
        Stack<int> indices = [];
        int[] days = new int[temperatures.Length];
        for (int i = 0; i < temperatures.Length; i++)
        {
            while (indices.TryPeek(out int stackTop)
                && temperatures[stackTop] < temperatures[i])
            {
                indices.Pop();
                days[stackTop] = i - stackTop;
            }
            indices.Push(i);
        }
        return days;
    }
}

public class MinStack
{
    private readonly Stack<(int val, int min)> stack
        = new([(0, int.MaxValue)]);

    /// <summary>
    /// pushes the element val onto the stack.
    /// </summary>
    /// <param name="val"></param>
    public void Push(int val)
        => stack.Push((val, Math.Min(GetMin(), val)));

    /// <summary>
    /// removes the element on the top of the stack.
    /// </summary>
    public void Pop() => stack.Pop();

    /// <summary>
    /// gets the top element of the stack.
    /// </summary>
    /// <returns></returns>
    public int Top() => stack.Peek().val;

    /// <summary>
    /// retrieves the minimum element in the stack.
    /// </summary>
    /// <returns></returns>
    public int GetMin() => stack.Peek().min;
}

/**
 * Your MinStack object will be instantiated and called as such:
 * MinStack obj = new MinStack();
 * obj.Push(val);
 * obj.Pop();
 * int param_3 = obj.Top();
 * int param_4 = obj.GetMin();
 */