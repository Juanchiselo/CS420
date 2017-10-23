using System.Text.RegularExpressions;
using UnityEngine;

public sealed class EightPuzzle
{
    private static readonly EightPuzzle instance = new EightPuzzle();
    
    private EightPuzzle() { }

    public static EightPuzzle Instance
    {
        get
        {
            return instance;
        }
    }

    public static bool IsPuzzleValid(string puzzleState)
    {
        Regex regex = new Regex("^[0-8]{9}$");

        if (!regex.IsMatch(puzzleState))
            return false;

        return true;
    }

    public static bool IsSolvable(string puzzleState)
    {    
        return CountInversions(puzzleState) % 2 == 0;
    }

    public static int CountInversions(string puzzleState)
    {
        int inversions = 0;
        int[] puzzleStateArray = new int[puzzleState.Length];

        for(int i = 0; i < puzzleState.Length; i++)
        {
            puzzleStateArray[i] = int.Parse(puzzleState[i].ToString());
        }

        for(int i = 0; i < puzzleStateArray.Length; i++)
        {
            for(int j = i + 1; j < puzzleStateArray.Length; j++)
            {
                if (puzzleStateArray[i] > puzzleStateArray[j])
                    inversions++;
            }
        }

        Debug.Log("Inversions: " + inversions);

        return inversions;
    }
    
}
