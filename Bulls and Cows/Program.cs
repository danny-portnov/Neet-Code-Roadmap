// See https://aka.ms/new-console-template for more information

Console.WriteLine(Solution.GetHint("1807", "7810"));
Console.WriteLine(Solution.GetHint("1123", "0111"));
Console.WriteLine(Solution.GetHint("2211", "1122"));




public static class Solution
{

    /// <summary>
    /// You are playing the Bulls and Cows game with your friend. You write down a secret number
    /// and ask your friend to guess what the number is.
    /// When your friend makes a guess, you provide a hint with the following info:
    /// The number of "bulls", which are digits in the guess that are in the correct position. 
    /// The number of "cows", which are digits in the guess that are in your secret number but are
    /// located in the wrong position.Specifically, the non-bull digits in the guess that could be rearranged such that they become bulls.
    /// Given the secret number secret and your friend's guess guess, return the hint for your friend's guess.
    /// The hint should be formatted as "xAyB", where x is the number of bulls and y is the number of cows.
    /// Note that both secret and guess may contain duplicate digits.
    /// </summary>
    /// <param name="secret"></param>
    /// <param name="guess"></param>
    /// <returns></returns>
    public static string GetHint(string secret, string guess)
    {
        int cows = 0, bulls = 0;
        int[] secretHist = new int[10], guessHist = new int[10];
        for (int i = 0; i < secret.Length; i++)
        {
            if (secret[i] == guess[i])
            {
                bulls++;
            }
            else
            {
                secretHist[secret[i] - '0']++;
                guessHist[guess[i] - '0']++;
            }
        }
        cows = secretHist.Select((c, i) => Math.Min(c, guessHist[i])).Sum();
        return $"{bulls}A{cows}B";
    }
}