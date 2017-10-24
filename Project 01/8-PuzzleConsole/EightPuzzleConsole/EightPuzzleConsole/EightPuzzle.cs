using System.Text.RegularExpressions;
using System;

public sealed class EightPuzzle
{
    private static readonly EightPuzzle instance = new EightPuzzle();
    private static Regex regex = new Regex("^[0-8]{9}$");

    private EightPuzzle() { }

    public static EightPuzzle Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>
    /// Checks if the puzzle state in a string format is in
    /// a valid format.
    /// </summary>
    /// <param name="puzzleState">The state of the puzzle.</param>
    /// <returns>Whether the puzzle state string is valid or not.</returns>
    public static bool IsPuzzleValid(string puzzleState)
    {
        if (!regex.IsMatch(puzzleState))
            return false;

        return true;
    }

    /// <summary>
    /// Checks if a valid puzzle state string is
    /// solvable or not by counting the amount of inversions
    /// and checking its parity.
    /// </summary>
    /// <param name="puzzleState">The state of the puzzle.</param>
    /// <returns>Whether the state of the puzzle is solvable or not.</returns>
    public static bool IsSolvable(string puzzleState)
    {    
        return CountInversions(puzzleState) % 2 == 0;
    }

    /// <summary>
    /// Counts the number of inversions in a puzzle state.
    /// </summary>
    /// <param name="puzzleState"></param>
    /// <returns></returns>
    public static int CountInversions(string puzzleState)
    {
        int inversions = 0;
        int[] puzzleStateArray = new int[puzzleState.Length];

        for(int i = 0; i < puzzleState.Length; i++)
            puzzleStateArray[i] = int.Parse(puzzleState[i].ToString());

        for(int i = 0; i < puzzleStateArray.Length; i++)
        {
            if (!(puzzleStateArray[i] == 0))
            {
                for (int j = i + 1; j < puzzleStateArray.Length; j++)
                {
                    Console.WriteLine("I: " + puzzleStateArray[i] + " - J: " + puzzleStateArray[j]);

                    if (!(puzzleStateArray[j] == 0))
                    {
                        if (puzzleStateArray[i] > puzzleStateArray[j])
                            inversions++;

                        Console.WriteLine("Inversions: " + inversions);
                    }
                }
            }
        }

        Console.WriteLine("Total Inversions: " + inversions);

        return inversions;
    }

    public static void Solve()
    {
    }
    
}
