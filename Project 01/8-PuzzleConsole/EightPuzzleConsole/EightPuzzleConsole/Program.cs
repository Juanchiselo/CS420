using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightPuzzleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string puzzleState = string.Empty;

            Console.Write("Enter a puzzle state: ");
            puzzleState = Console.ReadLine();

            if (EightPuzzle.IsPuzzleValid(puzzleState))
            {
                Console.WriteLine("Puzzle is valid.");
                if (EightPuzzle.IsSolvable(puzzleState))
                {
                    Console.WriteLine("Puzzle is solvable.");
                }
                else
                    Console.WriteLine("Puzzle is not solvable.");
            }
            else
                Console.WriteLine("Puzzle is invalid.");

            Console.ReadLine();
        }
    }
}
